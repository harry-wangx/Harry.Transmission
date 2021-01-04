using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Harry.Transmission
{
    public interface IEndPoint : IDisposable
    {
        void Send(byte[] data);

        void Open();

        void Close();
    }
}
