using LdapLib.Parameters;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Repository
{
    public interface ILdapLibRepository<T> where T : Principal
    {
        void Delete(string samAccountName);
        void Delete(IdentityType identityType, string identityValue);
        PrincipalSearchResult<T> GetAll();
        SearchResult Find(LdapFindParameters parameters);
        T FindByIdentity(IdentityType identityType, string identityValue);
        SearchResultCollection Search(LdapSearchParameters parameters);
    }
}