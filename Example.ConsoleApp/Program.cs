using System;
using Harry.Transmission;
using Harry.Transmission.Test;

namespace Example.ConsoleApp
{
    class Program
    {
        static ICommTunnel tunnel;
        static void Main(string[] args)
        {
            CommTunnelBuilder builder = new CommTunnelBuilder();
            builder.UseSerialPort();
            builder.Consumers.Add(new DataConsumerDemo());
            tunnel = builder.Build();
            tunnel.DataReceived += Tunnel_DataReceived;

            tunnel.Open();

            Console.WriteLine("开始监听");
            Console.ReadLine();
            tunnel.Close();
            tunnel.Dispose();

        }

        private static void Tunnel_DataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine($"缓存数量:{tunnel.Collector.Count} 接收到的数据:{string.Join(",", e.Data)}");
        }
    }
}
