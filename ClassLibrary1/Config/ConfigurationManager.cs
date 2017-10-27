using System.Web.Configuration;

namespace ClassLibrary1
{
    public interface IConfigurationManager
    {
        string GetAppSetting(string key);
        void UpdateAppSetting(string key, string newValue);
    }

    /// <summary>
    /// A wrapper to get web.config configuration settings
    /// </summary>
    /// <seealso cref="IConfigurationManager" />
    public class WebConfigConfigurationManager : IConfigurationManager
    {
        public string GetAppSetting(string key)
        {
            return WebConfigurationManager.AppSettings[key];
        }

        public void UpdateAppSetting(string key, string newValue)
        {
            WebConfigurationManager.AppSettings[key] = newValue;
        }
    }
}