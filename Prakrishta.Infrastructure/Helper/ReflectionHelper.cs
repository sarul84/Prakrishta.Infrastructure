//----------------------------------------------------------------------------------
// <copyright file="ReflectionHelper.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>6/26/2019</date>
// <summary>The Reflection Helper class</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Helper
{
    using Prakrishta.Infrastructure.Extensions;
    using System.Diagnostics;
    using System.Reflection;

    /// <summary>
    /// Defines the helper methods for reflection
    /// </summary>
    public class ReflectionHelper
    {
        #region |Methods|

        /// <summary>
        /// The method to get Calling Method Info
        /// </summary>
        /// <returns>The calling method information</returns>
        public static string GetCallingMethodInfo()
        {
            StackFrame? stackFrame = new StackTrace().GetFrame(1);
            string? callingType = stackFrame?.GetMethod()?.ReflectedType?.FullName;
            string? callingMethod = stackFrame?.GetMethod()?.Name;
            return $"{callingType}.{callingMethod}";
        }

        /// <summary>
        /// The helper method to invoke private methods of object and get value
        /// </summary>
        /// <param name="reflectedSource">The object from which private method going to be invoked</param>
        /// <param name="methodName">The method name</param>
        /// <param name="methodParameters">The method parameters</param>
        /// <returns>The response <see cref="object"/></returns>
        public static object? InvokePrivateMethod(object reflectedSource, string methodName, params object[] methodParameters)
        {
            object? result = null;
            MethodInfo? methodInfo = reflectedSource.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (methodInfo != null)
            {
                result = methodInfo.Invoke(reflectedSource, methodParameters);
            }

            return result;
        }

        /// <summary>
        /// The helper method to invoke private methods of object and get value
        /// </summary>
        /// <typeparam name="T">The generic result type</typeparam>
        /// <param name="reflectedSource">The object from which private method going to be invoked</param>
        /// <param name="methodName">The method name</param>
        /// <param name="methodParameters">The method parameters</param>
        /// <returns>The response <see cref="T"/></returns>
        public static T? InvokePrivateMethod<T>(object reflectedSource, string methodName, params object[] methodParameters)
        {
            object? result = null;
            MethodInfo? methodInfo = reflectedSource.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (methodInfo != null)
            {
                result = methodInfo?.Invoke(reflectedSource, methodParameters);
            }

            return result.GetValue<T>();
        }

        /// <summary>
        /// The method to get value from Private Property of the object
        /// </summary>
        /// <param name="reflectedSource">The object from which the private field is going to be accessed</param>
        /// <param name="propertyName">The property name</param>
        /// <returns>The response <see cref="object"/></returns>
        public static object InvokePrivateProperty(object reflectedSource, string propertyName)
        {
            object result = null;
            PropertyInfo propertyInfo = reflectedSource.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (propertyInfo != null)
            {
                result = propertyInfo.GetValue(reflectedSource, null);
            }

            return result;
        }

        /// <summary>
        /// The method to get value from Private Property of the object
        /// </summary>
        /// <typeparam name="T">The generic result type</typeparam>
        /// <param name="reflectedSource">The object from which the private field is going to be accessed</param>
        /// <param name="propertyName">The property name</param>
        /// <returns>The <see cref="T"/></returns>
        public static T InvokePrivateProperty<T>(object reflectedSource, string propertyName)
        {
            object result = null;
            PropertyInfo propertyInfo = reflectedSource.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (propertyInfo != null)
            {
                result = propertyInfo.GetValue(reflectedSource, null);
            }

            return result.GetValue<T>();
        }

        #endregion
    }
}
