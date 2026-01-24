//----------------------------------------------------------------------------------
// <copyright file="ISelect.cs" company="Prakrishta Technologies">
//     Copyright (c) 2026 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>24/01/2026</date>
// <summary>Contract that defines select operations</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.GenericInterfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Interface that contains definitions for select operations
    /// </summary>
    public interface ISelect<TEntity> where TEntity : class
    {
        /// <summary>
        /// Projects entities matching the predicate into a different shape.
        /// </summary>
        /// <typeparam name="TResult">Projection result type</typeparam>
        /// <param name="predicate">Filter condition</param>
        /// <param name="selector">Projection selector</param>
        IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TResult>> selector);
    }

    /// <summary>
    /// Interface that contains definitions for select operations by key
    /// </summary>
    public interface ISelectByKey<in TIdentity, TEntity> where TEntity : class
    {
        /// <summary>
        /// Projects entities matching the predicate into a different shape.
        /// </summary>
        /// <typeparam name="TResult">Projection result type</typeparam>
        /// <param name="id">Identity key</param>
        /// <param name="selector">Projection selector</param>
        IEnumerable<TResult> Select<TResult>(TIdentity id, Expression<Func<TEntity, TResult>> selector);
    }
}
