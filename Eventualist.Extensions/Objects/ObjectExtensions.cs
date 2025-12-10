using System;
using System.Runtime.CompilerServices;

namespace Eventualist.Extensions.Objects
{
    /// <summary>
    /// Extension methods for object types
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Determines whether an object is null
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="obj">The object to check</param>
        /// <returns>True if the object is null, otherwise false</returns>
        /// <example>
        /// <code>
        /// string? text = null;
        /// text.IsNull() // Returns true
        /// "hello".IsNull() // Returns false
        /// </code>
        /// </example>
        public static bool IsNull<T>(this T? obj) where T : class
        {
            return obj is null;
        }

        /// <summary>
        /// Determines whether an object is not null
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="obj">The object to check</param>
        /// <returns>True if the object is not null, otherwise false</returns>
        /// <example>
        /// <code>
        /// string? text = "hello";
        /// text.IsNotNull() // Returns true
        /// ((string?)null).IsNotNull() // Returns false
        /// </code>
        /// </example>
        public static bool IsNotNull<T>(this T? obj) where T : class
        {
            return obj is not null;
        }

        /// <summary>
        /// Throws an ArgumentNullException if the object is null
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="obj">The object to check</param>
        /// <param name="paramName">The name of the parameter (automatically captured)</param>
        /// <returns>The non-null object</returns>
        /// <exception cref="ArgumentNullException">Thrown when the object is null</exception>
        /// <example>
        /// <code>
        /// string? text = GetText();
        /// text.ThrowIfNull(); // Throws if text is null
        /// </code>
        /// </example>
        public static T ThrowIfNull<T>(
            this T? obj,
            [CallerArgumentExpression("obj")] string? paramName = null) where T : class
        {
            ArgumentNullException.ThrowIfNull(obj, paramName);
            return obj;
        }

        /// <summary>
        /// Throws an ArgumentNullException with a custom message if the object is null
        /// </summary>
        /// <typeparam name="T">The type of the object</typeparam>
        /// <param name="obj">The object to check</param>
        /// <param name="message">The custom error message</param>
        /// <param name="paramName">The name of the parameter (automatically captured)</param>
        /// <returns>The non-null object</returns>
        /// <exception cref="ArgumentNullException">Thrown when the object is null</exception>
        public static T ThrowIfNull<T>(
            this T? obj,
            string message,
            [CallerArgumentExpression("obj")] string? paramName = null) where T : class
        {
            if (obj is null)
            {
                throw new ArgumentNullException(paramName, message);
            }
            return obj;
        }
    }
}
