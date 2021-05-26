using System;

namespace ReflectionVariance.ConsoleApp
{
    internal class VarianceSamples
    {
        public static void Execute()
        {
            Console.WriteLine("Variance in C# types and generic types:");

            var handler = new Handler();
            var requestHandler = new RequestHandler();
            Console.WriteLine($"handler instance is IHandler: {handler is IHandler}");
            Console.WriteLine($"handler instance is Object: {handler is Object}");
            Console.WriteLine($"handler instance is Handler: {handler is Handler}");
            Console.WriteLine($"handler instance is RequestHandler: {handler is RequestHandler}");
            Console.WriteLine();
            Console.WriteLine($"requestHandler instance is IHandler: {requestHandler is IHandler}");
            Console.WriteLine($"requestHandler instance is Object: {requestHandler is Object}");
            Console.WriteLine($"requestHandler instance is Handler: {requestHandler is Handler}");
            Console.WriteLine($"requestHandler instance is RequestHandler: {requestHandler is RequestHandler}");

            Console.WriteLine();

            // assignment to less specific type
            Handler handler2 = handler;
            handler2 = requestHandler;

            // assignment to more specific type
            RequestHandler requestHandler2 = requestHandler;
            //requestHandler2 = handler; // compile error - cannot implicitly convert
            //requestHandler2 = (RequestHandler)handler; // runtime error - cannot cast


            // Method1
            Method1(handler);
            Console.WriteLine("Can we pass handler to Method1? Yes.");

            Method1(requestHandler);
            Console.WriteLine("Can we pass requestHandler to Method1? Yes.");

            // Method2
            //Method2(handler); // Cannot convert
            Console.WriteLine("Can we pass handler to Method2? NO.");

            Method2(requestHandler);
            Console.WriteLine("Can we pass requestHandler to Method2? Yes.");

            // Method3
            handler = Method3();
            //requestHandler = Method3(); // Cannot convert H to RH
            handler = Method4();
            requestHandler = Method4();

            // Generic interfaces
            var h1 = new Handler<BaseCommand>();
            var h2 = new Handler<SpecialCommand>();

            ProcessBase(h1);
            //ProcessBase(h2); // cannot convert
            //ProcessSpecial(h1); // cannot convert
            ProcessSpecial(h2);
        }

        public static void Method1(Handler handler)
        {
        }
        public static void Method2(RequestHandler handler)
        {
        }
        public static Handler Method3() { return null; }
        public static RequestHandler Method4() { return null; }
        public static void ProcessBase(IHandler<BaseCommand> handler) { }
        public static void ProcessSpecial(IHandler<SpecialCommand> handler) { }

    }
}