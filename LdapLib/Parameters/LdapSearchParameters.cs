using System.DirectoryServices;

namespace LdapLib.Parameters
{
    public class LdapSearchParameters : LdapFindParameters
    {
        public int PageSize { get; set; }
        public int SizeLimit { get; set; }
        public SortOption SortOption { get; set; }

        public LdapSearchParameters() { }

        public LdapSearchParameters(string filter, string[] propertiesToLoad, int pageSize, int sizeLimit, SortOption option)
        {
            Filter = filter;
            PropertiesToLoad = propertiesToLoad;
            PageSize = pageSize;
            SizeLimit = sizeLimit;
            SortOption = option;
        }
    }
}