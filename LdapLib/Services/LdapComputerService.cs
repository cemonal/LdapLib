using LdapLib.Repository;
using System;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Services
{
    /// <summary>
    /// Represents a service for interacting with computer objects in an LDAP directory.
    /// </summary>
    public class LdapComputerService : LdapLibRepository<ComputerPrincipal>, ILdapComputerService
    {
        public LdapComputerService(LdapConnection ldapConnection) : base(ldapConnection)
        {
            DefaultFilter = "(&(objectCategory=computer){0})";
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<ComputerPrincipal> FindByBadPasswordAttempt(DateTime time, MatchType type)
        {
            return ComputerPrincipal.FindByBadPasswordAttempt(Context, time, type);
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<ComputerPrincipal> FindByExpirationTime(DateTime time, MatchType type)
        {
            return ComputerPrincipal.FindByExpirationTime(Context, time, type);
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<ComputerPrincipal> FindByLockoutTime(DateTime time, MatchType type)
        {
            return ComputerPrincipal.FindByLockoutTime(Context, time, type);
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<ComputerPrincipal> FindByLogonTime(DateTime time, MatchType type)
        {
            return ComputerPrincipal.FindByLogonTime(Context, time, type);
        }

        /// <inheritdoc/>
        public PrincipalSearchResult<ComputerPrincipal> FindByPasswordSetTime(DateTime time, MatchType type)
        {
            return ComputerPrincipal.FindByPasswordSetTime(Context, time, type);
        }
    }
}