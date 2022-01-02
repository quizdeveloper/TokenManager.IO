using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenManager.Core.Utils;

namespace TokenManager.Core.AppSetting
{
    public class AppSettingUtils
    {
        public static object GetAppConfig(string pstrKey, string pstrType)
        {
            try
            {
                var objAppReader = new AppSettingsReader();
                return objAppReader.GetValue(pstrKey, Type.GetType(pstrType));
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Read string value from app.config
        /// </summary>
        /// <param name="pstrKey"></param>
        /// <returns></returns>
        public static string GetAppConfig(string pstrKey)
        {
            try
            {
                var objAppReader = new AppSettingsReader();
                return objAppReader.GetValue(pstrKey, typeof(string)).ToString();
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static bool GetBool(string key, bool defaultValue = false)
        {
            try
            {
                return (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]) && ConfigurationManager.AppSettings[key].ToBool());
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string GetConnection(string key, string defaultValue = "")
        {
            try
            {
                return (!string.IsNullOrEmpty(ConfigurationManager.ConnectionStrings[key].ConnectionString) ? ConfigurationManager.ConnectionStrings[key].ConnectionString : defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static int GetInt32(string key, int defaultValue = 0)
        {
            try
            {
                return (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]) ? ConfigurationManager.AppSettings[key].ToInt() : defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static long GetInt64(string key, long defaultValue = 0L)
        {
            try
            {
                return (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]) ? ConfigurationManager.AppSettings[key].ToLong() : defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string GetString(string key, string defaultValue = "")
        {
            try
            {
                return (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[key]) ? ConfigurationManager.AppSettings[key].Trim() : defaultValue);
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
