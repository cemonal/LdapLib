using System.Configuration;

namespace LdapLib.Config
{
    public class LdapContainerSettingsElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key => this["key"] as string;

        [ConfigurationProperty("path", IsRequired = true)]
        public string Path => this["path"] as string;

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type => this["type"] as string;
    }
}