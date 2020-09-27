using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Harry.Transmission.Test
{
    public class DataConsumerDemo : DataConsumerBase<Byte>
    {
        public const int FrameLength = 4;

        public DataConsumerDemo()
        {
            FrameTitle = new byte[] { 0xAA, 0xAB };
        }

        public List<byte[]> Frames { get; private set; }

        protected override bool TryParse(IReadOnlyList<byte> dataList, int index, out byte[] frame)
        {
            frame = null;

            if (dataList == null || dataList.Count <= 0 || (index + FrameLength) > dataList.Count)
                return false;

            if (!CheckFrameTitle(dataList, index))
                return false;

            //校验
            if (GetSum(dataList, index, FrameLength - 1) != dataList[index + FrameLength - 1])
            {
                return false;
            }

            ////检查帧尾
            //for (int i = 1; i <= frameTail.Length; i++)
            //{
            //    if (frameTail[frameTail.Length - i] != data[index + length - i]) return false;
            //}

            //拷贝数据到临时数组
            frame = dataList.Skip(index).Take(FrameLength).ToArray();

            return true;

        }

        protected override void OnConsume(List<byte[]> frames)
        {
            Frames = frames;
            if (frames != null && frames.Count > 0)
            {
                foreach (var data in frames)
                {
                    Console.WriteLine("frame:" + string.Join(",", data));
                }
            }
        }

        /// <summary>
        /// 和校验
        /// </summary>
        public static byte GetSum(IReadOnlyList<byte> data, int beginIndex, int length)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data), "参加校验的数据不能为null");
            if (beginIndex + length > data.Count)
                throw new ArgumentOutOfRangeException(nameof(data), $"参加校验的数据长度不足 data length:{data.Count} beginIndex:{beginIndex} length:{length}");

            int sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += data[beginIndex + i];
            }
            return (byte)sum;
        }
    }
}
