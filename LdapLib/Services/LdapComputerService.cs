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
    }
}