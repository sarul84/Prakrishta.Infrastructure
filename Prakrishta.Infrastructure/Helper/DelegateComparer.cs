//----------------------------------------------------------------------------------
// <copyright file="DelegateComparer.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>3/3/2019</date>
// <summary>The delegate comparer class</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Helper
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The delegate comparer class
    /// </summary>
    /// <typeparam name="T">The generic type parameter</typeparam>
    public sealed class DelegateComparer<T> : IEqualityComparer<T>
    {
        #region |Private fields|
        /// <summary>
        /// Holds equal func
        /// </summary>
        private readonly Func<T, T, bool> equals;

        /// <summary>
        /// Holds get hash code func
        /// </summary>
        private readonly Func<T, int> getHashCode;
        #endregion

        #region |Constructor|
        /// <summary>
        /// Initializes a new instance of <see cref="DelegateComparer<T>"/> class
        /// </summary>
        /// <param name="equals">The equals filter func</param>
        /// <param name="getHashCode">The gethascode func</param>
        public DelegateComparer(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            this.equals = equals ?? throw new ArgumentNullException(nameof(equals));
            this.getHashCode = getHashCode ?? throw new ArgumentNullException(nameof(getHashCode));
        }
        #endregion

        #region |Interface Implementation|
        /// <inheritdoc />
        public bool Equals(T x, T y)
        {
            return this.equals(x, y);
        }

        /// <inheritdoc />
        public int GetHashCode(T obj)
        {
            if (this.getHashCode != null)
            {
                return this.getHashCode(obj);
            }
            else
            {
                return obj.GetHashCode();
            }
        }
        #endregion
    }
}
