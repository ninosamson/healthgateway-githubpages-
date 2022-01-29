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
namespace HealthGateway.Laboratory.Models.PHSA;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// An instance of a PHSA Laboratory Summary.
/// </summary>
public class PhsaLaboratorySummary
{
    /// <summary>
    /// Gets or sets the last refresh date of the Laboratory Summary.
    /// </summary>
    [JsonPropertyName("lastRefreshDate")]
    public DateTime LastRefreshDate { get; set; }

    /// <summary>
    /// Gets or sets the hash of the Laboratory Summary.
    /// </summary>
    [JsonPropertyName("hash")]
    public string Hash { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the laboratory battery count of the Laboratory Summary.
    /// </summary>
    [JsonPropertyName("batteryCount")]
    public int BatteryCount { get; set; }

    /// <summary>
    /// Gets or sets the laboratory order count of the Laboratory Summary.
    /// </summary>
    [JsonPropertyName("labOrderCount")]
    public int LabOrderCount { get; set; }

    /// <summary>
    /// Gets or sets the list of PHSA Laboratory Orders.
    /// </summary>
    [JsonPropertyName("plisLabOrders")]
    public IEnumerable<PhsaLaboratoryOrder>? LabOrders { get; set; }
}
