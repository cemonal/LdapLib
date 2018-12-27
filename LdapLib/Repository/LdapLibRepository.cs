using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using LdapLib.Config;
using LdapLib.Helpers;
using LdapLib.Parameters;

namespace LdapLib.Repository
{
    public abstract class LdapLibRepository<T> : ILdapLibRepository<T> where T : Principal
    {
        protected PrincipalContext Context { get; }
        private DirectoryEntry DirectoryEntry { get; }
        protected string DefaultFilter { get; set; }
        protected internal LdapSettingsElement Settings { get; }

        protected LdapLibRepository(LdapConnection ldapConnection)
        {
            Context = ldapConnection.Context;
            DirectoryEntry = ldapConnection.DirectoryEntry;
            Settings = ldapConnection.Settings;
        }

        /// <summary>
        /// Deletes the principal object by sAM account name from the store.
        /// </summary>
        /// <param name="samAccountName">sAM account name</param>
        public void Delete(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            Delete(IdentityType.SamAccountName, samAccountName);
        }

        /// <summary>
        /// Deletes the principal object from the store.
        /// </summary>
        /// <param name="identityType">An IdentityType enumeration value that specifies the format of the identityValue parameter.</param>
        /// <param name="identityValue">The identity of the principal. This parameter can be any format that is contained in the IdentityType enumeration.</param>
        public void Delete(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);
            principal.Delete();
            principal.Save();
        }

        public SearchResultCollection GetAll()
        {
            return Search(new LdapSearchParameters { Filter = string.Format(DefaultFilter, ""), PageSize = int.MaxValue, SizeLimit = 0, PropertiesToLoad = new string[0] });
        }

        public SearchResultCollection GetAll(string[] propertiesToLoad)
        {
            return Search(new LdapSearchParameters { Filter = string.Format(DefaultFilter, ""), PageSize = int.MaxValue, SizeLimit = 0, PropertiesToLoad = propertiesToLoad });
        }

        public SearchResultCollection GetAll(string[] propertiesToLoad, SortOption sortOption)
        {
            return Search(new LdapSearchParameters { Filter = string.Format(DefaultFilter, ""), PageSize = int.MaxValue, SizeLimit = 0, PropertiesToLoad = propertiesToLoad, SortOption = sortOption });
        }

        /// <summary>
        /// Returns a collection of group objects that specify the groups of which the current principal is a member.
        /// </summary>
        /// <param name="samAccountName">SAM account name</param>
        /// <returns></returns>
        public PrincipalSearchResult<Principal> GetGroups(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            return GetGroups(IdentityType.SamAccountName, samAccountName);
        }

        /// <summary>
        /// Returns a collection of group objects that specify the groups of which the current principal is a member.
        /// </summary>
        /// <param name="identityType">A IdentityType enumeration value that specifies the format of the identityValue parameter.</param>
        /// <param name="identityValue">The identity of the user principal. This parameter can be any format that is contained in the IdentityType enumeration.</param>
        /// <returns></returns>
        public PrincipalSearchResult<Principal> GetGroups(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);

            return principal.GetGroups();
        }

        public SearchResult Find(LdapFindParameters parameters)
        {
            return LdapHelper.FindOne(DirectoryEntry, string.Format(DefaultFilter, parameters.Filter), parameters.PropertiesToLoad);
        }

        /// <summary>
        /// Find by identity
        /// </summary>
        /// <param name="identityType">An IdentityType enumeration value that specifies the format of the identityValue parameter.</param>
        /// <param name="identityValue">The identity of the principal. This parameter can be any format that is contained in the IdentityType enumeration.</param>
        /// <returns></returns>
        public T FindByIdentity(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            return LdapHelper.FindByIdentity<T>(Context, identityType, identityValue);
        }
        
        public bool IsMemberOf(IdentityType identityType, string identityValue, IdentityType groupIdentityType, string groupIdentityValue)
        {
            var principal = FindByIdentity(identityType, identityValue);
            return principal.IsMemberOf(LdapHelper.FindByIdentity<GroupPrincipal>(Context, groupIdentityType, groupIdentityValue));
        }

        public PrincipalSearchResult<T> PrincipalSearch()
        {
            return LdapHelper.PrincipalSearch<T>(Context);
        }

        public SearchResultCollection Search(LdapSearchParameters parameters)
        {
            return LdapHelper.FindAll(DirectoryEntry, string.Format(DefaultFilter, parameters.Filter), parameters.PropertiesToLoad, parameters.SortOption, parameters.PageSize, parameters.SizeLimit);
        }
    }
}