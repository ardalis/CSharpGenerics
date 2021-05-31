using System;
using System.Linq;

// avoid adding delegates to the global namespace
namespace Delegates
{
    // define a delegate type which gives a name (Operation)
    // to a method signature: T MethodName(T op1, T op2)
    public delegate T Operation<T>(T op1, T op2);

    public class NewMath<T> where T : struct
    {
        // define an instance of the delegate (defaulting to null)
        public Operation<T> Add;

        private T _op1;
        private T _op2;

        public NewMath(T op1, T op2, Operation<T> addFunction)
        {
            Add += addFunction;
            Add += addFunction; // note you can wire up more than one function, just like events
            _op1 = op1;
            _op2 = op2; 
        }

        public T Sum()
        {
            return Add(_op1, _op2);
        }
    }
public static class DelegateRunner
{
    public static int MyAdd(int op1, int op2)
    {
        return (new[] { op1, op2 }).Sum();
    }

    public static void Execute()
    {
        var instance = new NewMath<int>(1, 2, MyAdd);
    }

        static int sum = 0;
        public static void Execute2()
        {
            var instance = new NewMath<int>(1, 2, (a,b) => {
                sum += a;
                sum += b;
                return sum;
                });

            Console.WriteLine($"Adding 1 and 2 yields {instance.Sum()}"); // if 2 functions are wired up, the sum will be 6 not 3 because the methods all run

            var another = new NewMath<decimal>(1.5m, 2.2m, (a, b) => a + b);
            Console.WriteLine($"Adding decimal values yields {another.Sum()}");

        }
    }
}