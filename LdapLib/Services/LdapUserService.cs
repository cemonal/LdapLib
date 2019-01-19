using System;
using LdapLib.Repository;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Services
{
    public class LdapUserService : LdapLibRepository<UserPrincipal>, ILdapUserService
    {
        public LdapUserService(LdapConnection ldapConnection) : base(ldapConnection)
        {
            DefaultFilter = "(&(objectCategory=person)(objectClass=user){0})";
        }

        /// <summary>
        /// Changes the account password from the old password to the new password.
        /// </summary>
        /// <param name="samAccountName">sAM account name of the user</param>
        /// <param name="oldPassword">The password that is changed.</param>
        /// <param name="newPassword">The new password.</param>
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
        /// Expires the password for this account. This will force the user to change his/her password at the next logon.
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
        /// Returns a collection of principal objects that contains all the authorization groups of which this user is a member. This function only returns groups that are security groups; distribution groups are not returned.
        /// </summary>
        /// <param name="identityType">A IdentityType enumeration value that specifies the format of the identityValue parameter.</param>
        /// <param name="identityValue">The identity of the user principal. This parameter can be any format that is contained in the IdentityType enumeration.</param>
        /// <returns></returns>
        public PrincipalSearchResult<Principal> GetAuthorizationGroups(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);
            return principal.GetAuthorizationGroups();
        }
        
        /// <summary>
        /// Returns a collection of PrincipalSearchResult<T> objects for users that have an incorrect password attempt recorded in the specified date and time range.
        /// </summary>
        /// <param name="time">A DateTime object that identifies the date and time of the incorrect password try. </param>
        /// <param name="type">A MatchType enumeration value that specifies the type of match that is applied to the time parameter.</param>
        /// <returns>A PrincipalSearchResult<T> that contains one or more UserPrincipal objects, or an empty collection if no results are found.</returns>
        public PrincipalSearchResult<UserPrincipal> FindByBadPasswordAttempt(DateTime time, MatchType type)
        {
            return UserPrincipal.FindByBadPasswordAttempt(Context, time, type);
        }

        /// <summary>
        /// Returns a collection of PrincipalSearchResult<T> objects for users that have an account expiration time in the specified date and time range.
        /// </summary>
        /// <param name="time">A DateTime object that identifies the date and time of the incorrect password try.</param>
        /// <param name="type">A MatchType enumeration value that specifies the type of match that is applied to the time parameter.</param>
        /// <returns>A PrincipalSearchResult<T> that contains one or more UserPrincipal objects, or an empty collection if no results are found.</returns>
        public PrincipalSearchResult<UserPrincipal> FindByExpirationTime(DateTime time, MatchType type)
        {
            return UserPrincipal.FindByExpirationTime(Context, time, type);
        }

        /// <summary>
        /// Returns a collection of PrincipalSearchResult<T> objects for users that have an account lockout time in the specified date and time range.
        /// </summary>
        /// <param name="time">A DateTime object that identifies the date and time of the incorrect password try.</param>
        /// <param name="type">A MatchType enumeration value that specifies the type of match that is applied to the time parameter.</param>
        /// <returns>A PrincipalSearchResult<T> that contains one or more UserPrincipal objects, or an empty collection if no results are found.</returns>
        public PrincipalSearchResult<UserPrincipal> FindByLockoutTime(DateTime time, MatchType type)
        {
            return UserPrincipal.FindByLockoutTime(Context, time, type);
        }

        /// <summary>
        /// Returns a collection of PrincipalSearchResult<T> objects for users that have account logon recorded in the specified date and time range.
        /// </summary>
        /// <param name="time">A DateTime object that identifies the date and time of the incorrect password try.</param>
        /// <param name="type">A MatchType enumeration value that specifies the type of match that is applied to the time parameter.</param>
        /// <returns>A PrincipalSearchResult<T> that contains one or more UserPrincipal objects, or an empty collection if no results are found.</returns>
        public PrincipalSearchResult<UserPrincipal> FindByLogonTime(DateTime time, MatchType type)
        {
            return UserPrincipal.FindByLogonTime(Context, time, type);
        }

        /// <summary>
        /// Returns a collection of PrincipalSearchResult<T> objects for users that have set their password within the specified date and time range.
        /// </summary>
        /// <param name="time">A DateTime object that identifies the date and time of the incorrect password try.</param>
        /// <param name="type">A MatchType enumeration value that specifies the type of match that is applied to the time parameter.</param>
        /// <returns>A PrincipalSearchResult<T> that contains one or more UserPrincipal objects, or an empty collection if no results are found.</returns>
        public PrincipalSearchResult<UserPrincipal> FindByPasswordSetTime(DateTime time, MatchType type)
        {
            return UserPrincipal.FindByPasswordSetTime(Context, time, type);
        }

        /// <summary>
        /// Returns a Boolean value that specifies whether the account is currently locked out.
        /// </summary>
        /// <param name="samAccountName">sAM account name of the user</param>
        /// <returns></returns>
        public bool IsAccountLockedOut(string samAccountName)
        {
            if (string.IsNullOrEmpty(samAccountName)) throw new ArgumentNullException(nameof(samAccountName), "sAM account name cannot be empty!");

            return IsAccountLockedOut(IdentityType.SamAccountName, samAccountName);
        }

        /// <summary>
        /// Returns a Boolean value that specifies whether the account is currently locked out.
        /// </summary>
        /// <param name="identityType">A IdentityType enumeration value that specifies the format of the identityValue parameter.</param>
        /// <param name="identityValue">The identity of the user principal. This parameter can be any format that is contained in the IdentityType enumeration.</param>
        /// <returns></returns>
        public bool IsAccountLockedOut(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);
            return principal.IsAccountLockedOut();
        }

        /// <summary>
        /// Unlocks the account if it is currently locked out.
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
        /// Unlocks the account if it is currently locked out.
        /// </summary>
        /// <param name="identityType">A IdentityType enumeration value that specifies the format of the identityValue parameter.</param>
        /// <param name="identityValue">The identity of the user principal. This parameter can be any format that is contained in the IdentityType enumeration.</param>
        public void UnlockAccount(IdentityType identityType, string identityValue)
        {
            if (string.IsNullOrEmpty(identityValue)) throw new ArgumentNullException(nameof(identityValue), "Identity value cannot be empty!");

            var principal = FindByIdentity(identityType, identityValue);

            principal.UnlockAccount();
            principal.Save();
        }

        /// <summary>
        /// Refreshes an expired password.
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