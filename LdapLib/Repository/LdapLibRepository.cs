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

        public PrincipalSearchResult<T> PrincipalSearch()
        {
            return LdapHelper.PrincipalSearch<T>(Context);
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

        public SearchResultCollection Search(LdapSearchParameters parameters)
        {
            return LdapHelper.FindAll(DirectoryEntry, string.Format(DefaultFilter, parameters.Filter), parameters.PropertiesToLoad, parameters.SortOption, parameters.PageSize, parameters.SizeLimit);
        }

        public SearchResultCollection GetAll()
        {
            return Search(new LdapSearchParameters { Filter = string.Format(DefaultFilter, ""), PageSize = int.MaxValue, SizeLimit = int.MinValue, PropertiesToLoad = new string[0] });
        }

        public SearchResultCollection GetAll(string[] propertiesToLoad)
        {
            return Search(new LdapSearchParameters { Filter = string.Format(DefaultFilter, ""), PageSize = int.MaxValue, SizeLimit = int.MinValue, PropertiesToLoad = propertiesToLoad });
        }

        public SearchResultCollection GetAll(string[] propertiesToLoad, SortOption sortOption)
        {
            return Search(new LdapSearchParameters { Filter = string.Format(DefaultFilter, ""), PageSize = int.MaxValue, SizeLimit = int.MinValue, PropertiesToLoad = propertiesToLoad, SortOption = sortOption });
        }
    }
}