using LdapLib.Attributes;

namespace LdapLib.Test
{
    internal class LdapUser
    {
        [LdapProperty("cn")]
        public string Cn { get; set; }

        [LdapProperty("department")]
        public string Department { get; set; }
        
        [LdapProperty("givenname")]
        public string GivenName { get; set; }

        [LdapProperty("mail")]
        public string Mail { get; set; }
        
        private string _manager;
        [LdapProperty("manager")]
        public string Manager
        {
            get => _manager;
            set => _manager = FixManager(value);
        }

        private static string FixManager(string manager)
        {
            return string.IsNullOrEmpty(manager) ? "" : manager.Split(',')[0].Replace("CN=", "");
        }

        [LdapProperty("mobile")]
        public string Mobile { get; set; }

        [LdapProperty("samaccountname")]
        public string SamAccountname { get; set; }

        [LdapProperty("sn")]
        public string Surname { get; set; }

        [LdapProperty("streetaddress")]
        public string StreetAddress { get; set; }

        [LdapProperty("telephonenumber")]
        public string TelephoneNumber { get; set; }

        [LdapProperty("title")]
        public string Title { get; set; }

        [LdapProperty("useraccountcontrol")]
        public int UserAccountControl { get; set; }
    }
}