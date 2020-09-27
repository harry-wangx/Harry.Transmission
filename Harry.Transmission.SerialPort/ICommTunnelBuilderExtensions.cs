using Harry.Transmission.SerialPort;
using System;
using System.IO.Ports;

namespace Harry.Transmission
{
    public static class ICommTunnelBuilderExtensions
    {
        /// <summary>
        /// 添加串口通道
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="portName">要使用的端口（例如 COM1）</param>
        /// <param name="baudRate">波特率</param>
        /// <param name="parity"></param>
        /// <param name="dataBits"></param>
        /// <param name="stopBits"></param>
        /// <param name="configAction"></param>
        /// <returns></returns>
        public static ICommTunnelBuilder UseSerialPort(this ICommTunnelBuilder builder,
            string portName,
            int baudRate,
            Parity parity,
            int dataBits,
            StopBits stopBits,
            Action<System.IO.Ports.SerialPort> configAction = null)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.Source = (new SerialPortCommTunnelSource()
            {
                PortName = portName,
                BaudRate = baudRate,
                Parity = parity,
                DataBits = dataBits,
                StopBits = stopBits,
                ConfigAction = configAction
            });
            return builder;
        }

        /// <summary>
        /// 添加串口通道
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="portName">要使用的端口（例如 COM1）</param>
        /// <param name="baudRate">波特率</param>
        /// <param name="parity"></param>
        /// <param name="dataBits"></param>
        /// <param name="configAction"></param>
        /// <returns></returns>
        public static ICommTunnelBuilder UseSerialPort(this ICommTunnelBuilder builder,
            string portName, int baudRate, Parity parity, int dataBits,
            Action<System.IO.Ports.SerialPort> configAction = null)
        {
            return UseSerialPort(builder, portName, baudRate, parity, dataBits, StopBits.One, configAction);
        }

        /// <summary>
        /// 添加串口通道
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="portName">要使用的端口（例如 COM1）</param>
        /// <param name="baudRate">波特率</param>
        /// <param name="parity"></param>
        /// <param name="configAction"></param>
        /// <returns></returns>
        public static ICommTunnelBuilder UseSerialPort(this ICommTunnelBuilder builder,
            string portName, int baudRate, Parity parity,
            Action<System.IO.Ports.SerialPort> configAction = null)
        {
            return UseSerialPort(builder, portName, baudRate, parity, 8, configAction);
        }

        /// <summary>
        /// 添加串口通道
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="portName">要使用的端口（例如 COM1）</param>
        /// <param name="baudRate">波特率</param>
        /// <param name="configAction"></param>
        /// <returns></returns>
        public static ICommTunnelBuilder UseSerialPort(this ICommTunnelBuilder builder,
            string portName, int baudRate,
            Action<System.IO.Ports.SerialPort> configAction = null)
        {
            return UseSerialPort(builder, portName, baudRate, Parity.None, configAction);
        }

        /// <summary>
        /// 添加串口通道
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="portName">要使用的端口（例如 COM1）</param>
        /// <param name="configAction"></param>
        /// <returns></returns>
        public static ICommTunnelBuilder UseSerialPort(this ICommTunnelBuilder builder,
            string portName,
            Action<System.IO.Ports.SerialPort> configAction = null)
        {
            return UseSerialPort(builder, portName, 9600, configAction);
        }

        /// <summary>
        /// 添加串口通道
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configAction"></param>
        /// <returns></returns>
        public static ICommTunnelBuilder UseSerialPort(this ICommTunnelBuilder builder,
            Action<System.IO.Ports.SerialPort> configAction = null)
        {
            return UseSerialPort(builder, "COM1", configAction);
        }
    }
}
