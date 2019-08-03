//----------------------------------------------------------------------------------
// <copyright file="HttpDeleteTypedClient.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>7/5/2019</date>
// <summary>Http Delete TypedClient class</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.TypedClients
{    
    using System.Diagnostics;
    using System.Net.Http;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Defines the <see cref="HttpDeleteTypedClient" /> class
    /// </summary>
    public sealed class HttpDeleteTypedClient : TypedClientBase
    {
        #region |Constructors|

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpDeleteTypedClient"/> class.
        /// </summary>
        /// <param name="logger">The logger object of <see cref="ILogger"/> type</param>
        /// <param name="client">The http client <see cref="HttpClient"/></param>
        public HttpDeleteTypedClient(ILogger logger, HttpClient client)
            : base(logger, client)
        {
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
            var request = base.GetRequest(HttpMethod.Delete, url);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var response = this.Client.DeleteAsync(url).GetAwaiter().GetResult();
            stopwatch.Stop();

            return this.DeserializeResponse<T>(url, request, response, stopwatch.ElapsedMilliseconds, memberName, lineNumber, filePath).GetAwaiter().GetResult();
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
            var request = base.GetRequest(HttpMethod.Delete, url);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var response = await this.Client.DeleteAsync(url).ConfigureAwait(false);
            stopwatch.Stop();

            return await this.DeserializeResponse<T>(url, request, response, stopwatch.ElapsedMilliseconds, memberName, lineNumber, filePath);
        }

        #endregion
    }
}
