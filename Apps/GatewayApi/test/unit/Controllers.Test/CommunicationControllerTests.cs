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
namespace HealthGateway.GatewayApi.Test.Controllers
{
    using DeepEqual.Syntax;
    using HealthGateway.Common.Data.Constants;
    using HealthGateway.Common.Data.ViewModels;
    using HealthGateway.Common.Services;
    using HealthGateway.Database.Models;
    using HealthGateway.GatewayApi.Controllers;
    using Moq;
    using Xunit;

    /// <summary>
    /// CommunicationController's Unit Tests.
    /// </summary>
    public class CommunicationControllerTests
    {
        /// <summary>
        /// Successfully Create Comment - Happy Path scenario.
        /// </summary>
        [Fact]
        public void ShouldGetCommunication()
        {
            RequestResult<Communication?> expectedResult = new()
            {
                ResourcePayload = new Communication
                {
                    Subject = "Mocked Subject",
                    Text = "Mocked Text",
                },
                ResultStatus = ResultType.Success,
            };

            Mock<ICommunicationService> communicationServiceMock = new();
            communicationServiceMock.Setup(s => s.GetActiveCommunication(CommunicationType.Banner)).Returns(expectedResult);

            CommunicationController controller = new(communicationServiceMock.Object);
            RequestResult<Communication?> actualResult = controller.Get();

            expectedResult.ShouldDeepEqual(actualResult);
        }
    }
}
