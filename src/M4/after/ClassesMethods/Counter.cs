namespace Counter
{
    // these examples are not threadsafe
    public class BaseClass
    {
        public static int GlobalCounter { get; set; }
        public int LocalCounter { get; set; }
    }
    public class Generic<T> : BaseClass
    {
        public static int Counter { get; set; }
        public int AnotherCounter
        {
            get { return BaseClass.GlobalCounter; }
            set { BaseClass.GlobalCounter = value; }
        }
    }
}