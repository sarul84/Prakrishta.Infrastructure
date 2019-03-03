//----------------------------------------------------------------------------------
// <copyright file="IUpdateItemAsync.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>3/3/2019</date>
// <summary>Contract that defines methods to update items</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.AsyncInterfaces
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// THe interface for update methods
    /// </summary>
    /// <typeparam name="TEntity">The input entity type</typeparam>
    /// <typeparam name="TResult">The output type</typeparam>
    public interface IUpdateItemAsync<in TEntity, TResult>
        where TEntity : class
    {
        /// <summary>
        /// Update record with modified details
        /// </summary>
        /// <param name="entity">The modified entity</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>The updated entity</returns>
        Task<TResult> UpdateAsync(TEntity entity, CancellationToken token = default(CancellationToken));
    }

    /// <summary>
    /// THe interface for update methods
    /// </summary>
    /// <typeparam name="TIdentity">The identity type</typeparam>
    /// <typeparam name="TEntity">The input entity type</typeparam>
    /// <typeparam name="TResult">The output type</typeparam>
    public interface IUpdateItemAsync<in TIdentity, in TEntity, TResult>
        where TEntity : class
    {
        /// <summary>
        /// Update record with modified details
        /// </summary>
        /// <param name="entity">The modified entity</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>The updated entity</returns>
        Task<TResult> UpdateAsync(TIdentity id, TEntity entity, CancellationToken token = default(CancellationToken));
    }

    /// <summary>
    /// THe interface for update methods
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IUpdateItemAsync<TEntity> where TEntity : class
    {
        /// <summary>
        /// Update record with modified details
        /// </summary>
        /// <param name="entity">The modified entity</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>The updated entity</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default(CancellationToken));
    }

    /// <summary>
    /// The interface for update collection of records
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IUpdateCollectionAsync<TEntity> where TEntity : class
    {
        /// <summary>
        /// Update collection of entities
        /// </summary>
        /// <param name="entities">The list of modified entities</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>The awaitable task</returns>
        Task UpdateAsync(IEnumerable<TEntity> entities, CancellationToken token = default(CancellationToken));
    }
}
