//----------------------------------------------------------------------------------
// <copyright file="TypedClientResolver.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>5/30/2019</date>
// <summary>Dynamic Typed Client Resolver</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Helper
{
    using Microsoft.Extensions.Http;
    using Prakrishta.Infrastructure.GenericInterfaces;
    using System;
    using System.Net.Http;

    /// <summary>
    /// Defines the methods to resolve http typed client dynamically
    /// </summary>
    /// <typeparam name="TInterface">The generic typed client type parameter</typeparam>
    public class TypedClientResolver<TInterface> : IDynamicResolver<TInterface, string>
    {
        #region |Private Fields|

        /// <summary>
        /// Defines the http client factory
        /// </summary>
        private readonly IHttpClientFactory factory;

        /// <summary>
        /// Defines the typedClient factory
        /// </summary>
        private readonly ITypedHttpClientFactory<TInterface> typedClient;

        #endregion

        #region |Constructors|

        /// <summary>
        /// Initializes a new instance of the <see cref="TypedClientResolver{TInterface}"/> class.
        /// </summary>
        /// <param name="factory">The http client factory object</param>
        /// <param name="typedClient">The typedClient factory object</param>
        public TypedClientResolver(IHttpClientFactory factory, ITypedHttpClientFactory<TInterface> typedClient)
        {
            this.typedClient = typedClient ?? throw new ArgumentNullException("Unable to resolve typed client factory, please make sure it is configured with service collection");
            this.factory = factory ?? throw new ArgumentNullException("Unable to resolve http client factory, please make sure it is configured with service");
        }

        #endregion

        #region |Methods|

        /// <summary>
        /// Method to get the resolved service by key
        /// </summary>
        /// <param name="key">The key to get service</param>
        /// <returns>The resolved service</returns>
        public TInterface GetService(string key)
        {
            var httpClient = this.factory.CreateClient(key);
            return this.typedClient.CreateClient(httpClient);
        }

        #endregion
    }
}
