using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eventualist.Extensions.Collections
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Returns true if the list is empty
        /// </summary>
        /// <typeparam name="T">the type of the collection</typeparam>
        /// <param name="list">the underlying collection</param>
        /// <returns>true if empty, false otherwise</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            return !list.Any();
        }

        /// <summary>
        /// Returns true if list is not empty, 
        /// </summary>
        /// <typeparam name="T">the type of the collection</typeparam>
        /// <param name="list">the underlying collection</param>
        /// <returns>true if not empty, false otherwise</returns>
        public static bool IsNotEmpty<T>(this IEnumerable<T> list)
        {
            return list.Any();
        }
        /// <summary>
        /// Creates an ordered string separated by separator
        /// </summary>
        /// <typeparam name="T">the type of the collection</typeparam>
        /// <typeparam name="TKey">the key to be used</typeparam>
        /// <param name="list">the underlying collection</param>
        /// <param name="keyselector">the selector used for the key</param>
        /// <param name="separator">the separator to be used</param>
        /// <returns>an ordered string</returns>
        public static string CreateOrderedString<T, TKey>(this IEnumerable<T> list, Func<T, TKey> keyselector, string separator = ",")
        {
            var orderedList = list.OrderBy(keyselector).AsEnumerable();
            return string.Join(separator, orderedList);
        }

        /// <summary>
        /// Divides a collection into a number of collection maxlength groupSize
        /// </summary>
        /// <typeparam name="T">the type of the collection</typeparam>
        /// <param name="list">the underlying collection</param>
        /// <param name="groupSize">the max length of resulting collections</param>
        /// <returns>a collection of collections</returns>
        public static IEnumerable<IEnumerable<T>> Divide<T>(this IEnumerable<T> list, int groupSize = 3)
        {
            if (groupSize == 0)
            {
                throw new Exception("Cannot make groups of size zero");
            }
            List<List<T>> result = new List<List<T>>();
            int itemCounter = 1;
            List<T> temp = new List<T>();
            foreach (var element in list)
            {
                if (itemCounter % groupSize == 0)
                {
                    temp.Add(element);
                    result.Add(temp);
                    temp = new List<T>();
                    itemCounter++;
                }
                else
                {
                    temp.Add(element);
                    itemCounter++;
                }
            }

            if (temp.IsNotEmpty())
            {
                result.Add(temp);
            }


            return result.AsEnumerable();
        }
    }
}
