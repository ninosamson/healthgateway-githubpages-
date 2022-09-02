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
namespace HealthGateway.ClinicalDocument.Services
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using HealthGateway.ClinicalDocument.Models;
    using HealthGateway.Common.Data.Constants;
    using HealthGateway.Common.Data.ViewModels;
    using HealthGateway.Common.Models.PHSA;
    using HealthGateway.Common.Services;
    using Microsoft.Extensions.Logging;

    /// <inheritdoc/>
    public class ClinicalDocumentService : IClinicalDocumentService
    {
        private readonly ILogger<ClinicalDocumentService> logger;
        private readonly IPersonalAccountsService personalAccountsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalDocumentService"/> class.
        /// </summary>
        /// <param name="logger">Injected logger.</param>
        /// <param name="personalAccountsService">The injected personal accounts service.</param>
        public ClinicalDocumentService(ILogger<ClinicalDocumentService> logger, IPersonalAccountsService personalAccountsService)
        {
            this.logger = logger;
            this.personalAccountsService = personalAccountsService;
        }

        private static ActivitySource Source { get; } = new(nameof(ClinicalDocumentService));

        /// <inheritdoc/>
        public async Task<RequestResult<IEnumerable<ClinicalDocumentRecord>>> GetRecordsAsync(string hdid)
        {
            RequestResult<IEnumerable<ClinicalDocumentRecord>> requestResult = new()
            {
                ResultStatus = ResultType.Error,
                PageSize = 0,
            };
            using Activity? activity = Source.StartActivity();
            RequestResult<PersonalAccount?> response = await this.personalAccountsService.GetPatientAccountAsync(hdid).ConfigureAwait(true);
            if (response.ResultStatus == ResultType.Success)
            {
                this.logger.LogDebug("PID Fetched: {Pid}", response.ResourcePayload?.PatientIdentity?.Pid);
            }
            else
            {
                requestResult.ResultError = response.ResultError;
            }

            activity?.Stop();
            return requestResult;
        }

        /// <inheritdoc/>
        public async Task<RequestResult<EncodedMedia>> GetFileAsync(string hdid, string fileId)
        {
            RequestResult<EncodedMedia> requestResult = new()
            {
                ResultStatus = ResultType.Error,
                PageSize = 0,
            };
            using Activity? activity = Source.StartActivity();
            RequestResult<PersonalAccount?> response = await this.personalAccountsService.GetPatientAccountAsync(hdid).ConfigureAwait(true);
            if (response.ResultStatus == ResultType.Success)
            {
                this.logger.LogDebug("PID Fetched: {Pid}", response.ResourcePayload?.PatientIdentity?.Pid);
            }
            else
            {
                requestResult.ResultError = response.ResultError;
            }

            activity?.Stop();
            return requestResult;
        }
    }
}