using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Harry.Transmission
{
    public class DataReceivedEventArgs : EventArgs
    {
        public DataReceivedEventArgs(ICommTunnel commTunnel, byte[] data)
        {
            this.CommTunnel = commTunnel;
            this.Data = data;
        }
        public ICommTunnel CommTunnel { get; private set; }

        public Byte[] Data { get; private set; }
    }
}
