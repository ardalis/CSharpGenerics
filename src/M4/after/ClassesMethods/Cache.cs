using System;
using System.Collections.Generic;

namespace CodeProjectCache
{
    // Cache<T>
    // https://www.codeproject.com/Articles/1033606/Cache-T-A-threadsafe-Simple-Efficient-Generic-In-m

    public class Cache<TKey, TObject> // TODO: IDisposable
    {
        private Dictionary<TKey, TObject> _cache = new();
        // omitted: timers and locking. See original source for real code

        public void AddOrUpdate(TKey key, TObject itemToCache)
        {
            if (_cache.ContainsKey(key))
            {
                _cache[key] = itemToCache;
            }
            else
            {
                _cache.Add(key, itemToCache);
            }
        }

        public TObject Get(TKey key)
        {
            if (_cache.ContainsKey(key))
            {
                return _cache[key];
            }
            return default(TObject);
        }
    }

    // non-generic class inherits from generic
    // can have as many instances as desired, or use a global one
    public class Cache : Cache<string, object>
    {
        private static Lazy<Cache> _globalCache = new Lazy<Cache>();
        public static Cache Global => _globalCache.Value;
    }
}