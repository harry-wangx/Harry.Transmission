using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Harry.Transmission
{
    public interface ICommTunnel : IDisposable
    {
        event EventHandler<DataReceivedEventArgs> DataReceived;

        IDataCollector<byte> Collector { get; }

        void Send(byte[] data);

        void Open();

        void Close();
        
    }
}
