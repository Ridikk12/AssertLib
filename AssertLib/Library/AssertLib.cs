using AssertLib.Library.Comparers;
using AssertLib.Library.Comparers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AssertLib
{
    public static class AssertLib
    {
        private static readonly ISimpleTypeComparer _simpleTypeComparer = new SimpleTypeComparer();
        private static readonly IComplexTypeComparer _complexTypeComparer = new ComplexTypeComparer();

        public static AssertObject Expect(this object obj)
        {
            AssertObject objToAssert = new AssertObject(obj)
            {
                ExpectedResult = true
            };

            return objToAssert;
        }

        public static bool Eq(this AssertObject obj, object compareTo)
        {
     
            if (IsSimple(obj.ObjectToAssert.GetType()) && IsSimple(compareTo.GetType()))
                _simpleTypeComparer.CompareEqual(obj, compareTo);
            else
            {
                if (obj.Properties)
                    _complexTypeComparer.CompareEqualProperties(obj, compareTo);
                else
                    _complexTypeComparer.CompareEqual(obj, compareTo);
            }

            return true;
        }

        public static void IsGreater(this AssertObject obj, object compareTo)
        {
            if (IsSimple(obj.ObjectToAssert.GetType()) && IsSimple(compareTo.GetType()))
                if (!_simpleTypeComparer.CompareGreater(obj.ObjectToAssert, compareTo))
                    throw (new ExpectationFailedExceptin(
                        obj.ObjectToAssert.ToString(),
                        compareTo.ToString(),
                        $"Expected:{obj.ObjectToAssert.ToString()} got: {compareTo.ToString()}"));
        }

        public static AssertObject Not(this AssertObject obj)
        {
            obj.ExpectedResult = false;
            return obj;
        }

        private static bool IsSimple(Type type)
        {
            return type.IsPrimitive
              || type.Equals(typeof(string));
        }

  
        public static bool RaiseError(this AssertObject parent)
        {
            if (parent.ObjectToAssert is Delegate)
            {
                try
                {
                    (parent.ObjectToAssert as Delegate).Method.Invoke((parent.ObjectToAssert as Delegate).Target, null);
                }
                catch (Exception)
                {
                    return true;
                }
            }

            throw new ExpectationFailedExceptin("Expected exception");
        }

        public static AssertObject Properties(this AssertObject parent)
        {
            parent.Properties = true;
            return parent;
        }

        //Could be much better - was trying with dynamic but don't have enough time to do that now
        public static AssertObject PropertiesWithout<T>(this AssertObject obj, Expression<Func<T,string>> properties)
        {
            var toExclude = GetMemberName(properties);
            obj.PropertiesToExclude.Add(toExclude);
            obj.Properties = true;
            return obj;
        }

        private static string GetMemberName(this LambdaExpression expr)
        {
            var lexpr = expr;
            MemberExpression mexpr = null;
            if (lexpr.Body is MemberExpression)
            {
                mexpr = (MemberExpression)lexpr.Body;
            }
            else if (lexpr.Body is UnaryExpression)
            {
                mexpr = (MemberExpression)((UnaryExpression)lexpr.Body).Operand;
            }
            if (mexpr == null)
            {
                return null;
            }
            return mexpr.Member.Name;
        }

    }
}
