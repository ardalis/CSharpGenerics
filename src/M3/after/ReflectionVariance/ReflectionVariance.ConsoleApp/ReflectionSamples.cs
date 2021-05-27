using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReflectionVariance.ConsoleApp
{
    internal class ReflectionSamples
    {
        internal static void Execute()
        {
            var types = new List<Type>
            {
                // typeof(IProcessor<>),
                // typeof(IProcessor<Customer>),
                // typeof(Processor<>),
                // typeof(Processor<Customer>),
                // typeof(CustomerProcessor)
                typeof(IPipeline<,>)
            };
            ListTypeDetails(types);

            // methods
            // var methods = typeof(IProcessor<>).GetMethods();

            // Create a Pipeline Instance
            Type[] typeArguments = { typeof(Request), typeof(Response) };

            var specificType = typeof(Pipeline<,>).MakeGenericType(typeArguments);

            var createdInstance = Activator.CreateInstance(specificType);

            ListTypeDetails(new List<Type> { createdInstance.GetType() });

            ((dynamic)createdInstance).DoWork(new Request());
        }

        private static void ListTypeDetails(List<Type> types)
        {
            Console.WriteLine("Type Name".PadRight(20) + "|" + "IsGenericType?".PadRight(20) + "|" +
                "IsGenericDefinition?".PadRight(20) + "|" + "Generic Arguments");
            foreach (var type in types)
            {
                string output = type.Name.PadRight(20) + "|";
                output += type.IsGenericType.ToString().PadRight(20) + "|";
                output += type.IsGenericTypeDefinition.ToString().PadRight(20) + "|";
                output += type.GetGenericArguments().Count();
                Console.WriteLine(output);
                ListParameterDetails(type);
                // ListGenericMethods(type);
                // Console.WriteLine(output);
            }
        }

        private static void ListParameterDetails(Type type)
        {
            var parameters = type.GetGenericArguments();

            foreach (var parameter in parameters)
            {
                if (parameter.IsGenericParameter)
                {
                    DisplayGenericParameter(parameter);
                }
                else
                {
                    DisplayTypeArgument(parameter);
                }
            }
        }

        private static void DisplayGenericParameter(Type parameter)
        {
            var constraints = parameter.GetGenericParameterConstraints();

            Console.WriteLine($"  Type parameter (position, name, constraints, attributeMask): {parameter.GenericParameterPosition}, {parameter.Name}, {constraints.Count()}, {parameter.GenericParameterAttributes}");

            if (constraints.Any())
            {
                Console.WriteLine("    Constraint Name |Interface? |Class? |Enum? ");
                foreach (var constraint in constraints)
                {
                    Console.WriteLine("    " + constraint.Name.PadRight(16) + "|" +
                        constraint.IsInterface.ToString().PadRight(11) + "|" +
                        constraint.IsClass.ToString().PadRight(7) + "|" +
                        constraint.IsEnum.ToString().PadRight(6)
                        );
                }

            }
        }

        private static void DisplayTypeArgument(Type parameter)
        {
            Console.WriteLine($"  Type argument: {parameter.Name}");
        }

        private static void ListGenericMethods(Type type)
        {
            var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                                .Where(method => method.DeclaringType.Name != "Object");
            Console.WriteLine($"Methods of type {type.Name}:");
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
                    Console.WriteLine("Executing generic method...");
                    var genParams = method.GetGenericArguments();
                    foreach (var genParam in genParams)
                    {
                        if (genParam.IsGenericParameter)
                        {
                            Console.WriteLine($"Generic Param: {genParam.GenericParameterPosition} {genParam.Name} ");
                        }
                    }
                    MethodInfo genericMethod = method.MakeGenericMethod(typeof(Customer));
                    object instance = null;
                    if (!genericMethod.IsStatic)
                    {
                        instance = Activator.CreateInstance(type);
                    }
                    genericMethod.Invoke(instance, new[] { new Customer("Steve", "Smith") });
                }
            }
            Console.WriteLine();
        }
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

    public abstract class BaseRequest { }
    public interface IPipeline<TInput, TOutput>
        where TInput : BaseRequest
        where TOutput : IDisposable, new()
    {
        TOutput DoWork(TInput request);
    }

    public class Pipeline<TInput, TOutput> : IPipeline<TInput, TOutput>
            where TInput : BaseRequest
            where TOutput : IDisposable, new()
    {
        public TOutput DoWork(TInput request)
        {
            var response = new TOutput();
            Console.WriteLine($"Got request : {request}; returning response: {response}");
            return response;
        }
    }


    public class Request : BaseRequest { }
    public class Response : IDisposable
    {
        public void Dispose()
        {
        }
    }


}