using System;
using System.Collections.Generic;
using System.Text;

namespace Eventualist.Extensions.Collections
{
    public class ExtendedDictionary<K,V>:Dictionary<K,V>
    {
        public V this[K key, V defaultValue]
        {
            set => base[key] = value;
            get => !this.ContainsKey(key) ? defaultValue : this[key];
        }

        public new V this[K key]
        {
            set => base[key] = value;
            get => !this.ContainsKey(key) ? default(V) : base[key];
        }
    }
}
