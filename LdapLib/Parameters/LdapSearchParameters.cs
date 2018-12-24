using System.DirectoryServices;

namespace LdapLib.Parameters
{
    public class LdapSearchParameters : LdapFindParameters
    {
        public int PageSize { get; set; }
        public int SizeLimit { get; set; }
        public SortOption SortOption { get; set; }
    }
}