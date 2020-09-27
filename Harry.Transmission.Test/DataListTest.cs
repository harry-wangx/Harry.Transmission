using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace Harry.Transmission.Test
{
    public class DataListTest
    {
        protected readonly ITestOutputHelper Output;

        public DataListTest(ITestOutputHelper tempOutput)
        {
            Output = tempOutput;
        }

        [Fact]
        public void AddRange()
        {
            DataList<byte> data = new DataList<byte>();
            data.AddRange(new byte[] { 0xAA, 0xAA, 0xAA, 0xAB });

            Assert.Equal(0xAA, data[0]);
            Assert.Equal(0xAA, data[1]);
            Assert.Equal(0xAA, data[2]);
            Assert.Equal(0xAB, data[3]);
        }

        [Fact]
        public void RemoveRange()
        {
            DataList<byte> data = new DataList<byte>();
            data.AddRange(new byte[] { 0xAA, 0xAB, 0xAC, 0xAD });

            data.RemoveRange(1, 2);

            Assert.Equal(0xAA, data[0]);
            Assert.Equal(0xAD, data[1]);

        }

        [Fact]
        public void ForEach()
        {
            DataList<byte> data = new DataList<byte>();
            data.AddRange(new byte[] { 0xAA, 0xAB, 0xAC, 0xAD });

            var index = 0;
            foreach (var item in data)
            {
                Assert.True(item == data[index++]);
            }

        }
    }
}
