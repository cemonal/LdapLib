using System.DirectoryServices;

namespace LdapLib.Parameters
{
    /// <summary>
    /// Represents parameters for LDAP search operations, including paging and sorting options.
    /// </summary>
    public class LdapSearchParameters : LdapFindParameters
    {
        /// <summary>
        /// Gets or sets the maximum number of objects the server can return in a paged search. The default is zero, which means do not do a paged search.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of objects that the server returns in a search. The default value is zero, which means to use the server-determined default size limit of 1000 entries.
        /// </summary>
        public int SizeLimit { get; set; }

        /// <summary>
        /// Gets or sets a SortOption object that specifies the property and direction that the search results should be sorted on.
        /// </summary>
        public SortOption SortOption { get; set; }
    }
}