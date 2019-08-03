//----------------------------------------------------------------------------------
// <copyright file="RetryHelper.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>7/7/2019</date>
// <summary>Retry Helper class</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="RetryHelper" /> class
    /// </summary>
    public sealed class RetryHelper
    {
        #region |Methods|

        /// <summary>
        /// The Do
        /// </summary>
        /// <param name="action">The action<see cref="Action"/></param>
        /// <param name="interval">The interval<see cref="TimeSpan"/></param>
        /// <param name="retries">The retries<see cref="int"/></param>
        public void Do(Action action, TimeSpan interval, int retries = 3)
        {
            Try<object, Exception>(() =>
            {
                action();
                return null;
            }, interval, retries);
        }

        /// <summary>
        /// The Do
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="action">The action<see cref="Action"/></param>
        /// <param name="interval">The interval<see cref="TimeSpan"/></param>
        /// <param name="retries">The retries<see cref="int"/></param>
        public void Do<E>(Action action, TimeSpan interval, int retries = 3) where E : Exception
        {
            Try<object, E>(() =>
            {
                action();
                return null;
            }, interval, retries);
        }

        /// <summary>
        /// The Do
        /// </summary>
        /// <typeparam name="T">The generic return type</typeparam>
        /// <param name="action">The action<see cref="Func{T}"/></param>
        /// <param name="interval">The interval<see cref="TimeSpan"/></param>
        /// <param name="retries">The retries<see cref="int"/></param>
        /// <returns>The <see cref="T"/></returns>
        public T Do<T>(Func<T> action, TimeSpan interval, int retries = 3)
        {
            return Try<T, Exception>(
                  action
                , interval
                , retries);
        }

        /// <summary>
        /// The Do
        /// </summary>
        /// <typeparam name="E">The generic exception type</typeparam>
        /// <typeparam name="T">The generic return type</typeparam>
        /// <param name="action">The action<see cref="Func{T}"/></param>
        /// <param name="interval">The interval<see cref="TimeSpan"/></param>
        /// <param name="retries">The retries<see cref="int"/></param>
        /// <returns>The <see cref="T"/></returns>
        public T Do<E, T>(Func<T> action, TimeSpan interval, int retries = 3) where E : Exception
        {
            return Try<T, E>(
                  action
                , interval
                , retries);
        }

        /// <summary>
        /// The Try
        /// </summary>
        /// <typeparam name="T">The generic return type</typeparam>
        /// <typeparam name="E">The generic exception type</typeparam>
        /// <param name="action">The action<see cref="Func{T}"/></param>
        /// <param name="interval">The interval<see cref="TimeSpan"/></param>
        /// <param name="retries">The retries<see cref="int"/></param>
        /// <returns>The <see cref="T"/></returns>
        private T Try<T, E>(Func<T> action, TimeSpan interval, int retries = 3) where E : Exception
        {
            var exceptions = new List<E>();

            for (int retry = 0; retry < retries; retry++)
            {
                try
                {
                    if (retry > 0)
                    {
                        Task.Delay(interval).GetAwaiter();
                    }
                    return action();
                }
                catch (E ex)
                {
                    exceptions.Add(ex);
                }
            }

            throw new AggregateException(exceptions);
        }

        #endregion
    }
}
