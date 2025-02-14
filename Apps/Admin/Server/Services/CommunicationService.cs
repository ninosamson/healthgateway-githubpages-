// -------------------------------------------------------------------------
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
namespace HealthGateway.Admin.Server.Services
{
    using System.Collections.Generic;
    using AutoMapper;
    using HealthGateway.Admin.Common.Models;
    using HealthGateway.Common.Data.Constants;
    using HealthGateway.Common.Data.ViewModels;
    using HealthGateway.Common.ErrorHandling;
    using HealthGateway.Database.Constants;
    using HealthGateway.Database.Delegates;
    using HealthGateway.Database.Wrapper;
    using Microsoft.Extensions.Logging;

    /// <inheritdoc/>
    public class CommunicationService : ICommunicationService
    {
        private readonly IMapper autoMapper;
        private readonly ICommunicationDelegate communicationDelegate;
        private readonly ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommunicationService"/> class.
        /// </summary>
        /// <param name="logger">Injected Logger Provider.</param>
        /// <param name="communicationDelegate">The communication delegate to interact with the DB.</param>
        /// <param name="autoMapper">The inject automapper provider.</param>
        public CommunicationService(ILogger<CommunicationService> logger, ICommunicationDelegate communicationDelegate, IMapper autoMapper)
        {
            this.logger = logger;
            this.communicationDelegate = communicationDelegate;
            this.autoMapper = autoMapper;
        }

        /// <inheritdoc/>
        public RequestResult<Communication> Add(Communication communication)
        {
            this.logger.LogTrace("Communication received: {Id)}", communication.Id.ToString());

            if (communication.EffectiveDateTime < communication.ExpiryDateTime)
            {
                this.logger.LogTrace("Adding communication... {Id)}", communication.Id.ToString());
                DBResult<Database.Models.Communication> dbResult = this.communicationDelegate.Add(this.autoMapper.Map<Database.Models.Communication>(communication));
                return new RequestResult<Communication>
                {
                    ResourcePayload = this.autoMapper.Map<Communication>(dbResult.Payload),
                    ResultStatus = dbResult.Status == DBStatusCode.Created ? ResultType.Success : ResultType.Error,
                    ResultError = dbResult.Status == DBStatusCode.Created
                        ? null
                        : new RequestResultError
                        {
                            ResultMessage = dbResult.Message,
                            ErrorCode = ErrorTranslator.ServiceError(ErrorType.CommunicationInternal, ServiceType.Database),
                        },
                };
            }

            return new RequestResult<Communication>
            {
                ResourcePayload = null,
                ResultStatus = ResultType.Error,
                ResultError = new RequestResultError
                {
                    ResultMessage = "Effective Date should be before Expiry Date.",
                    ErrorCode = ErrorTranslator.InternalError(ErrorType.InvalidState),
                },
            };
        }

        /// <inheritdoc/>
        public RequestResult<Communication> Update(Communication communication)
        {
            if (communication.EffectiveDateTime < communication.ExpiryDateTime)
            {
                this.logger.LogTrace("Updating communication... {Id)}", communication.Id.ToString());

                DBResult<Database.Models.Communication> dbResult = this.communicationDelegate.Update(this.autoMapper.Map<Database.Models.Communication>(communication));
                return new RequestResult<Communication>
                {
                    ResourcePayload = this.autoMapper.Map<Communication>(dbResult.Payload),
                    ResultStatus = dbResult.Status == DBStatusCode.Updated ? ResultType.Success : ResultType.Error,
                    ResultError = dbResult.Status == DBStatusCode.Updated
                        ? null
                        : new RequestResultError
                        {
                            ResultMessage = dbResult.Message,
                            ErrorCode = ErrorTranslator.ServiceError(ErrorType.CommunicationInternal, ServiceType.Database),
                        },
                };
            }

            return new RequestResult<Communication>
            {
                ResourcePayload = null,
                ResultStatus = ResultType.Error,
                ResultError = new RequestResultError
                {
                    ResultMessage = "Effective Date should be before Expiry Date.",
                    ErrorCode = ErrorTranslator.InternalError(ErrorType.InvalidState),
                },
            };
        }

        /// <inheritdoc/>
        public RequestResult<IEnumerable<Communication>> GetAll()
        {
            this.logger.LogTrace("Getting communication entries...");
            DBResult<IEnumerable<Database.Models.Communication>> dbResult = this.communicationDelegate.GetAll();
            RequestResult<IEnumerable<Communication>> requestResult = new()
            {
                ResourcePayload = this.autoMapper.Map<IEnumerable<Communication>>(dbResult.Payload),
                ResultStatus = dbResult.Status == DBStatusCode.Read ? ResultType.Success : ResultType.Error,
                ResultError = dbResult.Status == DBStatusCode.Read
                    ? null
                    : new RequestResultError
                    {
                        ResultMessage = dbResult.Message,
                        ErrorCode = ErrorTranslator.ServiceError(ErrorType.CommunicationInternal, ServiceType.Database),
                    },
            };
            return requestResult;
        }

        /// <inheritdoc/>
        public RequestResult<Communication> Delete(Communication communication)
        {
            if (communication.CommunicationStatusCode == CommunicationStatus.Processed)
            {
                this.logger.LogError("Processed communication can't be deleted.");
                return new RequestResult<Communication>
                {
                    ResultStatus = ResultType.Error,
                    ResultError = new RequestResultError
                    {
                        ResultMessage = "Processed communication can't be deleted.",
                        ErrorCode = ErrorTranslator.InternalError(ErrorType.InvalidState),
                    },
                };
            }

            DBResult<Database.Models.Communication> dbResult = this.communicationDelegate.Delete(this.autoMapper.Map<Database.Models.Communication>(communication));
            RequestResult<Communication> result = new()
            {
                ResourcePayload = this.autoMapper.Map<Communication>(dbResult.Payload),
                ResultStatus = dbResult.Status == DBStatusCode.Deleted ? ResultType.Success : ResultType.Error,
                ResultError = dbResult.Status == DBStatusCode.Deleted
                    ? null
                    : new RequestResultError
                    {
                        ResultMessage = dbResult.Message,
                        ErrorCode = ErrorTranslator.ServiceError(ErrorType.CommunicationInternal, ServiceType.Database),
                    },
            };
            return result;
        }
    }
}
