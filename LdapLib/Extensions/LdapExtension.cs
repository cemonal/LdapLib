using LdapLib.Attributes;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace LdapLib.Extensions
{
    /// <summary>
    /// Provides extension methods for LDAP-related operations.
    /// </summary>
    public static class LdapExtension
    {
        /// <summary>
        /// Gets the value of a property from the search result.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="searchResult">The search result containing the property.</param>
        /// <param name="key">The key of the property to retrieve.</param>
        /// <returns>The value of the property or a default value if not found.</returns>
        public static T GetValue<T>(this SearchResult searchResult, string key)
        {
            var type = typeof(T);
            var nullable = type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
            var defaultValue = nullable ? default(T) : Activator.CreateInstance<T>();

            return searchResult.GetValue(key, defaultValue);
        }

        /// <summary>
        /// Gets the value of a property from the search result with a specified default value.
        /// </summary>
        /// <typeparam name="T">The type of the property value.</typeparam>
        /// <param name="searchResult">The search result containing the property.</param>
        /// <param name="key">The key of the property to retrieve.</param>
        /// <param name="defaultValue">The default value if the property is not found.</param>
        /// <returns>The value of the property or the specified default value if not found.</returns>
        public static T GetValue<T>(this SearchResult searchResult, string key, T defaultValue)
        {
            var value = searchResult.Properties.PropertyNames?.OfType<string>().FirstOrDefault(name => string.Equals(name, key, StringComparison.InvariantCultureIgnoreCase));

            if (value == null)
                return defaultValue;

            try
            {
                return (T)Convert.ChangeType(value, typeof(T), new CultureInfo("en-US"));
            }
            catch (InvalidCastException)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Converts the search result to an object of the specified type, using property mapping.
        /// </summary>
        /// <typeparam name="T">The target type to cast the search result to.</typeparam>
        /// <param name="searchResult">The search result to be cast.</param>
        /// <returns>An object of the specified type with mapped properties from the search result.</returns>
        public static T Cast<T>(this SearchResult searchResult) where T : class
        {
            var obj = Activator.CreateInstance(typeof(T), true);

            foreach (var prop in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var code = !prop.TryGetAttribute<LdapPropertyAttribute>(out var ldapPropertyAttribute) ? prop.Name : ldapPropertyAttribute?.Code;

                var method = typeof(LdapExtension).GetMethod("GetValue", new[] { typeof(SearchResult), typeof(string) });

                if (method == null)
                    continue;

                var typedMethod = method.MakeGenericMethod(prop.PropertyType);
                var value = Convert.ChangeType(typedMethod.Invoke(null, new object[] { searchResult, code }), prop.PropertyType);
                prop.SetValue(obj, value, null);
            }

            return (T)obj;
        }

        /// <summary>
        /// Converts a <see cref="SearchResultCollection"/> to a list of <see cref="SearchResult"/> objects.
        /// </summary>
        /// <param name="collection">The <see cref="SearchResultCollection"/> to convert.</param>
        /// <returns>A list of <see cref="SearchResult"/> objects.</returns>
        public static List<SearchResult> ToList(this SearchResultCollection collection)
        {
            return collection.DynamicCast<SearchResult>().ToList();
        }

        /// <summary>
        /// Converts a <see cref="SearchResultCollection"/> to a list of objects of the specified type using property mapping.
        /// </summary>
        /// <typeparam name="T">The target type to cast the search results to.</typeparam>
        /// <param name="collection">The <see cref="SearchResultCollection"/> to convert.</param>
        /// <returns>A list of objects of the specified type with mapped properties from the search results.</returns>
        public static List<T> ToList<T>(this SearchResultCollection collection)
        {
            var type = typeof(T);
            var result = new List<T>(collection.Count);

            result.AddRange(from SearchResult item in collection let method = typeof(LdapExtension).GetMethod("Cast", new[] { typeof(SearchResult) }) let typedMethod = method.MakeGenericMethod(type) select (T)typedMethod.Invoke(null, new object[] { item }));

            return result;
        }

        /// <summary>
        /// Retrieves members of the specified type from a <see cref="PrincipalCollection"/>.
        /// </summary>
        /// <typeparam name="T">The type of principal to retrieve.</typeparam>
        /// <param name="collection">The <see cref="PrincipalCollection"/> to retrieve members from.</param>
        /// <returns>A list of members of the specified principal type.</returns>
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

        /// <summary>
        /// Retrieves members of the specified type from a <see cref="PrincipalSearchResult"/>.
        /// </summary>
        /// <typeparam name="T">The type of principal to retrieve.</typeparam>
        /// <param name="collection">The <see cref="PrincipalSearchResult"/> to retrieve members from.</param>
        /// <returns>A list of members of the specified principal type.</returns>
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