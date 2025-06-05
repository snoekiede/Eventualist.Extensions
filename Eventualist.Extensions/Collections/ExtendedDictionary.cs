using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Eventualist.Extensions.Collections
{
    /// <summary>
    /// Dictionary extension that provides default value handling and safer key access
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary</typeparam>
    public class ExtendedDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public ExtendedDictionary() : base() { }

        /// <summary>
        /// Constructor with custom equality comparer
        /// </summary>
        public ExtendedDictionary(IEqualityComparer<TKey> comparer) : base(comparer) { }

        /// <summary>
        /// Constructor with initial capacity
        /// </summary>
        public ExtendedDictionary(int capacity) : base(capacity) { }

        /// <summary>
        /// Constructor with initial dictionary values
        /// </summary>
        public ExtendedDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary) { }

        /// <summary>
        /// Constructor with initial capacity and custom equality comparer
        /// </summary>
        public ExtendedDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }

        /// <summary>
        /// Constructor with initial dictionary values and custom equality comparer
        /// </summary>
        public ExtendedDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
            : base(dictionary, comparer) { }

        /// <summary>
        /// Gets or sets the value associated with the specified key, returning a default value if the key is not found
        /// </summary>
        /// <param name="key">The key of the value to get or set</param>
        /// <param name="defaultValue">The default value to return if the key is not found</param>
        /// <returns>The value associated with the key, or the default value if the key is not found</returns>
        /// <exception cref="ArgumentNullException">Thrown when key is null</exception>
        public TValue this[TKey key, TValue defaultValue]
        {
            get => TryGetValue(key, out TValue? value) ? value : defaultValue;
            set => base[key] = value;
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key, returning the type's default value if the key is not found
        /// </summary>
        /// <param name="key">The key of the value to get or set</param>
        /// <returns>The value associated with the key, or the default value for the type if the key is not found</returns>
        /// <exception cref="ArgumentNullException">Thrown when key is null</exception>
        public new TValue this[TKey key]
        {
            get => TryGetValue(key, out TValue? value) ? value : default!;
            set => base[key] = value;
        }

        /// <summary>
        /// Gets the value associated with the specified key or a default value if the key doesn't exist
        /// </summary>
        /// <param name="key">The key of the value to get</param>
        /// <param name="defaultValue">The default value to return if the key is not found</param>
        /// <returns>The value associated with the key, or defaultValue if the key is not found</returns>
        /// <exception cref="ArgumentNullException">Thrown when key is null</exception>
        public TValue GetValueOrDefault(TKey key, TValue defaultValue)
        {
            return TryGetValue(key, out TValue? value) ? value : defaultValue;
        }

        /// <summary>
        /// Gets the value associated with the specified key or the default value for the type if the key doesn't exist
        /// </summary>
        /// <param name="key">The key of the value to get</param>
        /// <returns>The value associated with the key, or default(TValue) if the key is not found</returns>
        /// <exception cref="ArgumentNullException">Thrown when key is null</exception>
        public TValue GetValueOrDefault(TKey key)
        {
            return GetValueOrDefault(key, default!);
        }

        /// <summary>
        /// Adds a key/value pair to the dictionary if the key does not already exist
        /// </summary>
        /// <param name="key">The key of the element to add</param>
        /// <param name="value">The value to be added if the key does not exist</param>
        /// <returns>The existing value if the key exists, otherwise the new value</returns>
        /// <exception cref="ArgumentNullException">Thrown when key is null</exception>
        public TValue GetOrAdd(TKey key, TValue value)
        {
            if (!ContainsKey(key))
            {
                Add(key, value);
                return value;
            }
            return this[key];
        }

        /// <summary>
        /// Adds a key/value pair to the dictionary by using the specified function if the key does not already exist
        /// </summary>
        /// <param name="key">The key of the element to add</param>
        /// <param name="valueFactory">The function used to generate a value for the key</param>
        /// <returns>The value for the key, either existing or newly added</returns>
        /// <exception cref="ArgumentNullException">Thrown when key or valueFactory is null</exception>
        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            ArgumentNullException.ThrowIfNull(valueFactory);

            if (!ContainsKey(key))
            {
                TValue value = valueFactory(key);
                Add(key, value);
                return value;
            }
            return this[key];
        }
    }
}