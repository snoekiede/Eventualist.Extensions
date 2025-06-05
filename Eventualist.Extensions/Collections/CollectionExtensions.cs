using System;
using System.Collections.Generic;
using System.Linq;

namespace Eventualist.Extensions.Collections
{
    /// <summary>
    /// Extension methods for collections and enumerables
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Returns true if the collection is empty
        /// </summary>
        /// <typeparam name="T">The type of the collection elements</typeparam>
        /// <param name="collection">The collection to check</param>
        /// <returns>True if empty, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown when collection is null</exception>
        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);
            return !collection.Any();
        }

        /// <summary>
        /// Returns true if the collection is not empty
        /// </summary>
        /// <typeparam name="T">The type of the collection elements</typeparam>
        /// <param name="collection">The collection to check</param>
        /// <returns>True if not empty, false otherwise</returns>
        /// <exception cref="ArgumentNullException">Thrown when collection is null</exception>
        public static bool IsNotEmpty<T>(this IEnumerable<T> collection)
        {
            ArgumentNullException.ThrowIfNull(collection);
            return collection.Any();
        }

        /// <summary>
        /// Creates an ordered string with elements separated by the specified separator
        /// </summary>
        /// <typeparam name="T">The type of the collection elements</typeparam>
        /// <typeparam name="TKey">The type of the sorting key</typeparam>
        /// <param name="collection">The collection to order and join</param>
        /// <param name="keySelector">The selector used for the key</param>
        /// <param name="separator">The separator to use between elements</param>
        /// <param name="elementStringSelector">Optional function to convert each element to string</param>
        /// <returns>An ordered string of collection elements</returns>
        /// <exception cref="ArgumentNullException">Thrown when collection or keySelector is null</exception>
        public static string CreateOrderedString<T, TKey>(
            this IEnumerable<T> collection, 
            Func<T, TKey> keySelector, 
            string separator = ",",
            Func<T, string>? elementStringSelector = null)
        {
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentNullException.ThrowIfNull(keySelector);
            
            var orderedList = collection.OrderBy(keySelector);
            
            if (elementStringSelector != null)
            {
                return string.Join(separator, orderedList.Select(elementStringSelector));
            }
            
            return string.Join(separator, orderedList);
        }

        /// <summary>
        /// Divides a collection into groups of specified size
        /// </summary>
        /// <typeparam name="T">The type of the collection elements</typeparam>
        /// <param name="collection">The collection to divide</param>
        /// <param name="groupSize">The maximum size of each group</param>
        /// <returns>A collection of collection groups</returns>
        /// <exception cref="ArgumentNullException">Thrown when collection is null</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when groupSize is less than 1</exception>
        public static IEnumerable<IEnumerable<T>> Divide<T>(this IEnumerable<T> collection, int groupSize = 3)
        {
            ArgumentNullException.ThrowIfNull(collection);
            
            if (groupSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(groupSize), "Group size must be at least 1");
            }

#if NET6_0_OR_GREATER
            // Use built-in Chunk method if available
            return collection.Chunk(groupSize);
#else
            // Manual implementation for older frameworks
            return DivideImplementation(collection, groupSize);
#endif
        }

#if !NET6_0_OR_GREATER
        private static IEnumerable<IEnumerable<T>> DivideImplementation<T>(IEnumerable<T> collection, int groupSize)
        {
            var currentGroup = new List<T>(groupSize);
            
            foreach (var item in collection)
            {
                currentGroup.Add(item);
                
                if (currentGroup.Count == groupSize)
                {
                    yield return currentGroup;
                    currentGroup = new List<T>(groupSize);
                }
            }
            
            if (currentGroup.Count > 0)
            {
                yield return currentGroup;
            }
        }
#endif




    }
}
