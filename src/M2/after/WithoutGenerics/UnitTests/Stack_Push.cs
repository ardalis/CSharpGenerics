using Xunit;

namespace WithoutGenerics
{
    public class Stack_Push
    {
        [Fact]
        public void AddsItemToTopOfStack()
        {
            var stack = new Stack();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);

            var item = (int)stack.Pop();

            Assert.Equal(3, item);
        }
    }
}
