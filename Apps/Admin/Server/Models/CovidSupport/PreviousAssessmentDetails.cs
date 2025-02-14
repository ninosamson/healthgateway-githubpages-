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
namespace HealthGateway.Admin.Server.Models.CovidSupport;

using System;
using System.Text.Json.Serialization;

/// <summary>
/// Model object representing previous assessment details.
/// </summary>
public class PreviousAssessmentDetails
{
    /// <summary>
    /// Gets or sets the date time of assessment.
    /// </summary>
    [JsonPropertyName("dateTimeOfAssessment")]
    public DateTime DateTimeOfAssessment { get; set; }

    /// <summary>
    /// Gets or sets the form id.
    /// </summary>
    [JsonPropertyName("formId")]
    public string? FormId { get; set; }
}
