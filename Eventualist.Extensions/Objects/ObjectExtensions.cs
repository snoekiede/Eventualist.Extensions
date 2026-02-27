using System;
using System.Runtime.CompilerServices;

namespace Eventualist.Extensions.Objects
{
    /// <summary>
    /// Extension methods for object types
    /// </summary>
    public static class ObjectExtensions
    {
        extension(Object o)
        {
            /// <summary>
            /// Determines whether an object is null
            /// </summary>
            /// <returns>True if the object is null, otherwise false</returns>
            /// <example>
            /// <code>
            /// string? text = null;
            /// text.IsNull() // Returns true
            /// "hello".IsNull() // Returns false
            /// </code>
            /// </example>
            public bool IsNull<T>() where T : class
            {
                return o is null;
            }

            /// <summary>
            /// Determines whether an object is not null
            /// </summary>
            /// <returns>True if the object is not null, otherwise false</returns>
            /// <example>
            /// <code>
            /// string? text = "hello";
            /// text.IsNotNull() // Returns true
            /// ((string?)null).IsNotNull() // Returns false
            /// </code>
            /// </example>
            public bool IsNotNull<T>() where T : class
            {
                return o is not null;
            }

            /// <summary>
            /// Throws an ArgumentNullException if the object is null
            /// </summary>
            /// <typeparam name="T">The type of the object</typeparam>
            /// <param name="paramName">The name of the parameter (automatically captured)</param>
            /// <returns>The non-null object</returns>
            /// <exception cref="ArgumentNullException">Thrown when the object is null</exception>
            /// <example>
            /// <code>
            /// string? text = GetText();
            /// text.ThrowIfNull(); // Throws if text is null
            /// </code>
            /// </example>
            public T ThrowIfNull<T>([CallerArgumentExpression("o")] string? paramName = null) where T : class
            {
                ArgumentNullException.ThrowIfNull(o, paramName);
                return o as T;
            }

            /// <summary>
            /// Throws an ArgumentNullException with a custom message if the object is null
            /// </summary>
            /// <typeparam name="T">The type of the object</typeparam>
            /// <param name="message">The custom error message</param>
            /// <param name="paramName">The name of the parameter (automatically captured)</param>
            /// <returns>The non-null object</returns>
            /// <exception cref="ArgumentNullException">Thrown when the object is null</exception>
            public T ThrowIfNull<T>(string message,
                [CallerArgumentExpression("o")] string? paramName = null) where T : class
            {
                if (o is null)
                {
                    throw new ArgumentNullException(paramName, message);
                }
                return o as T;
            }
        }


        




    }
}
