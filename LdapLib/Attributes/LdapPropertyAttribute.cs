using System;

namespace LdapLib.Attributes
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Property)]
    public class LdapPropertyAttribute : Attribute
    {
        /// <summary>
        /// Code
        /// </summary>
        public string Code { get; }

        public LdapPropertyAttribute(string code)
        {
            Code = code;
        }
    }
}