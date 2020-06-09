// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StartupConfiguration.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using OBeautifulCode.Serialization;
    using OBeautifulCode.Serialization.Json;

    /// <summary>
    /// A configuration object providing the ability to override default behaviors when initializing
    /// a Web API service.
    /// </summary>
    public class StartupConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartupConfiguration"/> class.
        /// </summary>
        /// <param name="jsonConfigurationType">Type of the json configuration.</param>
        /// <param name="initializeLogPolicy">The initialize log policy.</param>
        public StartupConfiguration(JsonSerializationConfigurationType jsonConfigurationType = null, InitializeLogPolicy initializeLogPolicy = null)
        {
            this.InitializeLogPolicy = initializeLogPolicy ?? BasicWebApiLogPolicy.Initialize;
            this.Serializer = new ObcJsonSerializer(
                jsonConfigurationType ?? typeof(NullJsonSerializationConfiguration).ToJsonSerializationConfigurationType());
        }

        /// <summary>
        /// Gets the serializer.
        /// </summary>
        /// <value>The serializer.</value>
        public ISerializer Serializer { get; private set; }

        /// <summary>
        /// Gets a function to initialize the log policy.
        /// </summary>
        /// <value>The initialize log policy function.</value>
        public InitializeLogPolicy InitializeLogPolicy { get; private set; }
    }
}
