using System;
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
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "samAccountName cannot be empty!");

            return GetMembers(IdentityType.SamAccountName, samAccountName);
        }

        public PrincipalSearchResult<Principal> GetMembers(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "identityValue cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);
            return principal.GetMembers();
        }
    }
}