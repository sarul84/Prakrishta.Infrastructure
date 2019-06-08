//----------------------------------------------------------------------------------
// <copyright file="AsyncHelper.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/8/2019</date>
// <summary>Helper class that defines method to run asynchronous methods</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Helper
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Asynchronous method execution helper class
    /// Thanks to Chris Pratt: https://cpratt.co/async-tips-tricks/
    /// </summary>
    public sealed class AsyncHelper
    {
        #region |Private Fields|

        /// <summary>
        /// Holds task factory object
        /// </summary>
        private static readonly TaskFactory taskFactory = new TaskFactory(CancellationToken.None,
                    TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        #endregion

        #region |Methods|

        /// <summary>
        /// Execute's an async Task<TResult> method which has a TResult return type synchronously
        /// </summary>
        /// <typeparam name="TResult">Return Type</typeparam>
        /// <param name="func">Task<TResult> method to execute</param>
        /// <returns></returns>
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
            => taskFactory.StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();

        /// <summary>
        /// Execute's an async Task<TResult> method which has a void return value synchronously
        /// </summary>
        /// <param name="func">Task method to execute</param>
        public static void RunSync(Func<Task> func)
            => taskFactory.StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();

        #endregion
    }
}
