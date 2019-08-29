﻿//-------------------------------------------------------------------------
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
namespace HealthGateway.Controllers
{
    using HealthGateway.Models;
    using HealthGateway.Service;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The Immunization controller.
    /// </summary>
    [Authorize]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        /// <summary>
        /// Gets or sets the patient data service.
        /// </summary>
        private readonly IPatientService service;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatientController"/> class.
        /// </summary>
        /// <param name="svc">The patient data service.</param>
        public PatientController(IPatientService svc)
        {
            this.service = svc;
        }

        /// <summary>
        /// Gets a json of patient record.
        /// </summary>
        /// <returns>The patient record.</returns>
        /// <response code="200">Returns the patient record.</response>
        /// <response code="401">The client is not authorzied to retrieve the record.</response>
        [HttpGet]
        [Produces("application/json")]
        [Route("{id}")]
        public PatientModel GetPatient(string id)
        {
            // return this.service.GetPatient();
            return new PatientModel() { PersonalHealthNumber = "123" };
        }
    }
}