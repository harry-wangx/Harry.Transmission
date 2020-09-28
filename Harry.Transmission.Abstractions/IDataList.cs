using System;
using System.Collections.Generic;

namespace Harry.Transmission
{
    public interface IDataList<T> : IReadOnlyList<T>
    {
        void AddRange(IEnumerable<T> collection);

        void RemoveRange(int index, int count);
    }
}
