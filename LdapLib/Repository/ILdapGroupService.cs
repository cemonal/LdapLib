using System.DirectoryServices.AccountManagement;

namespace LdapLib.Repository
{
    public interface ILdapGroupService : ILdapLibRepository<GroupPrincipal>
    {
        PrincipalSearchResult<Principal> GetMembers(string samAccountName, bool recursive = false);
        PrincipalSearchResult<Principal> GetMembers(IdentityType identityType, string identityValue, bool recursive = false);
    }
}