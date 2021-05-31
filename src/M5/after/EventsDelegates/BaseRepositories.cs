using System;
using System.Collections.Generic;

namespace BaseRepositories
{
    public interface IWriteRepository<T>
    {
        void Add(T entity);
        event EventHandler<EntityAddedEventArgs<T>> EntityAdded;
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

      

    public abstract class RepositoryBase<T> : IWriteRepository<T>
    {
        public event EventHandler<EntityAddedEventArgs<T>> EntityAdded;

        // derived classes need to remember to call OnEntityAdded()
        public abstract void Add(T entity);

        protected virtual void OnEntityAdded(EntityAddedEventArgs<T> eventArgs)
        {
            EntityAdded?.Invoke(this, eventArgs);
        }
    }
    public class Repository<T> : RepositoryBase<T>
    {
        private static List<T> _data = new();

        public override void Add(T entity)
        {
            _data.Add(entity);
            // don't forget to add this!
            OnEntityAdded(new EntityAddedEventArgs<T>(entity));
        }
    }

    public abstract class RepositoryBase2<T> : IWriteRepository<T>
    {
        public event EventHandler<EntityAddedEventArgs<T>> EntityAdded;

        // derived classes should call base.Add(entity) after their functionality
        public virtual void Add(T entity)
        {
            OnEntityAdded(new EntityAddedEventArgs<T>(entity));
        }

        protected virtual void OnEntityAdded(EntityAddedEventArgs<T> eventArgs)
        {
            EntityAdded?.Invoke(this, eventArgs);
        }

    }
    public class Repository2<T> : RepositoryBase2<T>
    {
        private static List<T> _data = new();

        public override void Add(T entity)
        {
            _data.Add(entity);
            // don't forget to add this!
            base.Add(entity);
        }
    }

    public abstract class RepositoryBase3<T> : IWriteRepository<T>
    {
        public event EventHandler<EntityAddedEventArgs<T>> EntityAdded;

        // often named AddImpl, DoAdd, DerivedAdd, etc.
        protected abstract void DoAdd(T entity);
        
        public void Add(T entity)
        {
            DoAdd(entity);
            OnEntityAdded(new EntityAddedEventArgs<T>(entity));
        }

        protected virtual void OnEntityAdded(EntityAddedEventArgs<T> eventArgs)
        {
            EntityAdded?.Invoke(this, eventArgs);
        }
    }
    public class Repository3<T> : RepositoryBase3<T>
    {
        private static List<T> _data = new();

        protected override void DoAdd(T entity)
        {
            _data.Add(entity);
        }
    }

}