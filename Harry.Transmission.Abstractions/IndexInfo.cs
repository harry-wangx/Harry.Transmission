using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harry.Transmission
{
    public struct IndexInfo
    {
        public IndexInfo(int index, int length)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));

            this.Index = index;
            this.Length = length;
        }

        public int Index { get; private set; }

        public int Length { get; private set; }
    }
}
