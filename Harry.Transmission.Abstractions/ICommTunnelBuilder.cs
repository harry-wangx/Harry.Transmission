using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Transmission
{
    public interface ICommTunnelBuilder
    {

        IServiceProvider Services { get; set; }

        ICommTunnelBuilder Use(Func<CommDelegate, CommDelegate> middleware);

        ICommTunnel Build();


        int CollectorMaxCount { get; set; }

        ICommTunnelSource Source { get; set; }

        IList<IDataConsumer<byte>> Consumers { get; }

        Func<ICommTunnelBuilder, IDataCollector<byte>> CreateDataCollector { get; set; }

        
    }
}
