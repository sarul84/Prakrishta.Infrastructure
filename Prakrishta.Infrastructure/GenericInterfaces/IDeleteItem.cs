//----------------------------------------------------------------------------------
// <copyright file="IDeleteItem.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/8/2019</date>
// <summary>Contract that defines methods to delete an item</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.GenericInterfaces
{
    /// <summary>
    /// Interface that has definitions to delete an item or entity
    /// </summary>
    /// <typeparam name="TIdentity">Identity type</typeparam>
    /// <typeparam name="TReturn">Return type</typeparam>
    /// <typeparam name="TEntity">Entity type that needs to be removed</typeparam>
    public interface IDeleteItem<in TIdentity, out TReturn, TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Delete an entity or item for the given filter criteria
        /// </summary>
        /// <param name="id">Filter criteria</param>
        /// <param name="entity">Entity to be removed or deleted</param>
        /// <returns>Returns a type that indicates the operation outcome</returns>
        TReturn Delete(TIdentity id, TEntity entity);
    }

    /// <summary>
    /// Interface that has definitions to delete an item or entity
    /// </summary>
    /// <typeparam name="TIdentity">Identity type</typeparam>
    /// <typeparam name="TEntity">Entity type that needs to be removed</typeparam>
    public interface IDeleteItem<in TIdentity, TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Delete an entity or item for the given filter criteria
        /// </summary>
        /// <param name="id">Filter criteria</param>
        /// <param name="entity">Entity to be removed or deleted</param>
        /// <returns>Returns a type that indicates the operation outcome</returns>
        void Delete(TIdentity id, TEntity entity);
    }

    /// <summary>
    /// Interface that has definitions to delete an item or entity
    /// </summary>
    /// <typeparam name="TEntity">Entity type that needs to be removed</typeparam>
    public interface IDeleteItem<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Delete an entity or item for the given filter criteria
        /// </summary>
        /// <param name="entity">Entity to be removed or deleted</param>
        /// <returns>Returns a type that indicates the operation outcome</returns>
        void Delete(TEntity entity);
    }
}
