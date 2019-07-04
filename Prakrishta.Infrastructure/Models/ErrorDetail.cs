//----------------------------------------------------------------------------------
// <copyright file="ErrorDetail.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>7/3/2019</date>
// <summary>Error Detail model class</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the <see cref="ErrorDetail" /> class
    /// </summary>
    public sealed class ErrorDetail
    {
        #region |Properties|

        /// <summary>
        /// Gets or sets the Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the StatusCode
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the EventId
        /// </summary>
        public string EventId { get; set; }

        #endregion

        #region |Methods|

        /// <summary>
        /// Formats the object using Json serializer
        /// </summary>
        /// <returns>The serialized <see cref="string"/></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        #endregion
    }
}
