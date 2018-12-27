using System.DirectoryServices;

namespace LdapLib.Parameters
{
    public class LdapSearchParameters : LdapFindParameters
    {
        /// <summary>
        /// The maximum number of objects the server can return in a paged search. The default is zero, which means do not do a paged search.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The maximum number of objects that the server returns in a search. The default value is zero, which means to use the server-determined default size limit of 1000 entries.
        /// </summary>
        public int SizeLimit { get; set; }

        /// <summary>
        /// A SortOption object that specifies the property and direction that the search results should be sorted on.
        /// </summary>
        public SortOption SortOption { get; set; }
    }
}