namespace LdapLib.Parameters
{
    public class LdapFindParameters
    {
        /// <summary>
        /// A StringCollection object that contains the set of properties to retrieve during the search.
        /// </summary>
        public string[] PropertiesToLoad { get; set; }

        /// <summary>
        /// The search filter string in LDAP format.
        /// </summary>
        public string Filter { get; set; }
    }
}