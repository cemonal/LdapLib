using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Helpers
{
    public static class LdapHelper
    {
        /// <summary>
        /// Principal search
        /// </summary>
        /// <typeparam name="T">Principal object type</typeparam>
        /// <param name="context">The PrincipalContext that specifies the server or domain against which operations are performed.</param>
        /// <returns>A Principal object that matches the specified identity value and type or null if no matches are found.</returns>
        public static PrincipalSearchResult<T> PrincipalSearch<T>(PrincipalContext context) where T : Principal
        {
            object result;

            if (typeof(T) == typeof(UserPrincipal))
            {
                using (var principal = new UserPrincipal(context))
                {
                    var searcher = new PrincipalSearcher { QueryFilter = principal };
                    result = searcher.FindAll();
                }
            }
            else if (typeof(T) == typeof(GroupPrincipal))
            {
                using (var principal = new GroupPrincipal(context))
                {
                    var searcher = new PrincipalSearcher { QueryFilter = principal };
                    result = searcher.FindAll();
                }
            }
            else if (typeof(T) == typeof(ComputerPrincipal))
            {
                using (var principal = new ComputerPrincipal(context))
                {
                    var searcher = new PrincipalSearcher { QueryFilter = principal };
                    result = searcher.FindAll();
                }
            }
            else
            {
                var searcher = new PrincipalSearcher();
                result = searcher.FindAll();
            }

            return (PrincipalSearchResult<T>)result;
        }

        /// <summary>
        /// Executes the search and returns only the first entry that is found.
        /// </summary>
        /// <param name="directoryEntry">The node in the Active Directory Domain Services hierarchy where the search starts.</param>
        /// <param name="filter">The search filter string in Lightweight Directory Access Protocol (LDAP) format.</param>
        /// <param name="propertiesToLoad">The set of properties to retrieve during the search.</param>
        /// <returns>A SearchResult object that contains the first entry that is found during the search.</returns>
        public static SearchResult FindOne(DirectoryEntry directoryEntry, string filter, string[] propertiesToLoad = null)
        {
            SearchResult response;

            var settings = LdapConfigurationsHelper.GetSettings();
            var searchScope = (SearchScope)Enum.Parse(typeof(SearchScope), settings.Scope);

            using (var searcher = new DirectorySearcher(directoryEntry, filter) { SearchScope = searchScope, PageSize = 1 })
            {
                if (propertiesToLoad != null)
                    foreach (var item in propertiesToLoad)
                        searcher.PropertiesToLoad.Add(item);

                response = searcher.FindOne();
            }

            return response;
        }

        /// <summary>
        /// Find by identity
        /// </summary>
        /// <typeparam name="T">Principal object type</typeparam>
        /// <param name="context">The PrincipalContext that specifies the server or domain against which operations are performed.</param>
        /// <param name="identityValue">The identity of the principal.</param>
        /// <returns>Returns a principal object that matches the specified identity value.</returns>
        public static T FindByIdentity<T>(PrincipalContext context, string identityValue) where T : Principal
        {
            object result;

            if (typeof(T) == typeof(UserPrincipal))
                result = UserPrincipal.FindByIdentity(context, identityValue);
            else if (typeof(T) == typeof(GroupPrincipal))
                result = GroupPrincipal.FindByIdentity(context, identityValue);
            else if (typeof(T) == typeof(ComputerPrincipal))
                result = ComputerPrincipal.FindByIdentity(context, identityValue);
            else
                result = Principal.FindByIdentity(context, identityValue);

            return (T)result;
        }

        /// <summary>
        /// Find by identity
        /// </summary>
        /// <typeparam name="T">Principal object type</typeparam>
        /// <param name="context">The PrincipalContext that specifies the server or domain against which operations are performed.</param>
        /// <param name="identityType">An IdentityType enumeration value that specifies the format of the identityValue parameter.</param>
        /// <param name="identityValue">The identity of the principal. This parameter can be any format that is contained in the IdentityType enumeration.</param>
        /// <returns>Returns a principal object that matches the specified identity value.</returns>
        public static T FindByIdentity<T>(PrincipalContext context, IdentityType identityType, string identityValue) where T : Principal
        {
            object result;
            
            if (typeof(T) == typeof(UserPrincipal))
                result = UserPrincipal.FindByIdentity(context, identityType, identityValue);
            else if (typeof(T) == typeof(GroupPrincipal))
                result = GroupPrincipal.FindByIdentity(context, identityType, identityValue);
            else if (typeof(T) == typeof(ComputerPrincipal))
                result = ComputerPrincipal.FindByIdentity(context, identityType, identityValue);
            else
                result = Principal.FindByIdentity(context, identityType, identityValue);

            return (T)result;
        }

        /// <summary>
        /// Executes the search and returns a collection of the entries that are found.
        /// </summary>
        /// <param name="directoryEntry">The node in the Active Directory Domain Services hierarchy where the search starts.</param>
        /// <param name="filter">The search filter string in Lightweight Directory Access Protocol (LDAP) format. </param>
        /// <param name="propertiesToLoad">The set of properties to retrieve during the search.</param>
        /// <param name="sortOption">Gets or sets a value indicating the property on which the results are sorted.</param>
        /// <param name="pageSize">Gets or sets a value indicating the page size in a paged search.</param>
        /// <param name="sizeLimit">Gets or sets a value indicating the maximum number of the objects that the server returns in a search.</param>
        /// <returns>A SearchResultCollection object that contains the results of the search.</returns>
        public static SearchResultCollection FindAll(DirectoryEntry directoryEntry, string filter, string[] propertiesToLoad = null, SortOption sortOption = null, int pageSize = int.MaxValue, int sizeLimit = int.MaxValue)
        {
            SearchResultCollection response;

            if (sortOption == null)
                sortOption = new SortOption();

            var settings = LdapConfigurationsHelper.GetSettings();
            var searchScope = (SearchScope)Enum.Parse(typeof(SearchScope), settings.Scope, true);

            using (var searcher = new DirectorySearcher(directoryEntry, filter) { SearchScope = searchScope, PageSize = pageSize, SizeLimit = sizeLimit, Sort = sortOption })
            {
                if (propertiesToLoad != null)
                    foreach (var item in propertiesToLoad)
                        searcher.PropertiesToLoad.Add(item);

                response = searcher.FindAll();
            }

            return response;
        }
    }
}