

using System.Configuration;
using System.Web.Configuration;

namespace CyAwareWebApi.Utils
{
    public class Configurator
    {
        private static Configurator instance;

        private Configurator()
        {
        }

        public static Configurator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Configurator();
                }
                return instance;
            }
        }

        public string getValue(string key)
        {
            string configValue = "";

            configValue = WebConfigurationManager.AppSettings[key];

            return configValue;
        }
    }
}