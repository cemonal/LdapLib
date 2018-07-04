using LdapLib.Repository;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Services
{
    public class LdapGroupService : LdapLibRepository<GroupPrincipal>
    {
        public LdapGroupService(LdapConnection ldapConnection) : base(ldapConnection)
        {
            ObjectClass = ldapConnection.Settings.GroupObjectClass;
        }

        public PrincipalSearchResult<Principal> GetMembers(string samAccountName)
        {
            return GetMembers(IdentityType.SamAccountName, samAccountName);
        }

        public PrincipalSearchResult<Principal> GetMembers(IdentityType identityType, string identityValue)
        {
            var principal = FindByIdentity(identityType, identityValue);
            return principal.GetMembers();
        }
    }
}