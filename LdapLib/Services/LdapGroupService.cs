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
            ObjectClass = ldapConnection.Settings.GroupObjectClass;
        }

        public static LdapGroupService GetInstance(LdapConnection ldapConnection)
        {
            if (_instance != null) return _instance;

            lock (Padlock)
                if (_instance == null) // create the instance only if the instance is null
                    _instance = Activator.CreateInstance(typeof(LdapGroupService), ldapConnection) as LdapGroupService;

            return _instance;
        }

        /// <summary>
        /// Get members of the group
        /// </summary>
        /// <param name="samAccountName">samAccountName of the group</param>
        /// <returns></returns>
        public PrincipalSearchResult<Principal> GetMembers(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "samAccountName cannot be empty!");

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