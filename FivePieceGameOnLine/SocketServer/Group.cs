using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    public class Group<K,V>
    {
        private Dictionary<K, V> dict = null;
        public static Group<K,V> CreateGroup()
        {
            Group<K, V> g = new Group<K, V>();
            return g;
        }
        public Group()
        {
            dict = new Dictionary<K, V>();
        }
        public bool Add(K key,V value)
        {
            if (dict.ContainsKey(key)) return false;
            dict.Add(key, value);
            return true;
        }
        public V Remove(K key)
        {
            if(dict.ContainsKey(key))
            {
                V v = dict[key];
                dict.Remove(key);
                return v;
            }
            return default(V);
        }
        public bool ContainsKey(K key)
        {
            return this.dict.ContainsKey(key);
        }
        public K[] Keys()
        {
            return this.dict.Keys.ToArray<K>();
        }
        public V[] Values()
        {
            return this.dict.Values.ToArray<V>();
        }
        public void Clear()
        {
            this.dict.Clear();
        }
        public V this[K key]
        {
            get {
                if (dict.ContainsKey(key)) return dict[key];
                return default(V);
            }
        }
    }
}
