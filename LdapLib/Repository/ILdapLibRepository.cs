using LdapLib.Parameters;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Repository
{
    public interface ILdapLibRepository<T> where T : Principal
    {
        void Delete(string samAccountName);
        void Delete(IdentityType identityType, string identityValue);
        SearchResultCollection GetAll();
        SearchResultCollection GetAll(string[] propertiesToLoad);
        SearchResultCollection GetAll(string[] propertiesToLoad, SortOption sortOption);
        PrincipalSearchResult<Principal> GetGroups(string samAccountName);
        PrincipalSearchResult<Principal> GetGroups(IdentityType identityType, string identityValue);
        SearchResult Find(LdapFindParameters parameters);
        T FindByIdentity(IdentityType identityType, string identityValue);
        bool IsMemberOf(IdentityType identityType, string identityValue, IdentityType groupIdentityType, string groupIdentityValue);
        PrincipalSearchResult<T> PrincipalSearch();
        SearchResultCollection Search(LdapSearchParameters parameters);
    }
}