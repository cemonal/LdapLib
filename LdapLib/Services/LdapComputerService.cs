using System;
using System.DirectoryServices.AccountManagement;
using LdapLib.Repository;

namespace LdapLib.Services
{
    public class LdapComputerService : LdapLibRepository<ComputerPrincipal>
    {
        public LdapComputerService(LdapConnection ldapConnection) : base(ldapConnection)
        {
            DefaultFilter = "(&(objectCategory=computer){0})";
        }

        /// <summary>
        /// Returns a PrincipalSearchResult<T> collection of ComputerPrincipal objects that have had bad password attempts within the parameters specified.
        /// </summary>
        /// <param name="time">A DateTime structure that is used in conjunction with the MatchType to find computers with bad password attempts.</param>
        /// <param name="type">The MatchType that specifies the type of comparison to use in the search.</param>
        /// <returns></returns>
        public PrincipalSearchResult<ComputerPrincipal> FindByBadPasswordAttempt(DateTime time, MatchType type)
        {
            return ComputerPrincipal.FindByBadPasswordAttempt(Context, time, type);
        }

        /// <summary>
        /// Returns a PrincipalSearchResult<T> collection of ComputerPrincipal objects that have an expiration time within the specified date and time range.
        /// </summary>
        /// <param name="time">A DateTime structure that is used in conjunction with the MatchType to filter search results.</param>
        /// <param name="type">The MatchType that specifies the type of comparison to use in the search.</param>
        /// <returns></returns>
        public PrincipalSearchResult<ComputerPrincipal> FindByExpirationTime(DateTime time, MatchType type)
        {
            return ComputerPrincipal.FindByExpirationTime(Context, time, type);
        }

        /// <summary>
        /// Returns a PrincipalSearchResult<T> collection of ComputerPrincipal objects that have a lockout time within the specified date and time range.
        /// </summary>
        /// <param name="time">A DateTime structure that is used in conjunction with the MatchType to filter search results.</param>
        /// <param name="type">The MatchType that specifies the type of comparison to use in the search.</param>
        /// <returns></returns>
        public PrincipalSearchResult<ComputerPrincipal> FindByLockoutTime(DateTime time, MatchType type)
        {
            return ComputerPrincipal.FindByLockoutTime(Context, time, type);
        }

        /// <summary>
        /// Returns a PrincipalSearchResult<T> collection of ComputerPrincipal objects that have a logon time within the specified date and time range.
        /// </summary>
        /// <param name="time">A DateTime structure that is used in conjunction with the MatchType to filter search results.</param>
        /// <param name="type">The MatchType that specifies the type of comparison to use in the search.</param>
        /// <returns></returns>
        public PrincipalSearchResult<ComputerPrincipal> FindByLogonTime(DateTime time, MatchType type)
        {
            return ComputerPrincipal.FindByLogonTime(Context, time, type);
        }

        /// <summary>
        /// Returns a PrincipalSearchResult<T> collection of ComputerPrincipal objects that have a password set time within the specified date and time range.
        /// </summary>
        /// <param name="time">A DateTime structure that is used in conjunction with the MatchType to filter search results.</param>
        /// <param name="type">The MatchType that specifies the type of comparison to use in the search.</param>
        /// <returns></returns>
        public PrincipalSearchResult<ComputerPrincipal> FindByPasswordSetTime(DateTime time, MatchType type)
        {
            return ComputerPrincipal.FindByPasswordSetTime(Context, time, type);
        }
    }
}