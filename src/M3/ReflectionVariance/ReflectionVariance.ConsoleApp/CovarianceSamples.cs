using System;

namespace ReflectionVariance.ConsoleApp
{
    internal class CovarianceSamples
    {
        internal static void Execute()
        {
            var h1 = new Handler<BaseCommand>();
            var h2 = new Handler<SpecialCommand>();

        }

        public interface ISequence<out T>
        {
            T GetNext();
            // void Insert(T item); // compile error
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
}