using System.Collections.Generic;

namespace enki.common.core
{
    public class ObjectUtils
    {
        /// <summary>
        /// Compare two object properties values.
        /// </summary>
        /// <param name="item1">First item to compare.</param>
        /// <param name="item2">Second item to compare.</param>
        /// <param name="diffProperties">Out parameter with list of different properties.</param>
        /// <typeparam name="T">Item object type to compare.</typeparam>
        /// <returns>True if objects has same properties. False if not.</returns>
        public static bool GenericCompare<T>(T item1, T item2, out IList<string> diffProperties)
        {
            diffProperties = new List<string>();
            foreach (var property in item1.GetType().GetProperties())
            {
                var value1 = property.GetValue(item1, null);
                var value2 = property.GetValue(item2, null);

                if (!((value1 == null && value2 == null) || value1.Equals(value2)))
                {
                    diffProperties.Add(property.Name);
                    return false;
                }
            }
            return true;
        }
    }
}