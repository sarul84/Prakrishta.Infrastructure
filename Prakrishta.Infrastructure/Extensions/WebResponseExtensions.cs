//----------------------------------------------------------------------------------
// <copyright file="WebResponseExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>3/3/2019</date>
// <summary>Extension class for WebResponse</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using System.IO;
    using System.Net;
    using System.Threading.Tasks;

    public static class WebResponseExtensions
    {

        /// <summary>
        /// Gets response from webresponse using streams
        /// </summary>
        /// <param name="this">The original web response</param>
        /// <returns>The response string</returns>
        public static string ReadToEnd(this WebResponse source)
        {
            using (Stream stream = source.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Gets response from webresponse using streams
        /// </summary>
        /// <param name="this">The original web response</param>
        /// <returns>The response string</returns>
        public static async Task<string> ReadToEndAsync(this WebResponse source)
        {
            using (Stream stream = source.GetResponseStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    return await reader.ReadToEndAsync();
                }
            }
        }
    }
}
