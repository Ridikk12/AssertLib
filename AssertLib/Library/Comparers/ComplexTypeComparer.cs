using AssertLib.Library.Comparers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AssertLib.Library.Comparers
{
    public class ComplexTypeComparer : IComplexTypeComparer
    {
        public bool CompareEqual(object subject, object compareTo)
        {
            if (subject == null && compareTo == null) return true;
            if (subject == null || compareTo == null) return false;
            return subject.Equals(compareTo);

        }

        public bool CompareEqualProperties(object subject, object compareTo, string propertyToExclude)
        {
            if (subject == null && compareTo == null) return true;
            if (subject == null || compareTo == null) return false;

            var obj1Class = subject.GetType();
            var obj2Class = compareTo.GetType();

            var obj1Properties = obj1Class.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);
            var obj2Properties = obj2Class.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);

            foreach (var obj1Property in obj1Properties)
            {
                if (propertyToExclude == obj1Property.Name)
                    continue;
                var fieldName = obj1Property.Name;
                var obj2Property = obj2Properties.Where(f => f.Name == fieldName).SingleOrDefault();
                if (obj2Property == null)
                    return false;

                if (!PropertiesEquals(subject, compareTo, obj1Property, obj2Property))
                    return false;
            }

            return true;
        }

        private static bool PropertiesEquals(
            object subject,
            object compareTo,
            System.Reflection.PropertyInfo obj1Property,
            System.Reflection.PropertyInfo obj2Property)
        {
            return obj1Property.PropertyType == obj2Property.PropertyType
                                && (obj1Property.GetValue(subject).Equals(obj2Property.GetValue(compareTo)));
        }




    }
}
