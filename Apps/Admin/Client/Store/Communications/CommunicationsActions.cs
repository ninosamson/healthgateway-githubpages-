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
namespace HealthGateway.Admin.Client.Store.Communications;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using HealthGateway.Admin.Common.Models;
using HealthGateway.Common.Data.ViewModels;

/// <summary>
/// Static class that implements all actions for the feature.
/// </summary>
[SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Team decision")]
public static class CommunicationsActions
{
    /// <summary>
    /// The action representing the initiation of an add.
    /// </summary>
    public class AddAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddAction"/> class.
        /// </summary>
        /// <param name="communication">Represents the communication model.</param>
        public AddAction(Communication communication)
        {
            this.Communication = communication;
        }

        /// <summary>
        /// Gets or sets the communication.
        /// </summary>
        public Communication Communication { get; set; }
    }

    /// <summary>
    /// The action representing a failed add.
    /// </summary>
    public class AddFailAction : BaseFailAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddFailAction"/> class.
        /// </summary>
        /// <param name="error">The request error.</param>
        public AddFailAction(RequestError error)
            : base(error)
        {
        }
    }

    /// <summary>
    /// The action representing a successful add.
    /// </summary>
    public class AddSuccessAction : BaseSuccessAction<RequestResult<Communication>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddSuccessAction"/> class.
        /// </summary>
        /// <param name="data">Communication data.</param>
        public AddSuccessAction(RequestResult<Communication> data)
            : base(data)
        {
        }
    }

    /// <summary>
    /// The action representing the initiation of a load.
    /// </summary>
    public class LoadAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadAction"/> class.
        /// </summary>
        public LoadAction()
        {
        }
    }

    /// <summary>
    /// The action representing a failed load.
    /// </summary>
    public class LoadFailAction : BaseFailAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadFailAction"/> class.
        /// </summary>
        /// <param name="error">The request error.</param>
        public LoadFailAction(RequestError error)
            : base(error)
        {
        }
    }

    /// <summary>
    /// The action representing a successful load.
    /// </summary>
    public class LoadSuccessAction : BaseSuccessAction<RequestResult<IEnumerable<Communication>>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadSuccessAction"/> class.
        /// </summary>
        /// <param name="requestResultModel">Communications data.</param>
        public LoadSuccessAction(RequestResult<IEnumerable<Communication>> requestResultModel)
            : base(requestResultModel)
        {
        }
    }

    /// <summary>
    /// The action representing the initiation of an update.
    /// </summary>
    public class UpdateAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAction"/> class.
        /// </summary>
        /// <param name="communication">Represents the communication model.</param>
        public UpdateAction(Communication communication)
        {
            this.Communication = communication;
        }

        /// <summary>
        /// Gets or sets the communication.
        /// </summary>
        public Communication Communication { get; set; }
    }

    /// <summary>
    /// The action representing a failed update.
    /// </summary>
    public class UpdateFailAction : BaseFailAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateFailAction"/> class.
        /// </summary>
        /// <param name="error">The request error.</param>
        public UpdateFailAction(RequestError error)
            : base(error)
        {
        }
    }

    /// <summary>
    /// The action representing a successful update.
    /// </summary>
    public class UpdateSuccessAction : BaseSuccessAction<RequestResult<Communication>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSuccessAction"/> class.
        /// </summary>
        /// <param name="data">Communication data.</param>
        public UpdateSuccessAction(RequestResult<Communication> data)
            : base(data)
        {
        }
    }

    /// <summary>
    /// The action representing the initiation of a deletion.
    /// </summary>
    public class DeleteAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAction"/> class.
        /// </summary>
        /// <param name="communication">Represents the communication model.</param>
        public DeleteAction(Communication communication)
        {
            this.Communication = communication;
        }

        /// <summary>
        /// Gets or sets the communication.
        /// </summary>
        public Communication Communication { get; set; }
    }

    /// <summary>
    /// The action representing a failed deletion.
    /// </summary>
    public class DeleteFailAction : BaseFailAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteFailAction"/> class.
        /// </summary>
        /// <param name="error">The request error.</param>
        public DeleteFailAction(RequestError error)
            : base(error)
        {
        }
    }

    /// <summary>
    /// The action representing a successful deletion.
    /// </summary>
    public class DeleteSuccessAction : BaseSuccessAction<RequestResult<Communication>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSuccessAction"/> class.
        /// </summary>
        /// <param name="data">Communication data.</param>
        public DeleteSuccessAction(RequestResult<Communication> data)
            : base(data)
        {
        }
    }

    /// <summary>
    /// The action that clears the state.
    /// </summary>
    public class ResetStateAction
    {
    }

    /// <summary>
    /// The action that toggles whether a particular communication is expanded.
    /// </summary>
    public class ToggleIsExpandedAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleIsExpandedAction"/> class.
        /// </summary>
        /// <param name="id">Represents the ID of the communication.</param>
        public ToggleIsExpandedAction(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the ID of the communication.
        /// </summary>
        public Guid Id { get; set; }
    }
}
