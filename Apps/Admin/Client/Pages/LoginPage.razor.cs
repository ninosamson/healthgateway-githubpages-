﻿// -------------------------------------------------------------------------
//  Copyright © 2019 Province of British Columbia
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
// -------------------------------------------------------------------------

namespace HealthGateway.Admin.Client.Pages;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

/// <summary>
/// Backing logic for the Login page.
/// </summary>
public partial class LoginPage : ComponentBase
{
    /// <summary>
    /// Gets or sets the URL to return to after logging in.
    /// </summary>
    [Parameter]
    [SupplyParameterFromQuery(Name = "returnUrl")]
    public string? ReturnPath { get; set; }

    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

    [Inject]
    private NavigationManager NavigationManager { get; set; } = default!;

    private string LogInUrl => this.NavigationManager.GetUriWithQueryParameters(
        "/authentication/login",
        new Dictionary<string, object?>
        {
            ["returnUrl"] = this.ReturnPath,
        });

    /// <inheritdoc/>
    protected override async Task OnInitializedAsync()
    {
        AuthenticationState authState = await this.AuthenticationStateProvider.GetAuthenticationStateAsync().ConfigureAwait(true);
        if (authState.User.Identity is { IsAuthenticated: true })
        {
            this.NavigationManager.NavigateTo(this.ReturnPath ?? "/", replace: true);
        }
    }
}
