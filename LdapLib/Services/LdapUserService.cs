using LdapLib.Repository;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Services
{
    public class LdapUserService : LdapLibRepository<UserPrincipal>
    {
        public LdapUserService(LdapConnection ldapConnection) : base(ldapConnection)
        {
            ObjectClass = Settings.UserObjectClass;
        }

        /// <summary>
        /// Change password of Account
        /// </summary>
        /// <param name="samAccountName">sAMAccountName</param>
        /// <param name="oldPassword">Old password</param>
        /// <param name="newPassword">New password</param>
        public void ChangePassword(string samAccountName, string oldPassword, string newPassword)
        {
            var principal = FindByIdentity(IdentityType.SamAccountName, samAccountName);
            principal.ChangePassword(oldPassword, newPassword);
            principal.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="samAccountName">sAMAccountName</param>
        public void ExpirePasswordNow(string samAccountName)
        {
            var principal = FindByIdentity(IdentityType.SamAccountName, samAccountName);
            principal.ExpirePasswordNow();
            principal.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="samAccountName">sAMAccountName</param>
        /// <returns></returns>
        public PrincipalSearchResult<Principal> GetAuthorizationGroups(string samAccountName)
        {
            return GetAuthorizationGroups(IdentityType.SamAccountName, samAccountName);
        }

        public PrincipalSearchResult<Principal> GetAuthorizationGroups(IdentityType identityType, string identityValue)
        {
            var principal = FindByIdentity(identityType, identityValue);
            return principal.GetAuthorizationGroups();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="samAccountName">sAMAccountName</param>
        /// <returns></returns>
        public PrincipalSearchResult<Principal> GetGroups(string samAccountName)
        {
            return GetGroups(IdentityType.SamAccountName, samAccountName);
        }

        public PrincipalSearchResult<Principal> GetGroups(IdentityType identityType, string identityValue)
        {
            var principal = FindByIdentity(identityType, identityValue);
            return principal.GetGroups();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="samAccountName">sAMAccountName</param>
        /// <returns></returns>
        public bool IsAccountLockedOut(string samAccountName)
        {
            return IsAccountLockedOut(IdentityType.SamAccountName, samAccountName);
        }

        public bool IsAccountLockedOut(IdentityType identityType, string identityValue)
        {
            var principal = FindByIdentity(identityType, identityValue);
            return principal.IsAccountLockedOut();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="samAccountName">sAMAccountName</param>
        public void UnlockAccount(string samAccountName)
        {
            var principal = FindByIdentity(IdentityType.SamAccountName, samAccountName);

            principal.UnlockAccount();
            principal.Save();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="samAccountName">sAMAccountName</param>
        public void RefreshExpiredPassword(string samAccountName)
        {
            var principal = FindByIdentity(IdentityType.SamAccountName, samAccountName);

            principal.RefreshExpiredPassword();
            principal.Save();
        }
    }
}