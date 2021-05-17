namespace WithoutGenerics
{
    public class MemoStack
    {
        private MemoStackItem _top = null;

        public void Push(Memo memo)
        {
            var item = new MemoStackItem(_top, memo);
            _top = item;
        }

        public Memo Pop()
        {
            if (_top is null) return null;
            var val = _top.Value;
            _top = _top.NextItem;
            return val;
        }

        private class MemoStackItem
        {
            public MemoStackItem NextItem { get; private set; }
            public Memo Value { get; private set; }

            public MemoStackItem(MemoStackItem nextItem, Memo value)
            {
                NextItem = nextItem;
                Value = value;
            }
        }
    }
}
