//----------------------------------------------------------------------------------
// <copyright file="ICrudRepository.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/8/2019</date>
// <summary>Contract that defines CRUD operations</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.GenericInterfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface that contains definitions for CRUD operations
    /// </summary>
    /// <typeparam name="TIdentity">Identity filter condition</typeparam>
    /// <typeparam name="TFilter">Filter criteria</typeparam>
    /// <typeparam name="TReturn">Return type</typeparam>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface ICrudRepository<in TIdentity, in TFilter, out TReturn, TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Method that defines add operation
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        /// <returns>New entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Get record for the given id
        /// </summary>
        /// <param name="id">Identity field</param>
        /// <returns>Entity that matches filter condition</returns>
        TEntity Get(TIdentity id);

        /// <summary>
        /// Get single record for the given filter criteria
        /// </summary>
        /// <param name="filter">Filter criteria</param>
        /// <returns>Entity that matches filter condition</returns>
        TEntity GetSingle(TFilter filter);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Collection of entities</returns>
        ICollection<TEntity> GetAll();

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity">Entity that holds modified or updated value</param>
        /// <returns>Returns value type specified to indicate operation status</returns>
        TReturn Update(TEntity entity);

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity">Entity that needs to be removed or deleted</param>
        /// <returns>Returns value type specified to indicate operation status</returns>
        TReturn Delete(TEntity entity);
    }

    /// <summary>
    /// Interface that contains definitions for CRUD operations
    /// </summary>
    /// <typeparam name="TIdentity">Identity filter condition</typeparam>
    /// <typeparam name="TReturn">Return type</typeparam>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface ICrudRepository<in TIdentity, out TReturn, TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Method that defines add operation
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        /// <returns>New entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Get record for the given id
        /// </summary>
        /// <param name="id">Identity field</param>
        /// <returns>Entity that matches filter condition</returns>
        TEntity Get(TIdentity id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Collection of entities</returns>
        ICollection<TEntity> GetAll();

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity">Entity that holds modified or updated value</param>
        /// <returns>Returns value type specified to indicate operation status</returns>
        TReturn Update(TEntity entity);

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity">Entity that needs to be removed or deleted</param>
        /// <returns>Returns value type specified to indicate operation status</returns>
        TReturn Delete(TEntity entity);
    }

    /// <summary>
    /// Interface that contains definitions for CRUD operations
    /// </summary>
    /// <typeparam name="TIdentity">Identity filter condition</typeparam>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface ICrudRepository<in TIdentity, TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Method that defines add operation
        /// </summary>
        /// <param name="entity">Entity to be added</param>
        /// <returns>New entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Get record for the given id
        /// </summary>
        /// <param name="id">Identity field</param>
        /// <returns>Entity that matches filter condition</returns>
        TEntity Get(TIdentity id);

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <returns>Collection of entities</returns>
        ICollection<TEntity> GetAll();

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entity">Entity that holds modified or updated value</param>
        void Update(TEntity entity);

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity">Entity that needs to be removed or deleted</param>
        void Delete(TEntity entity);
    }
}
