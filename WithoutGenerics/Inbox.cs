using System;

namespace WithoutGenerics
{
    public class Inbox
    {
        public void ListContents(Stack stack)
        {
            Memo memo = (Memo)stack.Pop();
            while (memo != null)
            {
                Print(memo.Contents);
                memo = (Memo)stack.Pop();
            }
        }

        private void Print(string input)
        {
            // print the input
        }
    }
}
