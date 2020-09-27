using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace Harry.Transmission.Test
{
    public class DataConsumerTest
    {
        [Fact]
        public void TryConsume()
        {
            DataList<byte> data = new DataList<byte>();
            data.AddRange(new byte[] { 0xAA, 0xAB, 0xAA, 0xAB, 0xAC, 0x01, 0xAA, 0xAB, 0xAA, 0xAB, 0xAC, 0x01, 0xAA });

            DataConsumerDemo consumer = new DataConsumerDemo();

            var result = consumer.TryConsume(data, out IList<IndexInfo> indexes);

            Assert.True(result);

            Assert.NotNull(consumer.Frames);
            Assert.NotNull(indexes);

            Assert.Equal(2, consumer.Frames.Count);
            Assert.Equal(2, indexes.Count);

            Assert.Equal(0xAA, consumer.Frames[0][0]);
            Assert.Equal(0xAB, consumer.Frames[0][1]);
            Assert.Equal(0xAC, consumer.Frames[0][2]);
            Assert.Equal(0x01, consumer.Frames[0][3]);

            Assert.Equal(0xAA, consumer.Frames[1][0]);
            Assert.Equal(0xAB, consumer.Frames[1][1]);
            Assert.Equal(0xAC, consumer.Frames[1][2]);
            Assert.Equal(0x01, consumer.Frames[1][3]);

            Assert.Equal(2, indexes[0].Index);
            Assert.Equal(DataConsumerDemo.FrameLength, indexes[0].Length);

            Assert.Equal(8, indexes[1].Index);
            Assert.Equal(DataConsumerDemo.FrameLength, indexes[1].Length);
        }
    }
}
