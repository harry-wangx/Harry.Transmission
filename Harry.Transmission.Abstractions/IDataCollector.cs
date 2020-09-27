using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Harry.Transmission
{
    public interface IDataCollector<T> : IDisposable
    {
        void AddRange(IEnumerable<T> collection);

        int Count { get; }

        void TryConsume();
    }
}
