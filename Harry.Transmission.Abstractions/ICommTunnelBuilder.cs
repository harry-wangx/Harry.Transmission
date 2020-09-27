using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Transmission
{
    public interface ICommTunnelBuilder
    {
        int CollectorMaxCount { get; set; }

        ICommTunnelSource Source { get; set; }

        IList<IDataConsumer<byte>> Consumers { get; }

        Func<ICommTunnelBuilder, IDataCollector<byte>> CreateDataCollector { get; set; }

        ICommTunnel Build();
    }
}
