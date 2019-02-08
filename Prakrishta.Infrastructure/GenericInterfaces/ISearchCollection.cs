//----------------------------------------------------------------------------------
// <copyright file="ISearchCollection.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/8/2019</date>
// <summary>Contract that defines method for searching and returning collections</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.GenericInterfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface that has definitions to get collection of entities 
    /// based on filter conditions
    /// </summary>
    /// <typeparam name="TEntity">Entity type that is going to be returned</typeparam>
    public interface ISearchCollection<TEntity>
    {
        /// <summary>
        /// Get all entities for the given filter criteria
        /// </summary>
        /// <param name="entity">Filter criteria</param>
        /// <returns>Collection of entities</returns>
        ICollection<TEntity> GetAll(TEntity entity);
    }

    /// <summary>
    /// Interface that has definitions to get collection of entities 
    /// based on filter conditions
    /// </summary>
    /// <typeparam name="TIdentity">Filter type</typeparam>
    /// <typeparam name="TEntity">Entity type that is going to be returned</typeparam>
    public interface ISearchCollection<in TIdentity, TEntity>
    {
        /// <summary>
        /// Get all entities for the given filter criteria
        /// </summary>
        /// <param name="entity">Filter criteria</param>
        /// <returns>Collection of entities</returns>
        ICollection<TEntity> GetAll(TIdentity id);
    }

    /// <summary>
    /// Interface that has definitions to get collection of entities 
    /// based on filter conditions
    /// </summary>
    /// <typeparam name="TIdentity1">Filter type</typeparam>
    /// <typeparam name="TIdentity2">Filter type</typeparam>
    /// <typeparam name="TEntity">Entity type that is going to be returned</typeparam>
    public interface ISearchCollection<in TIdentity1, in TIdentity2, TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Get all entities for the given filter criteria
        /// </summary>
        /// <param name="id1">Filter criteria1</param>
        /// <param name="id2">Filter criteria2</param>
        /// <returns>Collection of entities</returns>
        ICollection<TEntity> GetAll(TIdentity1 id1, TIdentity2 id2);
    }

    /// <summary>
    /// Interface that has definitions to get collection of entities 
    /// based on filter conditions
    /// </summary>
    /// <typeparam name="TIdentity1">Filter type</typeparam>
    /// <typeparam name="TIdentity2">Filter type</typeparam>
    /// <typeparam name="TIdentity3">Filter type</typeparam>
    /// <typeparam name="TEntity">Entity type that is going to be returned</typeparam>
    public interface ISearchCollection<in TIdentity1, in TIdentity2, in TIdentity3, TEntity>
    {
        /// <summary>
        /// Get all entities for the given filter criteria
        /// </summary>
        /// <param name="id1">Filter criteria1</param>
        /// <param name="id2">Filter criteria2</param>
        /// <param name="id3">Filter criteria3</param>
        /// <returns>Collection of entities</returns>
        ICollection<TEntity> GetAll(TIdentity1 id1, TIdentity2 id2, TIdentity3 id3);
    }
}
