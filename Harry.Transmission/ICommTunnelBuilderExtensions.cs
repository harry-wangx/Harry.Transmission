using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Harry.Transmission
{
    public static class ICommTunnelBuilderExtensions
    {
        /// <summary>
        /// 添加消费者
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="consumer"></param>
        /// <returns></returns>
        public static ICommTunnelBuilder AddConsumer(this ICommTunnelBuilder builder, IDataConsumer<byte> consumer)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (consumer == null) throw new ArgumentNullException(nameof(consumer));

            builder.Consumers.Add(consumer);
            return builder;
        }
    }
}
