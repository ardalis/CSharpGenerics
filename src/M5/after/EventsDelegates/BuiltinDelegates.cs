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
        private List<T> _args = new();

        public NewMath(IEnumerable<T> args, Operation<T> addFunction, Predicate<T> predicate)
        {
            Add += addFunction;
            _predicate = predicate;
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
            var instance = new NewMath<int>(input, (a,b) => a+b, Big);

            Console.WriteLine($"NewMath sum of filtered inputs is {instance.Sum()}");
        }
    }
}