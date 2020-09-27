using System;
using System.Collections.Generic;
using System.Linq;

namespace Harry.Transmission
{
    public class DataCollector<T> : IDataCollector<T>
    {
        private readonly object locker = new object();
        const int MAXLENGTH = 100_000;

        //消费者集合
        private readonly List<IDataConsumer<T>> consumerList = new List<IDataConsumer<T>>();

        //缓冲区
        private readonly DataList<T> buffer;

        //最大保留数据长度
        private int maxLength;

        private volatile bool disposed = false;


        public DataCollector(IEnumerable<IDataConsumer<T>> consumers, int maxLength)
        {
            if (maxLength < 1 || maxLength > MAXLENGTH) throw new ArgumentOutOfRangeException(nameof(maxLength), $"值只能在1-{MAXLENGTH}之间");

            this.maxLength = maxLength;

            if (consumers != null)
            {
                consumerList.AddRange(consumers);
            }
            buffer = new DataList<T>((int)(maxLength * 1.2));
        }

        public DataCollector(IEnumerable<IDataConsumer<T>> consumers) : this(consumers, 1024)
        {

        }


        public void AddRange(IEnumerable<T> collection)
        {
            if (CheckDisposed()) return;

            if (collection == null || collection.Count() <= 0) return;

            lock (locker)
            {
                if (CheckDisposed()) return;

                buffer.AddRange(collection);
            }

            TryConsume();
        }

        public int Count
        {
            get
            {
                lock (locker)
                {
                    return buffer.Count;
                }
            }
        }


        public void TryConsume()
        {
            if (CheckDisposed()) return;
            lock (locker)
            {
                if (CheckDisposed()) return;

                //需要保留的数量(从后数)
                int count = int.MaxValue;
                List<IndexInfo> indexesList = new List<IndexInfo>();
                foreach (var consumer in consumerList)
                {
                    if (consumer.TryConsume(buffer, out IList<IndexInfo> indexes))
                    {
                        var orderIndexes = indexes.OrderByDescending(m => m.Index);

                        int i = 0;
                        foreach (var item in orderIndexes)
                        {
                            if (i == 0)
                            {
                                //获得需要保留的数据长度
                                count = Math.Min(count, buffer.Count - (item.Index + item.Length));
                            }
                            i++;
                            //从后向前移除已经消费的数据
                            buffer.RemoveRange(item.Index, item.Length);
                        }
                    }
                }

                if (count < int.MaxValue)
                {
                    //清空前面的数据
                    buffer.RemoveRange(0, buffer.Count - count);
                }

                //检测数据量是否超范围
                if (buffer.Count > maxLength)
                {
                    //超范围,从前面删掉多余的
                    buffer.RemoveRange(0, buffer.Count - maxLength);
                }
            }
        }

        public void Dispose()
        {
            if (CheckDisposed()) return;
            disposed = true;
            //好像也没啥可适放的
        }

        protected virtual bool CheckDisposed()
        {
            return disposed;
        }
    }

}
