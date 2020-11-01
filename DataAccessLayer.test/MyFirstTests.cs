using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataAccessLayer.test
{
    public class MyFirstTests : IDisposable
    {
        private int _counter;
        private string _phrase;

        public MyFirstTests()
        {
            _counter = 0;
            _phrase = "Hello";
        }

        [Fact]
        public void CountShoulEqualZero()
        {
            Assert.Equal(0, _counter);
            Assert.Equal("Hello", _phrase);
        }

        [Theory]
        [InlineData(4, "GoodBye")]
        public void TestTheory(int number, string saying)
        {
            Assert.Equal(0, _counter);
            number += _counter;
            Assert.Equal(number, _counter);
            Assert.Equal(saying,_phrase);
        }

        public void Dispose()
        {
            //Dispose Objects
        }
    }
}
