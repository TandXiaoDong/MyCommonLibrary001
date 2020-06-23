using System.Runtime.InteropServices;
using System.Web;

namespace SparkSoft.Platform.Common.Http
{
    /// <summary>
    ///   网络操作工具类
    /// </summary>
    public static class NetworkUtils
    {
        [DllImport("wininet")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

        /// <summary> 
        ///   检测本机是否联网 
        /// </summary> 
        /// <returns></returns> 
        public static bool IsConnectedInternet()
        {
            int i;
            return InternetGetConnectedState(out i, 0);
        }

        /// <summary>
        ///  提取开启代理/cdn服务后的客户端真实IP
        /// </summary>
        /// <returns></returns>
        public static string GetRealIp()
        {
            string ip;
            string xForwardedFor = HttpContext.Current.Request.Headers["X-Forwarded-For"];
            if (!string.IsNullOrWhiteSpace(xForwardedFor))
            {
                ip = xForwardedFor;
            }
            else
            {
                string cfConnectingIp = HttpContext.Current.Request.Headers["CF-Connecting-IP"];
                ip = !string.IsNullOrWhiteSpace(cfConnectingIp) ? cfConnectingIp : HttpContext.Current.Request.UserHostAddress;
            }
            return ip;
        }
    }
}