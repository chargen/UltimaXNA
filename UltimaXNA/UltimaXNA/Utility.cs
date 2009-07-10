﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;

namespace UltimaXNA
{
    /// <summary>
    /// Utility class used to host common functions that do not fit inside a specific object.
    /// </summary>
    public static class Utility
    {
        #region Buffer Formatting
        /// <summary>
        /// Formats the buffer to an output to a hex editor view of the data
        /// </summary>
        /// <param name="buffer">The buffer to be formatted</param>
        /// <returns>A System.String containing the formatted buffer</returns>
        public static string FormatBuffer(byte[] buffer)
        {
            if (buffer == null)
                return string.Empty;

            string formatted = FormatBuffer(buffer, buffer.Length);

            return formatted;
        }

        /// <summary>
        /// Formats the buffer to an output to a hex editor view of the data
        /// </summary>
        /// <param name="buffer">The buffer to be formatted</param>
        /// <param name="length">The length in bytes to format</param>
        /// <returns>A System.String containing the formatted buffer</returns>
        public static string FormatBuffer(byte[] buffer, int length)
        {
            if (buffer == null)
                return string.Empty;

            StringBuilder builder = new StringBuilder();
            MemoryStream ms = new MemoryStream(buffer);

            FormatBuffer(builder, ms, length);

            return builder.ToString();
        }

        /// <summary>
        /// Formats the stream buffer to an output to a hex editor view of the data
        /// </summary>
        /// <param name="buffer">The stream to be formatted</param>
        /// <param name="length">The length in bytes to format</param>
        /// <returns>A System.String containing the formatted buffer</returns>
        public static string FormatBuffer(Stream input, int length)
        {
            StringBuilder builder = new StringBuilder();

            FormatBuffer(builder, input, length);

            return builder.ToString();
        }


        /// <summary>
        /// Formats the stream buffer to an output to a hex editor view of the data
        /// </summary>
        /// <param name="builder">The string builder to output the formatted buffer to</param>
        /// <param name="buffer">The stream to be formatted</param>
        /// <param name="length">The length in bytes to format</param>
        public static void FormatBuffer(StringBuilder builder, Stream input, int length)
        {
            length = (int)Math.Min(length, input.Length);
            builder.AppendLine("        0  1  2  3  4  5  6  7   8  9  A  B  C  D  E  F");
            builder.AppendLine("       -- -- -- -- -- -- -- --  -- -- -- -- -- -- -- --");

            int byteIndex = 0;

            int whole = length >> 4;
            int rem = length & 0xF;

            for (int i = 0; i < whole; ++i, byteIndex += 16)
            {
                StringBuilder bytes = new StringBuilder(49);
                StringBuilder chars = new StringBuilder(16);

                for (int j = 0; j < 16; ++j)
                {
                    int c = input.ReadByte();

                    bytes.Append(c.ToString("X2"));

                    if (j != 7)
                    {
                        bytes.Append(' ');
                    }
                    else
                    {
                        bytes.Append("  ");
                    }

                    if (c >= 0x20 && c < 0x80)
                    {
                        chars.Append((char)c);
                    }
                    else
                    {
                        chars.Append('.');
                    }
                }

                builder.Append(byteIndex.ToString("X4"));
                builder.Append("   ");
                builder.Append(bytes.ToString());
                builder.Append("  ");
                builder.AppendLine(chars.ToString());
            }

            if (rem != 0)
            {
                StringBuilder bytes = new StringBuilder(49);
                StringBuilder chars = new StringBuilder(rem);

                for (int j = 0; j < 16; ++j)
                {
                    if (j < rem)
                    {
                        int c = input.ReadByte();

                        bytes.Append(c.ToString("X2"));

                        if (j != 7)
                        {
                            bytes.Append(' ');
                        }
                        else
                        {
                            bytes.Append("  ");
                        }

                        if (c >= 0x20 && c < 0x80)
                        {
                            chars.Append((char)c);
                        }
                        else
                        {
                            chars.Append('.');
                        }
                    }
                    else
                    {
                        bytes.Append("   ");
                    }
                }

                builder.Append(byteIndex.ToString("X4"));
                builder.Append("   ");
                builder.Append(bytes.ToString());
                builder.Append("  ");
                builder.AppendLine(chars.ToString());
            }
        }
        #endregion

        #region Console Helpers
        private static Stack<ConsoleColor> consoleColors = new Stack<ConsoleColor>();

        /// <summary>
        /// Pushes the color to the console
        /// </summary>
        public static void PushColor(ConsoleColor color)
        {
            try
            {
                consoleColors.Push(Console.ForegroundColor);
                Console.ForegroundColor = color;
            }
            catch
            {
            }
        }

        /// <summary>
        /// Pops the color of the console to the previous value.
        /// </summary>
        public static void PopColor()
        {
            try
            {
                Console.ForegroundColor = consoleColors.Pop();
            }
            catch
            {

            }
        }
        #endregion

        #region Encoding
        private static Encoding utf8, utf8WithEncoding;

        public static Encoding UTF8
        {
            get
            {
                if (utf8 == null)
                    utf8 = new UTF8Encoding(false, false);

                return utf8;
            }
        }

        public static Encoding UTF8WithEncoding
        {
            get
            {
                if (utf8WithEncoding == null)
                    utf8WithEncoding = new UTF8Encoding(true, false);

                return utf8WithEncoding;
            }
        }
        #endregion

        public static bool InRange(Point from, Point to, int range)
        {
            return (from.X >= (to.X - range)) && (from.X <= (to.X + range)) && (from.Y >= (to.Y - range)) && (from.Y <= (to.Y + range));
        }
    }
}