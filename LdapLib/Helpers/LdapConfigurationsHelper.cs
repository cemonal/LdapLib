using LdapLib.Config;

namespace LdapLib.Helpers
{
    public static class LdapConfigurationsHelper
    {
        public static LdapSettingsElement GetSettings()
        {
            LdapSettingsElement result = null;
            var config = LdapConfiguration.GetConfig();
            var settings = config.LdapSettings;

            if (settings.Count > 0)
                result = settings[0];

            return result;
        }
    }
}
