using LdapLib.Config;
using LdapLib.Helpers;
using System;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Security.Authentication;

namespace LdapLib
{
    public class LdapConnection : IDisposable
    {
        private string Username { get; }
        private string Password { get; }
        protected internal PrincipalContext Context { get; }
        protected internal DirectoryEntry DirectoryEntry { get; }
        protected internal LdapSettingsElement Settings { get; }

        public LdapConnection(string container)
        {
            var settings = LdapConfigurationsHelper.GetSettings();
            Settings = settings ?? throw new ConfigurationErrorsException("Please check your LdapConfiguration on app / web.config");

            Username = settings.Username;
            Password = settings.Password;

            Context = new PrincipalContext(ContextType.Domain, settings.Server, container, Username, Username);

            if (!ValidateCredentials())
            {
                Context = null;
                throw new InvalidCredentialException("Credentials are invalid!");
            }

            DirectoryEntry = new DirectoryEntry($"LDAP://{settings.Server}/{container}", Username, Password, settings.UseSsl ? AuthenticationTypes.SecureSocketsLayer : AuthenticationTypes.Secure);
        }

        public LdapConnection(string server, string container, string username, string password, AuthenticationTypes authenticationType)
        {
            Username = username;
            Password = password;

            Context = new PrincipalContext(ContextType.Domain, server, container, username, password);

            if (!ValidateCredentials())
            {
                Context = null;
                throw new InvalidCredentialException("Credentials are invalid!");
            }

            DirectoryEntry = new DirectoryEntry($"LDAP://{server}/{container}", username, password, authenticationType);
        }

        public void Dispose()
        {
            Context?.Dispose();
            DirectoryEntry?.Dispose();
        }

        private bool ValidateCredentials()
        {
            if (Context == null) throw new Exception("There is no connection!");

            return Context.ValidateCredentials(Username, Password);
        }
    }
}
