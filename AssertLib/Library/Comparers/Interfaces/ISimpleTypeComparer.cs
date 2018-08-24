using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssertLib.Library.Comparers.Interfaces
{
    public interface ISimpleTypeComparer : IComparer
    {
        bool CompareEqual(object parent, object compareTo);
        bool CompareGreater<T, D>(T parent, D compareTo)
         where T : IComparable
         where D : IComparable;

    }
}
