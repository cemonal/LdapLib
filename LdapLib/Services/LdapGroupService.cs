using System;
using LdapLib.Repository;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Services
{
    public class LdapGroupService : LdapLibRepository<GroupPrincipal>, ILdapGroupService
    {
        public LdapGroupService(LdapConnection ldapConnection) : base(ldapConnection)
        {
            DefaultFilter = "(&(objectClass=group){0})";
        }

        /// <summary>
        /// Returns a collection of the principal objects that is contained in the group.
        /// </summary>
        /// <param name="samAccountName">sAM account name of the group</param>
        /// <param name="recursive">A Boolean value that specifies whether the group is searched recursively.</param>
        /// <returns></returns>
        public PrincipalSearchResult<Principal> GetMembers(string samAccountName, bool recursive = false)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            return GetMembers(IdentityType.SamAccountName, samAccountName, recursive);
        }

        /// <summary>
        /// Returns a collection of the principal objects that is contained in the group.
        /// </summary>
        /// <param name="identityType">An IdentityType enumeration value that specifies the format of the identityValue parameter.</param>
        /// <param name="identityValue">The identity of the group principal. This parameter can be any format that is contained in the IdentityType enumeration.</param>
        /// <param name="recursive">A Boolean value that specifies whether the group is searched recursively.</param>
        /// <returns></returns>
        public PrincipalSearchResult<Principal> GetMembers(IdentityType identityType, string identityValue, bool recursive = false)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);
            return principal.GetMembers(recursive);
        }
    }
}