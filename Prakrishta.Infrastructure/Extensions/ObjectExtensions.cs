//----------------------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/9/2019</date>
// <summary>Extension clss that has methods for extending Object behavior</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using System;

    /// <summary>
    /// Object extension class
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Check if object is numeric type
        /// </summary>
        /// <param name="value">Object value</param>
        /// <returns>Indicates if the object is numeric type</returns>
        public static bool IsNumber(this object value)
        {
            return value is sbyte
                    || value is byte
                    || value is short
                    || value is ushort
                    || value is int
                    || value is uint
                    || value is long
                    || value is ulong
                    || value is float
                    || value is double
                    || value is decimal;
        }

        /// <summary>
        /// if the object this method is called on is not null, runs the given function and returns the value.
        /// if the object is null, returns default(TResult)
        /// </summary>
        /// <param name="target">Source object</param>
        /// <param name="getValue">Delegate method to be executed if not null</param>
        public static TResult IfNotNull<TSource, TResult>(this TSource target, Func<TSource, TResult> getValue)
        {
            if (target != null)
                return getValue(target);
            else
                return default(TResult);
        }

        /// <summary>
        /// when dealing with databases, a new null type shows up, the DBNull. 
        /// This extention method detects it along with the null
        /// </summary>
        /// <param name="value">Object value</param>
        /// <returns>Indicates if object is null or DBNull</returns>
        public static bool IsNullOrDBNull(this object value)
        {
            if (value == null || value.GetType() == typeof(DBNull))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
