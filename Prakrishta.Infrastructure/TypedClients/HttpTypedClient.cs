﻿//----------------------------------------------------------------------------------
// <copyright file="HttpTypedClient.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>7/5/2019</date>
// <summary>Http Typed Client class</summary>
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
    /// Defines the <see cref="HttpTypedClient" /> class
    /// </summary>
    public sealed class HttpTypedClient : TypedClientBase
    {
        #region |Private Fields|

        /// <summary>
        /// Defines the logger
        /// </summary>
        private readonly ILogger logger;

        #endregion

        #region |Constructors|

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpTypedClient"/> class.
        /// </summary>
        /// <param name="logger">The logger object of <see cref="ILogger"/> type</param>
        /// <param name="client">The http client <see cref="HttpClient"/></param>
        public HttpTypedClient(ILogger logger, HttpClient client)
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

        /// <summary>
        /// The Get method that retrieves content from URL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The url<see cref="string"/></param>
        /// <param name="memberName">The member name<see cref="string"/></param>
        /// <param name="lineNumber">The line number<see cref="int"/></param>
        /// <param name="filePath">The file path<see cref="string"/></param>
        /// <returns>The deserialized object of <see cref="Task{T}"/> type</returns>
        public async Task<T> GetAsync<T>(string url, [CallerMemberName] string memberName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = null)
            where T : class
        {
            T result = null;
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var response = await this.Client.GetAsync(url).ConfigureAwait(false);
            stopWatch.Stop();

            if (response?.Content != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!string.IsNullOrEmpty(jsonString))
                {
                    try
                    {
                        result = JsonConvert.DeserializeObject<T>(jsonString);
                    }
                    catch
                    {
                        throw new ApiResponseException(new Models.ErrorDetail
                        {
                            StatusCode = 500,
                            Message = "Error deserializing content"
                        });
                    }

                }
                else
                {
                    this.logger.LogInformation($"No content received for the request. Status Code {response.StatusCode}. Request Url: {this.Client.BaseAddress}{url}," +
                        $" Caller: {memberName}, Line number {lineNumber}, File name: {filePath}");
                }
            }
            else if (response != null)
            {
                this.logger.LogError($"{response?.ReasonPhrase}. " +
                        $"Status code: {response?.StatusCode}" +
                        $"Request Url: {this.Client.BaseAddress}{url}" +
                        $"Time taken: {stopWatch.ElapsedMilliseconds}ms" +
                        $" Caller: {memberName}, Line number {lineNumber}, File name: {filePath}");

                if (!string.IsNullOrEmpty(response.ReasonPhrase))
                {
                    throw new ApiResponseException(new Models.ErrorDetail
                    {
                        StatusCode = (int)response.StatusCode,
                        Message = response.ReasonPhrase
                    });
                }
            }

            this.logger.LogInformation($"The get method has taken {stopWatch.ElapsedMilliseconds} ms. Request Url: {this.Client.BaseAddress}{url}");

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

        /// <summary>
        /// The Post method that posts data to URL
        /// </summary>
        /// <typeparam name="T">the generic type parameter</typeparam>
        /// <param name="url">The url <see cref="string"/></param>
        /// <param name="jsonObject">The json content object<see cref="JObject"/></param>
        /// <param name="memberName">The member name<see cref="string"/></param>
        /// <param name="lineNumber">The line number<see cref="int"/></param>
        /// <param name="filePath">The filePath<see cref="string"/></param>
        /// <returns>The <see cref="Task{T}"/> object</returns>
        public async Task<T> PostAsync<T>(string url, JObject jsonObject, [CallerMemberName] string memberName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = null) where T : class
        {
            T result = null;

            var request = base.AddHttpRequestMessage(HttpMethod.Post, jsonObject.ToString(Formatting.Indented), url);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var response = await this.Client.PostAsync(url, request.Content).ConfigureAwait(false);
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
                            $"Status Code {response.StatusCode}. " +
                            $"Request Url: {this.Client.BaseAddress}{url}," +
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
        /// The put method that posts data to URL
        /// </summary>
        /// <typeparam name="T">the generic type parameter</typeparam>
        /// <param name="url">The url <see cref="string"/></param>
        /// <param name="jsonObject">The json content object<see cref="JObject"/></param>
        /// <param name="memberName">The member name<see cref="string"/></param>
        /// <param name="lineNumber">The line number<see cref="int"/></param>
        /// <param name="filePath">The filePath<see cref="string"/></param>
        /// <returns>The <see cref="Task{T}"/> object</returns>
        public async Task<T> PutAsync<T>(string url, JObject jsonObject, [CallerMemberName] string memberName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = null) where T : class
        {
            T result = null;

            var request = base.AddHttpRequestMessage(HttpMethod.Put, jsonObject.ToString(Formatting.Indented), url);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var response = await this.Client.PutAsync(url, request.Content).ConfigureAwait(false);
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
