using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Eventualist.Extensions.Functions
{
    /// <summary>
    /// Extension methods for function memoization and manipulation
    /// </summary>
    public static class FunctionExtensions
    {
        /// <summary>
        /// Creates a memoized version of a parameterless function that caches its result
        /// </summary>
        /// <typeparam name="TResult">The return type of the function</typeparam>
        /// <param name="function">The function to memoize</param>
        /// <returns>A memoized version of the function</returns>
        /// <exception cref="ArgumentNullException">Thrown when function is null</exception>
        public static Func<TResult> Memoize<TResult>(this Func<TResult> function)
        {
            ArgumentNullException.ThrowIfNull(function);

            // Using lazy initialization for thread safety and deferred execution
            var lazy = new Lazy<TResult>(function, LazyThreadSafetyMode.ExecutionAndPublication);
            return () => lazy.Value;
        }

        /// <summary>
        /// Creates a memoized version of a function with one parameter
        /// </summary>
        /// <typeparam name="TParam">The parameter type</typeparam>
        /// <typeparam name="TResult">The return type of the function</typeparam>
        /// <param name="function">The function to memoize</param>
        /// <param name="comparer">Optional custom equality comparer for the parameter</param>
        /// <returns>A memoized version of the function</returns>
        /// <exception cref="ArgumentNullException">Thrown when function is null</exception>
        public static Func<TParam, TResult> Memoize<TParam, TResult>(
            this Func<TParam, TResult> function,
            IEqualityComparer<TParam>? comparer = null)
        {
            ArgumentNullException.ThrowIfNull(function);

            var cache = new ConcurrentDictionary<TParam, Lazy<TResult>>(comparer ?? EqualityComparer<TParam>.Default);

            return param =>
            {
                // Using GetOrAdd with Lazy<T> ensures the function is only executed once, 
                // even if multiple threads try to get the same key simultaneously
                var lazy = cache.GetOrAdd(param,
                    key => new Lazy<TResult>(() => function(key), LazyThreadSafetyMode.ExecutionAndPublication));
                return lazy.Value;
            };
        }

        /// <summary>
        /// Creates a memoized version of a function with two parameters
        /// </summary>
        /// <returns>A memoized version of the function</returns>
        /// <exception cref="ArgumentNullException">Thrown when function is null</exception>
        public static Func<TParam1, TParam2, TResult> Memoize<TParam1, TParam2, TResult>(
            this Func<TParam1, TParam2, TResult> function)
        {
            ArgumentNullException.ThrowIfNull(function);

            var cache = new ConcurrentDictionary<(TParam1, TParam2), Lazy<TResult>>();

            return (param1, param2) =>
            {
                var key = (param1, param2);
                var lazy = cache.GetOrAdd(key,
                    k => new Lazy<TResult>(() => function(k.Item1, k.Item2), LazyThreadSafetyMode.ExecutionAndPublication));
                return lazy.Value;
            };
        }

        /// <summary>
        /// Creates a memoized version of a function with three parameters
        /// </summary>
        /// <returns>A memoized version of the function</returns>
        /// <exception cref="ArgumentNullException">Thrown when function is null</exception>
        public static Func<TParam1, TParam2, TParam3, TResult> Memoize<TParam1, TParam2, TParam3, TResult>(
            this Func<TParam1, TParam2, TParam3, TResult> function)
        {
            ArgumentNullException.ThrowIfNull(function);

            var cache = new ConcurrentDictionary<(TParam1, TParam2, TParam3), Lazy<TResult>>();

            return (param1, param2, param3) =>
            {
                var key = (param1, param2, param3);
                var lazy = cache.GetOrAdd(key,
                    k => new Lazy<TResult>(() => function(k.Item1, k.Item2, k.Item3),
                    LazyThreadSafetyMode.ExecutionAndPublication));
                return lazy.Value;
            };
        }

        /// <summary>
        /// Creates a memoized version of a function with four parameters
        /// </summary>
        /// <returns>A memoized version of the function</returns>
        /// <exception cref="ArgumentNullException">Thrown when function is null</exception>
        public static Func<TParam1, TParam2, TParam3, TParam4, TResult> Memoize<TParam1, TParam2, TParam3, TParam4, TResult>(
            this Func<TParam1, TParam2, TParam3, TParam4, TResult> function)
        {
            ArgumentNullException.ThrowIfNull(function);

            var cache = new ConcurrentDictionary<(TParam1, TParam2, TParam3, TParam4), Lazy<TResult>>();

            return (param1, param2, param3, param4) =>
            {
                var key = (param1, param2, param3, param4);
                var lazy = cache.GetOrAdd(key,
                    k => new Lazy<TResult>(() => function(k.Item1, k.Item2, k.Item3, k.Item4),
                    LazyThreadSafetyMode.ExecutionAndPublication));
                return lazy.Value;
            };
        }

        /// <summary>
        /// Creates a memoized version of a function with five parameters
        /// </summary>
        /// <returns>A memoized version of the function</returns>
        /// <exception cref="ArgumentNullException">Thrown when function is null</exception>
        public static Func<TParam1, TParam2, TParam3, TParam4, TParam5, TResult> Memoize<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>(
            this Func<TParam1, TParam2, TParam3, TParam4, TParam5, TResult> function)
        {
            ArgumentNullException.ThrowIfNull(function);

            var cache = new ConcurrentDictionary<(TParam1, TParam2, TParam3, TParam4, TParam5), Lazy<TResult>>();

            return (param1, param2, param3, param4, param5) =>
            {
                var key = (param1, param2, param3, param4, param5);
                var lazy = cache.GetOrAdd(key,
                    k => new Lazy<TResult>(() => function(k.Item1, k.Item2, k.Item3, k.Item4, k.Item5),
                    LazyThreadSafetyMode.ExecutionAndPublication));
                return lazy.Value;
            };
        }

        /// <summary>
        /// Creates a memoized version of a function with six parameters
        /// </summary>
        /// <returns>A memoized version of the function</returns>
        /// <exception cref="ArgumentNullException">Thrown when function is null</exception>
        public static Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult> Memoize<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult>(
            this Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult> function)
        {
            ArgumentNullException.ThrowIfNull(function);

            var cache = new ConcurrentDictionary<(TParam1, TParam2, TParam3, TParam4, TParam5, TParam6), Lazy<TResult>>();

            return (param1, param2, param3, param4, param5, param6) =>
            {
                var key = (param1, param2, param3, param4, param5, param6);
                var lazy = cache.GetOrAdd(key,
                    k => new Lazy<TResult>(() => function(k.Item1, k.Item2, k.Item3, k.Item4, k.Item5, k.Item6),
                    LazyThreadSafetyMode.ExecutionAndPublication));
                return lazy.Value;
            };
        }

        /// <summary>
        /// Creates a memoized function that will cache results for a limited time
        /// </summary>
        /// <param name="function">The function to memoize</param>
        /// <param name="expiration">The time period after which cached values expire</param>
        /// <returns>A time-limited memoized version of the function</returns>
        public static Func<TParam, TResult> MemoizeWithExpiration<TParam, TResult>(
            this Func<TParam, TResult> function,
            TimeSpan expiration)
        {
            ArgumentNullException.ThrowIfNull(function);

            var cache = new ConcurrentDictionary<TParam, (TResult Value, DateTime Expiration)>();

            return param =>
            {
                if (cache.TryGetValue(param, out var cached) && DateTime.UtcNow < cached.Expiration)
                {
                    return cached.Value;
                }

                // Use a lock-free approach with Interlocked.CompareExchange
                var result = function(param);
                var expirationTime = DateTime.UtcNow.Add(expiration);
                var newValue = (result, expirationTime);

                // Add or update atomically
                cache.AddOrUpdate(param, newValue, (_, _) => newValue);

                return result;
            };
        }

        /// <summary>
        /// Composes two functions, passing the result of the first to the second
        /// </summary>
        /// <param name="first">The first function to apply</param>
        /// <param name="second">The second function to apply to the result of the first</param>
        /// <returns>A composed function</returns>
        public static Func<TParam, TResult> Compose<TParam, TIntermediate, TResult>(
            this Func<TParam, TIntermediate> first,
            Func<TIntermediate, TResult> second)
        {
            ArgumentNullException.ThrowIfNull(first);
            ArgumentNullException.ThrowIfNull(second);

            return param => second(first(param));
        }
    }
}