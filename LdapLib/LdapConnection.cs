using LdapLib.Config;
using LdapLib.Helpers;
using System;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Security.Authentication;

namespace LdapLib
{
    public sealed class LdapConnection : IDisposable, ICloneable
    {
        private string Username { get; }
        private string Password { get; }

        internal PrincipalContext Context { get; }
        internal DirectoryEntry DirectoryEntry { get; }
        internal LdapSettingsElement Settings { get; }

        public LdapConnection(string container)
        {
            var settings = LdapConfigurationsHelper.GetSettings();
            Settings = settings ?? throw new ConfigurationErrorsException("Please check your LdapConfiguration on app / web.config");

            Username = settings.Username;
            Password = settings.Password;

            Context = new PrincipalContext(ContextType.Domain, settings.Server, container, string.IsNullOrEmpty(settings.Domain) ? Username : settings.Domain + "\\" + settings.Username, Password);

            if (!ValidateCredentials())
            {
                Context = null;
                throw new InvalidCredentialException("Invalid credentials!");
            }

            DirectoryEntry = new DirectoryEntry($"LDAP://{Settings.Server}/{container}", string.IsNullOrEmpty(settings.Domain) ? Username : settings.Domain + "\\" + settings.Username, Password, settings.UseSsl ? AuthenticationTypes.SecureSocketsLayer : AuthenticationTypes.Secure);
        }

        public LdapConnection(string server, string container, string username, string password, AuthenticationTypes authenticationType)
        {
            Username = username;
            Password = password;

            Context = new PrincipalContext(ContextType.Domain, server, container, username, password);

            if (!ValidateCredentials())
            {
                Context = null;
                throw new InvalidCredentialException("Invalid credentials!");
            }

            if (!server.StartsWith("LDAP://"))
                server = string.Concat("LDAP://", server);

            DirectoryEntry = new DirectoryEntry($"{server}/{container}", username, password, authenticationType);
        }

        public LdapConnection(string server, string container, string domain, string username, string password, AuthenticationTypes authenticationType)
        {
            Username = username;
            Password = password;

            Context = new PrincipalContext(ContextType.Domain, server, container, domain + "\\" + username, password);

            if (!ValidateCredentials())
            {
                Context = null;
                throw new InvalidCredentialException("Invalid credentials!");
            }

            if (!server.StartsWith("LDAP://"))
                server = string.Concat("LDAP://", server);

            DirectoryEntry = new DirectoryEntry($"{server}/{container}", domain + "\\" + username, password, authenticationType);
        }

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context?.Dispose();
                    DirectoryEntry?.Dispose();
                }
            }

            _disposed = true;
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool ValidateCredentials()
        {
            if (Context == null) throw new InvalidOperationException("There is no connection!");
            
            return Context.ValidateCredentials(Username, Password);
        }

        public object Clone()
        {
            return MemberwiseClone() as LdapConnection ?? throw new InvalidOperationException("LdapConnection couldn't be copied because object was null.");
        }
    }
}