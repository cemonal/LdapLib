using System.Configuration;

namespace LdapLib.Config
{
    /// <summary>
    /// Represents a collection of LDAP settings elements used for configuring connections to LDAP servers.
    /// </summary>
    public class LdapSettingsCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Gets or sets an LDAP settings element at the specified index in the collection.
        /// </summary>
        /// <param name="index">The index of the element to get or set.</param>
        /// <returns>The LDAP settings element at the specified index.</returns>
        public LdapSettingsElement this[int index]
        {
            get => BaseGet(index) as LdapSettingsElement;
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }

        /// <summary>
        /// Creates a new instance of the LDAP settings element.
        /// </summary>
        /// <returns>A new instance of the LDAP settings element.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new LdapSettingsElement();
        }

        /// <summary>
        /// Gets the key for the specified LDAP settings element.
        /// </summary>
        /// <param name="element">The LDAP settings element.</param>
        /// <returns>The key for the specified LDAP settings element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (LdapSettingsElement)element;
        }
    }
}
