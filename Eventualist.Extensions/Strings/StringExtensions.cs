using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Eventualist.Extensions.Strings
{
    public static class StringExtensions
    {
        /// <summary>
        /// return the alternative text if the underlying string is null or empty
        /// </summary>
        /// <param name="text">the underlying text</param>
        /// <param name="alternativeText">the text to be shown if it is null or empty</param>
        /// <returns>either text or the alternative text</returns>
        public static string ShowIfNone(this string text, string alternativeText = "None")
        {
            if (!String.IsNullOrEmpty(text))
            {
                return text;
            }
            else
            {
                return alternativeText;
            }
        }

        /// <summary>
        /// does the filename have the correct extensions (specific to my website, might be removed)
        /// </summary>
        /// <param name="filename">the filename to be tested</param>
        /// <returns>true if it has an allowed extension, false otherwise</returns>
        public static bool HasCorrectExtension(this string filename)
        {
            var allowedExtensions = new[] { "png", "jpg", "jpeg" };
            if (string.IsNullOrEmpty(filename))
            {
                return false;
            }

            var extension = filename.Split('.').LastOrDefault();
            if (string.IsNullOrEmpty(extension))
            {
                return false;
            }

            var lowerCaseExtension = extension.ToLower();
            return (allowedExtensions.Contains(lowerCaseExtension));

        }

        /// <summary>
        /// returns a mime type string based on the extension
        /// </summary>
        /// <param name="extension">the extension</param>
        /// <returns>a mimetype string</returns>
        public static string ConvertToMimeType(this string extension)
        {
            var genericExtension = extension.ToLower();
            switch (genericExtension)
            {
                case ".png": return "image/png";
                case ".jpg": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                default: return "unknown";
            }
        }


        /// <summary>
        /// Parse a date (this is specific to the website and might be removed)
        /// </summary>
        /// <param name="dateString">the underlying datestring</param>
        /// <returns>a datetime, or null if there was parsing error</returns>
        public static DateTime? ParseDateFromDateTimePicker(this string dateString)
        {
            var elements = dateString.Split('/');
            if (elements.Length != 3)
            {
                return null;
            }

            Int32.TryParse(elements[0], out int day);
            Int32.TryParse(elements[1], out int month);
            Int32.TryParse(elements[2], out int year);
            DateTime result = new DateTime(year, month, day);
            return result;
        }

        /// <summary>
        /// Convert a string to titlecase
        /// </summary>
        /// <param name="text">the text to be transformed</param>
        /// <returns>a transformed string</returns>
        public static string Titleize(this string text)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text).ToSentenceCase();
        }

        /// <summary>
        /// Convert a string to sentence case
        /// </summary>
        /// <param name="str">the string to be transformed</param>
        /// <returns>transformed string</returns>
        public static string ToSentenceCase(this string str)
        {
            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }

        /// <summary>
        /// Abbreviate a string on words
        /// </summary>
        /// <param name="str">the string to abbreviated</param>
        /// <param name="maxLength">the maximum length</param>
        /// <param name="abbreviationSymbol">to show there is more text</param>
        /// <returns>a transformed string</returns>
        public static string Abbreviate(this string str, int maxLength = 40, string abbreviationSymbol = "...")
        {
            if (str.Length <= maxLength)
            {
                return str;
            }
            else
            {
                var words = str.Split(' ');
                var wordList = new List<string>();
                int currentLength = 0;
                foreach (var word in words)
                {
                    if (currentLength <= maxLength)
                    {
                        wordList.Add(word);
                        currentLength += word.Length;
                    }
                    else
                    {
                        break;
                    }
                }

                return string.Join(" ", wordList);
            }
        }
    }
}
