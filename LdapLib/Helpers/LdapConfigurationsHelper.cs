using System.Linq;
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

        public static LdapContainerSettingsCollection GetComputerContainerSettings()
        {
            return GetContainerSettings().Where(x => x.Type == ContainerTypes.Computer.ToString()) as LdapContainerSettingsCollection;
        }

        public static LdapContainerSettingsCollection GetGroupContainerSettings()
        {
            return GetContainerSettings().Where(x => x.Type == ContainerTypes.Group.ToString()) as LdapContainerSettingsCollection;
        }

        public static LdapContainerSettingsCollection GetUserContainerSettings()
        {
            return GetContainerSettings().Where(x => x.Type == ContainerTypes.User.ToString()) as LdapContainerSettingsCollection;
        }

        public static LdapContainerSettingsCollection GetContainerSettings()
        {
            var config = LdapConfiguration.GetConfig();
            return config.LdapContainerSettings;
        }
    }
}