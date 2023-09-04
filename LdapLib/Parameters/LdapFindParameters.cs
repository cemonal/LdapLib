namespace LdapLib.Parameters
{
    /// <summary>
    /// Represents parameters for LDAP find operations.
    /// </summary>
    public class LdapFindParameters
    {
        /// <summary>
        /// Gets or sets a StringCollection object that contains the set of properties to retrieve during the search.
        /// </summary>
        public string[] PropertiesToLoad { get; set; }

        /// <summary>
        /// Gets or sets the search filter string in LDAP format.
        /// </summary>
        public string Filter { get; set; }
    }
}