using System.Configuration;

namespace LdapLib.Config
{
    /// <summary>
    /// Represents a configuration element that defines settings for an LDAP container, such as computer, group, or user.
    /// </summary>
    public class LdapContainerSettingsElement : ConfigurationElement
    {
        /// <summary>
        /// Gets or sets the key associated with the LDAP container settings.
        /// </summary>
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key => this["key"] as string;

        /// <summary>
        /// Gets or sets the LDAP path associated with the container.
        /// </summary>
        [ConfigurationProperty("path", IsRequired = true)]
        public string Path => this["path"] as string;

        /// <summary>
        /// Gets or sets the type of the LDAP container, such as computer, group, or user.
        /// </summary>
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type => this["type"] as string;
    }
}
