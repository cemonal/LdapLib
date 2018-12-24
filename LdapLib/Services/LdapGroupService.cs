using System;
using LdapLib.Repository;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Services
{
    public class LdapGroupService : LdapLibRepository<GroupPrincipal>
    {
        // A private static instance of the same class
        private static LdapGroupService _instance;
        private static readonly object Padlock = new object();

        public LdapGroupService(LdapConnection ldapConnection) : base(ldapConnection)
        {
            _instance = this;
            DefaultFilter = "(&(objectClass=group){0})";
        }

        public static LdapGroupService GetInstance(LdapConnection ldapConnection = null)
        {
            if (_instance != null) return _instance;

            // create the instance only if the instance is null
            lock (Padlock)
            {
                if (ldapConnection == null) throw new ArgumentNullException(nameof(ldapConnection), "There is no created instance of this service before. You have to set ldap connection if you want to create new instance for Singleton.");
                _instance = Activator.CreateInstance(typeof(LdapGroupService), ldapConnection) as LdapGroupService;
            }

            return _instance;
        }

        /// <summary>
        /// Get members of the group
        /// </summary>
        /// <param name="samAccountName">sAM account name of the group</param>
        /// <returns></returns>
        public PrincipalSearchResult<Principal> GetMembers(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            return GetMembers(IdentityType.SamAccountName, samAccountName);
        }

        /// <summary>
        /// Get members of the group
        /// </summary>
        /// <param name="identityType">Identity type</param>
        /// <param name="identityValue">Identity value</param>
        /// <returns></returns>
        public PrincipalSearchResult<Principal> GetMembers(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);
            return principal.GetMembers();
        }
    }
}