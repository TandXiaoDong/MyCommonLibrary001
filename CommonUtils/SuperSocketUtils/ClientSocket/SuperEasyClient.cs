using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using SuperSocket.ClientEngine;
using CommonUtils.ByteHelper;
using CommonUtils.Logger;
using SuperSocket.ClientSocket.AppBase;
using System.Configuration;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.ProtoBase;

namespace SuperSocket.ClientSocket
{
    public class SuperEasyClient
    {
        public delegate void NoticeConnect(bool IsConnect,string tipMessage);
        public delegate void NoticeMessage(MyPackageInfo packageInfo);

        public static event NoticeConnect NoticeConnectEvent;
        public static event NoticeMessage NoticeMessageEvent;

        public static EasyClient<MyPackageInfo> client;
        public static string serverUrl = "";
        public static string serverPort = "";

        private static void SendNoticeMessage(MyPackageInfo packageInfo)
        {
            NoticeMessageEvent(packageInfo);
        }

        private static void SendNoticeConnect(bool IsConnect,string tipMessage)
        {
            NoticeConnectEvent(IsConnect,tipMessage);
        }

        /// <summary>
        /// 连接服务器
        /// </summary>
        public static async void ConnectServer()
        {
            //if (client != null || !client.IsConnected)
            //    return;
            client = new EasyClient<MyPackageInfo>();

            LogHelper.Log.Info("开始连接服务...");
            client = new EasyClient<MyPackageInfo>();
            client.ReceiveBufferSize = 4100;
            client.Initialize(new MyReceiveFilter());
            client.Connected += OnClientConnected;
            client.NewPackageReceived += OnPagckageReceived;
            client.Error += OnClientError;
            client.Closed += OnClientClosed;

            //var webSocketUrl = System.Configuration.ConfigurationManager.AppSettings["WebSocketURL"].ToString();//ip
            //var webSocketPort = System.Configuration.ConfigurationManager.AppSettings["WebSocketPort"].ToString();//port
            var connected = await client.ConnectAsync(new IPEndPoint(IPAddress.Parse(serverUrl), int.Parse(serverPort)));
        }

        private static void OnClientClosed(object sender, EventArgs e)
        {
            int attmpts = 1;
            SendNoticeConnect(client.IsConnected,"已断开与设备的连接");
            LogHelper.Log.Info("已断开与服务的连接...");
            //do
            //{
            //    LogHelper.Log.Info("尝试重新连接，重连次数为"+attmpts);
            //    Thread.Sleep(3000);
            //    ConnectServer();
            //    attmpts++;
            //} while (!client.IsConnected && attmpts > 3);
            //LogHelper.Log.Info("重新连接成功，退出循环");
        }

        private static void OnClientError(object sender, ErrorEventArgs e)
        {
            LogHelper.Log.Info("客户端错误：" + e.Exception.Message);
            SendNoticeConnect(client.IsConnected,e.Exception.Message);
        }

        private static void OnPagckageReceived(object sender, PackageEventArgs<MyPackageInfo> e)
        {
            if (e.Package.Data.Length > 1 || e.Package.Header.Length > 0)
            {
                SendNoticeMessage(e.Package);
            }
            //LogHelper.Log.Info("收到服务消息【Byte】:"+"head:"+BitConverter.ToString(e.Package.Header)+" body:"+BitConverter.ToString(e.Package.Data));
        }

        private static void OnClientConnected(object sender, EventArgs e)
        {
            SendNoticeConnect(client.IsConnected,"已连接到设备");
            LogHelper.Log.Info("已连接到服务器...");
        }

        /// <summary>
        /// 发送命令和消息到服务器
        /// </summary>
        /// <param name="command"></param>
        /// <param name="message"></param>
        public static void SendMessage(DeviceFunCodeEnum command, string message)
        {
            if (client == null || !client.IsConnected || message.Length <= 0)
                return;
            var response = BitConverter.GetBytes((ushort)command).Reverse().ToList();
            var arr = System.Text.Encoding.UTF8.GetBytes(message);
            response.AddRange(BitConverter.GetBytes((ushort)arr.Length).Reverse().ToArray());
            response.AddRange(arr);
            client.Send(response.ToArray());
            LogHelper.Log.Info($"发送{command.GetDescription()}数据：" + message + " byte:" + BitConverter.ToString(response.ToArray()));
        }

        public static void SendMessage(DeviceFunCodeEnum command, byte[] message)
        {
            if (client == null || !client.IsConnected)
                return;
            var response = BitConverter.GetBytes((ushort)command).Reverse().ToList();
            if (message.Length > 0)
            {
                response.AddRange(BitConverter.GetBytes((ushort)message.Length).Reverse().ToArray());
                response.AddRange(message);
            }
            client.Send(response.ToArray());
            LogHelper.Log.Info($"发送指令：" + BitConverter.ToString(response.ToArray()));
        }
    }
}
