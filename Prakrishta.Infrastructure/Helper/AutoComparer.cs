//----------------------------------------------------------------------------------
// <copyright file="AutoComparer.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/10/2019</date>
// <summary>The auto comparer class</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Helper
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the auto comparer class
    /// </summary>
    /// <typeparam name="T">The generic type parameter</typeparam>
    /// <typeparam name="K">The generic type parameter</typeparam>
    public sealed class AutoComparer<T, K> : IEqualityComparer<T>
    {
        #region |Private Fields|

        /// <summary>
        /// Defines the projection
        /// </summary>
        private readonly Func<T, K> projection;

        #endregion

        #region |Constructors|

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoComparer{T, K}"/> class.
        /// </summary>
        /// <param name="projection">The projection func</param>
        public AutoComparer(Func<T, K> projection)
        {
            this.projection = projection ?? throw new ArgumentNullException(nameof(projection));
        }

        #endregion

        #region |Methods|

        /// <inheritdoc />
        public bool Equals(T x, T y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null)
            {
                return false;
            }
            if (y == null)
            {
                return false;
            }

            var xData = this.projection(x);
            var yData = this.projection(y);

            return EqualityComparer<K>.Default.Equals(xData, yData);
        }

        /// <inheritdoc />
        public int GetHashCode(T obj)
        {
            if (obj == null)
            {
                return 0;
            }

            var objData = this.projection(obj);

            return EqualityComparer<K>.Default.GetHashCode(objData);
        }

        #endregion
    }
}
