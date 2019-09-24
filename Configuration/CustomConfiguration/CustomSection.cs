using System.Configuration;

namespace CustomConfiguration
{
    class CustomSection : ConfigurationSection
    {
        [ConfigurationProperty("element")]
        public CustomElement Element
        {
            get { return ((CustomElement)(base["element"])); }
            set { base["element"] = value; }
        }
    }
}
