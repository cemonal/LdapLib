﻿using System;
using LdapLib.Repository;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Services
{
    public class LdapUserService : LdapLibRepository<UserPrincipal>
    {
        // A private static instance of the same class
        private static LdapUserService _instance;
        private static readonly object Padlock = new object();

        public LdapUserService(LdapConnection ldapConnection) : base(ldapConnection)
        {
            _instance = this;
            DefaultFilter = "(&(objectCategory=person)(objectClass=user){0})";
        }

        public static LdapUserService GetInstance(LdapConnection ldapConnection = null)
        {
            if (_instance != null) return _instance;

            // create the instance only if the instance is null
            lock (Padlock)
            {
                if (ldapConnection == null) throw new ArgumentNullException(nameof(ldapConnection), "There is no created instance of this service before. You have to set ldap connection if you want to create new instance for Singleton.");
                _instance = Activator.CreateInstance(typeof(LdapUserService), ldapConnection) as LdapUserService;
            }

            return _instance;
        }

        /// <summary>
        /// Change password of the account
        /// </summary>
        /// <param name="samAccountName">sAM account name of the user</param>
        /// <param name="oldPassword">Old password</param>
        /// <param name="newPassword">New password</param>
        public void ChangePassword(string samAccountName, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(oldPassword), "Old password cannot be empty!");
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(newPassword), "New password cannot be empty!");

            var principal = FindByIdentity(IdentityType.SamAccountName, samAccountName);
            principal.ChangePassword(oldPassword, newPassword);
            principal.Save();
        }

        /// <summary>
        /// Expire password of the user
        /// </summary>
        /// <param name="samAccountName">sAM account name of the user</param>
        public void ExpirePasswordNow(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            var principal = FindByIdentity(IdentityType.SamAccountName, samAccountName);
            principal.ExpirePasswordNow();
            principal.Save();
        }

        /// <summary>
        /// Get authorization groups of the user
        /// </summary>
        /// <param name="samAccountName">sAM account name of the user</param>
        /// <returns></returns>
        public PrincipalSearchResult<Principal> GetAuthorizationGroups(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            return GetAuthorizationGroups(IdentityType.SamAccountName, samAccountName);
        }

        /// <summary>
        /// Get authorization groups of the user
        /// </summary>
        /// <param name="identityType">Identity type</param>
        /// <param name="identityValue">Identity value</param>
        /// <returns></returns>
        public PrincipalSearchResult<Principal> GetAuthorizationGroups(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);
            return principal.GetAuthorizationGroups();
        }

        /// <summary>
        /// Get groups of the user
        /// </summary>
        /// <param name="samAccountName">sAM account name of the user</param>
        /// <returns></returns>
        public PrincipalSearchResult<Principal> GetGroups(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            return GetGroups(IdentityType.SamAccountName, samAccountName);
        }

        /// <summary>
        /// Get groups of the user
        /// </summary>
        /// <param name="identityType">Identity type</param>
        /// <param name="identityValue">Identity value</param>
        /// <returns></returns>
        public PrincipalSearchResult<Principal> GetGroups(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);
            return principal.GetGroups();
        }

        /// <summary>
        /// Is account locked out?
        /// </summary>
        /// <param name="samAccountName">sAM account name of the user</param>
        /// <returns></returns>
        public bool IsAccountLockedOut(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            return IsAccountLockedOut(IdentityType.SamAccountName, samAccountName);
        }

        /// <summary>
        /// Is account locked out?
        /// </summary>
        /// <param name="identityType">Identity type</param>
        /// <param name="identityValue">Identity value</param>
        /// <returns></returns>
        public bool IsAccountLockedOut(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);
            return principal.IsAccountLockedOut();
        }

        /// <summary>
        /// Unlock account
        /// </summary>
        /// <param name="samAccountName">sAM account name of the user</param>
        public void UnlockAccount(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            var principal = FindByIdentity(IdentityType.SamAccountName, samAccountName);

            principal.UnlockAccount();
            principal.Save();
        }

        /// <summary>
        /// Refresh expired password of the user
        /// </summary>
        /// <param name="samAccountName">sAM account name of the user</param>
        public void RefreshExpiredPassword(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            var principal = FindByIdentity(IdentityType.SamAccountName, samAccountName);

            principal.RefreshExpiredPassword();
            principal.Save();
        }
    }
}