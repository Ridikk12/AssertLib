using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssertLib.Library.Comparers.Interfaces
{
    public interface ISimpleTypeComparer : IComparer
    {
        bool CompareEqual(AssertObject parent, object compareTo);
        bool CompareGreater(object parent, object compareTo);

    }
}
