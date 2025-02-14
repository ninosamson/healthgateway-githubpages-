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
namespace HealthGateway.Admin.Client.Services;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HealthGateway.Admin.Common.Models;
using HealthGateway.Common.Data.ViewModels;
using Refit;

/// <summary>
/// API for interacting with user feedback.
/// </summary>
public interface IUserFeedbackApi
{
    /// <summary>
    /// Gets all user feedback.
    /// </summary>
    /// <returns>The wrapped collection of models.</returns>
    [Get("/")]
    Task<ApiResponse<RequestResult<IEnumerable<UserFeedbackView>>>> GetAll();

    /// <summary>
    /// Updates a user feedback.
    /// </summary>
    /// <param name="userFeedbackView">The model to update.</param>
    /// <returns>The wrapped model.</returns>
    [Patch("/")]
    Task<ApiResponse<RequestResult<UserFeedbackView>>> Update([Body] UserFeedbackView userFeedbackView);

    /// <summary>
    /// Associate existing admin tags to the feedback with matching id.
    /// </summary>
    /// <returns>The feedback model wrapped in a request result.</returns>
    /// <param name="tagIds">The collection of tag IDs.</param>
    /// <param name="feedbackId">The feedback ID.</param>
    [Put("/{feedbackId}/Tag")]
    Task<ApiResponse<RequestResult<UserFeedbackView>>> AssociateTags([Body] IEnumerable<Guid> tagIds, Guid feedbackId);
}
