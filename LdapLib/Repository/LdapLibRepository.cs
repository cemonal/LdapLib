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

        /// <inheritdoc/>
        public void Delete(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            Delete(IdentityType.SamAccountName, samAccountName);
        }

        /// <inheritdoc/>
        public void Delete(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);
            principal.Delete();
            principal.Save();
        }

        /// <inheritdoc/>
        public SearchResultCollection GetAll()
        {
            return Search(new LdapSearchParameters { Filter = string.Format(DefaultFilter, ""), PageSize = int.MaxValue, SizeLimit = 0, PropertiesToLoad = new string[0] });
        }

        /// <inheritdoc/>
        public SearchResultCollection GetAll(string[] propertiesToLoad)
        {
            return Search(new LdapSearchParameters { Filter = string.Format(DefaultFilter, ""), PageSize = int.MaxValue, SizeLimit = 0, PropertiesToLoad = propertiesToLoad });
        }

        /// <inheritdoc/>
        public SearchResultCollection GetAll(string[] propertiesToLoad, SortOption sortOption)
        {
            return Search(new LdapSearchParameters { Filter = string.Format(DefaultFilter, ""), PageSize = int.MaxValue, SizeLimit = 0, PropertiesToLoad = propertiesToLoad, SortOption = sortOption });
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<Principal> GetGroups(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            return GetGroups(IdentityType.SamAccountName, samAccountName);
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<Principal> GetGroups(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);

            return principal.GetGroups();
        }

        /// <inheritdoc/>
        public SearchResult Find(LdapFindParameters parameters)
        {
            return LdapHelper.FindOne(DirectoryEntry, string.Format(DefaultFilter, parameters.Filter), parameters.PropertiesToLoad);
        }

        /// <inheritdoc/>
        public T FindByIdentity(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            return LdapHelper.FindByIdentity<T>(Context, identityType, identityValue);
        }

        /// <inheritdoc/>
        public bool IsMemberOf(IdentityType identityType, string identityValue, IdentityType groupIdentityType, string groupIdentityValue)
        {
            var principal = FindByIdentity(identityType, identityValue);
            return principal.IsMemberOf(LdapHelper.FindByIdentity<GroupPrincipal>(Context, groupIdentityType, groupIdentityValue));
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<T> PrincipalSearch()
        {
            return LdapHelper.PrincipalSearch<T>(Context);
        }

        /// <inheritdoc/>
        public SearchResultCollection Search(LdapSearchParameters parameters)
        {
            return LdapHelper.FindAll(DirectoryEntry, string.Format(DefaultFilter, parameters.Filter), parameters.PropertiesToLoad, parameters.SortOption, parameters.PageSize, parameters.SizeLimit);
        }

        /// <inheritdoc/>
        public T Create(T principal, string password)
        {
            principal.Save();

            if (!string.IsNullOrEmpty(password))
            {
                var userPrincipal = principal as UserPrincipal;

                if (userPrincipal != null)
                    userPrincipal.SetPassword(password);
            }

            return FindByIdentity(IdentityType.Guid, principal.Guid.ToString());
        }

        /// <inheritdoc/>
        public void Update(T principal)
        {
            principal.Save();
        }
    }
}