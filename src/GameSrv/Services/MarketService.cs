﻿using System.Net;
using System.Net.Sockets;
using NLog;
using SystemModule.Packets.ServerPackets;
using SystemModule.Sockets.AsyncSocketClient;
using SystemModule.Sockets.Event;

namespace GameSrv.Services
{
    public class MarketService
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly ScoketClient _clientScoket;
        private byte[] ReceiveBuffer { get; set; }
        private int BuffLen { get; set; }

        public MarketService()
        {
            _clientScoket = new ScoketClient(new IPEndPoint(IPAddress.Parse(M2Share.Config.MarketSrvAddr), M2Share.Config.MarketSrvPort), 4096);
            _clientScoket.OnConnected += MarketScoketConnected;
            _clientScoket.OnDisconnected += MarketScoketDisconnected;
            _clientScoket.OnReceivedData += MarketSocketRead;
            _clientScoket.OnError += MarketSocketError;
            ReceiveBuffer = new byte[10 * 2048];
        }
        
        public void Start()
        {
            if (M2Share.Config.EnableMarket)
            {
                _clientScoket.Connect();
            }
        }

        public void Stop()
        {
            if (M2Share.Config.EnableMarket)
            {
                _clientScoket.Disconnect();
            }
        }

        public bool IsConnected => _clientScoket.IsConnected;

        public void CheckConnected()
        {
            if (!M2Share.Config.EnableMarket)
            {
                return;
            }
            if (_clientScoket.IsConnected)
            {
                return;
            }
            if (_clientScoket.IsBusy)
            {
                return;
            }
            _clientScoket.Connect(M2Share.Config.sDBAddr, M2Share.Config.nDBPort);
        }

        public bool SendRequest<T>(int queryId, ServerRequestMessage message, T packet)
        {
            if (!_clientScoket.IsConnected)
            {
                return false;
            }
            var requestPacket = new ServerRequestData();
            requestPacket.QueryId = queryId;
            requestPacket.Message = EDCode.EncodeBuffer(SerializerUtil.Serialize(message));
            requestPacket.Packet = EDCode.EncodeBuffer(SerializerUtil.Serialize(packet));
            var sginId = HUtil32.MakeLong((ushort)(queryId ^ 170), (ushort)(requestPacket.Message.Length + requestPacket.Packet.Length + 6));
            requestPacket.Sgin = EDCode.EncodeBuffer(BitConverter.GetBytes(sginId));
            SendMessage(SerializerUtil.Serialize(requestPacket));
            return true;
        }

        private void SendMessage(byte[] sendBuffer)
        {
            var serverMessage = new ServerDataPacket
            {
                PacketCode = Grobal2.RunGateCode,
                PacketLen = (ushort)sendBuffer.Length
            };
            var dataBuff = serverMessage.GetBuffer();
            var data = new byte[ServerDataPacket.FixedHeaderLen + sendBuffer.Length];
            MemoryCopy.BlockCopy(dataBuff, 0, data, 0, data.Length);
            MemoryCopy.BlockCopy(sendBuffer, 0, data, dataBuff.Length, sendBuffer.Length);
            _clientScoket.Send(data);
        }

        private void MarketScoketDisconnected(object sender, DSCClientConnectedEventArgs e)
        {
            _clientScoket.IsConnected = false;
            _logger.Error("数据库(拍卖行)服务器[" + e.RemoteEndPoint + "]断开连接...");
        }

        private void MarketScoketConnected(object sender, DSCClientConnectedEventArgs e)
        {
            _clientScoket.IsConnected = true;
            _logger.Info("数据库(拍卖行)服务器[" + e.RemoteEndPoint + "]连接成功...");
            // SendRequest(0, null, null); //todo 链接成功后进行第一次主动拉取拍卖行数据
        }

        private void MarketSocketError(object sender, DSCClientErrorEventArgs e)
        {
            _clientScoket.IsConnected = false;
            switch (e.ErrorCode)
            {
                case SocketError.ConnectionRefused:
                    _logger.Error("数据库(拍卖行)服务器[" + M2Share.Config.MarketSrvAddr + ":" + M2Share.Config.MarketSrvPort + "]拒绝链接...");
                    break;
                case SocketError.ConnectionReset:
                    _logger.Error("数据库(拍卖行)服务器[" + M2Share.Config.MarketSrvAddr + ":" + M2Share.Config.MarketSrvPort + "]关闭连接...");
                    break;
                case SocketError.TimedOut:
                    _logger.Error("数据库(拍卖行)服务器[" + M2Share.Config.MarketSrvAddr + ":" + M2Share.Config.MarketSrvPort + "]链接超时...");
                    break;
            }
        }

        private void MarketSocketRead(object sender, DSCClientDataInEventArgs e)
        {
            HUtil32.EnterCriticalSection(M2Share.UserDBCriticalSection);
            try
            {
                var nMsgLen = e.BuffLen;
                var packetData = e.Buff;
                if (BuffLen > 0)
                {
                    MemoryCopy.BlockCopy(packetData, 0, ReceiveBuffer, BuffLen, packetData.Length);
                    ProcessServerPacket(ReceiveBuffer, BuffLen + nMsgLen);
                }
                else
                {
                    ProcessServerPacket(packetData, nMsgLen);
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception);
            }
            finally
            {
                HUtil32.LeaveCriticalSection(M2Share.UserDBCriticalSection);
            }
        }

        private void ProcessServerPacket(Span<byte> buff, int buffLen)
        {
            try
            {
                var srcOffset = 0;
                var nLen = buffLen;
                var dataBuff = buff;
                while (nLen >= ServerDataPacket.FixedHeaderLen)
                {
                    var packetHead = dataBuff[..ServerDataPacket.FixedHeaderLen];
                    var message = ServerPacket.ToPacket<ServerDataPacket>(packetHead);
                    if (message.PacketCode != Grobal2.RunGateCode)
                    {
                        srcOffset++;
                        dataBuff = dataBuff.Slice(srcOffset, ServerDataPacket.FixedHeaderLen);
                        nLen -= 1;
                        _logger.Debug($"解析封包出现异常封包，PacketLen:[{dataBuff.Length}] Offset:[{srcOffset}].");
                        continue;
                    }
                    var nCheckMsgLen = Math.Abs(message.PacketLen + ServerDataPacket.FixedHeaderLen);
                    if (nCheckMsgLen > nLen)
                    {
                        break;
                    }
                    var messageData = SerializerUtil.Deserialize<ServerRequestData>(dataBuff[ServerDataPacket.FixedHeaderLen..]);
                    ProcessServerData(messageData);
                    nLen -= nCheckMsgLen;
                    if (nLen <= 0)
                    {
                        break;
                    }
                    dataBuff = dataBuff.Slice(nCheckMsgLen, nLen);
                    BuffLen = nLen;
                    srcOffset = 0;
                    if (nLen < ServerDataPacket.FixedHeaderLen)
                    {
                        break;
                    }
                }
                if (nLen > 0)//有部分数据被处理,需要把剩下的数据拷贝到接收缓冲的头部
                {
                    MemoryCopy.BlockCopy(dataBuff, 0, ReceiveBuffer, 0, nLen);
                    BuffLen = nLen;
                }
                else
                {
                    BuffLen = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void ProcessServerData(ServerRequestData responsePacket)
        {
            if (responsePacket != null)
            {
                var respCheckCode = responsePacket.QueryId;
                var nLen = responsePacket.Message.Length + responsePacket.Packet.Length + 6;
                if (nLen >= 12)
                {
                    var queryId = HUtil32.MakeLong((ushort)(respCheckCode ^ 170), (ushort)nLen);
                    if (queryId <= 0 || responsePacket.Sgin.Length <= 0)
                    {
                        M2Share.Config.nLoadDBErrorCount++;
                        return;
                    }
                    var signatureBuff = BitConverter.GetBytes(queryId);
                    var sginBuff = EDCode.DecodeBuff(responsePacket.Sgin);
                    if (BitConverter.ToInt16(signatureBuff) == BitConverter.ToInt16(sginBuff))
                    {
                        PlayerDataService.Enqueue(respCheckCode, responsePacket);
                    }
                    else
                    {
                        M2Share.Config.nLoadDBErrorCount++;
                    }
                }
            }
            else
            {
                _logger.Error("错误的封包数据");
            }
        }
    }
}