using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Harry.Transmission.Test
{
    public class DataCollectorTest
    {
        [Fact]
        public void TryConsume()
        {
            var consumer = new DataConsumerDemo();
            DataCollector<byte> collector = new DataCollector<byte>(new IDataConsumer<byte>[] { consumer }, 20);

            collector.AddRange(new byte[] { 0xAA, 0xAB, 0xAA, 0xAB, 0xAC, 0x01, 0xAA, 0xAB, 0xAA, 0xAB, 0xAC, 0x01, 0xAA });


            Assert.NotNull(consumer.Frames);

            Assert.Equal(2, consumer.Frames.Count);

            Assert.Equal(0xAA, consumer.Frames[0][0]);
            Assert.Equal(0xAB, consumer.Frames[0][1]);
            Assert.Equal(0xAC, consumer.Frames[0][2]);
            Assert.Equal(0x01, consumer.Frames[0][3]);

            Assert.Equal(0xAA, consumer.Frames[1][0]);
            Assert.Equal(0xAB, consumer.Frames[1][1]);
            Assert.Equal(0xAC, consumer.Frames[1][2]);
            Assert.Equal(0x01, consumer.Frames[1][3]);

            Assert.Equal(1, collector.Count);

        }
    }
}
