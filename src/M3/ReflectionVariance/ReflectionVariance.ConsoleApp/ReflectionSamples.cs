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
                typeof(IProcessor<>),
                typeof(IProcessor<Customer>),
                typeof(Processor<>),
                typeof(Processor<Customer>),
                typeof(CustomerProcessor)
            };
            ListTypeDetails(types);

            // methods
            // var methods = typeof(IProcessor<>).GetMethods();

        }

        private static void ListTypeDetails(List<Type> types)
        {
            Console.WriteLine("Type Name".PadRight(20) + "|" + "IsGenericType?".PadRight(20) + "|" +
                "IsGenericDefinition?".PadRight(20));
            foreach (var type in types)
            {
                string output = type.Name.PadRight(20) + "|";
                output += type.IsGenericType.ToString().PadRight(20) + "|";
                output += type.IsGenericTypeDefinition;
                Console.WriteLine(output);
                ListGenericMethods(type);
                Console.WriteLine(output);
            }
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
}