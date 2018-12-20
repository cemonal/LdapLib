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
        // A private static instance of the same class
        private static T _instance;
        private static readonly object Padlock = new object();
        private PrincipalContext Context { get; }
        private DirectoryEntry DirectoryEntry { get; }
        public string ObjectClass { get; set; }
        protected internal LdapSettingsElement Settings { get; }

        protected LdapLibRepository(LdapConnection ldapConnection)
        {
            _instance = this as T;

            Context = ldapConnection.Context;
            DirectoryEntry = ldapConnection.DirectoryEntry;
            Settings = ldapConnection.Settings;
        }

        public static T GetInstance(LdapConnection ldapConnection)
        {
            if (_instance != null) return _instance;

            lock (Padlock)
                if (_instance == null) // create the instance only if the instance is null
                    _instance = Activator.CreateInstance(typeof(T), ldapConnection) as T;

            return _instance;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="samAccountName">samAccountName</param>
        public void Delete(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "samAccountName cannot be empty!");

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

        public virtual SearchResult Find(LdapFindParameters parameters)
        {
            return LdapHelper.FindOne(DirectoryEntry, parameters.Filter, parameters.PropertiesToLoad);
        }

        /// <summary>
        /// Find by identity
        /// </summary>
        /// <param name="identityType">Identity type</param>
        /// <param name="identityValue">Identity value</param>
        /// <returns></returns>
        public T FindByIdentity(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            return LdapHelper.FindByIdentity<T>(Context, identityType, identityValue);
        }

        public virtual SearchResultCollection Search(LdapSearchParameters parameters)
        {
            return LdapHelper.FindAll(DirectoryEntry, parameters.Filter, parameters.PropertiesToLoad, parameters.SortOption, parameters.PageSize, parameters.SizeLimit);
        }
    }
}