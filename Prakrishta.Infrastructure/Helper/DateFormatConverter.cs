//----------------------------------------------------------------------------------
// <copyright file="DateFormatConverter.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/9/2019</date>
// <summary>Json converter annotation class that defines method to convert date to specific 
// format during serialization date to specific format during serialization</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Helper
{
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Date Format converter class to be used with Json Converter annotation
    /// </summary>
    public class DateFormatConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Initializes new instances of <see cref="DateFormatConverter"/> class
        /// </summary>
        /// <param name="format">Date format</param>
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
