//----------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>2/9/2019</date>
// <summary>Extension method for string type</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using Newtonsoft.Json;
    using Prakrishta.Infrastructure.Helper;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Drawing;
    
    /// <summary>
    /// Class that has extension methods for string type
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Parse string value to enum value
        /// </summary>
        /// <typeparam name="TEnum">Enum type to which the conversion should happen</typeparam>
        /// <param name="value">string value</param>
        /// <param name="ignoreCase">indicates if case insenstive</param>
        /// <returns>Enum value</returns>
        public static TEnum ToEnum<TEnum>(this string value, bool ignoreCase = false)
            where TEnum : struct
        {
            return EnumHelper.Parse<TEnum>(value, ignoreCase);
        }

        /// <summary>
        /// Converts string to title case string
        /// </summary>
        /// <param name="inputString">Input value</param>
        /// <returns>Title case string</returns>
        public static string ToTitleCase(this string inputString) => CultureInfo.InvariantCulture?.TextInfo?.ToTitleCase(inputString.ToLower());

        /// <summary>
        /// Converts string to its boolean equivalent
        /// </summary>
        /// <param name="value">string to convert</param>
        /// <returns>boolean equivalent</returns>
        /// <remarks>
        ///     <exception cref="ArgumentException">
        ///         thrown in the event no boolean equivalent found or an empty or whitespace
        ///         string is passed
        ///     </exception>
        /// </remarks>
        public static bool ToBoolean(this string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(nameof(value));
            }
            string val = value.ToLower().Trim();
            switch (val)
            {
                case "false":
                case "f":
                case "no":
                case "n":
                case "0":
                    return false;               
                case "true":
                case "t":
                case "yes":
                case "y":
                case "1":
                    return true;                
                default:
                    throw new ArgumentException("Invalid boolean");
            }
        }
        
        public static bool? ToNullableBoolean(this string value)
        {
            value = value.NullableTrim();
            
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            
            string val = value.ToLower().Trim();
            switch (val)
            {
                case "false":
                case "f":
                case "no":
                case "n":
                case "0":
                    return false;               
                case "true":
                case "t":
                case "yes":
                case "y":
                case "1":
                    return true;                
                default:
                    return null;
            }
        }
        
        public static string NullableTrim(this string value)
        {
            if(!string.IsNullOrEmpty(value))
            {
                return value.Trim();
            }
            
            return value;
        }

        /// <summary>
        ///     Validate email address
        /// </summary>
        /// <param name="email">string email address</param>
        /// <returns>true or false if email if valid</returns>
        public static bool IsEmailAddress(this string email)
        {
            string pattern =
                "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$";
            return Regex.Match(email, pattern).Success;
        }

        /// <summary>
        /// Validate zip code if it satisfies the pattern (xxxxx-xxxx)
        /// </summary>
        /// <param name="zipCode">string zip code</param>
        /// <returns>True if it matches the pattern otherwise false</returns>
        public static bool IsValidUSAZip(this string zipCode)
        {
            string pattern = "^[0-9]{5}(?:-[0-9]{4})?$";
            return Regex.Match(zipCode, pattern).Success;
        }

        /// <summary>
        /// Validate zip code if it satisfies the pattern (xxxxxx)
        /// </summary>
        /// <param name="postalCode">string zip code</param>
        /// <returns>True if it matches the pattern otherwise false</returns>
        public static bool IsValidIndianPostalCode(this string postalCode)
        {
            string pattern = @"^\d{6}(-\d{4})?$";
            return Regex.Match(postalCode, pattern).Success;
        }

        /// <summary>
        /// Validate zip code if it satisfies the pattern
        /// Canadian postal codes can't contain the letters D, F, I, O, Q, or U, and cannot start with W or Z
        /// </summary>
        /// <param name="postalCode">string zip code</param>
        /// <returns>True if it matches the pattern otherwise false</returns>
        public static bool IsValidCanadianPostalCode(this string postalCode)
        {
            string pattern = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";
            return Regex.Match(postalCode, pattern).Success;
        }

        /// <summary>
        /// Truncate the string for the given length
        /// </summary>
        /// <param name="input">string to be truncated</param>
        /// <param name="maxLength">number of characters to truncate</param>
        /// <returns>Truncated string</returns>
        public static string Truncate(this string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input) || maxLength <= 0)
            {
                return string.Empty;
            }

            if (input.Length > maxLength)
            {
                return input.Substring(0, maxLength);
            }

            return input;
        }

        /// <summary>
        /// Converts a Json string to object of type T method applicable for multi hierarchy objects i.e
        /// having zero or many parent child relationships, Ignore loop references and do not serialize if cycles are detected.
        /// </summary>
        /// <typeparam name="T">object to convert to</typeparam>
        /// <param name="json">json</param>
        /// <returns>object</returns>
        public static T JsonToObject<T>(this string json)
        {
            var settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        /// <summary>
        /// Checks if the String contains only Unicode letters, digits. null will return false. 
        /// An empty String ("") will return false.
        /// </summary>
        /// <param name="val">string to check if is Alpha or Numeric</param>
        /// <returns></returns>
        public static bool IsAlphaNumeric(this string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return false;
            }
            return val.Trim().Replace(" ", "").All(char.IsLetterOrDigit);
        }

        /// <summary>
        ///  Validates if a string is valid IPv4
        ///  Regular expression taken from <a href="http://regexlib.com/REDetails.aspx?regexp_id=2035">Regex reference</a>
        /// </summary>
        /// <param name="val">string IP address</param>
        /// <returns>true if string matches valid IP address else false</returns>
        public static bool IsValidIPv4(this string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return false;
            }
            return Regex.Match(val,
                @"(?:^|\s)([a-z]{3,6}(?=://))?(://)?((?:25[0-5]|2[0-4]\d|[01]?\d\d?)\.(?:25[0-5]|2[0-4]\d|[01]?\d\d?)\.(?:25[0-5]|2[0-4]\d|[01]?\d\d?)\.(?:25[0-5]|2[0-4]\d|[01]?\d\d?))(?::(\d{2,5}))?(?:\s|$)")
                .Success;
        }

        /// <summary>
        /// Extracts the left part of the input string limited with the length parameter
        /// </summary>
        /// <param name="val">The input string to take the left part from</param>
        /// <param name="length">The total number characters to take from the input string</param>
        /// <returns>The substring starting at startIndex 0 until length</returns>
        /// <exception cref="System.ArgumentNullException">input is null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Length is smaller than zero or higher than the length of input</exception>
        public static string Left(this string val, int length)
        {
            if (string.IsNullOrEmpty(val))
            {
                throw new ArgumentNullException(nameof(val));
            }
            if (length < 0 || length > val.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length),
                    "length cannot be higher than total string length or less than 0");
            }
            return val.Substring(0, length);
        }

        /// <summary>
        /// Extracts the right part of the input string limited with the length parameter
        /// </summary>
        /// <param name="val">The input string to take the right part from</param>
        /// <param name="length">The total number characters to take from the input string</param>
        /// <returns>The substring taken from the input string</returns>
        /// <exception cref="System.ArgumentNullException">input is null</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">Length is smaller than zero or higher than the length of input</exception>
        public static string Right(this string val, int length)
        {
            if (string.IsNullOrEmpty(val))
            {
                throw new ArgumentNullException(nameof(val));
            }
            if (length < 0 || length > val.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length),
                    "length cannot be higher than total string length or less than 0");
            }
            return val.Substring(val.Length - length);
        }

        /// <summary>
        /// Get only digits from string if anything
        /// </summary>
        /// <param name="value">The original string value</param>
        /// <returns>The numeric value</returns>
        public static string Digits(this string value)
        {
            return new string(value?.Where(c => char.IsDigit(c)).ToArray());
        }

        /// <summary>
        /// Get has algorithm for the given input string
        /// </summary>
        /// <param name="input">The input string</param>
        /// <returns>The encrypted string</returns>
        public static string GetHashAlgorithm(this string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();
            byte[] byteValue = Encoding.UTF8.GetBytes(input);
            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);
            return Convert.ToBase64String(byteHash);
        }

        /// <summary>
        /// Compare check sum of the downloaded update file with the algorithm and check sum given
        /// </summary>
        /// <param name="checksum">The check sum value</param>
        /// <param name="hashingAlgorithm">The algorithm type</param>
        /// <param name="fileName">The file name that has be to validated</param>
        /// <returns>Returns true if it matches otherwise false</returns>
        public static bool IsValidChecksum(this string checksum, string hashingAlgorithm, string fileName)
        {
            using (var hashAlgorithm = HashAlgorithm.Create(hashingAlgorithm))
            {
                using (var stream = File.OpenRead(fileName))
                {
                    if (hashAlgorithm != null)
                    {
                        var hash = hashAlgorithm.ComputeHash(stream);
                        var fileChecksum = BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();

                        if (fileChecksum == checksum.ToLower())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        
        public static Nullable<T> ToNullable<T>(this string value) where T : struct
        {
            Nullable<T> result = new Nullable<T>();
            try
            {
                if(!string.IsNullOrEmpty(value) && value.Trim().Length > 0)
                {
                    TypeConverter conv = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)conv.ConvertFrom(value.Trim());
                }
            }
            catch { }
            
            return result;
        }
        
        public static bool IsGuid(this string value)
        {
            if(!string.IsNullOrEmpty(value))
            {
                return Regex.Match(value,
                "^[A-Fa-f0-9]{32}$|" +
                "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
                "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$").Success;
            }
            
            return false;
        }
        
        public static string ReturnNumericals(this string value)
        {
            if(value == null) 
            {
                return value;
            }
            
            return Regex.Replace(value, "[^0-9]", string.Empty);
        }
        
        public static string ReturnNumericals(this string value, bool allowDot)
        {
            if(!allowDot)
            {
                return ReturnNumericals(value);
            }
            else
            {
                return Regex.Replace(value, "[^0-9|^.]", string.Empty);
            }
        }
        
        public static string ReturnNumericals(this string value, bool allowDot, bool allowComma)
        {
            if(!allowDot && !allowComma)
            {
                return ReturnNumericals(value);
            }
            else if(allowDot && !allowComma)
            {
                return ReturnNumericals(value, true);
            }
            else if(!allowDot && allowComma)
            {
                return Regex.Replace(value, "[^0-9|^,]", string.Empty);
            }
            else
            {
                return Regex.Replace(value, "[^0-9|^.|^,]", string.Empty);
            }
        }
        
        public static Color ToColor(this string argb)
        {
            Color color = new Color();
            if(!string.IsNullOrEmpty(argb))
            {
                argb = argb.Replace("#", string.Empty);
                byte a = System.Convert.ToByte("ff", 16);
                byte pos = 0;
                if (argb.Length == 8)
                {
                    a = System.Convert.ToByte(argb.Substring(pos, 2), 16);
                    pos = 2;
                }
                byte r = System.Convert.ToByte(argb.Substring(pos, 2), 16);
                pos += 2;
                byte g = System.Convert.ToByte(argb.Substring(pos, 2), 16);
                pos += 2;
                byte b = System.Convert.ToByte(argb.Substring(pos, 2), 16);
                color = System.Drawing.Color.FromArgb(a, r, g, b);
            }
            
            return color;
        }
    }
}
