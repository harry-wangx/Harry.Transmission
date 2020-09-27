using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Harry.Transmission
{
#if NET40
    public class DataList<T> : List<T>
    {
        public DataList() : base()
        {
        }

        public DataList(int capacity) : base(capacity)
        {
        }

        public DataList(IEnumerable<T> collection) : base(collection)
        {

        }
    }
#else
    public class DataList<T> : IDataList<T>
    {
        private List<T> data = null;

        public DataList()
        {
            data = new List<T>();
        }

        public DataList(int capacity)
        {
            data = new List<T>(capacity);
        }

        public DataList(IEnumerable<T> collection)
        {
            data = new List<T>(collection);
        }

        public T this[int index]
        {
            get { return data[index]; }
        }

        public int Count
        {
            get
            {
                return data.Count;
            }
        }

        public void AddRange(IEnumerable<T> collection)
        {
            if (collection == null || collection.Count() <= 0) return;

            data.AddRange(collection);
        }

        public void RemoveRange(int index, int count)
        {
            data.RemoveRange(index, count);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return data.GetEnumerator();
        }
    }
#endif
}
