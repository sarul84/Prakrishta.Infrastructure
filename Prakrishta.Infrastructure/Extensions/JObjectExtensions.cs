//----------------------------------------------------------------------------------
// <copyright file="JObjectExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>3/3/2019</date>
// <summary>Extension class for Newton Json object</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.IO;

    public static class JObjectExtensions
    {
        /// <summary>
        /// Load a JObject from a string that contains JSON
        /// </summary>
        /// <param name="jObject">The jobject parameter</param>
        /// <param name="json">A string that contains JSON.</param>
        /// <param name="settings">The JsonLoadSettings used to load the JSON. If this is null,
        /// default load settings will be used.</param>
        /// <param name="dateParseHandling">The DateParseHandling settings value</param>
        /// <returns>A JObject populated from the string that contains JSON.</returns>
        public static JObject Parse(this JObject jObject, string json, JsonLoadSettings settings = null, 
            DateParseHandling dateParseHandling = DateParseHandling.None)
        {
            using (JsonReader reader = new JsonTextReader(new StringReader(json)) { DateParseHandling = dateParseHandling })
            {
                return JObject.Load(reader, settings);
            }
        }

        /// <summary>
        /// Load a JObject from a string that contains JSON
        /// </summary>
        /// <param name="jObject">The jobject parameter</param>
        /// <param name="json">A string that contains JSON.</param>
        /// <param name="settings">The JsonLoadSettings used to load the JSON. If this is null,
        /// default load settings will be used.</param>
        /// <param name="dateParseHandling">The DateParseHandling settings value</param>
        /// <param name="timeZoneHandling">The DateTimeZoneHandling settings value</param>
        /// <returns>A JObject populated from the string that contains JSON.</returns>
        public static JObject Parse(this JObject jObject, string json, JsonLoadSettings settings = null,
            DateParseHandling dateParseHandling = DateParseHandling.None, 
            DateTimeZoneHandling timeZoneHandling = DateTimeZoneHandling.Unspecified)
        {
            using (JsonReader reader = new JsonTextReader(new StringReader(json)) {
                DateParseHandling = dateParseHandling, DateTimeZoneHandling = timeZoneHandling })
            {
                return JObject.Load(reader, settings);
            }
        }

        /// <summary>
        /// Load a JObject from a string that contains JSON
        /// </summary>
        /// <param name="jObject">The jobject parameter</param>
        /// <param name="json">A string that contains JSON.</param>
        /// <param name="settings">The JsonLoadSettings used to load the JSON. If this is null,
        /// default load settings will be used.</param>
        /// <param name="dateParseHandling">The DateParseHandling settings value</param>
        /// <param name="timeZoneHandling">The DateTimeZoneHandling settings value</param>
        /// <param name="floatParseHandling">The FloatParseHandling settings value</param>
        /// <returns>A JObject populated from the string that contains JSON.</returns>
        public static JObject Parse(this JObject jObject, string json, JsonLoadSettings settings = null,
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
                return JObject.Load(reader, settings);
            }
        }        
    }
}
