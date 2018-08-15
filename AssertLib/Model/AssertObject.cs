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
        public Object ObjectToAssert { get;  private set; }
        public bool ExpectedResult { get; set; }
        public List<string> PropertiesToExclude { get; set; } = new List<string>();
        public bool Properties { get; set; }

        public AssertObject(Object obj)
        {
            Properties = false;
            ExpectedResult = true;
            ObjectToAssert = obj;
        }

 
    }
}
