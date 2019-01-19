using System;
using System.DirectoryServices.AccountManagement;

namespace LdapLib.Repository
{
    public interface ILdapComputerService : ILdapLibRepository<ComputerPrincipal>
    {
        PrincipalSearchResult<ComputerPrincipal> FindByBadPasswordAttempt(DateTime time, MatchType type);
        PrincipalSearchResult<ComputerPrincipal> FindByExpirationTime(DateTime time, MatchType type);
        PrincipalSearchResult<ComputerPrincipal> FindByLockoutTime(DateTime time, MatchType type);
        PrincipalSearchResult<ComputerPrincipal> FindByLogonTime(DateTime time, MatchType type);
        PrincipalSearchResult<ComputerPrincipal> FindByPasswordSetTime(DateTime time, MatchType type);
    }
}