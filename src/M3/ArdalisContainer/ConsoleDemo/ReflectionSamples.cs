using System;
using System.Linq;
using System.Reflection;

namespace ArdalisContainer.ConsoleApp
{
    public class ReflectionSamples
    {
        public static void ListGenericMethods(Type type)
        {
            var methods = type.GetMethods(BindingFlags.Public |
    BindingFlags.Instance | BindingFlags.Static)
                .Where(method => method.DeclaringType.Name != "Object");
            Console.WriteLine($"Methods of type {type}:");
            Console.WriteLine("Name        |IsGeneric   |IsGenDefin  |ContainsGenParams");
            int colWidth = 12;
            foreach (var method in methods)
            {
                int maxNameLength = Math.Min(method.Name.Length, colWidth);
                Console.Write(method.Name.Substring(0, maxNameLength).PadRight(colWidth));
                Console.Write("|");
                Console.Write(method.IsGenericMethod.ToString().PadRight(colWidth));
                Console.Write("|");
                Console.Write(method.IsGenericMethodDefinition.ToString().PadRight(colWidth));
                Console.Write("|");
                Console.WriteLine(method.ContainsGenericParameters.ToString().PadRight(colWidth));
                

                if (method.IsGenericMethod)
                {
                    Console.WriteLine("Calling generic method:");
                    MethodInfo genericMethod = method.MakeGenericMethod(typeof(Customer));

                    object instance = null; // use null for static methods
                    if(!genericMethod.IsStatic)
                    {
                        instance = Activator.CreateInstance(type);
                    }
                    genericMethod.Invoke(instance, new[] { new Customer("Steve","Smith") });
                }
            }
            Console.WriteLine();
        }

        public static void Execute()
        {
            var openProcessorInterface = typeof(IProcessor<>);
            Console.WriteLine($"Is IProcessor<> Generic? {openProcessorInterface.IsGenericType}");
            ListGenericMethods(openProcessorInterface);

            var closedProcessorInterface = typeof(IProcessor<Customer>);
            Console.WriteLine($"Is IProcessor<Customer> Generic? {closedProcessorInterface.IsGenericType}");
            ListGenericMethods(closedProcessorInterface);

            var openProcessorClass = typeof(Processor<>);
            Console.WriteLine($"Is Processor<> Generic? {openProcessorClass.IsGenericType}");
            ListGenericMethods(openProcessorClass);

            var closedProcessorClass = typeof(Processor<Customer>);
            Console.WriteLine($"Is Processor<Customer> Generic? {closedProcessorClass.IsGenericType}");
            ListGenericMethods(closedProcessorClass);

            var cpType = typeof(CustomerProcessor);
            Console.WriteLine($"Is CustomerProcessor Generic? {cpType.IsGenericType}");
            ListGenericMethods(cpType);

            Console.WriteLine();
            Console.ReadLine();
        }

        public interface IProcessor<T>
        {
            void Process(T input);
        }

        public interface ILogger
        {
            void Log<T>(T input);
        }

        public record Customer(string firstName, string lastName);
        public class Processor<T> : IProcessor<T>
        {
            public void Process(T input)
            {
                Console.WriteLine($"Generic Processor of T, processing {input}");
            }

        }

        public class CustomerProcessor : IProcessor<Customer>, ILogger
        {
            public void Log<T>(T input)
            {
                Console.WriteLine($"Log Generic Type: {input}");
            }

            public void Process(Customer input)
            {
                Console.WriteLine($"Process Customer: {input}");
            }

            // trying to invoke this on a generic class is more difficult
            // in general avoid having class<A> containing static method<B>
            public static void ExpediteProcess<TExpedited>(TExpedited input)
            {
                Console.WriteLine($">>> EXPEDITE! {input}");
            }
        }
    }
}

