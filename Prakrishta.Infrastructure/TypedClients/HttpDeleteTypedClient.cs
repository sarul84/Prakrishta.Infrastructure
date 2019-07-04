﻿//----------------------------------------------------------------------------------
// <copyright file="HttpDeleteTypedClient.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>7/5/2019</date>
// <summary>Http Delete TypedClient class</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.TypedClients
{
    using System;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Prakrishta.Infrastructure.Exceptions;    

    /// <summary>
    /// Defines the <see cref="HttpDeleteTypedClient" /> class
    /// </summary>
    public sealed class HttpDeleteTypedClient : TypedClientBase
    {
        #region |Private Fields|

        /// <summary>
        /// Defines the logger
        /// </summary>
        private readonly ILogger logger;

        #endregion

        #region |Constructors|

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpDeleteTypedClient"/> class.
        /// </summary>
        /// <param name="logger">The logger object of <see cref="ILogger"/> type</param>
        /// <param name="client">The http client <see cref="HttpClient"/></param>
        public HttpDeleteTypedClient(ILogger logger, HttpClient client)
            : base(client)
        {
            this.logger = logger;
        }

        #endregion

        #region |Methods|

        /// <summary>
        /// The Delete method that posts data to URL
        /// </summary>
        /// <typeparam name="T">the generic type parameter</typeparam>
        /// <param name="url">The url <see cref="string"/></param>
        /// <param name="memberName">The member name<see cref="string"/></param>
        /// <param name="lineNumber">The line number<see cref="int"/></param>
        /// <param name="filePath">The filePath<see cref="string"/></param>
        /// <returns>The <see cref="T"/> object</returns>
        public T Delete<T>(string url, [CallerMemberName] string memberName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = null) where T : class
        {
            T result = null;

            var request = base.GetRequest(HttpMethod.Delete, url);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var response = this.Client.DeleteAsync(url).GetAwaiter().GetResult();
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
        /// The Delete method that posts data to URL
        /// </summary>
        /// <typeparam name="T">the generic type parameter</typeparam>
        /// <param name="url">The url <see cref="string"/></param>
        /// <param name="memberName">The member name<see cref="string"/></param>
        /// <param name="lineNumber">The line number<see cref="int"/></param>
        /// <param name="filePath">The filePath<see cref="string"/></param>
        /// <returns>The <see cref="T"/> object</returns>
        public async Task<T> DeleteAsync<T>(string url, [CallerMemberName] string memberName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = null) where T : class
        {
            T result = null;

            var request = base.GetRequest(HttpMethod.Delete, url);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var response = await this.Client.DeleteAsync(url).ConfigureAwait(false);
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

        #endregion
    }
}