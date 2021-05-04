using Xunit;

namespace WithoutGenerics
{
    public class Stack_Pop
    {
        [Fact]
        public void ReturnsNullWhenStackEmpty()
        {
            var stack = new Stack();

            var result = stack.Pop();

            Assert.Null(result);
        }
    }
}
