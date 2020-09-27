using System;
using System.Collections.Generic;

namespace Harry.Transmission
{
    public interface IDataList<T>
#if NET40
        : IList<T>
#else
        : IReadOnlyList<T>
#endif

    {
        void AddRange(IEnumerable<T> collection);

        void RemoveRange(int index, int count);
    }
}
