// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObcJsonMediaTypeFormatter.cs" company="Naos Project">
//   Copyright (c) Naos Project 2019. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Naos.Bootstrapper
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Threading;
    using System.Threading.Tasks;
    using OBeautifulCode.Serialization;
    using OBeautifulCode.Serialization.Json;

    /// <summary>
    /// A set of credentials to use to sign in a user.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Obc", Justification = "Spelling/name is correct.")]
    public class ObcJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObcJsonMediaTypeFormatter"/> class.
        /// </summary>
        /// <param name="jsonConfigurationType">Type of the json configuration.</param>
        public ObcJsonMediaTypeFormatter(
            JsonSerializationConfigurationType jsonConfigurationType)
        {
            this.Serializer = new ObcJsonSerializer(jsonConfigurationType);
        }

        /// <summary>
        /// Gets the serializer.
        /// </summary>
        /// <value>The serializer.</value>
        public ISerializer Serializer { get; private set; }

        /// <inheritdoc />
        public override bool CanReadType(
            Type type)
        {
            return true;
        }

        /// <inheritdoc />
        public override bool CanWriteType(
            Type type)
        {
            return true;
        }

        /// <inheritdoc />
        public override async Task<object> ReadFromStreamAsync(
            Type             type,
            Stream           readStream,
            HttpContent      content,
            IFormatterLogger formatterLogger)
        {
            string contents = null;
            using (var streamReader = new StreamReader(readStream))
            {
                contents = streamReader.ReadToEnd();
            }

            var result = this.Serializer.Deserialize(contents, type);

            return await Task.FromResult(result);
        }

        /// <inheritdoc />
        public override Task<object> ReadFromStreamAsync(
            Type              type,
            Stream            readStream,
            HttpContent       content,
            IFormatterLogger  formatterLogger,
            CancellationToken cancellationToken)
        {
            string contents = null;
            using (var streamReader = new StreamReader(readStream))
            {
                contents = streamReader.ReadToEnd();
            }

            var result = this.Serializer.Deserialize(contents, type);

            return Task.FromResult(result);
        }

        /// <inheritdoc />
        public override async Task WriteToStreamAsync(
            Type             type,
            object           value,
            Stream           writeStream,
            HttpContent      content,
            TransportContext transportContext)
        {
            var contents = this.Serializer.SerializeToString(value);
            using (var streamWriter = new StreamWriter(writeStream))
            {
                await streamWriter.WriteAsync(contents);
            }
        }

        /// <inheritdoc />
        public override async Task WriteToStreamAsync(
            Type              type,
            object            value,
            Stream            writeStream,
            HttpContent       content,
            TransportContext  transportContext,
            CancellationToken cancellationToken)
        {
            var contents = this.Serializer.SerializeToString(value);
            using (var streamWriter = new StreamWriter(writeStream))
            {
                await streamWriter.WriteAsync(contents);
            }
        }
    }
}
