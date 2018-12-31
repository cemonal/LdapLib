using System;
using System.Collections.Generic;
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

        public static List<LdapContainerSettingsElement> GetComputerContainerSettings()
        {
            return GetContainerSettings().Where(x => string.Equals(x.Type, ContainerTypes.Computer.ToString(), StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        public static List<LdapContainerSettingsElement> GetGroupContainerSettings()
        {
            return GetContainerSettings().Where(x => string.Equals(x.Type, ContainerTypes.Group.ToString(), StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        public static List<LdapContainerSettingsElement> GetUserContainerSettings()
        {
            return GetContainerSettings().Where(x => string.Equals(x.Type, ContainerTypes.User.ToString(), StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        public static LdapContainerSettingsCollection GetContainerSettings()
        {
            var config = LdapConfiguration.GetConfig();
            return config.LdapContainerSettings;
        }
    }
}