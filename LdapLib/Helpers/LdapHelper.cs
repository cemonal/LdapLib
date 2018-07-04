using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Helpers
{
    public static class LdapHelper
    {
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

        public static SearchResult FindOne(DirectoryEntry directoryEntry, string query, string[] propertiesToLoad = null)
        {
            SearchResult response;

            var settings = LdapConfigurationsHelper.GetSettings();
            var searchScope = (SearchScope)Enum.Parse(typeof(SearchScope), settings.Scope);

            using (var searcher = new DirectorySearcher(directoryEntry, query) { SearchScope = searchScope, PageSize = 1 })
            {
                if (propertiesToLoad != null)
                    foreach (var item in propertiesToLoad)
                        searcher.PropertiesToLoad.Add(item);

                response = searcher.FindOne();
            }

            return response;
        }

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

        public static SearchResultCollection FindAll(DirectoryEntry directoryEntry, string query, string[] propertiesToLoad = null, SortOption sortOption = null, int pageSize = 3000, int sizeLimit = 5000)
        {
            SearchResultCollection response;

            if (sortOption == null)
                sortOption = new SortOption();

            var settings = LdapConfigurationsHelper.GetSettings();
            var searchScope = (SearchScope)Enum.Parse(typeof(SearchScope), settings.Scope);

            using (var searcher = new DirectorySearcher(directoryEntry, query) { SearchScope = searchScope, PageSize = pageSize, SizeLimit = sizeLimit, Sort = sortOption })
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