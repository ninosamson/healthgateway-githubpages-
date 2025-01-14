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

namespace HealthGateway.Admin.Client.Store;

using System.Collections.Generic;

/// <summary>
/// Record containing information related to an error returned from an external request.
/// </summary>
public record RequestError
{
    /// <summary>
    /// Gets the short description of the error.
    /// </summary>
    public string Message { get; init; } = string.Empty;

    /// <summary>
    /// Gets a detailed collection of properties associated with the error.
    /// </summary>
    public Dictionary<string, string> Details { get; init; } = new();
}
