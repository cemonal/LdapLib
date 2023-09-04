using System;
using LdapLib.Repository;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Services
{
    /// <summary>
    /// Provides LDAP-related operations specifically for <see cref="UserPrincipal"/> objects.
    /// </summary>
    public class LdapUserService : LdapLibRepository<UserPrincipal>, ILdapUserService
    {
        public LdapUserService(LdapConnection ldapConnection) : base(ldapConnection)
        {
            DefaultFilter = "(&(objectCategory=person)(objectClass=user){0})";
        }

        /// <inheritdoc/>
        public void ChangePassword(string samAccountName, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(oldPassword), "Old password cannot be empty!");
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(newPassword), "New password cannot be empty!");

            var principal = FindByIdentity(IdentityType.SamAccountName, samAccountName);
            principal.ChangePassword(oldPassword, newPassword);
            principal.Save();
        }

        /// <inheritdoc/>
        public void ExpirePasswordNow(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            var principal = FindByIdentity(IdentityType.SamAccountName, samAccountName);
            principal.ExpirePasswordNow();
            principal.Save();
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<Principal> GetAuthorizationGroups(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            return GetAuthorizationGroups(IdentityType.SamAccountName, samAccountName);
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<Principal> GetAuthorizationGroups(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);
            return principal.GetAuthorizationGroups();
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<UserPrincipal> FindByBadPasswordAttempt(DateTime time, MatchType type)
        {
            return UserPrincipal.FindByBadPasswordAttempt(Context, time, type);
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<UserPrincipal> FindByExpirationTime(DateTime time, MatchType type)
        {
            return UserPrincipal.FindByExpirationTime(Context, time, type);
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<UserPrincipal> FindByLockoutTime(DateTime time, MatchType type)
        {
            return UserPrincipal.FindByLockoutTime(Context, time, type);
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<UserPrincipal> FindByLogonTime(DateTime time, MatchType type)
        {
            return UserPrincipal.FindByLogonTime(Context, time, type);
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<UserPrincipal> FindByPasswordSetTime(DateTime time, MatchType type)
        {
            return UserPrincipal.FindByPasswordSetTime(Context, time, type);
        }

        /// <inheritdoc/>
        public bool IsAccountLockedOut(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            return IsAccountLockedOut(IdentityType.SamAccountName, samAccountName);
        }

        /// <inheritdoc/>
        public bool IsAccountLockedOut(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);
            return principal.IsAccountLockedOut();
        }

        /// <inheritdoc/>
        public void UnlockAccount(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            var principal = FindByIdentity(IdentityType.SamAccountName, samAccountName);

            principal.UnlockAccount();
            principal.Save();
        }

        /// <inheritdoc/>
        public void UnlockAccount(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);

            principal.UnlockAccount();
            principal.Save();
        }

        /// <inheritdoc/>
        public void RefreshExpiredPassword(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            var principal = FindByIdentity(IdentityType.SamAccountName, samAccountName);

            principal.RefreshExpiredPassword();
            principal.Save();
        }
    }
}