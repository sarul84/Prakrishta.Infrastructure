//----------------------------------------------------------------------------------
// <copyright file="EnumHelper.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/9/2019</date>
// <summary>Enum Helper has many methods that do operations with enum</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Globalization;
    using System.Reflection;

    /// <summary>
    /// Enum helper class
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// Define the enum description as follows and the specified description attribute is what will get returned from
        /// this method, otherwise, just the text of the enum name itself. 
        /// </summary>
        /// <param name="value">Enum value</param>
        /// <returns>Enum Description value</returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.
                                                    GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }

        /// <summary>
        /// Gets a list of all instances of enum items within the specified enum.
        /// </summary>
        public static ICollection<TEnum> GetItems<TEnum>(bool excludeNone = false) where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enum type.");
            }

            int noneValue = -1;
            if (excludeNone)
            {
                noneValue = GetNoneValue<TEnum>();
            }

            Collection<TEnum> list = new Collection<TEnum>();
            foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            {
                if (noneValue > -1 && noneValue == GetValue<TEnum>(value)) { continue; }
                list.Add(value);
            }

            return list;
        }

        /// <summary>
        /// Get None value from Enumeration type
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <returns>None index</returns>
        public static int GetNoneValue<TEnum>() where TEnum : struct, IConvertible
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enum type.");
            }

            if (Enum.TryParse("None", true, out TEnum noneValue))
            {
                return GetValue<TEnum>(noneValue);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Get none value from specified enum type
        /// </summary>
        /// <param name="enumType">Enum type</param>
        /// <returns>None index</returns>
        public static int GetNoneValue(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Type must be an enum type.");
            }

            string[] names = Enum.GetNames(enumType);
            int indexOfNone = -1;
            indexOfNone = Array.IndexOf<string>(names, "None");

            if (indexOfNone > -1)
            {
                int[] values = (int[])Enum.GetValues(enumType);
                return values[indexOfNone];
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Get index of specified enum from enum
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int GetValue<TEnum>(TEnum value) where TEnum : struct, IConvertible
        {
            Type type = typeof(TEnum);
            if (!type.IsEnum)
            {
                throw new ArgumentException("TEnum must be an enum type.");
            }

            return value.ToInt32(CultureInfo.InvariantCulture.NumberFormat);
        }

        public static int GetValue(object enumValue)
        {
            Type type = enumValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("Object must be an enum type.");
            }

            return ((IConvertible)enumValue).ToInt32(CultureInfo.InvariantCulture.NumberFormat);
        }

        /// <summary>
        /// Get all enum values as collection
        /// </summary>
        /// <param name="enumType">Enum type</param>
        /// <param name="excludeNone">Indicates if case sensitive or not</param>
        /// <returns>Collection enum index values</returns>
        public static Collection<int> GetValues(Type enumType, bool excludeNone = false)
        {
            int noneValue = -1;
            if (excludeNone)
            {
                noneValue = GetNoneValue(enumType);
            }

            Array items = Enum.GetValues(enumType);
            Collection<int> intValues = new Collection<int>();
            foreach (var en in items)
            {
                if (excludeNone && (int)en == noneValue)
                {
                    continue;
                }
                intValues.Add((int)en);
            }
            return intValues;
        }

        /// <summary>
        /// Get all enum values as collection
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="excludeNone">Indicates if case sensitive or not</param>
        /// <returns>Collection enum index values</returns>
        public static Collection<int> GetValues<TEnum>(bool excludeNone = false)
        {
            return GetValues(typeof(TEnum), excludeNone);
        }

        /// <summary>
        /// Parse string value to specific enum type value
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="value">string value</param>
        /// <param name="ignoreCase">Indicates if case sensitive or not</param>
        /// <returns>Parsed enum value</returns>
        public static TEnum Parse<TEnum>(string value, bool ignoreCase = false) 
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new ArgumentException("TEnum must be an enum type.");
            }

            var result = (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
            return result;
        }        
    }
}
