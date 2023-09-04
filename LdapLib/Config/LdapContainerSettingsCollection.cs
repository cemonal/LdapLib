using System.Collections.Generic;
using System.Configuration;

namespace LdapLib.Config
{
    /// <summary>
    /// Represents a collection of LDAP container settings elements used for configuring various types of LDAP containers.
    /// </summary>
    public class LdapContainerSettingsCollection : ConfigurationElementCollection, IEnumerable<LdapContainerSettingsElement>
    {
        /// <summary>
        /// Gets or sets an LDAP container settings element at the specified index in the collection.
        /// </summary>
        /// <param name="index">The index of the element to get or set.</param>
        /// <returns>The LDAP container settings element at the specified index.</returns>
        public LdapContainerSettingsElement this[int index]
        {
            get => BaseGet(index) as LdapContainerSettingsElement;
            set
            {
                if (BaseGet(index) != null)
                    BaseRemoveAt(index);

                BaseAdd(index, value);
            }
        }

        /// <summary>
        /// Gets the LDAP container settings element with the specified key.
        /// </summary>
        /// <param name="key">The key associated with the LDAP container settings element.</param>
        /// <returns>The LDAP container settings element with the specified key.</returns>
        public LdapContainerSettingsElement this[object key] => BaseGet(key) as LdapContainerSettingsElement;

        /// <summary>
        /// Creates a new instance of the LDAP container settings element.
        /// </summary>
        /// <returns>A new instance of the LDAP container settings element.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new LdapContainerSettingsElement();
        }

        /// <summary>
        /// Gets the key for the specified LDAP container settings element.
        /// </summary>
        /// <param name="element">The LDAP container settings element.</param>
        /// <returns>The key for the specified LDAP container settings element.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return (LdapContainerSettingsElement)element;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the LDAP container settings collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public new IEnumerator<LdapContainerSettingsElement> GetEnumerator()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return (LdapContainerSettingsElement)BaseGet(i);
            }
        }
    }
}
