using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LdapLib.Extensions
{
   public static class CustomExtension
    {
        internal static IEnumerable<T> DynamicCast<T>(this IEnumerable source)
        {
            return from dynamic current in source select (T)current;
        }

        internal static bool HasAttribute<TAttribute>(this MemberInfo member) where TAttribute : Attribute
        {
            var attributes = member.GetCustomAttributes(typeof(TAttribute), true);
            return attributes.Any();
        }

        internal static TAttribute GetAttribute<TAttribute>(this MemberInfo member) where TAttribute : Attribute
        {
            if (!member.HasAttribute<TAttribute>()) return null;

            var attribute = (TAttribute)member.GetCustomAttributes(typeof(TAttribute), true)[0];

            return attribute;
        }

        internal static bool TryGetAttribute<TAttribute>(this MemberInfo member, out TAttribute attribute) where TAttribute : Attribute
        {
            attribute = member.GetAttribute<TAttribute>();
            return attribute != null;
        }
    }
}