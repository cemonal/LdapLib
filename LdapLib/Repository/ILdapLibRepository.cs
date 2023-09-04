using LdapLib.Parameters;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Repository
{
    /// <summary>
    /// Represents a repository for LDAP-related operations on principal objects.
    /// </summary>
    /// <typeparam name="T">The type of principal object.</typeparam>
    public interface ILdapLibRepository<T> where T : Principal
    {
        /// <summary>
        /// Deletes the principal object by sAM account name from the store.
        /// </summary>
        /// <param name="samAccountName">sAM account name</param>
        void Delete(string samAccountName);

        /// <summary>
        /// Deletes the principal object from the store.
        /// </summary>
        /// <param name="identityType">An <see cref="IdentityType"/> enumeration value that specifies the format of the identityValue parameter.</param>
        /// <param name="identityValue">The identity of the principal. This parameter can be any format that is contained in the <see cref="IdentityType"/> enumeration.</param>
        void Delete(IdentityType identityType, string identityValue);

        /// <summary>
        /// Gets all principal objects from the store.
        /// </summary>
        /// <returns>A collection of principal objects.</returns>
        SearchResultCollection GetAll();

        /// <summary>
        /// Gets all principal objects from the store with specified properties to load.
        /// </summary>
        /// <param name="propertiesToLoad">The properties to load for each principal.</param>
        /// <returns>A collection of principal objects.</returns>
        SearchResultCollection GetAll(string[] propertiesToLoad);

        /// <summary>
        /// Gets all principal objects from the store with specified properties to load and sort option.
        /// </summary>
        /// <param name="propertiesToLoad">The properties to load for each principal.</param>
        /// <param name="sortOption">The sort option for the search results.</param>
        /// <returns>A collection of principal objects.</returns>
        SearchResultCollection GetAll(string[] propertiesToLoad, SortOption sortOption);

        /// <summary>
        /// Returns a collection of group objects that specify the groups of which the current principal is a member.
        /// </summary>
        /// <param name="samAccountName">SAM account name</param>
        /// <returns>A collection of group principal objects.</returns>
        PrincipalSearchResult<Principal> GetGroups(string samAccountName);

        /// <summary>
        /// Returns a collection of group objects that specify the groups of which the current principal is a member.
        /// </summary>
        /// <param name="identityType">An IdentityType enumeration value that specifies the format of the identityValue parameter.</param>
        /// <param name="identityValue">The identity of the user principal. This parameter can be any format that is contained in the IdentityType enumeration.</param>
        /// <returns>A collection of group principal objects.</returns>
        PrincipalSearchResult<Principal> GetGroups(IdentityType identityType, string identityValue);

        /// <summary>
        /// Finds a principal object in the store based on the provided parameters.
        /// </summary>
        /// <param name="parameters">The search parameters.</param>
        /// <returns>The search result containing the principal object.</returns>
        SearchResult Find(LdapFindParameters parameters);

        /// <summary>
        /// Finds a principal object in the store by identity.
        /// </summary>
        /// <param name="identityType">An IdentityType enumeration value that specifies the format of the identityValue parameter.</param>
        /// <param name="identityValue">The identity of the principal. This parameter can be any format that is contained in the IdentityType enumeration.</param>
        /// <returns>The found principal object.</returns>
        T FindByIdentity(IdentityType identityType, string identityValue);

        /// <summary>
        /// Checks if a principal object is a member of a specific group.
        /// </summary>
        /// <param name="identityType">An <see cref="IdentityType"/> enumeration value that specifies the format of the identityValue parameter.</param>
        /// <param name="identityValue">The identity of the principal. This parameter can be any format that is contained in the <see cref="IdentityType"/> enumeration.</param>
        /// <param name="groupIdentityType">An <see cref="IdentityType"/> enumeration value that specifies the format of the groupIdentityValue parameter.</param>
        /// <param name="groupIdentityValue">The identity of the group principal. This parameter can be any format that is contained in the <see cref="IdentityType"/> enumeration.</param>
        /// <returns>True if the principal is a member of the group, false otherwise.</returns>
        bool IsMemberOf(IdentityType identityType, string identityValue, IdentityType groupIdentityType, string groupIdentityValue);

        /// <summary>
        /// Performs a principal search.
        /// </summary>
        /// <returns>The search result containing the principal objects.</returns>
        PrincipalSearchResult<T> PrincipalSearch();

        /// <summary>
        /// Performs a search for principal objects based on the provided parameters.
        /// </summary>
        /// <param name="parameters">The search parameters.</param>
        /// <returns>The search results containing the principal objects.</returns>
        SearchResultCollection Search(LdapSearchParameters parameters);

        /// <summary>
        /// Creates a new principal object in the store.
        /// </summary>
        /// <param name="principal">The principal object to create.</param>
        /// <param name="password">The password for the principal, if applicable.</param>
        /// <returns>The created principal object.</returns>
        T Create(T principal, string password);

        /// <summary>
        /// Updates a principal object in the store.
        /// </summary>
        /// <param name="principal">The principal object to update.</param>
        void Update(T principal);
    }
}
