using System;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Repository
{
    public interface ILdapUserService : ILdapLibRepository<UserPrincipal>
    {
        void ChangePassword(string samAccountName, string oldPassword, string newPassword);
        void ExpirePasswordNow(string samAccountName);
        PrincipalSearchResult<Principal> GetAuthorizationGroups(string samAccountName);
        PrincipalSearchResult<Principal> GetAuthorizationGroups(IdentityType identityType, string identityValue);
        PrincipalSearchResult<UserPrincipal> FindByBadPasswordAttempt(DateTime time, MatchType type);
        PrincipalSearchResult<UserPrincipal> FindByExpirationTime(DateTime time, MatchType type);
        PrincipalSearchResult<UserPrincipal> FindByLockoutTime(DateTime time, MatchType type);
        PrincipalSearchResult<UserPrincipal> FindByLogonTime(DateTime time, MatchType type);
        PrincipalSearchResult<UserPrincipal> FindByPasswordSetTime(DateTime time, MatchType type);
        bool IsAccountLockedOut(string samAccountName);
        bool IsAccountLockedOut(IdentityType identityType, string identityValue);
        void UnlockAccount(string samAccountName);
        void UnlockAccount(IdentityType identityType, string identityValue);
        void RefreshExpiredPassword(string samAccountName);
    }
}