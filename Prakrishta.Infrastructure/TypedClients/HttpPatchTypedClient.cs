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
        #region |Private Fields|

        /// <summary>
        /// Defines the logger
        /// </summary>
        private readonly ILogger logger;

        #endregion

        #region |Constructors|

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpPatchTypedClient"/> class.
        /// </summary>
        /// <param name="logger">The logger object of <see cref="ILogger"/> type</param>
        /// <param name="client">The http client <see cref="HttpClient"/></param>
        public HttpPatchTypedClient(ILogger logger, HttpClient client)
            : base(client)
        {
            this.logger = logger;
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
            T result = null;

            var request = this.AddHttpRequestMessage(new HttpMethod("PATCH"),
                            jsonObject?.ToString(Formatting.Indented), url);

            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var response = this.Client.SendAsync(request).GetAwaiter().GetResult();

            stopwatch.Stop();

            if (response?.Content != null)
            {
                var jsonString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                try
                {
                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        result = JsonConvert.DeserializeObject<T>(jsonString);
                    }
                    else
                    {
                        this.logger.LogInformation($"No content received for the request. Status Code {response.StatusCode}. Request Url: {this.Client.BaseAddress}{url}," +
                            $" Caller: {memberName}, Line number {lineNumber}, File name: {filePath}");
                    }
                }
                catch
                {
                    throw new ApiResponseException(new Models.ErrorDetail
                    {
                        StatusCode = 500,
                        Message = $"Error deserialzing response: {jsonString}",
                        EventId = Guid.NewGuid().ToString()
                    });
                }
            }
            else if (response != null)
            {
                this.logger.LogError($"{response?.ReasonPhrase}. " +
                        $"Status code: {response?.StatusCode}" +
                        $"Request Url: {this.Client.BaseAddress}{url}" +
                        $"Time taken: {stopwatch.ElapsedMilliseconds}ms" +
                        $" Caller: {memberName}, Line number {lineNumber}, File name: {filePath}");

                throw new ApiResponseException(new Models.ErrorDetail
                {
                    StatusCode = (int)response.StatusCode,
                    Message = response.ReasonPhrase,
                    EventId = Guid.NewGuid().ToString()
                });
            }

            this.logger.LogInformation($"The {request.Method} method has taken {stopwatch.ElapsedMilliseconds} ms. " +
                $"Status code: {response.StatusCode}" +
                $"Request Url: {this.Client.BaseAddress}{url}");

            return result;
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
            T result = null;

            var request = this.AddHttpRequestMessage(new HttpMethod("PATCH"),
                                jsonObject?.ToString(Formatting.Indented), url);

            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var response = await this.Client.SendAsync(request).ConfigureAwait(false);

            stopwatch.Stop();

            if (response?.Content != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                try
                {
                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        result = JsonConvert.DeserializeObject<T>(jsonString);
                    }
                    else
                    {
                        this.logger.LogInformation($"No content received for the request. " +
                            $"Status Code {response.StatusCode}. Request Url: {this.Client.BaseAddress}{url}," +
                            $" Caller: {memberName}, Line number {lineNumber}, File name: {filePath}");
                    }
                }
                catch
                {
                    throw new ApiResponseException(new Models.ErrorDetail
                    {
                        StatusCode = 500,
                        Message = $"Error deserialzing response: {jsonString}",
                        EventId = Guid.NewGuid().ToString()
                    });
                }
            }
            else if (response != null)
            {
                this.logger.LogError($"{response?.ReasonPhrase}. " +
                        $"Status code: {response?.StatusCode}" +
                        $"Request Url: {this.Client.BaseAddress}{url}" +
                        $"Time taken: {stopwatch.ElapsedMilliseconds}ms" +
                        $" Caller: {memberName}, Line number {lineNumber}, File name: {filePath}");

                throw new ApiResponseException(new Models.ErrorDetail
                {
                    StatusCode = (int)response.StatusCode,
                    Message = response.ReasonPhrase,
                    EventId = Guid.NewGuid().ToString()
                });
            }

            this.logger.LogInformation($"The {request.Method} method has taken {stopwatch.ElapsedMilliseconds} ms. " +
                $"Status code: {response.StatusCode}" +
                $"Request Url: {this.Client.BaseAddress}{url}");

            return result;
        }

        #endregion
    }
}
