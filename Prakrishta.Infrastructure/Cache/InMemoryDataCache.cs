//----------------------------------------------------------------------------------
// <copyright file="InMemoryDataCache.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/21/2019</date>
// <summary>The in memory data cache store class</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Cache
{    
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Caching.Memory;

    /// <summary>
    /// Defines the <see cref="InMemoryDataCache" /> class
    /// </summary>
    public class InMemoryDataCache : IDataCache
    {
        #region |Private Fields|

        /// <summary>
        /// Defines the expirationConfiguration
        /// </summary>
        private readonly Dictionary<string, TimeSpan> expirationConfiguration;

        /// <summary>
        /// Defines the memoryCache
        /// </summary>
        private readonly IMemoryCache memoryCache;

        #endregion

        #region |Constructors|

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryDataCache"/> class.
        /// </summary>
        /// <param name="memoryCache">The memory cache<see cref="IMemoryCache"/> object</param>
        /// <param name="expirationConfiguration">The expiration configuration<see cref="Dictionary{string, TimeSpan}"/></param>
        public InMemoryDataCache(IMemoryCache memoryCache, Dictionary<string, TimeSpan> expirationConfiguration)
        {
            this.memoryCache = memoryCache;
            this.expirationConfiguration = expirationConfiguration;
        }

        #endregion

        #region |Methods|

        /// <inheritdoc />
        public void Add<TItem>(TItem item, ICacheKey<TItem> key)
        {
            var cachedObjectName = item.GetType().Name;
            var timespan = this.expirationConfiguration[cachedObjectName];

            this.memoryCache.Set(key.CacheKey, item, timespan);
        }

        /// <inheritdoc />
        public TItem Get<TItem>(ICacheKey<TItem> key) where TItem : class
        {
            if (this.memoryCache.TryGetValue(key.CacheKey, out TItem value))
            {
                return value;
            }

            return null;
        }

        #endregion
    }
}
