using System;
using System.Collections.Generic;

// avoid adding delegates to the global namespace
namespace BuiltinDelegates
{
    // define a delegate type which gives a name (Operation)
    // to a method signature: T MethodName(T op1, T op2)
    public delegate T Operation<T>(T op1, T op2);

    public class NewMath<T> where T : struct
    {
        // define an instance of the delegate (defaulting to null)
        public Operation<T> Add;

        private readonly Predicate<T> _predicate;
        private readonly Action<T> _outputIgnoredItems;
        private List<T> _args = new();

        public NewMath(IEnumerable<T> args, Operation<T> addFunction, Predicate<T> predicate, Action<T> outputIgnoredItems = null)
        {
            Add += addFunction;
            _predicate = predicate;
            _outputIgnoredItems = outputIgnoredItems;
            _args.AddRange(args);
        }

        public T Sum()
        {
            T total = default(T);
            foreach (var arg in _args)
            {
                if (_predicate(arg))
                {
                    total = Add(total, arg);
                }
                else
                {
                    _outputIgnoredItems?.Invoke(arg);
                }
            }
            return total;
        }
    }

    public static class DelegateRunner
    {
        public static bool Big(int input)
        {
            return input > 1000;
        }

        public static void Execute()
        {
            var input = new[] { 10, 1200, 20, 2000 };
            // var instance = new NewMath<int>(input, (a, b) => a + b, Big, (i) => Console.WriteLine($"Ignoring {i}."));
            var instance = new NewMath<int>(input, (a, b) => a + b, Big);

            Console.WriteLine($"NewMath filtered sum is  {instance.Sum()}");


        }














        //Predicate<int> predicate = Big;
        //Foo(Big);
        //Foo(predicate);

        public static void Foo(Predicate<int> predicate)
        {
            // how to convert
            // _args.Where(_predicate); ERROR cannot convert from Predicate to Func<int,bool>
            Predicate<int> big = x => x > 1000;
            Func<int, bool> FuncBig = new Func<int, bool>(big);



            // Predicate
            Predicate<int> predicate = i => i > 10;
            var result = intArray.Where(predicate); // ERROR – LINQ doesn’t work with Predicate

            // Func<T, bool>Func<T, bool> filter = i=> I > 10; // semantically equivalent
            result = intArray.Where(filter); // OK

            // conversion – given predicate above
            result = intArray.Where(i => predicate(i));
            result = intArray.Where(predicate);
        }
    }
}