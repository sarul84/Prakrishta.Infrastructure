//----------------------------------------------------------------------------------
// <copyright file="IDeleteItemAsync.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/16/2019</date>
// <summary>The contract that defines methods for deleting records</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.AsyncInterfaces
{
    using System;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that has methods for deleting records
    /// </summary>
    /// <typeparam name="TResult">The result entity type</typeparam>
    public interface IDeleteAllItemsAsync<TResult>
    {
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <returns>The awaitable task</returns>
        Task<TResult> DeleteAllAsync();
    }

    /// <summary>
    /// Interface that has methods for deleting records
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface IDeleteItemAsync<TEntity> where TEntity : class
    {
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="predicate">The filter criteria</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>The awaitable task</returns>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken token = default(CancellationToken));
    }

    /// <summary>
    /// Interface that has methods for deleting records
    /// </summary>
    /// <typeparam name="TIdentity">The identity type</typeparam>
    public interface IDeleteItemByIdAsync<in TIdentity>
    {
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="id">Identity key</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>The awaitable task</returns>
        Task DeleteAsync(TIdentity id, CancellationToken token = default(CancellationToken));
    }

    /// <summary>
    /// Interface that has methods for deleting records
    /// </summary>
    /// <typeparam name="TIdentity">The identity type</typeparam>
    /// /// <typeparam name="TResult">The result entity type</typeparam>
    public interface IDeleteItemByIdAsync<in TIdentity, TResult>
    {
        /// <summary>
        /// Delete an item
        /// </summary>
        /// <param name="id">Identity key</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>The awaitable task</returns>
        Task<TResult> DeleteAsync(TIdentity id, CancellationToken token = default(CancellationToken));
    }
}
