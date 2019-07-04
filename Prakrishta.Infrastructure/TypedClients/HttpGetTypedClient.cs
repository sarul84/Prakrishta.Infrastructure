//----------------------------------------------------------------------------------
// <copyright file="HttpGetTypedClient.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>7/4/2019</date>
// <summary>The Http Typed Client</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.TypedClients
{
    using System.Diagnostics;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Prakrishta.Infrastructure.Exceptions;

    /// <summary>
    /// Defines the <see cref="HttpGetTypedClient" /> class
    /// </summary>
    public sealed class HttpGetTypedClient : TypedClientBase
    {
        #region |Private Fields|

        /// <summary>
        /// Defines the logger
        /// </summary>
        private readonly ILogger logger;

        #endregion

        #region |Constructors|

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpGetTypedClient"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger"/></param>
        /// <param name="client">The client<see cref="HttpClient"/></param>
        public HttpGetTypedClient(ILogger logger, HttpClient client)
            : base(client)
        {
            this.logger = logger;
        }

        #endregion

        #region |Methods|

        /// <summary>
        /// The Get method that retrieves content from URL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">The url<see cref="string"/></param>
        /// <param name="memberName">The member name<see cref="string"/></param>
        /// <param name="lineNumber">The line number<see cref="int"/></param>
        /// <param name="filePath">The file path<see cref="string"/></param>
        /// <returns>The deserialized object of <see cref="T"/> type</returns>
        public T Get<T>(string url, [CallerMemberName] string memberName = null, [CallerLineNumber] int lineNumber = 0, [CallerFilePath] string filePath = null)
            where T : class
        {
            T result = null;
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var response = this.Client.GetAsync(url).GetAwaiter().GetResult();
            stopWatch.Stop();

            if (response?.Content != null)
            {
                var jsonString = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

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

        #endregion
    }
}
