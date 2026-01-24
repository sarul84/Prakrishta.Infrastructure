//----------------------------------------------------------------------------------
// <copyright file="ISpecification.cs" company="Prakrishta Technologies">
//     Copyright (c) 2026 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>01/24/2026</date>
// <summary>The specification contract</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Specifications.Interfaces
{
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="ISpecification" /> class
    /// </summary>
    public interface ISpecification<T> where T : class
    {
        /// <summary>
        /// Defines the apply specification method
        /// </summary>
        /// <param name="query">The queryable object<see cref="IQueryable{T}"/></param>
        IQueryable<T> ApplySpecification(IQueryable<T> query);
    }
}
