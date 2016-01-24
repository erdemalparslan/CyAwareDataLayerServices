using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CyAwareWebApi.Utils
{
    public static class Extensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }
        public static IEnumerable<T> ToIEnumerable<T>(this HashSet<T> source)
        {
            return new List<T>(source);
        }
    }
}