using System.Configuration;

namespace LdapLib.Config
{
    /// <summary>
    /// Represents the configuration section that provides access to LDAP-related settings.
    /// </summary>
    internal class LdapConfiguration : ConfigurationSection
    {
        /// <summary>
        /// Gets the instance of the LDAP configuration section from the configuration file.
        /// </summary>
        /// <returns>The instance of the LDAP configuration section.</returns>
        public static LdapConfiguration GetConfig()
        {
            return (LdapConfiguration)ConfigurationManager.GetSection("LdapConfiguration") ?? new LdapConfiguration();
        }

        /// <summary>
        /// Gets the collection of LDAP settings defined in the configuration.
        /// </summary>
        [ConfigurationProperty("LdapSettings")]
        public LdapSettingsCollection LdapSettings => (LdapSettingsCollection)this["LdapSettings"] ?? new LdapSettingsCollection();

        /// <summary>
        /// Gets the collection of LDAP container settings defined in the configuration.
        /// </summary>
        [ConfigurationProperty("LdapContainerSettings")]
        public LdapContainerSettingsCollection LdapContainerSettings => (LdapContainerSettingsCollection)this["LdapContainerSettings"] ?? new LdapContainerSettingsCollection();
    }
}
