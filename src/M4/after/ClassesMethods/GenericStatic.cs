using System;
using System.Collections;
using System.Collections.Generic;

namespace ClassesMethods
{
    // https://stackoverflow.com/a/2686133/13729
    // better type inference
    public static class Converter
    {
        public static TOut Convert<TIn, TOut>(TIn input) where TOut : class
        {
            return null;
        }
    }

    public static class ConvertTo<TOut> where TOut : class
    {
        public static TOut Convert<TIn>(TIn input)
        {
            return null;
        }
    }



    // generic static classes
    public abstract class BaseEntity { };
    public class Customer : BaseEntity { }
    public static class DataAccess<T> where T : BaseEntity
    {
        public static IEnumerable<T> List()
        {
            return new List<T>();
        }
    }

    public interface IDataAccess
    {
        IEnumerable List();
    }

    // generic constructors
    // this is ok - referencing a generic *in* a constructor
    public class Repository<TEntity, TDataAccess> where TEntity : BaseEntity
                                                 where TDataAccess : IDataAccess
    {
        private TDataAccess _dataAccess;
        public Repository(TDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public IEnumerable<TEntity> List()
        {
            return (IEnumerable<TEntity>)_dataAccess.List();
        }
    }

    // generic constructor
    // this isn't allowed - won't compile
    public class Handler
    {
        // public Handler<TRequest,TResponse>(Func<TRequest,TResponse> func)
        // {
        //     // execute func as part of construction
        // }

        public static Handler<TRequest, TResponse> CreateWithFunc<TRequest, TResponse>(Func<TRequest, TResponse> func)
        {
            return new Handler<TRequest, TResponse>(func);
        }
    }
    public class Handler<TRequest, TResponse>
    {
        private readonly Func<TRequest, TResponse> _func;

        public Handler(Func<TRequest, TResponse> func)
        {
            _func = func;
        }
    }

    // sample from https://stackoverflow.com/a/700986/13729
    public interface IGenericType<T>
    {
    }

    public static class Foo
    {
        public static Foo<T> Create<T>(IGenericType<T> instance)
        {
            return new Foo<T>(instance);
        }
    }
    public class Foo<T>
    {
        public Foo(IGenericType<T> instance)
        {

        }
    }

    public class SampleUsage
    {
        static void Execute()
        {
            // no inference
            Handler<int, int> h = new Handler<int, int>(a => a * a);
            //Handler<int, int> h2 = Handler.CreateWithFunc(a => a * a); // no type inference
        }
    }


// compile warning
class GenericClass<T>
{
    void GenericMethod<T>() {}
}
}