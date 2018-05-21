using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NiceLinq.Helpers
{
    internal class Tuple<T1, T2>
    {
        internal T1 First { get; private set; }
        internal T2 Second { get; private set; }
        internal Tuple(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }
    }

    internal static class Tuple
    {
        internal static Tuple<T1, T2> New<T1, T2>(T1 first, T2 second)
        {
            var tuple = new Tuple<T1, T2>(first, second);
            return tuple;
        }
    }
}
