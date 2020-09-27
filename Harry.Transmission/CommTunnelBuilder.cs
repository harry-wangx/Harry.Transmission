using System;
using System.Collections.Generic;

namespace Harry.Transmission
{
    public class CommTunnelBuilder : ICommTunnelBuilder
    {
        public int CollectorMaxCount { get; set; } = 1024;

        public ICommTunnelSource Source { get; set; }

        public IList<IDataConsumer<byte>> Consumers { get; } = new List<IDataConsumer<byte>>();

        public Func<ICommTunnelBuilder, IDataCollector<byte>> CreateDataCollector { get; set; } = builder =>
            new DataCollector<byte>(builder.Consumers, builder.CollectorMaxCount);


        public ICommTunnel Build()
        {
            if (Source == null) throw new Exception("请先设置Source");

            return this.Source.Build(this);
        }
    }
}
