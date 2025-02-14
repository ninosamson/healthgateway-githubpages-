import { injectable } from "inversify";
import Keycloak, { KeycloakConfig } from "keycloak-js";

import { OpenIdConnectConfiguration } from "@/models/configData";
import { OidcTokenDetails, OidcUserInfo } from "@/models/user";
import container from "@/plugins/container";
import { SERVICE_IDENTIFIER } from "@/plugins/inversify";
import { IAuthenticationService, ILogger } from "@/services/interfaces";

/** The number of seconds between initiation of a token refresh and expiry of the old token. */
const REFRESH_CUSHION = 30;

@injectable()
export class RestAuthenticationService implements IAuthenticationService {
    private logger = container.get<ILogger>(SERVICE_IDENTIFIER.Logger);

    private keycloak!: Keycloak;
    private scope!: string;
    private logonCallback!: string;
    private logoutCallback!: string;

    public async initialize(config: OpenIdConnectConfiguration): Promise<void> {
        this.scope = config.scope;
        this.logonCallback = config.callbacks["Logon"];
        this.logoutCallback = config.callbacks["Logout"];

        const [url, realm] = config.authority.split("/realms/");
        const keycloakConfig: KeycloakConfig = {
            url,
            realm,
            clientId: config.clientId,
        };
        this.keycloak = new Keycloak(keycloakConfig);

        this.keycloak.onReady = () => this.logger.verbose("Keycloak: onReady");
        this.keycloak.onAuthSuccess = () =>
            this.logger.verbose("Keycloak: onAuthSuccess");
        this.keycloak.onAuthError = (error) => {
            this.logger.verbose(`Keycloak: onAuthError - ${error.error}`);
            this.logger.error(error.error_description);
        };
        this.keycloak.onAuthRefreshSuccess = () =>
            this.logger.verbose("Keycloak: onAuthRefreshSuccess");
        this.keycloak.onAuthRefreshError = () =>
            this.logger.verbose("Keycloak: onAuthRefreshError");
        this.keycloak.onAuthLogout = () =>
            this.logger.verbose("Keycloak: onAuthLogout");
        this.keycloak.onTokenExpired = () =>
            this.logger.verbose("Keycloak: onTokenExpired");
        this.keycloak.onActionUpdate = (status) =>
            this.logger.verbose(`Keycloak: onActionUpdate - ${status}`);

        await this.keycloak.init({
            onLoad: "check-sso",
        });
    }

    public async signIn(
        redirectPath: string,
        idpHint?: string
    ): Promise<OidcTokenDetails> {
        this.logger.verbose("Checking authentication...");
        const escapedRedirectPath = encodeURI(redirectPath);
        const callbackUri = `${this.logonCallback}?redirect=${escapedRedirectPath}`;

        const tokenDetails = this.getOidcTokenDetails();
        if (tokenDetails !== null) {
            this.logger.verbose("Already authenticated");
            return tokenDetails;
        }

        this.logger.verbose(
            "Not yet authenticated; redirecting to Keycloak login..."
        );
        await this.keycloak.login({
            scope: this.scope,
            redirectUri: callbackUri,
            idpHint,
        });

        // if keycloak.login() doesn't cause a redirect, something is terribly wrong
        throw Error("Redirect to Keycloak login page failed");
    }

    public signOut(): Promise<void> {
        return this.keycloak.logout({
            redirectUri: this.logoutCallback,
        });
    }

    public refreshToken(): Promise<boolean> {
        return this.keycloak.updateToken(REFRESH_CUSHION + 5);
    }

    public getOidcTokenDetails(): OidcTokenDetails | null {
        if (!this.keycloak?.authenticated) {
            return null;
        }

        const currentTime = Math.ceil(new Date().getTime() / 1000);
        const tokenExpiryTime = this.keycloak.tokenParsed?.exp ?? currentTime;
        const timeSkew = this.keycloak.timeSkew ?? 0;

        return {
            idToken: this.keycloak.idToken ?? "",
            sessionState: this.keycloak.sessionId,
            refreshToken: this.keycloak.refreshToken,
            accessToken: this.keycloak.token ?? "",
            expired: this.keycloak.isTokenExpired(),
            refreshTokenTime: tokenExpiryTime + timeSkew - REFRESH_CUSHION,
            hdid: this.keycloak.idTokenParsed?.hdid ?? "",
        };
    }

    public async getOidcUserInfo(): Promise<OidcUserInfo> {
        if (!this.keycloak.userInfo) {
            await this.keycloak.loadUserInfo();
        }

        return this.keycloak.userInfo as OidcUserInfo;
    }

    public clearState(): void {
        this.keycloak.clearToken();
    }
}
