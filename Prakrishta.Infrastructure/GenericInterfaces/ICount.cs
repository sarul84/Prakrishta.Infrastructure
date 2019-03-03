//----------------------------------------------------------------------------------
// <copyright file="ICount.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/15/2019</date>
// <summary>Contract that defines methods for getting record count</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.GenericInterfaces
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Interface that has count methods
    /// </summary>
    public interface ICount
    {
        /// <summary>
        /// Gets record count
        /// </summary>
        /// <returns>Number of records</returns>
        int GetCount();
    }

    /// <summary>
    /// Interface that has count methods
    /// </summary>
    /// <typeparam name="TEntity">The type of entity</typeparam>
    public interface ICount<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets record count
        /// </summary>
        /// <param name="predicate">The filter condition</param>
        /// <returns>Number of records</returns>
        int GetCount(Expression<Func<TEntity, bool>> predicate);
    }

    /// <summary>
    /// Interface that has count methods
    /// </summary>
    /// <typeparam name="TIdentity">The identity key type</typeparam>
    public interface ICountByKey<in TIdentity>
    {
        /// <summary>
        /// Gets record count by Id
        /// </summary>
        /// <param name="id">The identity key</param>
        /// <returns>Number of records</returns>
        int GetCount(TIdentity id);
    }
}
