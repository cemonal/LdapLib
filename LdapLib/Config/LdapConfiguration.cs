using System.Configuration;

namespace LdapLib.Config
{
    internal class LdapConfiguration : ConfigurationSection
    {
        public static LdapConfiguration GetConfig()
        {
            return (LdapConfiguration)ConfigurationManager.GetSection("LdapConfiguration") ?? new LdapConfiguration();
        }

        [ConfigurationProperty("LdapSettings")]
        public LdapSettingsCollection LdapSettings => (LdapSettingsCollection)this["LdapSettings"] ?? new LdapSettingsCollection();

        [ConfigurationProperty("LdapContainerSettings")]
        public LdapContainerSettingsCollection LdapContainerSettings => (LdapContainerSettingsCollection)this["LdapContainerSettings"] ?? new LdapContainerSettingsCollection();
    }
}