using System;
using System.Linq;

namespace Harry.Transmission.SerialPort
{
    public class SerialPortCommTunnel : ICommTunnel
    {
        private volatile bool _disposed;
        private System.IO.Ports.SerialPort _serialPort;

        public event EventHandler<DataReceivedEventArgs> DataReceived;

        public SerialPortCommTunnel(SerialPortCommTunnelSource source, IDataCollector<byte> collector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            this.Collector = collector ?? throw new ArgumentNullException(nameof(collector));

            _serialPort = new System.IO.Ports.SerialPort(source.PortName, source.BaudRate, source.Parity, source.DataBits, source.StopBits);
            source.ConfigAction?.Invoke(_serialPort);
            _serialPort.DataReceived += SerialPort_DataReceived;
        }

        public IDataCollector<byte> Collector { get; private set; }

        public void Send(byte[] data)
        {
            if (CheckDisposed()) return;

            _serialPort.Write(data, 0, data.Length);
        }

        public void Open()
        {
            if (CheckDisposed()) return;

            if (_serialPort.IsOpen) return;
            _serialPort.Open();
        }

        public void Close()
        {
            if (CheckDisposed()) return;

            if (!_serialPort.IsOpen) return;
            _serialPort.Close();
        }

        protected virtual bool CheckDisposed()
        {
            return _disposed;
        }

        public void Dispose()
        {
            if (CheckDisposed()) return;

            _disposed = true;

            _serialPort.DataReceived -= SerialPort_DataReceived;

            if (_serialPort != null)
            {
                try
                {
                    if (_serialPort.IsOpen)
                    {
                        _serialPort.Close();
                    }
                }
                catch { }

                try
                {
                    _serialPort.Dispose();
                }
                catch { }

                try
                {
                    Collector.Dispose();
                }
                catch { }
            }

        }

        /// <summary>
        /// 接收到数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (CheckDisposed()) return;

            if (_serialPort.BytesToRead <= 0) return;

            var buf = new byte[_serialPort.BytesToRead];
            _serialPort.Read(buf, 0, buf.Length);

            //添加数据到收集器
            Collector.AddRange(buf);

            //如果有注册事件,处理数据副本
            var handler = DataReceived;
            if (handler != null)
                handler.Invoke(_serialPort, new DataReceivedEventArgs(this, buf));
        }
    }
}
