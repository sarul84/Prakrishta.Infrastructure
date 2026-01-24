//----------------------------------------------------------------------------------
// <copyright file="IMaxAsync.cs" company="Prakrishta Technologies">
//     Copyright (c) 2026 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>24/01/2026</date>
// <summary>Contract that defines Max operations</summary>
//-----------------------------------------------------------------------------------
namespace Prakrishta.Infrastructure.AsyncInterfaces
{
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface that contains definitions for Max operations
    /// </summary>
    public interface IMaxAsync
    {
        /// <summary>
        /// Gets the maximum value of a field.
        /// </summary>
        /// <typeparam name="TResult">Type of the field</typeparam>
        /// <param name="selector">Field selector</param>
        Task<TResult> GetMaxAsync<TResult>(Func<TResult> selector);
    }

    /// <summary>
    /// Interface that contains definitions for Max operations
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IMaxAsync<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets the maximum value of a selected field for entities matching the predicate.
        /// </summary>
        /// <typeparam name="TResult">Type of the field</typeparam>
        /// <param name="predicate">Filter condition</param>
        /// <param name="selector">Field selector</param>
        Task<TResult> GetMaxAsync<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);
    }

    /// <summary>
    /// Interface that contains definitions for Max operations
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TIdentity">Identity key type</typeparam>
    public interface IMaxByKeyAsync<in TIdentity, TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets the maximum value of a selected field for the entity with the given key.
        /// </summary>
        /// <typeparam name="TResult">Type of the field</typeparam>
        /// <param name="id">Identity key</param>
        /// <param name="selector">Field selector</param>
        Task<TResult> GetMaxAsync<TResult>(TIdentity id, Expression<Func<TEntity, TResult>> selector);
    }
}
