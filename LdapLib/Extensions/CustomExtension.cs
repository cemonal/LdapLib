using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LdapLib.Extensions
{
   public static class CustomExtension
    {
        internal static IEnumerable<T> DynamicCast<T>(this IEnumerable source)
        {
            return from dynamic current in source select (T)current;
        }
    }
}
