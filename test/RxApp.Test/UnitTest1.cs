using System;
using Xunit;
using RxApp.Controllers;

namespace RxApp.Test
{
    public class UnitTest1
    {
        private readonly FallbackController controller = new FallbackController();

        [Fact]
        public void GetReturnsHello()
        {
            var returnValue = controller.GetHello();
            Assert.Equal("Hello from backend", returnValue.Value);
        }

        [Fact]
        public void Test1()
        {

        }
    }
}
