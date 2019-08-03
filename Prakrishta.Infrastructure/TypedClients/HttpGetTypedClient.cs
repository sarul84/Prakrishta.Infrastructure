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
    
    /// <summary>
    /// Defines the <see cref="HttpGetTypedClient" /> class
    /// </summary>
    public sealed class HttpGetTypedClient : TypedClientBase
    {
        #region |Constructors|

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpGetTypedClient"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger"/></param>
        /// <param name="client">The client<see cref="HttpClient"/></param>
        public HttpGetTypedClient(ILogger logger, HttpClient client)
            : base(logger, client)
        {
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
            var request = base.GetRequest(HttpMethod.Get, url);

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var response = this.Client.GetAsync(url).GetAwaiter().GetResult();
            stopWatch.Stop();

            return this.DeserializeResponse<T>(url, request, response, stopWatch.ElapsedMilliseconds, memberName, lineNumber, filePath).GetAwaiter().GetResult();
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
            var request = base.GetRequest(HttpMethod.Get, url);

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var response = await this.Client.GetAsync(url).ConfigureAwait(false);
            stopWatch.Stop();

            return await this.DeserializeResponse<T>(url, request, response, stopWatch.ElapsedMilliseconds, memberName, lineNumber, filePath);
        }

        #endregion
    }
}
