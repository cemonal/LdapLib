﻿using System.DirectoryServices;
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
        public string ObjectClass { get; set; }
        protected internal LdapSettingsElement Settings { get; }

        protected LdapLibRepository(LdapConnection ldapConnection)
        {
            Context = ldapConnection.Context;
            DirectoryEntry = ldapConnection.DirectoryEntry;
            Settings = ldapConnection.Settings;
        }

        public void Delete(string samAccountName)
        {
            Delete(IdentityType.SamAccountName, samAccountName);
        }

        public void Delete(IdentityType identityType, string identityValue)
        {
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

        public T FindByIdentity(IdentityType identityType, string identityValue)
        {
            return LdapHelper.FindByIdentity<T>(Context, identityType, identityValue);
        }

        public virtual SearchResultCollection Search(LdapSearchParameters parameters)
        {
            return LdapHelper.FindAll(DirectoryEntry, parameters.Filter, parameters.PropertiesToLoad, parameters.SortOption, parameters.PageSize, parameters.SizeLimit);
        }
    }
}