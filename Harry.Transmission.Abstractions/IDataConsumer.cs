using System;
using System.Collections.Generic;

namespace Harry.Transmission
{
    public interface IDataConsumer<T>
    {
        /// <summary>
        /// 尝试消费
        /// </summary>
        /// <param name="dataList">数据源</param>
        /// <param name="indexes">已消费数据的索引信息</param>
        /// <returns>消费成功返回True,否则返回False</returns>

        bool TryConsume(IReadOnlyList<T> dataList, out IList<IndexInfo> indexes
        );
    }
}
