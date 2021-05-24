using ArdalisContainer.ConsoleApp;
using System;
using Xunit;

namespace ArdalisContainer.UnitTests
{
    public class ContainerResolve
    {
        Container _container = new();

        [Fact]
        public void ReturnsSimpleTypeOnceRegistered()
        {
            _container.Register(typeof(ContainerResolve));

            var instance = _container.Resolve(typeof(ContainerResolve));

            Assert.Equal(typeof(ContainerResolve), instance.GetType());
        }

        [Fact]
        public void ReturnsSimpleTypeByInterfaceOnceRegistered()
        {
            _container.Register(typeof(IWriter), typeof(ConsoleWriter));

            var instance = _container.Resolve(typeof(IWriter));

            Assert.Equal(typeof(ConsoleWriter), instance.GetType());
        }

        [Fact]
        public void ThrowsGivenRequestToResolveMissingType()
        {
            Action action = () => _container.Resolve(typeof(IWriter));

            var ex = Assert.Throws<TypeResolutionException>(action);

            Assert.Contains((typeof(IWriter)).ToString(), ex.Message);
        }

        [Fact]
        public void ReturnsSimpleTypeThatRequiresAnotherTypeByInterfaceOnceRegistered()
        {
            _container.Register(typeof(IWriter), typeof(ConsoleWriter));
            _container.Register(typeof(Report));

            var instance = _container.Resolve(typeof(Report));

            Assert.Equal(typeof(Report), instance.GetType());
        }

    }

    public interface IWriter
    {
        void WriteLine(string input);
    }

    public class ConsoleWriter : IWriter
    {
        public void WriteLine(string input)
        {
            Console.WriteLine(input);
        }
    }

    public class Report
    {
        private readonly IWriter _writer;

        public Report(IWriter writer)
        {
            _writer = writer;
        }
    }
}
