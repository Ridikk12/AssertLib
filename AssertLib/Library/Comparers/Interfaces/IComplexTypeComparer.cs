﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssertLib.Library.Comparers.Interfaces
{
    public interface IComplexTypeComparer : IComparer
    {
        bool CompareEqual(object subject, object compareTo);
        bool CompareEqualProperties(object subject, object compareTo, string propertyToExclude);

    }
}
