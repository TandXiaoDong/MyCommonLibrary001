using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase.Config;
using System.Configuration;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using SuperSocketServer.AppBase;
using CommonUtils.Logger;
using System.Threading;

namespace SuperSocket.SuperSocketServer
{
    public class SocketServerControl
    {
        private static MyServer appServer;
        private static SuperSocketConfig superConfig;
        private static string sessionID;
        public static byte[] message = new byte[] { };

        /// <summary>
        /// 启动socket 监听
        /// </summary>
        public static void StartServer()
        {
            appServer = new MyServer();
            superConfig = new SuperSocketConfig();
            //Setup the appServer
            if (!appServer.Setup(1001)) //Setup with listening port
            {
                LogHelper.Log.Info("Failed to setup!");
                return;
            }

            //Try to start the appServer
            if (!appServer.Start())
            {
                LogHelper.Log.Info("Failed to start!");
                return;
            }
            appServer.NewSessionConnected += MyServer_NewSessionConnected;
            appServer.SessionClosed += MyServer_SessionClosed;

            LogHelper.Log.Info("SuperSocket Server Start Success.....");
        }

        private static void MyServer_NewSessionConnected(MySession session)
        {
            var count = session.AppServer.SessionCount;
            sessionID = session.SessionID;
            LogHelper.Log.Info($"客户端连接进入，连接数量 {count} SessionID:{session.SessionID} RemoteEndPoint: {session.RemoteEndPoint} canID:{session.CaId}");

            Thread thread = new Thread(()=>
            {
                while (true)
                {
                    if(message.Length > 0)
                        SendMessage(message);
                    Thread.Sleep(50);
                }
            });
            thread.Start();
            thread.IsBackground = true;
        }

        private static void MyServer_SessionClosed(MySession session, CloseReason closeReason)
        {
            var count = session.AppServer.SessionCount;
            LogHelper.Log.Info($"服务端 失去 来自客户端的连接,sessionID:" + session.SessionID + " closeReason:" + closeReason.ToString() + " count:" + count);
        }

        public static void StopServer()
        {
            appServer.Stop();
        }

        public static void SendMessage(byte[] b)
        {
            var session = appServer.GetSessionByID(sessionID);
            if (session != null)
                session.Send(b,0,b.Length);
        }
    }
}
