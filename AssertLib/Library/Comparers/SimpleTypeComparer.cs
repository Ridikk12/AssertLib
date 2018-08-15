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
   

        public bool CompareEqual(AssertObject parent, object compareTo)
        {
            if (!(parent.ObjectToAssert.Equals(compareTo) == parent.ExpectedResult))
                throw new ExpectationFailedExceptin(
                    parent.ObjectToAssert.ToString(), 
                    compareTo.ToString(), 
                    $"Expected:{parent.ObjectToAssert.ToString()} got: {compareTo.ToString()}");
            return true;
        }

        public bool CompareGreater(object parent, object compareTo)
        {
            if (parent.GetType() == typeof(int) && compareTo.GetType() == typeof(int))
                return (int)parent > (int)compareTo;
            else if (parent.GetType() == typeof(float) && compareTo.GetType() == typeof(float))
                return (float)parent > (float)compareTo;
            else if (parent.GetType() == typeof(decimal) && compareTo.GetType() == typeof(decimal))
                return (decimal)parent > (decimal)compareTo;
            else
                throw (new ArgumentException($"Can not compare {parent.GetType()} to {compareTo.GetType()}"));
        }


    }
}
