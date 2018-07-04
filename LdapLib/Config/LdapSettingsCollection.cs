using System.Configuration;

namespace LdapLib.Config
{
    public class LdapSettingsCollection : ConfigurationElementCollection
    {
        public LdapSettingsElement this[int index]
        {
            get
            {
                return BaseGet(index) as LdapSettingsElement;
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new LdapSettingsElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((LdapSettingsElement)element);
        }
    }
}
