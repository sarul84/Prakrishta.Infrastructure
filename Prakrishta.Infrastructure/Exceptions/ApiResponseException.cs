//----------------------------------------------------------------------------------
// <copyright file="ApiResponseException.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>7/3/2019</date>
// <summary>The custome exception class to catch Api Response Exceptions</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Exceptions
{
    using Prakrishta.Infrastructure.Models;
    using System;

    /// <summary>
    /// Defines the <see cref="ApiResponseException" /> class
    /// </summary>
    public sealed class ApiResponseException : Exception
    {
        #region |Constructors|

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponseException"/> class.
        /// </summary>
        /// <param name="error">The error<see cref="ErrorDetail"/> object</param>
        public ApiResponseException(ErrorDetail error) : base(error.Message)
        {
            this.Error = error;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponseException"/> class.
        /// </summary>
        /// <param name="error">The error<see cref="ErrorDetail"/> object</param>
        /// <param name="innerException">The innerException<see cref="Exception"/></param>
        public ApiResponseException(ErrorDetail error, Exception innerException)
            : base(error.Message, innerException)
        {
            this.Error = error;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponseException"/> class.
        /// </summary>
        /// <param name="statusCode">The http status code<see cref="int"/></param>
        /// <param name="innerException">The inner exception<see cref="Exception"/> object</param>
        public ApiResponseException(int statusCode, Exception innerException)
            : base(statusCode.ToString(), innerException)
        {
            this.Error = new ErrorDetail { StatusCode = statusCode };
        }

        #endregion

        #region |Properties|

        /// <summary>
        /// Gets the ErrorDetail
        /// </summary>
        public ErrorDetail Error { get; }

        #endregion
    }
}
