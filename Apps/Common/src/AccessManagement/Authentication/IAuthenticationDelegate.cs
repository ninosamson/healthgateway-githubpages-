//-------------------------------------------------------------------------
// Copyright © 2019 Province of British Columbia
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//-------------------------------------------------------------------------
namespace HealthGateway.Common.AccessManagement.Authentication
{
    using System;
    using HealthGateway.Common.AccessManagement.Authentication.Models;

    /// <summary>
    /// The authorization service interface.
    /// This supports direct grant for OAuth2 Client Credentials Grant flows
    /// and Resource Owner Password Grant flows.
    /// </summary>
    public interface IAuthenticationDelegate
    {
        /// <summary>
        /// Authenticates as a 'system admin account' concept, using OAuth 2.0 Client Credentials Grant.
        /// </summary>
        /// <param name="tokenUri">Uri to request the the token from.</param>
        /// <param name="tokenRequest">Token request configuration.</param>
        /// <returns>An instance fo the <see cref="JwtModel"/> class.</returns>
        JwtModel AuthenticateAsSystem(Uri tokenUri, ClientCredentialsTokenRequest tokenRequest);

        /// <summary>
        /// Authenticates a resource owner user with direct grant from the default configuration section.
        /// </summary>
        /// <returns>The access token for the user information provided in configuration.</returns>
        string? AccessTokenAsUser();

        /// <summary>
        /// Authenticates a resource owner user with direct grant from a defined configuration section.
        /// </summary>
        /// <param name="sectionName">The configuration section used to lookup values.</param>
        /// <returns>The access token for the user information provided in configuration.</returns>
        string? AccessTokenAsUser(string sectionName);

        /// <summary>
        /// Authenticates a resource owner user with direct grant, no user intervention.
        /// Proxies to AuthenticateAsUser.
        /// </summary>
        /// <param name="tokenUri">Uri to request the the token from.</param>
        /// <param name="tokenRequest">Token request configuration.</param>
        /// <param name="cacheEnabled">if true caches the result.</param>
        /// <returns>The access token for the user tokenRequest.</returns>
        string? AccessTokenAsUser(Uri tokenUri, ClientCredentialsTokenRequest tokenRequest, bool cacheEnabled = true);

        /// <summary>
        /// Authenticates a resource owner user with direct grant, no user intervention.
        /// </summary>
        /// <param name="tokenUri">Uri to request the the token from.</param>
        /// <param name="tokenRequest">Token request configuration.</param>
        /// <param name="cacheEnabled">if true caches the result.</param>
        /// <returns>An instance of the <see cref="JwtModel"/> class.</returns>
        JwtModel AuthenticateAsUser(Uri tokenUri, ClientCredentialsTokenRequest tokenRequest, bool cacheEnabled = false);

        /// <summary>
        /// Authenticates a resource owner user with direct grant, no user intervention.
        /// </summary>
        /// <param name="tokenUri">Uri to request the the token from.</param>
        /// <param name="tokenRequest">Token request configuration.</param>
        /// <param name="cacheEnabled">if true caches the result.</param>
        /// <returns>An instance fo the <see cref="JwtModel"/> class and a bool representing if the objecft was cached or not.</returns>
        (JwtModel JwtModel, bool Cached) AuthenticateUser(Uri tokenUri, ClientCredentialsTokenRequest tokenRequest, bool cacheEnabled);

        /// <summary>
        /// Fetches the access token for the authenticated user from the http context.
        /// </summary>
        /// <returns>The access token for the user.</returns>
        string? FetchAuthenticatedUserToken();

        /// <summary>
        /// Fetches the HDID for the authenticated user from the http context.
        /// </summary>
        /// <returns>The users HDID.</returns>
        string? FetchAuthenticatedUserHdid();

        /// <summary>
        /// Fetches the id for the authenticated user from the http context.
        /// </summary>
        /// <returns>The user's id.</returns>
        string? FetchAuthenticatedUserId();
    }
}
