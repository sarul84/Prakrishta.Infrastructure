//----------------------------------------------------------------------------------
// <copyright file="HttpPatchTypedClient.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>7/5/2019</date>
// <summary>Http Patch Typed Client class.</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.TypedClients
{
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Prakrishta.Infrastructure.Exceptions;   

    /// <summary>
    /// Defines the <see cref="HttpPatchTypedClient" /> class
    /// </summary>
    public sealed class HttpPatchTypedClient : TypedClientBase
    {        
        #region |Constructors|

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpPatchTypedClient"/> class.
        /// </summary>
        /// <param name="logger">The logger object of <see cref="ILogger"/> type</param>
        /// <param name="client">The http client <see cref="HttpClient"/></param>
        public HttpPatchTypedClient(ILogger logger, HttpClient client)
            : base(logger, client)
        {
        }

        #endregion

        #region |Methods|

        /// <summary>
        /// The patch method that posts data to URL
        /// </summary>
        /// <typeparam name="T">the generic type parameter</typeparam>
        /// <param name="url">The url <see cref="string"/></param>
        /// <param name="jsonObject">The json content object<see cref="JObject"/></param>
        /// <param name="memberName">The member name<see cref="string"/></param>
        /// <param name="lineNumber">The line number<see cref="int"/></param>
        /// <param name="filePath">The filePath<see cref="string"/></param>
        /// <returns>The <see cref="T"/> object</returns>
        public T Patch<T>(string url, JObject jsonObject, [CallerMemberName] string memberName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = null) where T : class
        {
            var request = this.AddHttpRequestMessage(new HttpMethod("PATCH"),
                            jsonObject?.ToString(Formatting.Indented), url);

            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var response = this.Client.SendAsync(request).GetAwaiter().GetResult();

            stopwatch.Stop();

            return this.DeserializeResponse<T>(url, request, response, stopwatch.ElapsedMilliseconds, memberName, lineNumber, filePath).GetAwaiter().GetResult();
        }

        /// <summary>
        /// The Patch method that posts data to URL
        /// </summary>
        /// <typeparam name="T">the generic type parameter</typeparam>
        /// <param name="url">The url <see cref="string"/></param>
        /// <param name="jsonObject">The json content object<see cref="JObject"/></param>
        /// <param name="memberName">The member name<see cref="string"/></param>
        /// <param name="lineNumber">The line number<see cref="int"/></param>
        /// <param name="filePath">The filePath<see cref="string"/></param>
        /// <returns>The <see cref="T"/> object</returns>
        public async Task<T> PatchAsync<T>(string url, JObject jsonObject, [CallerMemberName] string memberName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = null)
            where T : class
        {
            var request = this.AddHttpRequestMessage(new HttpMethod("PATCH"),
                                jsonObject?.ToString(Formatting.Indented), url);

            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var response = await this.Client.SendAsync(request).ConfigureAwait(false);

            stopwatch.Stop();

            return await this.DeserializeResponse<T>(url, request, response, stopwatch.ElapsedMilliseconds, memberName, lineNumber, filePath);
        }

        #endregion
    }
}
