//----------------------------------------------------------------------------------
// <copyright file="TypedClientBase.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>7/3/2019</date>
// <summary>The http typed client base</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.TypedClients
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Prakrishta.Infrastructure.Exceptions;
    
    /// <summary>
    /// Base class for http typed clients
    /// </summary>
    public abstract class TypedClientBase
    {
        #region |Constants|

        /// <summary>
        /// Constant for media type header
        /// </summary>
        private const string MediaType = "application/json";

        #endregion

        #region |Private Fields|

        /// <summary>
        /// Holds httpclient object instance
        /// </summary>
        protected readonly HttpClient Client;

        /// <summary>
        /// Defines the Logger
        /// </summary>
        protected readonly ILogger Logger;

        #endregion

        #region |Constructors|

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedClientBase"/> class.
        /// </summary>
        /// <param name="logger">The logger object</param>
        /// <param name="client">The http client object</param>
        public TypedClientBase(ILogger logger, HttpClient client)
        {
            this.Logger = logger;
            this.Client = client;
            this.Client.DefaultRequestHeaders.Connection.Add("keep-alive");
        }

        #endregion

        #region |Methods|

        /// <summary>
        /// Adds authentication token to default request header
        /// </summary>
        /// <param name="authorizationToken">The authorization token</param>
        /// <param name="authorizationMethod">The authorization method</param>
        public void AddAuthorization(string authorizationToken, string authorizationMethod = "Bearer")
        {
            this.Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
        }

        /// <summary>
        /// Adds the specified header and its values into the System.Net.Http.Headers.HttpHeaders
        /// collection.
        /// </summary>
        /// <param name="name">The header to add to the collection.</param>
        /// <param name="values">A list of header values to add to the collection.</param>
        public void AddDefaultRequestHeader(string name, IEnumerable<string> values)
        {
            this.AddMediaType(MediaType);
            this.Client.DefaultRequestHeaders.Remove(name);
            this.Client.DefaultRequestHeaders.Add(name, values);
        }

        /// <summary>
        /// Adds the specified header and its value into the System.Net.Http.Headers.HttpHeaders
        /// collection.
        /// </summary>
        /// <param name="name">The header to add to the collection.</param>
        /// <param name="value">The content of the header.</param>
        public void AddDefaultRequestHeader(string name, string value)
        {
            this.AddMediaType(MediaType);
            this.Client.DefaultRequestHeaders.Remove(name);
            this.Client.DefaultRequestHeaders.Add(name, value);
        }

        /// <summary>
        /// Creates http request message object
        /// </summary>
        /// <param name="httpMethod">Http operation method type</param>
        /// <param name="query">The graphql query</param>
        /// <param name="url">The graphql api Uri</param>
        /// <returns>The new http request message object</returns>
        public HttpRequestMessage AddHttpRequestMessage(HttpMethod httpMethod, string query, string url)
        {
            return new HttpRequestMessage(httpMethod, url)
            {
                Content = new StringContent(query, Encoding.UTF8, MediaType)
            };
        }

        /// <summary>
        /// The method that creates HttpRequest object
        /// </summary>
        /// <param name="method">The method<see cref="HttpMethod"/></param>
        /// <param name="url">The url<see cref="string"/></param>
        /// <returns>The <see cref="HttpRequestMessage"/></returns>
        public HttpRequestMessage GetRequest(HttpMethod method, string url)
        {
            var request = new HttpRequestMessage(method, Client.BaseAddress + url);

            foreach (var header in Client.DefaultRequestHeaders)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            return request;
        }

        /// <summary>
        /// The method that deserializes http response message
        /// </summary>
        /// <typeparam name="T">the generic type parameter</typeparam>
        /// <param name="url">The url <see cref="string"/></param>
        /// <param name="request">The request<see cref="HttpRequestMessage"/></param>
        /// <param name="httpResponse">The http response<see cref="HttpResponseMessage"/></param>
        /// <param name="elapsedTime">The elapsed time<see cref="long"/></param>
        /// <param name="memberName">The member name<see cref="string"/></param>
        /// <param name="lineNumber">The line number<see cref="int"/></param>
        /// <param name="filePath">The filePath<see cref="string"/></param>
        /// <returns>The deserialized<see cref="T"/> object</returns>
        protected async Task<T> DeserializeResponse<T>(string url,
            HttpRequestMessage request,
            HttpResponseMessage httpResponse,
            long elapsedTime,
            string memberName,
            int lineNumber,
            string filePath)
        {
            T result = default(T);

            if (httpResponse?.Content != null && httpResponse?.IsSuccessStatusCode == true)
            {
                var jsonString = await httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                try
                {
                    if (!string.IsNullOrEmpty(jsonString))
                    {
                        result = JsonConvert.DeserializeObject<T>(jsonString);
                    }
                }
                catch
                {
                    throw new ApiResponseException(new Models.ErrorDetail
                    {
                        StatusCode = (int)httpResponse.StatusCode,
                        Message = $"Error deserialzing response. The response: {jsonString}",
                        EventId = Guid.NewGuid().ToString()
                    });
                }
            }
            else if (httpResponse != null)
            {
                this.LogError($"{this.Client.BaseAddress}{url}",
                    httpResponse.ReasonPhrase,
                    elapsedTime,
                    httpResponse.StatusCode,
                    memberName,
                    lineNumber,
                    filePath);

                throw new ApiResponseException(new Models.ErrorDetail
                {
                    StatusCode = (int)httpResponse.StatusCode,
                    Message = httpResponse.ReasonPhrase,
                    EventId = Guid.NewGuid().ToString()
                });
            }

            this.LogInformation($"{this.Client.BaseAddress}{url}", request.Method, elapsedTime,
                httpResponse.StatusCode, memberName, lineNumber, filePath);

            httpResponse?.Dispose();

            return result;
        }

        /// <summary>
        /// Add the value of the Accept header for an HTTP request.
        /// </summary>
        /// <param name="mediaType">The accept header media type</param>
        private void AddMediaType(string mediaType)
        {
            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        }

        /// <summary>
        /// The method that logs error
        /// </summary>
        /// <param name="url">The url<see cref="string"/></param>
        /// <param name="reason">The reason<see cref="string"/></param>
        /// <param name="time">The time<see cref="long"/></param>
        /// <param name="statusCode">The statusCode<see cref="HttpStatusCode"/></param>
        /// <param name="memberName">The member name<see cref="string"/></param>
        /// <param name="lineNumber">The line number<see cref="int"/></param>
        /// <param name="filePath">The filePath<see cref="string"/></param>
        private void LogError(string url, string reason, long time,
            HttpStatusCode statusCode,
            string memberName,
            int lineNumber,
            string filePath)
        {
            this.Logger.LogError($"{reason}. " +
                        $"Status code: {statusCode}" +
                        $"Request Url: {url}" +
                        $"Time taken: {time}ms" +
                        $" Caller: {memberName}, Line number {lineNumber}, File name: {filePath}");
        }

        /// <summary>
        /// The method that logs information
        /// </summary>
        /// <param name="url">The url<see cref="string"/></param>
        /// <param name="method">The method<see cref="HttpMethod"/></param>
        /// <param name="time">The elapsed time<see cref="long"/></param>
        /// <param name="statusCode">The statusCode<see cref="HttpStatusCode"/></param>
        /// <param name="memberName">The member name<see cref="string"/></param>
        /// <param name="lineNumber">The line number<see cref="int"/></param>
        /// <param name="filePath">The filePath<see cref="string"/></param>
        private void LogInformation(string url,
            HttpMethod method,
            long time,
            HttpStatusCode statusCode,
            string memberName,
            int lineNumber,
            string filePath)
        {
            this.Logger.LogInformation($"The {method} method has taken {time} ms. " +
                $"Status code: {statusCode}" +
                $"Request Url: {url} Caller: {memberName}, Line number {lineNumber}, File name: {filePath}");
        }

        #endregion
    }
}
