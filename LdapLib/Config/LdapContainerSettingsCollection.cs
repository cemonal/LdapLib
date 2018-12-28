using System.Configuration;

namespace LdapLib.Config
{
    public class LdapContainerSettingsCollection : ConfigurationElementCollection
    {
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

        public LdapContainerSettingsElement this[object key] => BaseGet(key) as LdapContainerSettingsElement;

        protected override ConfigurationElement CreateNewElement()
        {
            return new LdapContainerSettingsElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (LdapContainerSettingsElement)element;
        }
    }
}