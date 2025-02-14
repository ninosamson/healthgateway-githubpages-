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
namespace HealthGateway.Common.AccessManagement.Authorization.Handlers
{
    using System.Linq;
    using System.Threading.Tasks;
    using HealthGateway.Common.AccessManagement.Authorization.Requirements;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// FhirResourceAuthorizationHandler validates that a FhirRequirement has been met.
    /// </summary>
    public class FhirResourceAuthorizationHandler : BaseFhirAuthorizationHandler
    {
        private readonly ILogger<FhirResourceAuthorizationHandler> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="FhirResourceAuthorizationHandler"/> class.
        /// </summary>
        /// <param name="logger">the injected logger.</param>
        /// <param name="httpContextAccessor">The HTTP Context accessor.</param>
        public FhirResourceAuthorizationHandler(
            ILogger<FhirResourceAuthorizationHandler> logger,
            IHttpContextAccessor httpContextAccessor)
            : base(logger, httpContextAccessor)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Asserts that the user accessing the resource (hdid in route) is one of:
        ///     1) The owner of the resource.
        ///     2) System Delegated to access the resource.
        /// </summary>
        /// <param name="context">the AuthorizationHandlerContext context.</param>
        /// <returns>The Authorization Result.</returns>
        public override Task HandleAsync(AuthorizationHandlerContext context)
        {
            foreach (FhirRequirement requirement in context.PendingRequirements.OfType<FhirRequirement>())
            {
                string? resourceHDID = this.GetResourceHDID(requirement);
                if (resourceHDID == null)
                {
                    this.logger.LogWarning("Fhir resource Handler has been invoked without route resource being specified, ignoring");
                }
                else if (this.IsOwner(context, resourceHDID))
                {
                    context.Succeed(requirement);
                }
                else if (requirement.SupportsSystemDelegation && this.IsSystemDelegated(context, resourceHDID, requirement))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    this.logger.LogDebug("Non-owner access to {ResourceHdid} rejected; Supports delegation: {SupportsSystemDelegation}", resourceHDID, requirement.SupportsSystemDelegation);
                }
            }

            return Task.CompletedTask;
        }
    }
}
