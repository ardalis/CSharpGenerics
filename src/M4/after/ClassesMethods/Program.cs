using System;
using System.Collections.Generic;
using System.Linq;
using BasicDefinitions;
using CodeProjectCache;
using Generic.Commands;

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

            // working with generics that have a generic base class
            string[] inputs = { "Follow ", "Steve ", "on ", "Pluralsight.com ", "to ", "get ", "updates!" };
            ICommand c = new ConcatCommand(inputs); // can replace with ICommand<string>
            string results = (string)c.Execute();   // which eliminates the cast here
            Console.WriteLine(results);

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

            var r = new ResultClosed<string>(); // T is string even though its base class T is int
        }
    }




}
