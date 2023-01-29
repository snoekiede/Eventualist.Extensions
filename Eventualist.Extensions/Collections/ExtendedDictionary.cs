using System;
using System.Collections.Generic;

namespace Eventualist.Extensions.Collections
{
    public class ExtendedDictionary<K,V>:Dictionary<K,V>
    {
        public V this[K key, V defaultValue]
        {
            set => base[key ?? throw new ArgumentNullException(nameof(key))] = value;
            get => !this.ContainsKey(key ?? throw new ArgumentNullException(nameof(key))) ? defaultValue : this[key];
        }

        public new V this[K key]
        {
            set => base[key ?? throw new ArgumentNullException(nameof(key))] = value;
            get => !this.ContainsKey(key ?? throw new ArgumentNullException(nameof(key))) ? default(V) : base[key];
        }
    }
}
