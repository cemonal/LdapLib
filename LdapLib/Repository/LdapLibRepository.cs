using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using LdapLib.Config;
using LdapLib.Helpers;
using LdapLib.Parameters;

namespace LdapLib.Repository
{
    public abstract class LdapLibRepository<T> where T : Principal
    {
        private PrincipalContext Context { get; }
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
        /// Delete
        /// </summary>
        /// <param name="samAccountName">sAM account name</param>
        public void Delete(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            Delete(IdentityType.SamAccountName, samAccountName);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="identityType">Identity type</param>
        /// <param name="identityValue">Identity value</param>
        public void Delete(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);
            principal.Delete();
            principal.Save();
        }

        public virtual PrincipalSearchResult<T> PrincipalSearch()
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
        /// <param name="identityType">Identity type</param>
        /// <param name="identityValue">Identity value</param>
        /// <returns></returns>
        protected T FindByIdentity(IdentityType identityType, string identityValue)
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