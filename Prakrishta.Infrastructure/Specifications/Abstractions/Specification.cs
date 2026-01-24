//----------------------------------------------------------------------------------
// <copyright file="Specification.cs" company="Prakrishta Technologies">
//     Copyright (c) 2026 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>01/24/2026</date>
// <summary>The specification contract that defines pagination logics</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Specifications.Abstractions
{
    using Prakrishta.Infrastructure.Specifications.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Defines the <see cref="Specification" /> class
    /// </summary>
    public abstract class Specification<T> : ISpecification<T> where T : class
    {
        protected List<Expression<Func<T, object>>> Includes { get; } = new();
        protected Expression<Func<T, bool>>? Criteria { get; set; }
        protected List<(Expression<Func<T, object>> KeySelector, bool IsAscending)> OrderBys { get; } = new();

        protected int? PageNumber { get; private set; }
        protected int? PageSize { get; private set; }
        protected bool IsPagingEnabled => PageNumber.HasValue && PageSize.HasValue;

        protected void ApplyPaging(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        protected virtual void ApplyCriteria() { }
        protected virtual void ApplyIncludes() { }
        protected virtual void ApplyOrderBy() { }


        public virtual IQueryable<T> ApplySpecification(IQueryable<T> query)
        {
            ApplyCriteria();
            ApplyIncludes();
            ApplyOrderBy();

            if (Criteria != null)
                query = query.Where(Criteria);

            foreach (var (keySelector, isAscending) in OrderBys)
                query = isAscending ? query.OrderBy(keySelector) : query.OrderByDescending(keySelector);

            if (IsPagingEnabled)
                query = query.Skip((PageNumber!.Value - 1) * PageSize!.Value)
                             .Take(PageSize!.Value);

            return query;
        }
    }
}
