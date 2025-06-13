using AspectInjector.Broker;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Eventualist.Extensions.Functions
{
    [AttributeUsage(AttributeTargets.Method)]
    [Aspect(Scope.Global)]
    public class MemoizeAttribute : Attribute
    {
        private static readonly ConcurrentDictionary<MethodBase, ConcurrentDictionary<string, Lazy<object?>>> _methodCaches = new();

        [Advice(Kind.Around, Targets = Target.Method)]
        public object? Handle(
            [Argument(Source.Metadata)] MethodBase methodInfo,
            [Argument(Source.Arguments)] object[] arguments,
            [Argument(Source.Target)] Func<object[], object> proceed)
        {
            var cache = _methodCaches.GetOrAdd(methodInfo, _ => new ConcurrentDictionary<string, Lazy<object?>>());
            var key = arguments == null || arguments.Length == 0
                ? "noargs"
                : string.Join("|", arguments.Select(a => a?.GetHashCode() ?? 0));

            var lazy = cache.GetOrAdd(key, _ => new Lazy<object?>(() => proceed(arguments), LazyThreadSafetyMode.ExecutionAndPublication));
            return lazy.Value;
        }
    }
}