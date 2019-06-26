//----------------------------------------------------------------------------------
// <copyright file="ICacheKey.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/21/2019</date>
// <summary>Contract that defines Cache Key</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Cache
{
    /// <summary>
    /// Defines the <see cref="ICacheKey{TItem}" /> interface
    /// </summary>
    /// <typeparam name="TItem">The cached item type</typeparam>
    public interface ICacheKey<TItem>
    {
        #region |Properties|

        /// <summary>
        /// Gets the CacheKey
        /// </summary>
        string CacheKey { get; }

        #endregion
    }
}
