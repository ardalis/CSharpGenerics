using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Caching;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Repositories
{
    public interface IReadRepository<T>
    {
        IEnumerable<T> List();
    }

    public interface IWriteRepository<T>
    {
        void Add(T entity);
        event EventHandler<T> EntityAdded;
    }

    public class Repository<T> : IReadRepository<T>, IWriteRepository<T>
    {
        private static List<T> _data = new();
        public IEnumerable<T> List()
        {
            return _data.AsEnumerable();
        }

        public event EventHandler<T> EntityAdded;
        protected virtual void OnEntityAdded(T entityAdded)
        {
            EventHandler<T> handler = EntityAdded;
            EntityAdded?.Invoke(this, entityAdded);
        }

        public void Add(T entity)
        {
            _data.Add(entity);
            OnEntityAdded(entity);
        }
    }

    public class CachedRepository<T> : IReadRepository<T>
    {
        private readonly IReadRepository<T> _sourceRepo;
        private readonly IMemoryCache _cache;
        public CachedRepository(IReadRepository<T> sourceRepo,
            MemoryCache cache)
        {
            _sourceRepo = sourceRepo;
            _cache = cache;
        }

        public IEnumerable<T> List()
        {
            string key = nameof(T);

            var result = _cache.GetOrCreate(key, entry =>
            {
                //entry.
                return _sourceRepo.List();
            });

            return result;
        }
    }
}