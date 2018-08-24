using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssertLib
{
    public class AssertObject
    {
        public Object Subject { get; set; }
        public bool ExpectedResult { get; set; }
        public string PropertyToExclude { get; set; }
        public bool CheckProperties { get; set; }

        public AssertObject(Object obj)
        {
            CheckProperties = false;
            ExpectedResult = true;
            Subject = obj;
        }

        public void ExcludeProperty(string property)
        {
            PropertyToExclude = property;
            CheckProperties = true;
        }


    }
}
