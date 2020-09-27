using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Transmission
{
    public interface ICommTunnelSource
    {
        ICommTunnel Build(ICommTunnelBuilder builder);
    }
}
