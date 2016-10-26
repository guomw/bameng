using HotCoreUtils.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAMENG.CONFIG
{
    public class WebConfig
    {

        /// <summary>
        /// 数据库连接字符串（默认MssqlDBConnectionString）
        /// </summary>
        /// <returns>返回连接字符串</returns>
        public static string getConnectionString()
        {
            return ConfigHelper.MssqlDBConnectionString;
        }

        /// <summary>
        /// 判断判断是否debug模式
        /// </summary>
        /// <returns></returns>
        public static bool debugMode()
        {
            try
            {
                string debug = ConfigHelper.GetConfigString("debugMode", "false");
                if (!string.IsNullOrEmpty(debug))
                    return Convert.ToBoolean(debug);
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
