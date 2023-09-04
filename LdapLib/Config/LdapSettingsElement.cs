using System.Configuration;

namespace LdapLib.Config
{
    /// <summary>
    /// Represents a configuration element that defines settings for connecting to an LDAP server.
    /// </summary>
    public class LdapSettingsElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the domain associated with the LDAP server.
        /// </summary>
        [ConfigurationProperty("domain")]
        public string Domain => this["domain"] as string;

        /// <summary>
        /// Gets the password for authenticating with the LDAP server.
        /// </summary>
        [ConfigurationProperty("password", IsRequired = true)]
        public string Password => this["password"] as string;

        /// <summary>
        /// Gets the scope of the LDAP search operation (e.g., Subtree, Base).
        /// </summary>
        [ConfigurationProperty("scope", DefaultValue = "Subtree")]
        public string Scope => this["scope"] as string;

        /// <summary>
        /// Gets the LDAP server address.
        /// </summary>
        [ConfigurationProperty("server", IsRequired = true)]
        public string Server => this["server"] as string;

        /// <summary>
        /// Gets the username for authenticating with the LDAP server.
        /// </summary>
        [ConfigurationProperty("username", IsRequired = true)]
        public string Username => this["username"] as string;

        /// <summary>
        /// Gets the authentication type to use when connecting to the LDAP server (e.g., Secure, Anonymous).
        /// </summary>
        [ConfigurationProperty("authenticationType", DefaultValue = "Secure")]
        public string AuthenticationType => this["authenticationType"] as string;
    }
}
