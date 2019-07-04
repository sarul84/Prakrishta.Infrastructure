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
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;

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

        #endregion

        #region |Constructors|

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedClientBase"/> class.
        /// </summary>
        /// <param name="client">The http client object</param>
        public TypedClientBase(HttpClient client)
        {
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

        public HttpRequestMessage GetRequest(HttpMethod method, string url)
        {
            var request = new HttpRequestMessage(method, Client.BaseAddress + url);

            foreach(var header in Client.DefaultRequestHeaders)
            {
                request.Headers.Add(header.Key, header.Value);
            }

            return request;
        }

        /// <summary>
        /// Add the value of the Accept header for an HTTP request.
        /// </summary>
        /// <param name="mediaType">The accept header media type</param>
        private void AddMediaType(string mediaType)
        {
            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        }

        #endregion
    }
}
