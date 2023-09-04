using LdapLib.Repository;
using System;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Services
{
    /// <summary>
    /// Represents a service for interacting with computer objects in an LDAP directory.
    /// </summary>
    public interface ILdapComputerService : ILdapLibRepository<ComputerPrincipal>
    {
        /// <summary>
        /// Returns a collection of <see cref="ComputerPrincipal"/> objects that have had bad password attempts within the specified date and time range.
        /// </summary>
        /// <param name="time">A <see cref="DateTime"/> structure that is used in conjunction with the <see cref="MatchType"/> to find computers with bad password attempts.</param>
        /// <param name="type">The <see cref="MatchType"/> that specifies the type of comparison to use in the search.</param>
        /// <returns>A collection of <see cref="ComputerPrincipal"/> objects matching the search criteria.</returns>
        PrincipalSearchResult<ComputerPrincipal> FindByBadPasswordAttempt(DateTime time, MatchType type);

        /// <summary>
        /// Returns a collection of <see cref="ComputerPrincipal"/> objects that have an expiration time within the specified date and time range.
        /// </summary>
        /// <param name="time">A <see cref="DateTime"/> structure that is used in conjunction with the <see cref="MatchType"/> to filter search results.</param>
        /// <param name="type">The <see cref="MatchType"/> that specifies the type of comparison to use in the search.</param>
        /// <returns>A collection of <see cref="ComputerPrincipal"/> objects matching the search criteria.</returns>
        PrincipalSearchResult<ComputerPrincipal> FindByExpirationTime(DateTime time, MatchType type);

        /// <summary>
        /// Returns a collection of <see cref="ComputerPrincipal"/> objects that have a lockout time within the specified date and time range.
        /// </summary>
        /// <param name="time">A <see cref="DateTime"/> structure that is used in conjunction with the <see cref="MatchType"/> to filter search results.</param>
        /// <param name="type">The <see cref="MatchType"/> that specifies the type of comparison to use in the search.</param>
        /// <returns>A collection of <see cref="ComputerPrincipal"/> objects matching the search criteria.</returns>
        PrincipalSearchResult<ComputerPrincipal> FindByLockoutTime(DateTime time, MatchType type);

        /// <summary>
        /// Returns a collection of <see cref="ComputerPrincipal"/> objects that have a logon time within the specified date and time range.
        /// </summary>
        /// <param name="time">A <see cref="DateTime"/> structure that is used in conjunction with the <see cref="MatchType"/> to filter search results.</param>
        /// <param name="type">The <see cref="MatchType"/> that specifies the type of comparison to use in the search.</param>
        /// <returns>A collection of <see cref="ComputerPrincipal"/> objects matching the search criteria.</returns>
        PrincipalSearchResult<ComputerPrincipal> FindByLogonTime(DateTime time, MatchType type);

        /// <summary>
        /// Returns a collection of <see cref="ComputerPrincipal"/> objects that have a password set time within the specified date and time range.
        /// </summary>
        /// <param name="time">A <see cref="DateTime"/> structure that is used in conjunction with the <see cref="MatchType"/> to filter search results.</param>
        /// <param name="type">The <see cref="MatchType"/> that specifies the type of comparison to use in the search.</param>
        /// <returns>A collection of <see cref="ComputerPrincipal"/> objects matching the search criteria.</returns>
        PrincipalSearchResult<ComputerPrincipal> FindByPasswordSetTime(DateTime time, MatchType type);
    }
}
