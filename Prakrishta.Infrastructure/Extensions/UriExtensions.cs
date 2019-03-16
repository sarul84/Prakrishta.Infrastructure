//----------------------------------------------------------------------------------
// <copyright file="UriExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>3/16/2019</date>
// <summary>Uri Extensions class</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using System;
    using System.Linq;
    using System.Web;

    public static class UriExtensions
    {
        /// <summary>
        /// Adds or Updates the specified parameter to the Query String.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="paramName">Name of the parameter to add.</param>
        /// <param name="paramValue">Value for the parameter to add.</param>
        /// <returns>Url with added parameter.</returns>
        public static Uri AddOrUpdateParameter(this Uri url, string paramName, string paramValue)
        {
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (query.AllKeys.Contains(paramName))
            {
                query[paramName] = paramValue;
            }
            else
            {
                query.Add(paramName, paramValue);
            }
            uriBuilder.Query = query.ToString();

            return uriBuilder.Uri;
        }
    }
}
