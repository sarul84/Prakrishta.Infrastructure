//----------------------------------------------------------------------------------
// <copyright file="IUpdateItem.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/8/2019</date>
// <summary>Contract that defines methods to update an item</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.GenericInterfaces
{
    /// <summary>
    /// Interface that has definitions for updating an entity
    /// </summary>
    /// <typeparam name="TIdentity">Identity type</typeparam>
    /// <typeparam name="TReturn">Return type</typeparam>
    /// <typeparam name="TEntity">Entity type that needs to be updated</typeparam>
    public interface IUpdateItem<in TIdentity, out TReturn, TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Update an item or entity based on the filter criteria
        /// </summary>
        /// <param name="id">Filter criteria</param>
        /// <param name="entity">Modified entity</param>
        /// <returns>Returns type that indicates the operation outcome</returns>
        TReturn Update(TIdentity id, TEntity entity);
    }

    /// <summary>
    /// Interface that has definitions for updating an entity
    /// </summary>
    /// <typeparam name="TIdentity">Identity type</typeparam>
    /// <typeparam name="TEntity">Entity type that needs to be updated</typeparam>
    public interface IUpdateItem<in TIdentity, TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Update an item or entity based on the filter criteria
        /// </summary>
        /// <param name="id">Filter criteria</param>
        /// <param name="entity">Modified entity</param>
        void Update(TIdentity id, TEntity entity);
    }

    /// <summary>
    /// Interface that has definitions for updating an entity
    /// </summary>
    /// <typeparam name="TEntity">Entity type that needs to be updated</typeparam>
    public interface IUpdateItem<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Update an item or entity
        /// </summary>
        /// <param name="entity">Modified entity</param>
        void Update(TEntity entity);
    }
}
