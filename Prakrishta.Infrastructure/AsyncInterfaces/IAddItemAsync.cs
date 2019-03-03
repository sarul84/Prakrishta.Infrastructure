//----------------------------------------------------------------------------------
// <copyright file="IAddItemAsync.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/8/2019</date>
// <summary>Contract that defines methods to create an item</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.AsyncInterfaces
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that has definitions to add or create an item / entity async
    /// </summary>
    /// <typeparam name="TInput">Input entity type</typeparam>
    /// <typeparam name="TEntity">Entity type that has to be created</typeparam>
    public interface IAddItemAsync<in TInput, TEntity>
        where TEntity : class
        where TInput : class
    {
        /// <summary>
        /// Adds a new item or entity
        /// </summary>
        /// <param name="entity">Input entity object</param>
        /// <param name="token">The cancellation token</param>
        /// <returns>Returns newly created entity or item</returns>
        Task<TEntity> AddAsync(TInput entity, CancellationToken token = default(CancellationToken));
    }

    /// <summary>
    /// Interface that has definitions to add or create an item / entity
    /// </summary>
    /// <typeparam name="TEntity">Entity type that has to be created</typeparam>
    public interface IAddItemAsync<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Adds a new item or entity
        /// </summary>
        /// <param name="token">The cancellation token</param>
        /// <returns>Returns newly created entity or item</returns>
        Task<TEntity> AddAsync(CancellationToken token = default(CancellationToken));
    }

    /// <summary>
    /// Interface that has definitions to add collection of items
    /// </summary>
    /// <typeparam name="TEntity">Entity type that needs to be added</typeparam>
    public interface IAddCollectionAsync<TEntity> where TEntity : class
    {
        /// <summary>
        ///  Add collection of items
        /// </summary>
        /// <param name="entities">Entity collection to be added</param>
        Task AddRangeAsync(IEnumerable<TEntity> entities);
    }
}
