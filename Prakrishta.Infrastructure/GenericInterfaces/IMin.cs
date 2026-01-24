//----------------------------------------------------------------------------------
// <copyright file="IMin.cs" company="Prakrishta Technologies">
//     Copyright (c) 2026 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>24/01/2026</date>
// <summary>Contract that defines Max operations</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.GenericInterfaces
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Interface that contains definitions for Min operations
    /// </summary>
    public interface IMin<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets the minimum value of a field.
        /// </summary>
        /// <typeparam name="TResult">Type of the field</typeparam>
        /// <param name="selector">Field selector</param>
        /// <param name="predicate">Filter condition</param>
        TResult GetMin<TResult>(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector);
    }

    /// <summary>
    /// Interface that contains definitions for Min operations
    /// </summary>
    /// <typeparam name="TIdentity">Identity key type</typeparam>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IMinByKey<in TIdentity, TEntity>
    where TEntity : class
    {
        /// <summary>
        /// Gets the minimum value of a field.
        /// </summary>
        /// <typeparam name="TResult">Type of the field</typeparam>
        /// <param name="id">Identity key</param>
        /// <param name="selector">Field selector</param>
        TResult GetMin<TResult>(TIdentity id, Expression<Func<TEntity, TResult>> selector);
    }
}
