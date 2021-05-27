using System.Collections.Generic;

namespace ClassesMethods
{
    // https://stackoverflow.com/a/2686133/13729
    // better type inference
    public static class Converter
    {
        public static TOut Convert<TIn, TOut>(TIn input) where TOut : class
        {
            return null;
        }
    }

    public static class ConvertTo<TOut> where TOut : class
    {
        public static TOut Convert<TIn>(TIn intput)
        {
            return null;
        }
    }



    // generic static classes
    public abstract class BaseEntity { };
    public class Customer : BaseEntity { }
    public static class DataAccess<T> where T : BaseEntity
    {
        public static IEnumerable<T> List()
        {
            return new List<T>();
        }
    }


}