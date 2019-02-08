//----------------------------------------------------------------------------------
// <copyright file="ISearchSingle.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/8/2019</date>
// <summary>Contract that defines method for searching and returning single record</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.GenericInterfaces
{
    /// <summary>
    ///  Generic interface that has method to get single record
    /// </summary>
    /// <typeparam name="TEntity">Entity type that is going to be return</typeparam>
    public interface ISearchSingle<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Get record for the given filter criteria
        /// </summary>
        /// <param name="entity">Filter criteria</param>
        /// <returns>Returns single record of the expected entity type</returns>
        TEntity Get(TEntity entity);
    }

    /// <summary>
    ///  Generic interface that has method to get single record
    /// </summary>
    /// <typeparam name="TIdentity">Identity type that is going to be used as a filter</typeparam>
    /// <typeparam name="TEntity">Entity type that is going to be return</typeparam>
    public interface ISearchSingle<in TIdentity, out TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Get record for the given id
        /// </summary>
        /// <param name="id">Filter condition</param>
        /// <returns>Returns single record of the expected entity type</returns>
        TEntity Get(TIdentity id);
    }

    /// <summary>
    /// Generic interface that has method to get single record
    /// </summary>
    /// <typeparam name="TIdentity1">Identity type that is going to be used as a filter</typeparam>
    /// <typeparam name="TIdentity2">Identity type that is going to be used as a filter</typeparam>
    /// <typeparam name="TEntity">Entity type that is going to be return</typeparam>
    public interface ISearchSingle<in TIdentity1, in TIdentity2, out TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Get record for the given input parameters
        /// </summary>
        /// <param name="id1">Filter condition1</param>
        /// <param name="id2">Filter condition2</param>
        /// <returns>Returns single record of the expected entity type</returns>
        TEntity Get(TIdentity1 id1, TIdentity2 id2);
    }

    /// <summary>
    /// Generic interface that has method to get single record
    /// </summary>
    /// <typeparam name="TIdentity1">Identity type that is going to be used as a filter</typeparam>
    /// <typeparam name="TIdentity2">Identity type that is going to be used as a filter</typeparam>
    /// <typeparam name="TIdentity3">Identity type that is going to be used as a filter<</typeparam>
    /// <typeparam name="TEntity">Entity type that is going to be return</typeparam>
    public interface ISearchSingle<in TIdentity1, in TIdentity2, in TIdentity3, out TEntity>
        where TEntity: class
    {
        /// <summary>
        /// Get record for the given input parameters
        /// </summary>
        /// <param name="id1">Filter condition1</param>
        /// <param name="id2">Filter condition2</param>
        /// <param name="id3">Filter condition2</param>
        /// <returns>Returns single record of the expected entity type</returns>
        TEntity Get(TIdentity1 id1, TIdentity2 id2, TIdentity3 id3);
    }
}
