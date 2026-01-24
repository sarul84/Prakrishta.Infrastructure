//----------------------------------------------------------------------------------
// <copyright file="ISpecificationWithPaging.cs" company="Prakrishta Technologies">
//     Copyright (c) 2026 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>01/24/2026</date>
// <summary>The specification contract that defines pagination logics</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Specifications.Interfaces
{
    /// <summary>
    /// Defines the <see cref="ISpecificationWithPaging" /> class
    /// </summary>
    public interface ISpecificationWithPaging<T> : ISpecification<T> where T : class
    {
        /// <summary>
        /// Get the page number
        /// </summary>
        int PageNumber { get; }

        /// <summary>
        /// Get the page size
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// Gets a value indicating whether paging is enabled
        /// </summary>
        bool IsPagingEnabled { get; }
    }
}
