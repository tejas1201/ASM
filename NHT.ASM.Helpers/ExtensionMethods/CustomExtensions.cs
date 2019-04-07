using System.Collections.Generic;
using System.Linq;

namespace NHT.ASM.Helpers.ExtensionMethods
{
    public static class CustomExtensions
    {
        /// <summary>
        /// Checks whether collection is not null and has at least one record.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"> entity to check for null or collection</param>
        /// <returns></returns>
        public static bool HasAny<T>(this IEnumerable<T> item)
        {
            return item != null && item.Any();
        }

    }
}
