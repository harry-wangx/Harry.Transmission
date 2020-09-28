using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Harry.Transmission
{
    public abstract class DataConsumerBase<T> : IDataConsumer<T>
    {
        #region 属性
        /// <summary>
        /// 帧头
        /// </summary>
        protected T[] FrameTitle { get; set; }
        #endregion


        /// <summary>
        /// 尝试消费
        /// </summary>
        /// <param name="dataList">数据源</param>
        /// <param name="indexes">已消费数据的索引信息</param>
        /// <returns>消费成功返回True,否则返回False</returns>

        public virtual bool TryConsume(IReadOnlyList<T> dataList, out IList<IndexInfo> indexes
        )
        {
            indexes = null;
            if (dataList == null || dataList.Count <= 0)
                return false;

            var tmpIndexes = new List<IndexInfo>();

            List<T[]> frames = new List<T[]>();
            int index = 0;
            while (index < dataList.Count)
            {
                if (TryParse(dataList, index, out T[] data))
                {
                    if (data == null) throw new Exception("TryParse方法返回的data数据不能为null");
                    frames.Add(data);
                    tmpIndexes.Add(new IndexInfo(index, data.Length));
                    index += data.Length;
                }
                else
                {
                    //没有匹配的帧协议
                    index++;
                }
            }

            if (frames.Count > 0)
            {
                indexes = tmpIndexes;
                OnConsume(frames);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 尝试分割帧
        /// </summary>
        /// <param name="dataList">原始数据</param>
        /// <param name="index">当前索引</param>
        /// <param name="frame">帧对像</param>
        /// <returns>成功则返回True;否则返回False</returns>

        protected abstract bool TryParse(IReadOnlyList<T> dataList, int index, out T[] frame);

        /// <summary>
        /// 消费拿到的数据
        /// </summary>
        protected virtual void OnConsume(List<T[]> frames) { }



        /// <summary>
        /// 检查帧头
        /// </summary>
        /// <param name="dataList"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected virtual bool CheckFrameTitle(IReadOnlyList<T> dataList, int index)
        {
            if (FrameTitle == null || FrameTitle.Length <= 0)
                throw new Exception("请先设置帧头");

            for (int i = 0; i < FrameTitle.Length; i++)
            {
                if (!EqualityComparer<T>.Default.Equals(FrameTitle[i], dataList[index + i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
