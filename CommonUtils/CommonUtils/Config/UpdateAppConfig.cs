using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace CommonUtils.Config
{
    class UpdateAppConfig
    {
        ///<summary>     
        ///依据连接串名字connectionName返回数据连接字符串      
        ///</summary>     
        ///<param name="connectionName"></param>     
        ///<returns></returns>     
        private static string GetConnectionStringsConfig(string connectionName)
        {
            string connectionString =
            ConfigurationManager.ConnectionStrings[connectionName].ConnectionString.ToString();

            return connectionString;
        }

        /// <summary>
        /// 更新AppSet配置文件
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private static void UpdateAppSetValue(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] != null)
            {
                config.AppSettings.Settings[key].Value = value;
            }
            else
            {
                config.AppSettings.Settings.Add(key, value);
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

       private static void UpdateConnectStrValue(string key, string value)
        {
            System.Configuration.Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            if (config.ConnectionStrings.ConnectionStrings[key].ToString() == value)
            {
                return;
            }
            config.ConnectionStrings.ConnectionStrings[key].ConnectionString = value;
            config.Save(System.Configuration.ConfigurationSaveMode.Modified);
            System.Configuration.ConfigurationManager.RefreshSection("ConnectionStrings");
        }
    }
}
