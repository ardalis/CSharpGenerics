using System;

namespace ArdalisContainer.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reflecting on Generic Types and Methods");

            ReflectionSamples.Execute();

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
            handler = Method4();
            //requestHandler = Method3(); // Cannot convert H to RH
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

    public interface IHandler { }
    public class Handler : IHandler { }

    public class RequestHandler : Handler { }

    public interface IEndpoint<TRequest, TResponse>
    {
        TResponse Handle(TRequest request);
    }

    public interface IHandler<TCommand>
    {
        void Handle(TCommand command);
    }
    public class Handler<TCommand> : IHandler<TCommand>
    {
        public void Handle(TCommand command)
        {
        }
    }
    public class BaseCommand { }
    public class SpecialCommand : BaseCommand { }
}

