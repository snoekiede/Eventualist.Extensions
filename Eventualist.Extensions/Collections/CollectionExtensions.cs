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
        
        extension<T>(IEnumerable<T> collection)
        {
            /// <summary>
            /// Returns true if the collection is empty
            /// </summary>
            /// <returns>True if empty, false otherwise</returns>
            /// <exception cref="ArgumentNullException">Thrown when collection is null</exception>
            public bool IsEmpty()
            {
                ArgumentNullException.ThrowIfNull(collection);
                return !collection.Any();
            }

            /// <summary>
            /// Returns true if the collection is not empty
            /// </summary>
            /// <typeparam name="T">The type of the collection elements</typeparam>
            /// <returns>True if not empty, false otherwise</returns>
            /// <exception cref="ArgumentNullException">Thrown when collection is null</exception>
            public bool IsNotEmpty()
            {
                ArgumentNullException.ThrowIfNull(collection);
                return collection.Any();
            }

            /// <summary>
            /// Creates an ordered string with elements separated by the specified separator
            /// </summary>
            /// <typeparam name="TKey">The type of the sorting key</typeparam>
            /// <param name="keySelector">The selector used for the key</param>
            /// <param name="separator">The separator to use between elements</param>
            /// <param name="elementStringSelector">Optional function to convert each element to string</param>
            /// <returns>An ordered string of collection elements</returns>
            /// <exception cref="ArgumentNullException">Thrown when collection or keySelector is null</exception>
            public string CreateOrderedString<TKey>(
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
            /// <param name="groupSize">The maximum size of each group</param>
            /// <returns>A collection of collection groups</returns>
            /// <exception cref="ArgumentNullException">Thrown when collection is null</exception>
            /// <exception cref="ArgumentOutOfRangeException">Thrown when groupSize is less than 1</exception>
            [Obsolete("This method is obsolete. Use the built-in Chunk method available in .NET 6 and later.")]
            public IEnumerable<IEnumerable<T>> Divide(int groupSize = 3)
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

            /// <summary>
            /// Filters out null values from a collection
            /// </summary>
            /// <returns>A collection with all null values removed</returns>
            /// <exception cref="ArgumentNullException">Thrown when collection is null</exception>
            /// <example>
            /// <code>
            /// var items = new[] { "a", null, "b", null, "c" };
            /// var filtered = items.WhereNotNull(); // Returns ["a", "b", "c"]
            /// </code>
            /// </example>
            public IEnumerable<T> WhereNotNull()
            {
                ArgumentNullException.ThrowIfNull(collection);
                return collection.Where(item => item != null)!;
            }

            /// <summary>
            /// Determines whether the collection contains all of the specified items
            /// </summary>
            /// <param name="items">The items to check for</param>
            /// <returns>True if all items are present, otherwise false</returns>
            /// <exception cref="ArgumentNullException">Thrown when collection or items is null</exception>
            /// <example>
            /// <code>
            /// var numbers = new[] { 1, 2, 3, 4, 5 };
            /// numbers.ContainsAll(2, 4) // Returns true
            /// numbers.ContainsAll(2, 6) // Returns false
            /// </code>
            /// </example>
            public bool ContainsAll(params T[] items)
            {
                ArgumentNullException.ThrowIfNull(collection);
                ArgumentNullException.ThrowIfNull(items);

                var collectionSet = collection.ToHashSet();
                return items.All(item => collectionSet.Contains(item));
            }
        }
    }
        






}
