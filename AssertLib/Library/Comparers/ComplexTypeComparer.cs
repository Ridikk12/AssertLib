using AssertLib.Library.Comparers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AssertLib.Library.Comparers
{
    public class ComplexTypeComparer : IComplexTypeComparer
    {
        public bool CompareEqual(AssertObject parent, object compareTo)
        {
            if (parent.ObjectToAssert == null && compareTo == null) return true;
            if (parent.ObjectToAssert == null || compareTo == null) return false;
            if (!parent.ObjectToAssert.Equals(compareTo))
                throw new ExpectationFailedExceptin("Complex objects are not equal");
            return true;
        }

        public bool CompareEqualProperties(AssertObject parent, object compareTo)
        {
            if (parent.ObjectToAssert == null && compareTo == null) return true;
            if (parent.ObjectToAssert == null || compareTo == null) return false;

            var obj1Class = parent.ObjectToAssert.GetType();
            var obj2Class = compareTo.GetType();

            var obj1Properties = obj1Class.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
            var obj2Properties = obj2Class.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);

            foreach (var obj1Property in obj1Properties)
            {
                if (parent.PropertiesToExclude.Exists(x => x == obj1Property.Name))
                    continue;
                var fieldName = obj1Property.Name;
                var obj2Property = obj2Properties.Where(f => f.Name == fieldName).SingleOrDefault();
                if (obj2Property == null)
                    throw new ExpectationFailedExceptin(
                         obj1Property.GetValue(parent.ObjectToAssert).ToString(),
                         null);

                if (!PropertiesEquals(parent, compareTo, obj1Property, obj2Property))
                    throw new ExpectationFailedExceptin(
                        obj1Property.GetValue(parent.ObjectToAssert).ToString(),
                        obj2Property.GetValue(compareTo).ToString());
            }

            return true;
        }

        private static bool PropertiesEquals(AssertObject parent, object compareTo, System.Reflection.PropertyInfo obj1Property, System.Reflection.PropertyInfo obj2Property)
        {
            return obj1Property.PropertyType == obj2Property.PropertyType
                                && (obj1Property.GetValue(parent.ObjectToAssert).Equals(obj2Property.GetValue(compareTo)));
        }




    }
}
