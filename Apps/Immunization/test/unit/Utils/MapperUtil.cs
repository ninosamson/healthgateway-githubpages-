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

namespace HealthGateway.ImmunizationTests.Utils
{
    using AutoMapper;
    using HealthGateway.Immunization.MapProfiles;

    /// <summary>
    /// Static utility class to provide a fully initialized AutoMapper.
    /// NOTE:  Any newly added profiles will have to be registered.
    /// </summary>
    public static class MapperUtil
    {
        /// <summary>
        /// Creates an AutoMapper.
        /// </summary>
        /// <returns>A configured AutoMapper.</returns>
        public static IMapper InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AgentProfile());
                cfg.AddProfile(new DefinitionProfile());
                cfg.AddProfile(new EventProfile());
                cfg.AddProfile(new ForecastProfile());
                cfg.AddProfile(new LoadStateProfile());
                cfg.AddProfile(new TargetDiseaseProfile());
            });

            return config.CreateMapper();
        }
    }
}
