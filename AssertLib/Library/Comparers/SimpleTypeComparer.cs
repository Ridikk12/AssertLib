using AssertLib.Library.Comparers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssertLib.Library.Comparers
{
    public class SimpleTypeComparer : ISimpleTypeComparer
    {
        public bool CompareEqual(object parent, object compareTo)
        {
            return parent.Equals(compareTo);
        }

        public bool CompareGreater<T,D>(T parent, D compareTo) 
            where T : IComparable
            where D : IComparable

        {
            return parent.CompareTo(compareTo) > 0 ? true : false;
        }

    }
}
