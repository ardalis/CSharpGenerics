using System;
using System.Collections.Generic;
using System.Linq;
using BasicDefinitions;
using CodeProjectCache;
using Counter;
using Generic.Commands;

namespace ClassesMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generic Classes and Methods");

            // basics
            var r = new ResultFromClosed<string>(); // T is string even though its base class T is int


            // non-static generic classes
            // cache<T>
            // string message = "Generics are fun!";
            // var cache = new Cache<string, string>(); // per instance (thus per key/value type combination)
            // string key = "fun-message";
            // cache.AddOrUpdate(key, message);
            // string cachedResult = cache.Get(key);
            // Console.WriteLine(cachedResult);

            // string message2 = "Lazy loading is fun, too.";
            // string message2key = "lazy-message";
            // Cache.Global.AddOrUpdate(message2key, message2); // essentially as singleton
            // object cachedResult2 = Cache.Global.Get(message2key); // note: no strong typing
            // Console.WriteLine(cachedResult2);









            // working with generics that have a generic base class and non-generic analogs
            string[] inputs = { "Follow ", "Steve ", "on ", "Pluralsight.com ", "to ", "get ", "updates!" };
            ICommand<string> c = new ConcatCommand(inputs); // can replace with ICommand<string>
            string results = c.Execute();   // which eliminates the cast here
            Console.WriteLine(results);












            // counters -- these are not threadsafe
            BaseClass.GlobalCounter += 10;
            Generic<int>.Counter += 5;
            Generic<string>.Counter += 7;

            Console.WriteLine($"Counts: {BaseClass.GlobalCounter} {Generic<int>.Counter} {Generic<string>.Counter}");

            Generic<int>.GlobalCounter += 5;
            Generic<string>.GlobalCounter += 5;
            Console.WriteLine($"Counts: {BaseClass.GlobalCounter} {Generic<int>.Counter} {Generic<string>.Counter}");

            var intInstance = new Generic<int>();
            intInstance.AnotherCounter++;
            Console.WriteLine($"Counts: {BaseClass.GlobalCounter} {Generic<int>.Counter} {Generic<string>.Counter}");
            intInstance.LocalCounter++;
            var stringInstance = new Generic<string>();
            stringInstance.LocalCounter++;
            Console.WriteLine($"Counts: int: {intInstance.LocalCounter} string: {stringInstance.LocalCounter}");







            // using the non-generic command with just objects
            var stuff = new Object[] { 1, "hey", 4.3 };
            var collectCommand = new CollectCommand(stuff);
            var collectResult = (List<object>)collectCommand.Execute();
            // Console.WriteLine(String.Join(',', collectResult
            //                                     .Select(o => o.ToString()).ToArray()));


            // static classes/methods
            //string result = Converter.Convert("input"); // type arguments cannot be inferred
            string result0 = Converter.Convert<string, string>("input"); // must specify every type
            string result = ConvertTo<string>.Convert("input"); // only need to specify output type






            // extension methods -- cover in methods section
            string someMessage = "";
            Console.WriteLine($"Is message not empty? {someMessage.ToArray().WhereNotEmpty()}");
            Console.WriteLine($"Is message default? {someMessage.IsDefault()}");
            Console.WriteLine($"Is 0 default? {0.IsDefault()}");

            // swap - extension method
            int a = 1;
            int b = 2;
            a.Swap<int>(ref b); // specify type
            Console.WriteLine($"a: {a},b: {b}");
            a.Swap(ref b); // type inferred by argument
            // a and b should be back to original values
            Console.WriteLine($"a: {a},b: {b}");

            // swap strings
            string one = "one";
            string two = "two";
            // one.Swap(ref two); // Error - not a struct


            // static data access - not recommended typically
            IEnumerable<Customer> customers = DataAccess<Customer>.List();

        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }




}
