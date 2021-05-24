using System;
using System.Collections.Generic;
using System.Linq;

namespace ArdalisContainer.ConsoleApp
{
    public class Container
    {
        Dictionary<Type, Type> _registry = new();

        public void Register(Type type)
        {
            _registry.Add(type, type);
        }

        public object Resolve(Type type)
        {
            if(!_registry.ContainsKey(type))
            {
                throw new TypeResolutionException($"Request for type {type} received but not instance of that type has been registered.");
            }


            var typeToResolve = _registry[type];

            var constructorInfo = typeToResolve.GetConstructors()
                .First();
            var parameters = constructorInfo.GetParameters()
                .Select(p => Resolve(p.ParameterType))
                .ToArray();

            return Activator.CreateInstance(typeToResolve, parameters);
        }

        public void Register(Type requestedType, Type returnedType)
        {
            _registry.Add(requestedType, returnedType);

        }
    }

    public class TypeResolutionException : Exception
    {
        public TypeResolutionException(string message) : base(message)
        {
        }
    }
}
