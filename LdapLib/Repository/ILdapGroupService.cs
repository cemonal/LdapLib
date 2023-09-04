using LdapLib.Repository;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Services
{
    /// <summary>
    /// Represents a service for interacting with group objects in an LDAP directory.
    /// </summary>
    public interface ILdapGroupService : ILdapLibRepository<GroupPrincipal>
    {
        /// <summary>
        /// Returns a collection of principal objects that are contained in the group.
        /// </summary>
        /// <param name="samAccountName">sAM account name of the group</param>
        /// <param name="recursive">A <see cref="bool"/> value that specifies whether the group is searched recursively.</param>
        /// <returns>A collection of principal objects contained in the group.</returns>
        PrincipalSearchResult<Principal> GetMembers(string samAccountName, bool recursive = false);

        /// <summary>
        /// Returns a collection of principal objects that are contained in the group.
        /// </summary>
        /// <param name="identityType">An <see cref="IdentityType"/> enumeration value that specifies the format of the identityValue parameter.</param>
        /// <param name="identityValue">The identity of the group principal. This parameter can be any format that is contained in the <see cref="IdentityType"/> enumeration.</param>
        /// <param name="recursive">A <see cref="bool"/> value that specifies whether the group is searched recursively.</param>
        /// <returns>A collection of principal objects contained in the group.</returns>
        PrincipalSearchResult<Principal> GetMembers(IdentityType identityType, string identityValue, bool recursive = false);
    }
}
