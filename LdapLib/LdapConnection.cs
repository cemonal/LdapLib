using LdapLib.Config;
using LdapLib.Helpers;
using System;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Security.Authentication;

namespace LdapLib
{
    /// <summary>
    /// Represents a connection to an LDAP (Lightweight Directory Access Protocol) server.
    /// </summary>
    public sealed class LdapConnection : IDisposable, ICloneable
    {
        private string Username { get; }
        private string Password { get; }

        internal PrincipalContext Context { get; }
        internal DirectoryEntry DirectoryEntry { get; }
        internal LdapSettingsElement Settings { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LdapConnection"/> class based on container information.
        /// </summary>
        /// <param name="container">The container to connect to.</param>
        public LdapConnection(string container)
        {
            var settings = LdapConfigurationsHelper.GetSettings();
            Settings = settings ?? throw new ConfigurationErrorsException("Please check your LdapConfiguration on app / web.config");

            Username = settings.Username;
            Password = settings.Password;

            // Create a PrincipalContext for authentication and access control.
            Context = new PrincipalContext(ContextType.Domain, settings.Server.Replace("LDAP://", ""), container, string.IsNullOrEmpty(settings.Domain) ? Username : settings.Domain + "\\" + settings.Username, Password);

            if (!ValidateCredentials())
            {
                Context = null;
                throw new InvalidCredentialException("Invalid credentials! Please check your configurations.");
            }

            var server = Settings.Server;

            if (!server.StartsWith("LDAP://"))
                server = string.Concat("LDAP://", server);

            // Create a DirectoryEntry for directory operations.
            DirectoryEntry = new DirectoryEntry($"{server}/{container}", string.IsNullOrEmpty(settings.Domain) ? Username : settings.Domain + "\\" + settings.Username, Password, (AuthenticationTypes)Enum.Parse(typeof(AuthenticationTypes), settings.AuthenticationType, true));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LdapConnection"/> class with custom connection parameters.
        /// </summary>
        /// <param name="server">The LDAP server address.</param>
        /// <param name="container">The container to connect to.</param>
        /// <param name="domain">The domain for the authentication credentials.</param>
        /// <param name="username">The username for authentication.</param>
        /// <param name="password">The password for authentication.</param>
        /// <param name="authenticationType">The type of authentication to use.</param>
        public LdapConnection(string server, string container, string domain, string username, string password, AuthenticationTypes authenticationType)
        {
            Username = username;
            Password = password;

            // Create a PrincipalContext for authentication and access control.
            Context = new PrincipalContext(ContextType.Domain, server.Replace("LDAP://", ""), container, domain + "\\" + username, password);

            if (!ValidateCredentials())
            {
                Context = null;
                throw new InvalidCredentialException("Invalid credentials!");
            }

            if (!server.StartsWith("LDAP://"))
                server = string.Concat("LDAP://", server);

            // Create a DirectoryEntry for directory operations.
            DirectoryEntry = new DirectoryEntry($"{server}/{container}", domain + "\\" + username, password, authenticationType);
        }

        private bool _disposed;

        /// <summary>
        /// Disposes of resources associated with the <see cref="LdapConnection"/> instance.
        /// </summary>
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

        /// <summary>
        /// Validates the credentials used for the LDAP connection.
        /// </summary>
        /// <returns>Returns true if the credentials are valid; otherwise, false.</returns>
        private bool ValidateCredentials()
        {
            if (Context == null) throw new InvalidOperationException("There is no connection!");

            return Context.ValidateCredentials(Username, Password);
        }

        /// <summary>
        /// Creates a copy of the <see cref="LdapConnection"/> instance.
        /// </summary>
        /// <returns>A new <see cref="LdapConnection"/> instance with the same property values as the original.</returns>
        public object Clone()
        {
            return MemberwiseClone() as LdapConnection ?? throw new InvalidOperationException("LdapConnection couldn't be copied because object was null.");
        }
    }
}