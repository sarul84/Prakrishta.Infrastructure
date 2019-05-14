//----------------------------------------------------------------------------------
// <copyright file="JArrayExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>3/3/2019</date>
// <summary>Extension class for Newton JArray object</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.IO;

    public static class JArrayExtensions
    {
        /// <summary>
        /// Load a JArray from a string that contains JSON
        /// </summary>
        /// <param name="jArray">The JArray object</param>
        /// <param name="json">A string that contains JSON.</param>
        /// <param name="settings">The JsonLoadSettings used to load the JSON. If this is null,
        /// default load settings will be used.</param>
        /// <param name="dateParseHandling">The DateParseHandling settings value</param>
        /// <returns>A JArray populated from the string that contains JSON.</returns>
        public static JArray Parse(this JArray jArray, string json, JsonLoadSettings settings = null,
            DateParseHandling dateParseHandling = DateParseHandling.None)
        {
            using (JsonReader reader = new JsonTextReader(new StringReader(json)) { DateParseHandling = dateParseHandling })
            {
                return JArray.Load(reader, settings);
            }
        }

        /// <summary>
        /// Load a JArray from a string that contains JSON
        /// </summary>
        /// <param name="jArray">The JArray object</param>
        /// <param name="json">A string that contains JSON.</param>
        /// <param name="settings">The JsonLoadSettings used to load the JSON. If this is null,
        /// default load settings will be used.</param>
        /// <param name="dateParseHandling">The DateParseHandling settings value</param>
        /// <param name="timeZoneHandling">The DateTimeZoneHandling settings value</param>
        /// <returns>A JArray populated from the string that contains JSON.</returns>
        public static JArray Parse(this JArray jArray, string json, JsonLoadSettings settings = null,
            DateParseHandling dateParseHandling = DateParseHandling.None,
            DateTimeZoneHandling timeZoneHandling = DateTimeZoneHandling.Unspecified)
        {
            using (JsonReader reader = new JsonTextReader(new StringReader(json))
            {
                DateParseHandling = dateParseHandling,
                DateTimeZoneHandling = timeZoneHandling
            })
            {
                return JArray.Load(reader, settings);
            }
        }

        /// <summary>
        /// Load a JArray from a string that contains JSON
        /// </summary>
        /// <param name="jArray">The JArray object</param>
        /// <param name="json">A string that contains JSON.</param>
        /// <param name="settings">The JsonLoadSettings used to load the JSON. If this is null,
        /// default load settings will be used.</param>
        /// <param name="dateParseHandling">The DateParseHandling settings value</param>
        /// <param name="timeZoneHandling">The DateTimeZoneHandling settings value</param>
        /// <param name="floatParseHandling">The FloatParseHandling settings value</param>
        /// <returns>A JArray populated from the string that contains JSON.</returns>
        public static JArray Parse(this JArray jArray, string json, JsonLoadSettings settings = null,
            DateParseHandling dateParseHandling = DateParseHandling.None,
            DateTimeZoneHandling timeZoneHandling = DateTimeZoneHandling.Unspecified,
            FloatParseHandling floatParseHandling = FloatParseHandling.Decimal)
        {
            using (JsonReader reader = new JsonTextReader(new StringReader(json))
            {
                DateParseHandling = dateParseHandling,
                DateTimeZoneHandling = timeZoneHandling,
                FloatParseHandling = floatParseHandling
            })
            {
                return JArray.Load(reader, settings);
            }
        }
    }
}
