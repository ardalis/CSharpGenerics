using System;
using System.Reflection;

namespace ArdalisContainer.ConsoleApp
{
    public class ReflectionSamples
    {
        public static void ListGenericMethods(Type type)
        {
            var methods = type.GetMethods(BindingFlags.Public |
    BindingFlags.Instance);
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
                    // later
                    Console.WriteLine("Calling generic method:");
                    MethodInfo genericMethod = method.MakeGenericMethod(typeof(Customer));
                    genericMethod.Invoke(Activator.CreateInstance(type), new[] { new Customer() });
                }
            }
            Console.WriteLine();
        }

        public static void Execute()
        {
            var processorType = typeof(Processor<>);
            Console.WriteLine($"Is Processor<> Generic? {processorType.IsGenericType}");
            ListGenericMethods(processorType);

            var cpType = typeof(CustomerProcessor);
            Console.WriteLine($"Is CustomerProcessor Generic? {cpType.IsGenericType}");
            ListGenericMethods(cpType);

            Console.WriteLine();
            Console.ReadLine();
        }

        public interface IProcess<T>
        {
            void Process(T input);
        }

        public interface ILogger
        {
            void Log<T>(T input);
        }

        public class Customer { }
        public class Processor<T> : IProcess<T>
        {
            public void Process(T input)
            {
                Console.WriteLine($"Generic Processor of T, processing {input}");
            }
        }

        public class CustomerProcessor : IProcess<Customer>, ILogger
        {
            public void Log<T>(T input)
            {
                Console.WriteLine($"Log Generic Type: {input}");
            }

            public void Process(Customer input)
            {
                Console.WriteLine($"Process Customer: {input}");
            }
        }
    }
}

