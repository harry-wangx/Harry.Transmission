using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Harry.Transmission.Socket
{
    public class TcpListenerCommTunnel
    {
        TcpListener tcpListener=new TcpListener (IPAddress.Parse("127.0.0.1"), 123);

        public void Send(byte[] data)
        { 
            
        }
    }
}
