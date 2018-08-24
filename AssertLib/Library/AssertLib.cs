using AssertLib.Library.Comparers;
using AssertLib.Library.Comparers.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using AssertLib.Library;

namespace AssertLib
{
    public static class AssertLib
    {
        private static readonly ISimpleTypeComparer _simpleTypeComparer = new SimpleTypeComparer();
        private static readonly IComplexTypeComparer _complexTypeComparer = new ComplexTypeComparer();

        public static AssertObject Expect(this object obj)
        {
            return new AssertObject(obj)
            {
                ExpectedResult = true
            };
        }

        public static void Eq(this AssertObject obj, object compareTo)
        {
            if (CheckComplexWithProperties(obj))
            {
                if (!(_complexTypeComparer.CompareEqualProperties(obj.Subject, compareTo, obj.PropertyToExclude)
                    == obj.ExpectedResult))
                    throw new ExpectationFailedExceptin("Objects are not equal");
            }
            else if (CheckComplex(obj))
            {
                if (!(_complexTypeComparer.CompareEqual(obj.Subject, compareTo) == obj.ExpectedResult))
                    throw new ExpectationFailedExceptin("Objects are not equal");
            }
            else if (obj.Subject.GetType().IsPrimitive)
            {
                if (!(_simpleTypeComparer.CompareEqual(obj.Subject, compareTo) == obj.ExpectedResult))
                    throw new ExpectationFailedExceptin("Objects are not equal");
            }
        }

        private static bool CheckComplex(AssertObject obj)
        {
            return !obj.Subject.GetType().IsPrimitive;
        }

        private static bool CheckComplexWithProperties(AssertObject obj)
        {
            return !obj.Subject.GetType().IsPrimitive && obj.CheckProperties;
        }

        public static void IsGreater<T>(this AssertObject obj, T compareTo) where T : IComparable
        {
            if (!(_simpleTypeComparer.CompareGreater(obj.Subject as IComparable, compareTo) == obj.ExpectedResult))
                throw new ExpectationFailedExceptin("Object is not greater");
        }

        public static AssertObject Not(this AssertObject obj)
        {
            obj.ExpectedResult = false;
            return obj;
        }

        public static void RaiseError(this AssertObject parent)
        {
            if (parent.Subject is Delegate)
            {
                try
                {
                    (parent.Subject as Delegate).Method.Invoke((parent.Subject as Delegate).Target, null);
                }
                catch (Exception)
                {
                    return;
                }

                throw new ExpectationFailedExceptin("Expected exception");
            }

        }

        public static AssertObject Properties(this AssertObject parent)
        {
            parent.CheckProperties = true;
            return parent;
        }

        public static AssertObject PropertiesWithout<T>(this AssertObject obj, Expression<Func<T, string>> properties)
        {
            string toExclude = properties.GetMemberName();
            obj.ExcludeProperty(toExclude);
            return obj;
        }

   

    }
}
