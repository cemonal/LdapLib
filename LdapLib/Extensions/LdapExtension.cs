using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.Linq;

namespace LdapLib.Extensions
{
    public static class LdapExtension
    {
        public static T GetValue<T>(this SearchResult searchResult, string key)
        {
            object defaultValue = null;

            var type = typeof(T);
            var nullable = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

            if (!nullable)
                defaultValue = default(T);

            return searchResult.GetValue(key, (T)defaultValue);
        }

        public static T GetValue<T>(this SearchResult searchResult, string key, T defaultValue)
        {
            object result = null;

            var isKeyExists = searchResult.Properties.PropertyNames != null && (searchResult.Properties.PropertyNames.Cast<string>().ToArray().Contains(key));

            if (isKeyExists)
            {
                var value = searchResult.Properties[key][0].ToString();
                var type = typeof(T);
                var nullable = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

                result = string.IsNullOrEmpty(value) ? defaultValue : Convert.ChangeType(value, nullable ? type.GetGenericArguments()[0] : type, new CultureInfo("en-US"));
            }

            return (T)result;
        }

        public static List<SearchResult> ToList(this SearchResultCollection collection)
        {
            return collection.DynamicCast<SearchResult>().ToList();
        }

        public static List<T> RetrieveMembers<T>(this PrincipalCollection collection) where T : Principal
        {
            object result;

            if (collection == null) return null;

            if (typeof(T) == typeof(GroupPrincipal))
                result = collection.Where(x => x.StructuralObjectClass == "group").Select(x => (GroupPrincipal)x).ToList();
            else if (typeof(T) == typeof(UserPrincipal))
                result = collection.Where(x => x.StructuralObjectClass == "user").Select(x => (UserPrincipal)x).ToList();
            else
                result = collection.ToList();

            return (List<T>)result;
        }

        public static List<T> RetrieveMembers<T>(this PrincipalSearchResult<Principal> collection) where T : Principal
        {
            object result;

            if (collection == null) return null;

            if (typeof(T) == typeof(GroupPrincipal))
                result = collection.Where(x => x.StructuralObjectClass == "group").Select(x => (GroupPrincipal)x).ToList();
            else if (typeof(T) == typeof(UserPrincipal))
                result = collection.Where(x => x.StructuralObjectClass == "user").Select(x => (UserPrincipal)x).ToList();
            else
                result = collection.ToList();

            return (List<T>)result;
        }
    }
}