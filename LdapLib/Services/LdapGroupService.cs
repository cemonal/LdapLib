using System;
using LdapLib.Repository;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Services
{
    public class LdapGroupService : LdapLibRepository<GroupPrincipal>
    {
        public LdapGroupService(LdapConnection ldapConnection) : base(ldapConnection)
        {
            DefaultFilter = "(&(objectClass=group){0})";
        }

        /// <summary>
        /// Get members of the group
        /// </summary>
        /// <param name="samAccountName">sAM account name of the group</param>
        /// <returns></returns>
        public PrincipalSearchResult<Principal> GetMembers(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            return GetMembers(IdentityType.SamAccountName, samAccountName);
        }

        /// <summary>
        /// Get members of the group
        /// </summary>
        /// <param name="identityType">Identity type</param>
        /// <param name="identityValue">Identity value</param>
        /// <returns></returns>
        public PrincipalSearchResult<Principal> GetMembers(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);
            return principal.GetMembers();
        }
    }
}