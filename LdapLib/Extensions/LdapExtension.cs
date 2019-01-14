using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LdapLib.Attributes;

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
            var isKeyExists = searchResult.Properties.PropertyNames != null && searchResult.Properties.PropertyNames.Cast<string>().ToArray().Contains(key);

            if (!isKeyExists) return default(T);
            var value = searchResult.Properties[key][0].ToString();
            var type = typeof(T);
            var nullable = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

            var result = string.IsNullOrEmpty(value) ? defaultValue : Convert.ChangeType(value, nullable ? type.GetGenericArguments()[0] : type, new CultureInfo("en-US"));

            return (T)result;
        }

        public static T Cast<T>(this SearchResult searchResult) where T : class
        {
            var obj = Activator.CreateInstance(typeof(T), true);
            var padlock = new object();

            Parallel.ForEach(typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public), prop =>
            {
                lock (padlock)
                {
                    var code = !prop.TryGetAttribute<LdapPropertyAttribute>(out var ldapPropertyAttribute) ? prop.Name : ldapPropertyAttribute?.Code;

                    var method = typeof(LdapExtension).GetMethod("GetValue", new[] { typeof(SearchResult), typeof(string) });
                    var typedMethod = method.MakeGenericMethod(prop.PropertyType);
                    var value = Convert.ChangeType(typedMethod.Invoke(null, new object[] { searchResult, code }), prop.PropertyType);


                    prop.SetValue(obj, value, null);
                }
            });

            return (T)obj;
        }

        public static List<SearchResult> ToList(this SearchResultCollection collection)
        {
            return collection.DynamicCast<SearchResult>().ToList();
        }

        public static List<T> ToList<T>(this SearchResultCollection collection)
        {
            var type = typeof(T);
            var result = new List<T>(collection.Count);

            foreach (SearchResult item in collection)
            {
                var method = typeof(LdapExtension).GetMethod("Cast", new[] { typeof(SearchResult) });
                var typedMethod = method.MakeGenericMethod(type);
                var value = (T)typedMethod.Invoke(null, new object[] { item });
                result.Add(value);
            }

            return result;
        }

        public static List<T> RetrieveMembers<T>(this PrincipalCollection collection) where T : Principal
        {
            object result;

            if (collection == null) return null;

            if (typeof(T) == typeof(GroupPrincipal))
                result = collection.Where(x => x.StructuralObjectClass == "group").Select(x => (GroupPrincipal)x).ToList();
            else if (typeof(T) == typeof(UserPrincipal))
                result = collection.Where(x => x.StructuralObjectClass == "user").Select(x => (UserPrincipal)x).ToList();
            else if (typeof(T) == typeof(ComputerPrincipal))
                result = collection.Where(x => x.StructuralObjectClass == "computer").Select(x => (ComputerPrincipal)x).ToList();
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
            else if (typeof(T) == typeof(ComputerPrincipal))
                result = collection.Where(x => x.StructuralObjectClass == "computer").Select(x => (ComputerPrincipal)x).ToList();
            else
                result = collection.ToList();

            return (List<T>)result;
        }
    }
}