//----------------------------------------------------------------------------------
// <copyright file="IDataCache.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/21/2019</date>
// <summary>The contract that defines methods for Data Cache class</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Cache
{
    /// <summary>
    /// Defines the <see cref="IDataCache" /> interface
    /// </summary>
    public interface IDataCache
    {
        #region |Methods|

        /// <summary>
        /// The method to add objects to cache store
        /// </summary>
        /// <typeparam name="TItem">The generic type of data that is going to be stored in cache</typeparam>
        /// <param name="item">The item<see cref="TItem"/></param>
        /// <param name="key">The cache key object</param>
        void Add<TItem>(TItem item, ICacheKey<TItem> key);

        /// <summary>
        /// The method that retrieves data from cache store
        /// </summary>
        /// <typeparam name="TItem">The generic type of data that is going to be retrieved from cache</typeparam>
        /// <param name="key">The cache key<see cref="ICacheKey{TItem}"/> object</param>
        /// <returns>The <see cref="TItem"/></returns>
        TItem? Get<TItem>(ICacheKey<TItem> key) where TItem : class;

        #endregion
    }
}
