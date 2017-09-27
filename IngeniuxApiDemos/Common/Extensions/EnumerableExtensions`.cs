using System;
using System.Collections.Generic;

namespace IngeniuxApiDemos.Common.Extensions
{
    /// <summary>
    /// Oh the joys of syntactic sugar
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Simple foreach wrapper
        /// </summary>
        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "Method or lambda passed to extension method was null");
            }

            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}
