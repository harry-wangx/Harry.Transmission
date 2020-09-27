using System;
using System.IO.Ports;

namespace Harry.Transmission.SerialPort
{
    public class SerialPortCommTunnelSource : ICommTunnelSource
    {
        public string PortName { get; set; }

        public int BaudRate { get; set; }

        public Parity Parity { get; set; }

        public int DataBits { get; set; }

        public StopBits StopBits { get; set; }

        public Action<System.IO.Ports.SerialPort> ConfigAction { get; set; }


        public ICommTunnel Build(ICommTunnelBuilder builder)
        {
            var collector = builder?.CreateDataCollector?.Invoke(builder);
            if (collector == null) throw new Exception("创建串口通道失败.必须的collector不能为null.");
            return new SerialPortCommTunnel(this, collector);
        }

    }
}
