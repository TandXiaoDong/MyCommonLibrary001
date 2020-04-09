using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.ProtoBase;
using SuperSocket.SocketBase.Protocol;

namespace SuperSocket.ClientSocket.AppBase
{
   public  class MyPackageInfo:IPackageInfo
    {
        public MyPackageInfo(byte[] header, byte[] bodyBuffer)
        {
            //Key = ((header[0] * 256) + header[1]).ToString();
            Header = header;
            Data = bodyBuffer;
        }

        /// <summary>
        /// 服务器返回的字节数据头部
        /// </summary>
        public byte[] Header { get; set; }

        /// <summary>
        /// 服务器返回的字节数据
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 协议号对应自定义命令Name,会触发自定义命令
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 服务器返回的字符串数据
        /// </summary>
        public string Body
        {
            get
            {
                return Encoding.UTF8.GetString(Data);
            }
        }
    }
}
