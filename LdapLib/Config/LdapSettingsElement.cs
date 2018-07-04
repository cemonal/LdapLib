using System.Configuration;

namespace LdapLib.Config
{
    public class LdapSettingsElement : ConfigurationElement
    {
        [ConfigurationProperty("domain")]
        public string Domain => this["domain"] as string;

        [ConfigurationProperty("groupContainer")]
        public string GroupContainer => this["groupContainer"] as string;

        [ConfigurationProperty("groupFilter", IsRequired = true)]
        public string GroupFilter => this["groupFilter"] as string;

        [ConfigurationProperty("groupObjectClass", DefaultValue = "group")]
        public string GroupObjectClass => this["groupObjectClass"] as string;

        [ConfigurationProperty("password", IsRequired = true)]
        public string Password => this["password"] as string;

        [ConfigurationProperty("scope", DefaultValue = "Subtree")]
        public string Scope => this["scope"] as string;

        [ConfigurationProperty("server", IsRequired = true)]
        public string Server => this["server"] as string;

        [ConfigurationProperty("userContainer", IsRequired = true)]
        public string UserContainer => this["userContainer"] as string;

        [ConfigurationProperty("userFilter", IsRequired = true)]
        public string UserFilter => this["userFilter"] as string;

        [ConfigurationProperty("username", IsRequired = true)]
        public string Username => this["username"] as string;

        [ConfigurationProperty("userObjectClass", DefaultValue = "user")]
        public string UserObjectClass => this["userObjectClass"] as string;

        [ConfigurationProperty("useSSL", DefaultValue = false)]
        public bool UseSsl
        {
            get
            {
                return (bool)this["useSSL"];
            }
            set
            {
                this["useSSL"] = value;
            }
        }
    }
}