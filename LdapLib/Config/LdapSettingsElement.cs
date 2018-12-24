using System.Configuration;

namespace LdapLib.Config
{
    public class LdapSettingsElement : ConfigurationElement
    {
        [ConfigurationProperty("domain")]
        public string Domain => this["domain"] as string;
        
        [ConfigurationProperty("password", IsRequired = true)]
        public string Password => this["password"] as string;

        [ConfigurationProperty("scope", DefaultValue = "Subtree")]
        public string Scope => this["scope"] as string;

        [ConfigurationProperty("server", IsRequired = true)]
        public string Server => this["server"] as string;
        
        [ConfigurationProperty("username", IsRequired = true)]
        public string Username => this["username"] as string;

        [ConfigurationProperty("authenticationType", DefaultValue = "Secure")]
        public string AuthenticationType => this["authenticationType"] as string;
    }
}