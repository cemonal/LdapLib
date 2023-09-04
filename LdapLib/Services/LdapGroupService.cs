using LdapLib.Repository;
using System;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Services
{
    /// <summary>
    /// Represents a service for interacting with group objects in an LDAP directory.
    /// </summary>
    public class LdapGroupService : LdapLibRepository<GroupPrincipal>, ILdapGroupService
    {
        public LdapGroupService(LdapConnection ldapConnection) : base(ldapConnection)
        {
            DefaultFilter = "(&(objectClass=group){0})";
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<Principal> GetMembers(string samAccountName, bool recursive = false)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            return GetMembers(IdentityType.SamAccountName, samAccountName, recursive);
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<Principal> GetMembers(IdentityType identityType, string identityValue, bool recursive = false)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);
            return principal.GetMembers(recursive);
        }
    }
}