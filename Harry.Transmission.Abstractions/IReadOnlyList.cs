#if NET40
using System.Reflection;

namespace System.Collections.Generic
{
    public interface IReadOnlyList<out T> : IEnumerable<T>, IEnumerable
    {
        T this[int index] { get; }

        int Count { get; }
    }
}
#endif