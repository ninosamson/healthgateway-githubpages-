
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
namespace HealthGateway.Medication.Delegates
{
    using HealthGateway.Common.Models;
    using System.Collections.Generic;
    using HealthGateway.Medication.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation of IDrugLookupDelegate that uses a DB connection for data management
    /// </summary>
    public class EntityDrugLookupDelegate : IDrugLookupDelegate
    {
        /// <inheritdoc/>
        public Task<RequestResult<List<Medication>>> FindMedicationsByDIN(List<string> drugIdentifiers)
        {
            throw new System.NotImplementedException();
        }
    }
}