using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassesMethods
{
    // extension methods
    public static class GenericExtensions
    {
        // confirm a sequence is not empty
        public static bool WhereNotEmpty<T>(this IEnumerable<T> source)
        {
            return source.Any(); // LINQ is basically a collection of these kinds of methods
        }

        // see if any instance is its default
        public static bool IsDefault<T>(this T input) where T : IEquatable<T>
        {
            return input.Equals(default(T));
        }

    }

}