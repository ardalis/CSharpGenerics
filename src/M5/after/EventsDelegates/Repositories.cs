using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Caching;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Repositories
{
    public interface IWriteRepository<T>
    {
        void Add(T entity);
        event EventHandler<EntityAddedEventArgs<T>> EntityAdded;
    }








    public class Repository<T> : IReadRepository<T>, IWriteRepository<T>
    {
        private static List<T> _data = new();
        public IEnumerable<T> List()
        {
            return _data.AsEnumerable();
        }

        public void Add(T entity)
        {
            _data.Add(entity);
            OnEntityAdded(new EntityAddedEventArgs<T>(entity));
        }

        public event EventHandler<EntityAddedEventArgs<T>> EntityAdded;
        protected virtual void OnEntityAdded(EntityAddedEventArgs<T> eventArgs)
        {
            EntityAdded?.Invoke(this, eventArgs);
        }
    }




    // using an EventArgs type makes design more extensible
    public class EntityAddedEventArgs<T> : EventArgs
    {
        public EntityAddedEventArgs(T entityAdded)
        {
            EntityAdded = entityAdded;
        }

        public T EntityAdded { get; }
    }







    public interface IReadRepository<T>
    {
        IEnumerable<T> List();
    }



    // an example decorator for a repository that adds simple caching
    // see https://ardalis.com/building-a-cachedrepository-in-aspnet-core/
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