<script lang="ts">
import { library } from "@fortawesome/fontawesome-svg-core";
import {
    faBars,
    faSignInAlt,
    faSignOutAlt,
    faTimes,
    faUser,
} from "@fortawesome/free-solid-svg-icons";
import Vue from "vue";
import { Component, Ref, Watch } from "vue-property-decorator";
import { Action, Getter } from "vuex-class";

import RatingComponent from "@/components/modal/RatingComponent.vue";
import User, { OidcUserInfo } from "@/models/user";
import container from "@/plugins/container";
import { SERVICE_IDENTIFIER } from "@/plugins/inversify";
import { ILogger } from "@/services/interfaces";

library.add(faBars, faSignInAlt, faSignOutAlt, faTimes, faUser);

// eslint-disable-next-line @typescript-eslint/no-explicit-any
const options: any = {
    components: {
        RatingComponent,
    },
};

@Component(options)
export default class HeaderComponent extends Vue {
    @Action("toggleSidebar", { namespace: "navbar" })
    toggleSidebar!: () => void;

    @Action("setHeaderState", { namespace: "navbar" })
    setHeaderState!: (isOpen: boolean) => void;

    @Getter("isMobile")
    isMobileWidth!: boolean;

    @Getter("isOffline", { namespace: "config" })
    isOffline!: boolean;

    @Getter("oidcIsAuthenticated", { namespace: "auth" })
    oidcIsAuthenticated!: boolean;

    @Getter("isValidIdentityProvider", { namespace: "auth" })
    isValidIdentityProvider!: boolean;

    @Getter("isHeaderShown", { namespace: "navbar" })
    isHeaderShown!: boolean;

    @Getter("isSidebarOpen", { namespace: "navbar" })
    isSidebarOpen!: boolean;

    @Getter("isSidebarAvailable", { namespace: "navbar" })
    isSidebarAvailable!: boolean;

    @Getter("user", { namespace: "user" })
    user!: User;

    @Getter("oidcUserInfo", { namespace: "user" })
    oidcUserInfo!: OidcUserInfo | undefined;

    @Getter("patientRetrievalFailed", { namespace: "user" })
    patientRetrievalFailed!: boolean;

    @Ref("ratingComponent")
    readonly ratingComponent!: RatingComponent;

    private logger!: ILogger;

    private lastScrollTop = 0;
    private static minimunScrollChange = 2;

    private get userName(): string {
        if (this.oidcUserInfo === undefined) {
            return "";
        }
        return `${this.oidcUserInfo.given_name} ${this.oidcUserInfo.family_name}`;
    }

    private get userInitials(): string {
        const first = this.oidcUserInfo?.given_name;
        const last = this.oidcUserInfo?.family_name;
        if (first && last) {
            return first.charAt(0) + last.charAt(0);
        } else if (first) {
            return first.charAt(0);
        } else if (last) {
            return last.charAt(0);
        } else {
            return "?";
        }
    }

    @Watch("isMobileWidth")
    private onMobileWidth(): void {
        if (!this.isMobileWidth) {
            this.setHeaderState(false);
        }
    }

    private created(): void {
        this.logger = container.get<ILogger>(SERVICE_IDENTIFIER.Logger);
        this.$nextTick(() => {
            window.addEventListener("scroll", this.onScroll);
            if (!this.isMobileWidth) {
                this.setHeaderState(false);
            }
        });
    }

    private destroyed(): void {
        window.removeEventListener("scroll", this.onScroll);
    }

    private get isPcrTest(): boolean {
        return this.$route.path.toLowerCase().startsWith("/pcrtest");
    }

    private get isSidebarButtonShown(): boolean {
        return this.isSidebarAvailable && !this.isPcrTest && this.isMobileWidth;
    }

    private get isLoggedInMenuShown(): boolean {
        return this.oidcIsAuthenticated && !this.isPcrTest;
    }

    private get isLogOutButtonShown(): boolean {
        return this.oidcIsAuthenticated && this.isPcrTest;
    }

    private get isLogInButtonShown(): boolean {
        return !this.oidcIsAuthenticated && !this.isOffline && !this.isPcrTest;
    }

    private get isProfileLinkAvailable(): boolean {
        return (
            this.isLoggedInMenuShown &&
            this.isValidIdentityProvider &&
            !this.patientRetrievalFailed
        );
    }

    private onScroll(): void {
        let st = window.scrollY || document.documentElement.scrollTop;
        if (
            Math.abs(st - this.lastScrollTop) >
                HeaderComponent.minimunScrollChange &&
            this.isMobileWidth
        ) {
            if (st > this.lastScrollTop) {
                // downscroll
                if (this.isHeaderShown) {
                    this.setHeaderState(false);
                }
            } else {
                // upscroll
                if (!this.isHeaderShown) {
                    this.setHeaderState(true);
                }
            }
        }
        // For Mobile or negative scrolling
        this.lastScrollTop = st <= 0 ? 0 : st;
    }

    private handleToggleClick(): void {
        this.toggleSidebar();
    }

    private toggleMenu(): void {
        this.toggleSidebar();
    }

    private handleLogoutClick(): void {
        if (this.isValidIdentityProvider) {
            this.showRating();
        } else {
            this.processLogout();
        }
    }

    private showRating(): void {
        this.ratingComponent.showModal();
    }

    private processLogout(): void {
        this.logger.debug(`redirecting to logout view ...`);
        this.$router.push({ path: "/logout" });
    }
}
</script>

<template>
    <header class="sticky-top" :class="{ 'nav-up': !isHeaderShown }">
        <b-navbar type="dark" aria-label="Top Nav" class="p-0">
            <!-- Hamburger toggle -->
            <hg-button
                v-if="isSidebarButtonShown"
                class="m-2"
                variant="icon"
                @click="handleToggleClick"
            >
                <hg-icon
                    :icon="isSidebarOpen ? 'times' : 'bars'"
                    size="large"
                    class="menu-icon"
                    square
                />
            </hg-button>

            <!-- Brand -->
            <b-navbar-brand class="my-2 mr-0 ml-md-2 d-flex">
                <router-link to="/">
                    <img
                        class="img-fluid d-none d-md-block"
                        src="@/assets/images/gov/bcid-logo-rev-en.svg"
                        width="181"
                        height="44"
                        alt="Go to healthgateway home page"
                    />

                    <img
                        class="img-fluid d-md-none"
                        src="@/assets/images/gov/bcid-symbol-rev.svg"
                        width="30"
                        height="44"
                        alt="Go to healthgateway home page"
                    />
                </router-link>
                <router-link
                    to="/"
                    class="nav-link py-0 px-0 px-lg-5 mx-2 align-self-center"
                >
                    Health Gateway
                </router-link>
            </b-navbar-brand>

            <!-- Navbar links -->
            <b-navbar-nav class="nav-pills ml-auto">
                <b-nav-item-dropdown
                    v-if="isLoggedInMenuShown"
                    id="menuBtnLogout"
                    menu-class="drop-menu-position"
                    data-testid="headerDropdownBtn"
                    toggle-class="py-3 px-2 rounded-0"
                    right
                >
                    <!-- Using 'button-content' slot -->
                    <template #button-content>
                        <b-avatar
                            :text="userInitials"
                            class="mr-1 background-secondary"
                            data-testid="profileButtonInitials"
                        />
                    </template>
                    <span
                        class="dropdown-item-text text-center"
                        data-testid="profileUserName"
                    >
                        {{ userName }}
                    </span>
                    <b-dropdown-divider />
                    <b-dropdown-item
                        v-if="isProfileLinkAvailable"
                        id="menuBtnProfile"
                        data-testid="profileBtn"
                        to="/profile"
                    >
                        <hg-icon
                            icon="user"
                            size="medium"
                            data-testid="profileDropDownIcon"
                            class="mr-2"
                            fixed-width
                        />
                        <span data-testid="profileDropDownLabel">
                            Profile
                        </span>
                    </b-dropdown-item>
                    <b-dropdown-item-button
                        data-testid="logoutBtn"
                        @click="handleLogoutClick()"
                    >
                        <hg-icon
                            icon="sign-out-alt"
                            size="medium"
                            data-testid="logoutDropDownIcon"
                            class="mr-2"
                            fixed-width
                        />
                        <span>Log Out</span>
                    </b-dropdown-item-button>
                </b-nav-item-dropdown>
                <b-nav-item
                    v-else-if="isLogInButtonShown"
                    id="menuBtnLogin"
                    data-testid="loginBtn"
                    class="nav-link"
                    link-classes="d-flex align-items-center"
                    to="/login"
                >
                    <hg-icon icon="sign-in-alt" size="large" class="mr-2" />
                    <span>Log In</span>
                </b-nav-item>
                <b-nav-item
                    v-else-if="isLogOutButtonShown"
                    id="header-log-out-button"
                    data-testid="header-log-out-button"
                    class="nav-link"
                    link-classes="d-flex align-items-center"
                    to="/logout"
                >
                    <hg-icon icon="sign-out-alt" size="large" class="mr-2" />
                    <span>Log Out</span>
                </b-nav-item>
            </b-navbar-nav>
        </b-navbar>

        <RatingComponent ref="ratingComponent" @on-close="processLogout()" />
    </header>
</template>

<style lang="scss" scoped>
@import "@/assets/scss/_variables.scss";

.sticky-top {
    transition: all 0.3s;
    z-index: $z_header;
}

.navbar {
    min-height: $header-height;
}

.nav-up {
    top: -74px;
}

.menu-icon {
    min-width: 1em;
    min-height: 1em;
}

nav {
    .navbar-brand {
        a {
            color: white;
            text-decoration: none;
        }

        a:hover {
            text-decoration: underline;
        }

        font-weight: bold;
        line-height: 1;
        font-size: 1rem;

        @media (min-width: 360px) {
            font-size: 1.25rem;
        }

        @media (min-width: 576px) {
            font-size: 1.5rem;
        }

        @media (max-width: 319px) {
            display: none !important;
        }
    }

    button {
        svg {
            width: 1.5em;
            height: 1.5em;
        }
    }

    .nav-link {
        cursor: pointer;
    }

    .background-secondary {
        background-color: $hg-brand-secondary;
    }
}
</style>

<style lang="scss">
.drop-menu-position {
    position: absolute !important;
}
</style>
