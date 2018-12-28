using System.Collections.Generic;
using System.DirectoryServices;
using LdapLib.Extensions;
using LdapLib.Helpers;
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
            var result = new List<LdapUser>();

            var userContainerSettings = LdapConfigurationsHelper.GetUserContainerSettings();

            if (userContainerSettings == null) return;

            foreach (var item in userContainerSettings)
            {
                using (var connection = new LdapConnection(item.Path))
                {
                    var service = new LdapUserService(connection);

                    var list = service.Search(new LdapSearchParameters
                    {
                        PropertiesToLoad = new[] { "cn", "department", "givenname", "mail", "manager", "mobile", "samaccountname", "sn", "streetaddress", "telephonenumber", "title", "useraccountcontrol" },
                        Filter = "(sn=*)", //search users which surname is not empty
                        SortOption = new SortOption { Direction = SortDirection.Ascending, PropertyName = "samaccountname" }
                    }).ToList<LdapUser>();

                    result.AddRange(list);
                }
            }
        }
    }
}