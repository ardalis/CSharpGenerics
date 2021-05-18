namespace WithoutGenerics
{
    public static class Program
    {
        public static void Main()
        {
            var stack = new Stack();

            stack.Push(new Memo() { Contents = "one"});
            stack.Push(new Memo() { Contents = "two"});
            stack.Push(new Memo() { Contents = "three"});

            var inbox = new Inbox();

            inbox.ListContents(stack); // no type safety here - expects stack of Memos

        }
    }
}
