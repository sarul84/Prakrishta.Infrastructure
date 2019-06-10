//----------------------------------------------------------------------------------
// <copyright file="PropertyComparer.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>3/3/2019</date>
// <summary>Generic comparer for comparing properties in class using reflection</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Generic comparer for comparing properties in class using reflection
    /// Thanks to Seth Dingwell 
    /// https://www.codeproject.com/Articles/94272/A-Generic-IEqualityComparer-for-Linq-Distinct
    /// </summary>
    /// <typeparam name="T">The generic type param</typeparam>
    public sealed class PropertyComparer<T> : IEqualityComparer<T>
    {
        #region |Private fields|
        /// <summary>
        /// Holds getter method delegate object
        /// </summary>
        private readonly Func<T, object> getter;
        #endregion

        #region |Constructor|
        /// <summary>
        /// Initializes a new instance of <see cref="PropertyComparer<T>"/> class
        /// </summary>
        /// <param name="propertyName">The name of the property on type T 
        /// to perform the comparison on.</param>
        public PropertyComparer(string propertyName)
        {            
            var propertyInfo = typeof(T).GetProperty(propertyName,
                                    BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);            

            if (propertyInfo == null)
            {
                throw new ArgumentException($"{propertyName} is not a property of type {typeof(T)}.");
            }

           getter = (Func<T, object>)Delegate.CreateDelegate(
               typeof(Func<T, object>), null, propertyInfo.GetGetMethod());
        }
        #endregion

        #region |Interface implementation|
        /// <inheritdoc />
        public bool Equals(T x, T y)
        {
            object xValue = this.getter(x);
            object yValue = this.getter(y);

            if (xValue == null)
            {
                return yValue == null;
            }
            
            return xValue.Equals(yValue);
        }

        /// <inheritdoc />
        public int GetHashCode(T obj)
        {
            object propertyValue = this.getter(obj);

            if (propertyValue == null)
            {
                return 0;
            }
            else
            {
                return propertyValue.GetHashCode();
            }
        }
        #endregion
    }
}
