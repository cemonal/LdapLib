using LdapLib.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LdapLib.Helpers
{
    /// <summary>
    /// Provides helper methods for retrieving LDAP configuration settings and container settings.
    /// </summary>
    public static class LdapConfigurationsHelper
    {
        /// <summary>
        /// Retrieves the LDAP settings from the configuration.
        /// </summary>
        /// <returns>An instance of <see cref="LdapSettingsElement"/> containing LDAP configuration settings.</returns>
        public static LdapSettingsElement GetSettings()
        {
            LdapSettingsElement result = null;
            var config = LdapConfiguration.GetConfig();
            var settings = config.LdapSettings;

            if (settings.Count > 0)
                result = settings[0];

            return result;
        }

        /// <summary>
        /// Retrieves a list of LDAP container settings for computers.
        /// </summary>
        /// <returns>A list of <see cref="LdapContainerSettingsElement"/> for computer containers.</returns>
        public static List<LdapContainerSettingsElement> GetComputerContainerSettings()
        {
            return GetContainerSettings().Where(x => string.Equals(x.Type, ContainerTypes.Computer.ToString(), StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        /// <summary>
        /// Retrieves a list of LDAP container settings for groups.
        /// </summary>
        /// <returns>A list of <see cref="LdapContainerSettingsElement"/> for group containers.</returns>
        public static List<LdapContainerSettingsElement> GetGroupContainerSettings()
        {
            return GetContainerSettings().Where(x => string.Equals(x.Type, ContainerTypes.Group.ToString(), StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        /// <summary>
        /// Retrieves a list of LDAP container settings for users.
        /// </summary>
        /// <returns>A list of <see cref="LdapContainerSettingsElement"/> for user containers.</returns>
        public static List<LdapContainerSettingsElement> GetUserContainerSettings()
        {
            return GetContainerSettings().Where(x => string.Equals(x.Type, ContainerTypes.User.ToString(), StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        /// <summary>
        /// Retrieves the collection of LDAP container settings from the configuration.
        /// </summary>
        /// <returns>An instance of <see cref="LdapContainerSettingsCollection"/> containing LDAP container settings.</returns>
        public static LdapContainerSettingsCollection GetContainerSettings()
        {
            var config = LdapConfiguration.GetConfig();
            return config.LdapContainerSettings;
        }
    }
}
