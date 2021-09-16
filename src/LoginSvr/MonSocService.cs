using System.Threading;
using SystemModule;
using SystemModule.Sockets;

namespace LoginSvr
{
    /// <summary>
    /// Զ�̼�ط���δ���ƣ�
    /// </summary>
    public class MonSocService
    {
        private readonly ISocketServer socketServer;
        private Timer monThreandTime;
        private MasSocService _masSock;

        public MonSocService(MasSocService masSock)
        {
            _masSock = masSock;
            socketServer = new ISocketServer(ushort.MaxValue, 1024);
            socketServer.Init();
        }

        public void Start()
        {
            TConfig Config = LSShare.g_Config;
            socketServer.Start(Config.sMonAddr, Config.nMonPort);
            monThreandTime = new Timer(MonTimer, null, 5000, 20000);
        }

        private void MonTimer(object obj)
        {
            string sMsg = string.Empty;
            int nC = _masSock.m_ServerList.Count;
            for (var i = 0; i < _masSock.m_ServerList.Count; i++)
            {
                var msgServer = _masSock.m_ServerList[i];
                var sServerName = msgServer.sServerName;
                if (sServerName != "")
                {
                    sMsg = sMsg + sServerName + "/" + msgServer.nServerIndex + "/" + msgServer.nOnlineCount + "/";
                    if ((HUtil32.GetTickCount() - msgServer.dwKeepAliveTick) < 30000)
                    {
                        sMsg = sMsg + "���� ;";
                    }
                    else
                    {
                        sMsg = sMsg + "��ʱ ;";
                    }
                }
                else
                {
                    sMsg = "-/-/-/-;";
                }
            }
            var socketList = socketServer.GetSockets();
            for (var i = 0; i < socketList.Count; i++)
            {
                socketList[i].Socket.SendText(nC + ";" + sMsg);
            }
        }
    }
}