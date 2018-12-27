using System.DirectoryServices;
using LdapLib.Extensions;
using LdapLib.Parameters;
using LdapLib.Services;
using NUnit.Framework;

namespace LdapLib.Test
{
    [TestFixture]
    public class Test
    {
        [TestCase]
        public void TestCase()
        {
            const string container = "OU=,OU=,DC=,DC=";
            
            using (var connection = new LdapConnection(container))
            {
                var service = new LdapUserService(connection);

                var result = service.Search(new LdapSearchParameters
                {
                    PropertiesToLoad = new[] { "cn", "department", "givenname", "mail", "manager", "mobile", "samaccountname", "sn", "streetaddress", "telephonenumber", "title", "useraccountcontrol" },
                    Filter = "(sn=*)", //search users which surname is not empty
                    SortOption = new SortOption { Direction = SortDirection.Ascending, PropertyName = "samaccountname" }
                }).ToList<LdapUser>();
            }
        }
    }
}