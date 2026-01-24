//----------------------------------------------------------------------------------
// <copyright file="GuidExtensions.cs" company="Prakrishta Technologies">
//     Copyright (c) 2019 Prakrishta Technologies. All rights reserved.
// </copyright>
// <author>Arul Sengottaiyan</author>
// <date>3/3/2019</date>
// <summary>Guid Extensions class</summary>
//-----------------------------------------------------------------------------------

namespace Prakrishta.Infrastructure.Extensions
{
    using System;
    using System.Buffers.Text;
    using System.Runtime.InteropServices;

    public static class GuidExtensions
    {
        /// <summary>
        /// A GUID extension method that query if it is empty.
        /// </summary>
        /// <param name="value">The value to act on.</param>
        /// <returns>true if empty, false if not.</returns>
        public static bool IsEmpty(this Guid @value)
        {
            return value == Guid.Empty;
        }

        /// <summary>
        /// A GUID extension method that query if it is null or empty.
        /// </summary>
        /// <param name="value">The value to act on.</param>
        /// <returns>true if empty or null otherwise false.</returns>
        public static bool IsEmptyOrNull(this Guid? @value)
        {
            return value == null || value == Guid.Empty;
        }

        /// <summary>
        /// Converts GUID to base 64 encoded string.
        /// https://www.stevejgordon.co.uk/using-high-performance-dotnetcore-csharp-techniques-to-base64-encode-a-guid
        /// </summary>
        /// <param name="guid">The value to act on.</param>
        /// <returns>Encoded base 64 string</returns>
        public static string EncodeBase64String(this Guid guid)
        {
            const byte ForwardSlashByte = (byte)'/';
            const byte PlusByte = (byte)'+';
            const char Underscore = '_';
            const char Dash = '-';

            Span<byte> guidBytes = stackalloc byte[16];
            Span<byte> encodedBytes = stackalloc byte[24];

            MemoryMarshal.TryWrite(guidBytes, ref guid);
            Base64.EncodeToUtf8(guidBytes, encodedBytes, out _, out _);

            Span<char> chars = stackalloc char[22];

            for (var i = 0; i < 22; i++)
            {
                switch (encodedBytes[i])
                {
                    case ForwardSlashByte:
                        chars[i] = Dash;
                        break;
                    case PlusByte:
                        chars[i] = Underscore;
                        break;
                    default:
                        chars[i] = (char)encodedBytes[i];
                        break;
                }
            }

            return chars.ToString();
        }
    }
}
