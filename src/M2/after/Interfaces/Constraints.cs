using System;

public interface IFactory<T> where T : new()
{
    T Create();
}

interface IStructList<T> where T : struct { }
interface IClassList<T> where T : class { }
interface INullableClassList<T> where T : class? { }
interface INonNullList<T> where T : notnull { }
interface IClassFactory<T> where T : class, new() { }

public class BaseEntity { }
public class BaseEntity<TId> where TId : struct
{
    public TId Id { get; private set; }
}
public interface IEntity { }
interface IRepository1<T> where T : BaseEntity { } // a specific base class
interface IRepository2<T> where T : BaseEntity? { }
interface IRepository3<T> where T : IEntity { }
interface IRepository4<T> where T : IEntity? { }

interface IRepository<TEntity, TId> where TEntity : BaseEntity<TId>
                                   where TId : struct
{
    TEntity GetById(TId id);
}

public class Foo : BaseEntity<int> { }
public class Bar : BaseEntity<Guid> { }