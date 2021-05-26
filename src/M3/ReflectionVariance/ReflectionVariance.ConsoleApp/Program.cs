using System;

namespace ReflectionVariance.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reflection and Variance with @ardalis!");

            ReflectionSamples.Execute();
        }
    }

    public interface IProcessor<T>
    {
        void Process(T input);
    }
    public class Processor<T> : IProcessor<T>
    {
        public void Process(T input)
        {
            Console.WriteLine($"Generic Processor of T, processing {input}");
        }
    }

    public interface ILogger
    {
        void Log<T>(T input);
    }
    public record Customer(string firstName, string lastName);

    public class CustomerProcessor : IProcessor<Customer>, ILogger
    {
        public void Process(Customer input)
        {
            Console.WriteLine($"Processing Customer: {input}");
        }

        public static void ExpediteProcess<TExpedited>(TExpedited input)
        {
            Console.WriteLine($">>> EXPEDITE! {input}");
        }

        public void Log<T>(T input)
        {
            Console.WriteLine($"Logging Generic Type: {input}");
        }
    }
}
