using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassesMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generic Classes and Methods");

            // non-static generic classes
            // cache<T>
            string message = "Generics are fun!";
            var cache = new Cache<string, string>(); // per instance
            string key = "fun-message";
            cache.AddOrUpdate(key, message);
            var cachedResult = cache.Get(key);
            Console.WriteLine(cachedResult);

            string message2 = "Lazy loading is fun, too.";
            string message2key = "lazy-message";
            Cache.Global.AddOrUpdate(message2key, message2); // essentially as singleton
            var cachedResult2 = Cache.Global.Get(message2key);
            Console.WriteLine(cachedResult2);

            // extension methods -- cover in methods section
            Console.WriteLine($"Is message not empty? {message.ToArray().WhereNotEmpty()}");
            Console.WriteLine($"Is message default? {message.IsDefault()}");
            Console.WriteLine($"Is 0 default? {0.IsDefault()}");

            // static classes/methods
            //string result = Converter.Convert("input"); // type arguments cannot be inferred
            string result0 = Converter.Convert<string, string>("input"); // must specify every type
            string result = ConvertTo<string>.Convert("input"); // only need to specify input type

            // static data access - not recommended typically
            IEnumerable<Customer> customers = DataAccess<Customer>.List();


        }
    }

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

    // extension methods
    public static class GenericExtensions
    {
        // confirm a sequence is not empty
        public static bool WhereNotEmpty<T> (this IEnumerable<T> source)
        {
            return source.Any(); // LINQ is basically a collection of these kinds of methods
        }

        // see if any instance is its default
        public static bool IsDefault<T>(this T input) where T:IEquatable<T>
        {
            return input.Equals(default(T));
        }
        
    }



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
        public static TOut Convert<TIn>(TIn intput)
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



}
