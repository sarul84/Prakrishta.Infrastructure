//----------------------------------------------------------------------------------
// <copyright file="QueryParameterBuilder.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>7/5/2019</date>
// <summary>Query Parameter Builder helper class</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Web;

    /// <summary>
    /// Defines the <see cref="QueryParameterBuilder" /> class
    /// </summary>
    public sealed class QueryParameterBuilder
    {
        #region |Private Fields|

        /// <summary>
        /// Defines the builder
        /// </summary>
        private readonly UriBuilder builder;

        /// <summary>
        /// Defines the collection
        /// </summary>
        private readonly NameValueCollection collection;

        #endregion

        #region |Constructors|

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryParameterBuilder"/> class.
        /// </summary>
        public QueryParameterBuilder()
        {
            collection = HttpUtility.ParseQueryString(string.Empty);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryParameterBuilder"/> class.
        /// </summary>
        /// <param name="uri">The uri <see cref="string"/></param>
        public QueryParameterBuilder(string uri) : this()
        {
            if (!string.IsNullOrEmpty(uri))
            {
                builder = new UriBuilder(uri);
            }
        }

        #endregion

        #region |Properties|

        /// <summary>
        /// Gets the QueryParams
        /// </summary>
        public string QueryParams => $"?{collection.ToString()}";

        /// <summary>
        /// Gets the Uri
        /// </summary>
        public Uri Uri
        {
            get
            {
                if (builder != null)
                {
                    builder.Query = collection.ToString();
                }

                return builder?.Uri;
            }
        }

        #endregion

        #region |Methods|

        /// <summary>
        /// Add query parameter to URL name value collection
        /// </summary>
        /// <param name="keyValuePair">The key value pair<see cref="KeyValuePair{string, string}"/></param>
        /// <param name="ignoreEmptyValues">The ignore empty values<see cref="bool"/> flag</param>
        /// <returns>The <see cref="QueryParameterBuilder"/> object</returns>
        public QueryParameterBuilder AddParameter(KeyValuePair<string, string> keyValuePair, bool ignoreEmptyValues = true)
        {
            this.AddParameter(keyValuePair.Key, keyValuePair.Value, ignoreEmptyValues);

            return this;
        }

        /// <summary>
        /// Add query parameter to URL name value collection
        /// </summary>
        /// <param name="paramCollection">The key value pair<see cref="IDictionary{string, string}"/></param>
        /// <param name="ignoreEmptyValues">The ignore empty values<see cref="bool"/> flag</param>
        /// <returns>The <see cref="QueryParameterBuilder"/> object</returns>
        public void AddParameter(IDictionary<string, string> paramCollection, bool ignoreEmptyValues = true)
        {
            foreach(var keyValuePair in paramCollection)
            {
                this.AddParameter(keyValuePair.Key, keyValuePair.Value, ignoreEmptyValues);
            }           
        }

        /// <summary>
        /// Add query parameter to URL name value collection
        /// </summary>
        /// <param name="key">The key<see cref="string"/></param>
        /// <param name="value">The value<see cref="string"/></param>
        /// <param name="ignoreEmptyValues">The ignore empty values<see cref="bool"/> flag</param>
        /// <returns>The <see cref="QueryParameterBuilder"/></returns>
        public QueryParameterBuilder AddParameter(string key, string value, bool ignoreEmptyValues = true)
        {
            if (!(string.IsNullOrEmpty(value) && ignoreEmptyValues))
            {
                collection.Add(key, value);
            }

            return this;
        }

        /// <inheritdoc />
        public override string ToString() => Uri == null ? QueryParams : Uri?.ToString();

        #endregion
    }
}
