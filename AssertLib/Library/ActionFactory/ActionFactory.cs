using AssertLib.Library.Comparers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssertLib.Library.ActionFactory
{
    public class ActionFactory
    {

        public Func<object, object, bool> GetCompareAction(AssertObject subject, object compareTo)
        {
            //if (subject.Subject.GetType().IsPrimitive)
            //    return new SimpleTypeComparer().CompareEqual;
            //else if (!subject.Subject.GetType().IsPrimitive && subject.CheckProperties)
            //    return new ComplexTypeComparer().CompareEqualProperties(subject, compareTo);

            //if (!(obj.Subject.Equals(compareTo) && obj.ExpectedResult))
            //    throw new ExpectationFailedExceptin("Objects are not equal");

            throw new ArgumentException("No action for that type of objects");
        }





    }
}
