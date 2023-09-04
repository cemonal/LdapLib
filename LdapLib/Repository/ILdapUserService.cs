using System;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Repository
{
    /// <summary>
    /// Provides LDAP-related operations specifically for <see cref="UserPrincipal"/> objects.
    /// </summary>
    public interface ILdapUserService : ILdapLibRepository<UserPrincipal>
    {
        /// <summary>
        /// Changes the user's password from the old password to the new password.
        /// </summary>
        /// <param name="samAccountName">sAM account name of the user</param>
        /// <param name="oldPassword">The password that is changed.</param>
        /// <param name="newPassword">The new password.</param>
        void ChangePassword(string samAccountName, string oldPassword, string newPassword);

        /// <summary>
        /// Expires the user's password, forcing a password change at the next logon.
        /// </summary>
        /// <param name="samAccountName">sAM account name of the user</param>
        void ExpirePasswordNow(string samAccountName);

        /// <summary>
        /// Get authorization groups of the user.
        /// </summary>
        /// <param name="samAccountName">sAM account name of the user</param>
        /// <returns>A collection of authorization groups</returns>
        PrincipalSearchResult<Principal> GetAuthorizationGroups(string samAccountName);

        /// <summary>
        /// Returns a collection of PrincipalSearchResult<T> objects containing the authorization groups of which the specified user is a member.
        /// </summary>
        /// <param name="identityType">An <see cref="IdentityType"/> enumeration value that specifies the format of the identityValue parameter.</param>
        /// <param name="identityValue">The identity of the user principal. This parameter can be any format that is contained in the <see cref="IdentityType"/> enumeration.</param>
        /// <returns>A collection of authorization groups.</returns>
        PrincipalSearchResult<Principal> GetAuthorizationGroups(IdentityType identityType, string identityValue);

        /// <summary>
        /// Returns a collection of PrincipalSearchResult<T> objects for users that have an incorrect password attempt recorded in the specified date and time range.
        /// </summary>
        /// <param name="time">A <see cref="DateTime"/> object that identifies the date and time of the incorrect password try. </param>
        /// <param name="type">A <see cref="MatchType"/> enumeration value that specifies the type of match that is applied to the time parameter.</param>
        /// <returns>A PrincipalSearchResult<T> that contains one or more UserPrincipal objects, or an empty collection if no results are found.</returns>
        PrincipalSearchResult<UserPrincipal> FindByBadPasswordAttempt(DateTime time, MatchType type);

        /// <summary>
        /// Returns a collection of PrincipalSearchResult<T> objects for users that have an account expiration time in the specified date and time range.
        /// </summary>
        /// <param name="time">A <see cref="DateTime"/> object that identifies the date and time of the incorrect password try.</param>
        /// <param name="type">A <see cref="MatchType"/> enumeration value that specifies the type of match that is applied to the time parameter.</param>
        /// <returns>A PrincipalSearchResult<T> that contains one or more UserPrincipal objects, or an empty collection if no results are found.</returns>
        PrincipalSearchResult<UserPrincipal> FindByExpirationTime(DateTime time, MatchType type);

        /// <summary>
        /// Returns a collection of PrincipalSearchResult<T> objects for users that have an account lockout time in the specified date and time range.
        /// </summary>
        /// <param name="time">A <see cref="DateTime"/> object that identifies the date and time of the incorrect password try.</param>
        /// <param name="type">A <see cref="MatchType"/> enumeration value that specifies the type of match that is applied to the time parameter.</param>
        /// <returns>A PrincipalSearchResult<T> that contains one or more UserPrincipal objects, or an empty collection if no results are found.</returns>
        PrincipalSearchResult<UserPrincipal> FindByLockoutTime(DateTime time, MatchType type);

        /// <summary>
        /// Returns a collection of PrincipalSearchResult<T> objects for users that have account logon recorded in the specified date and time range.
        /// </summary>
        /// <param name="time">A <see cref="DateTime"/> object that identifies the date and time of the incorrect password try.</param>
        /// <param name="type">A <see cref="MatchType"/> enumeration value that specifies the type of match that is applied to the time parameter.</param>
        /// <returns>A PrincipalSearchResult<T> that contains one or more UserPrincipal objects, or an empty collection if no results are found.</returns>
        PrincipalSearchResult<UserPrincipal> FindByLogonTime(DateTime time, MatchType type);

        /// <summary>
        /// Returns a collection of PrincipalSearchResult<T> objects for users that have set their password within the specified date and time range.
        /// </summary>
        /// <param name="time">A <see cref="DateTime"/> object that identifies the date and time of the incorrect password try.</param>
        /// <param name="type">A <see cref="MatchType"/> enumeration value that specifies the type of match that is applied to the time parameter.</param>
        /// <returns>A PrincipalSearchResult<T> that contains one or more UserPrincipal objects, or an empty collection if no results are found.</returns>
        PrincipalSearchResult<UserPrincipal> FindByPasswordSetTime(DateTime time, MatchType type);

        /// <summary>
        /// Returns a Boolean value that specifies whether the account is currently locked out.
        /// </summary>
        /// <param name="samAccountName">sAM account name of the user</param>
        /// <returns>True if the account is locked out, otherwise false.</returns>
        bool IsAccountLockedOut(string samAccountName);

        /// <summary>
        /// Returns a Boolean value that specifies whether the account is currently locked out.
        /// </summary>
        /// <param name="identityType">An <see cref="IdentityType"/> enumeration value that specifies the format of the identityValue parameter.</param>
        /// <param name="identityValue">The identity of the user principal. This parameter can be any format that is contained in the <see cref="IdentityType"/> enumeration.</param>
        /// <returns>True if the account is locked out, otherwise false.</returns>
        bool IsAccountLockedOut(IdentityType identityType, string identityValue);

        /// <summary>
        /// Unlocks the user's account if it is currently locked out.
        /// </summary>
        /// <param name="samAccountName">sAM account name of the user</param>
        void UnlockAccount(string samAccountName);

        /// <summary>
        /// Unlocks the user's account if it is currently locked out.
        /// </summary>
        /// <param name="identityType">A IdentityType enumeration value that specifies the format of the identityValue parameter.</param>
        /// <param name="identityValue">The identity of the user principal. This parameter can be any format that is contained in the <see cref="IdentityType"/> enumeration.</param>
        void UnlockAccount(IdentityType identityType, string identityValue);

        /// <summary>
        /// Refreshes an expired password for the user.
        /// </summary>
        /// <param name="samAccountName">sAM account name of the user</param>
        void RefreshExpiredPassword(string samAccountName);
    }
}