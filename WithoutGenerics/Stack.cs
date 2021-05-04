using System;

namespace WithoutGenerics
{
    public class Stack
    {
        private StackItem _top = null;

        public void Push(object obj)
        {
            var item = new StackItem(_top, obj);
            _top = item;
        }

        public Object Pop()
        {
            if (_top is null) return null;
            var val = _top.Value;
            _top = _top.NextItem;
            return val;
        }

        private class StackItem
        {
            public StackItem NextItem { get; private set; }
            public object Value { get; private set; }

            public StackItem(StackItem nextItem, object value)
            {
                NextItem = nextItem;
                Value = value;
            }
        }
    }
}
