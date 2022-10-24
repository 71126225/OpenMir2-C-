﻿using LoginSvr.Conf;
using LoginSvr.Storage;
using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using SystemModule;
using SystemModule.Packet;
using SystemModule.Packet.ClientPackets;
using SystemModule.Packet.ServerPackets;
using SystemModule.Sockets;

namespace LoginSvr.Services
{
    public class ClientSession
    {
        private readonly MirLog _logger;
        private readonly AccountStorage _accountStorage;
        private readonly DataService _masSocService;
        private readonly ConfigManager _configManager;
        private readonly Channel<UserSessionData> _userMessageQueue;

        public ClientSession(MirLog logger, AccountStorage accountStorage, ConfigManager configManager, DataService masSocService)
        {
            _logger = logger;
            _accountStorage = accountStorage;
            _configManager = configManager;
            _masSocService = masSocService;
            _userMessageQueue = Channel.CreateUnbounded<UserSessionData>();
        }

        public void Start(CancellationToken stoppingToken)
        {
            Task.Factory.StartNew(async () =>
            {
                await ProcessUserMessage(stoppingToken);
            }, stoppingToken);
        }

        public void SendToQueue(UserSessionData userData)
        {
            _userMessageQueue.Writer.TryWrite(userData);
        }

        /// <summary>
        /// 处理封包消息
        /// </summary>
        /// <returns></returns>
        private async Task ProcessUserMessage(CancellationToken stoppingToken)
        {
            while (await _userMessageQueue.Reader.WaitToReadAsync(stoppingToken))
            {
                while (_userMessageQueue.Reader.TryRead(out var message))
                {
                    DecodeUserData(message.UserInfo, message.Msg);
                }
            }
        }

        private void DecodeUserData(UserInfo userInfo, string userData)
        {
            var sMsg = string.Empty;
            try
            {
                if (!userData.EndsWith("!"))
                {
                    return;
                }
                HUtil32.ArrestStringEx(userData, "#", "!", ref sMsg);
                if (string.IsNullOrEmpty(sMsg))
                    return;
                if (sMsg.Length < Grobal2.DEFBLOCKSIZE)
                    return;
                sMsg = sMsg.Substring(1, sMsg.Length - 1);
                ProcessUserMsg(userInfo, sMsg);
            }
            catch (Exception ex)
            {
                _logger.Information("[Exception] LoginService.DecodeUserData");
                _logger.Information(ex.StackTrace);
            }
        }

        private void ProcessUserMsg(UserInfo userInfo, string sMsg)
        {
            var sDefMsg = sMsg.Substring(0, Grobal2.DEFBLOCKSIZE);
            var sData = sMsg.Substring(Grobal2.DEFBLOCKSIZE, sMsg.Length - Grobal2.DEFBLOCKSIZE);
            var defMsg = EDCode.DecodePacket(sDefMsg);
            switch (defMsg.Ident)
            {
                case Grobal2.CM_SELECTSERVER:
                    if (!userInfo.SelServer)
                    {
                        AccountSelectServer(_configManager.Config, userInfo, sData);
                    }
                    break;
                case Grobal2.CM_PROTOCOL:
                    AccountCheckProtocol(userInfo, defMsg.Recog);
                    break;
                case Grobal2.CM_IDPASSWORD:
                    if (string.IsNullOrEmpty(userInfo.Account))
                    {
                        AccountLogin(_configManager.Config, userInfo, sData);
                    }
                    else
                    {
                        KickUser(_configManager.Config, ref userInfo);
                    }
                    break;
                case Grobal2.CM_ADDNEWUSER:
                    if (_configManager.Config.boEnableMakingID)
                    {
                        if (HUtil32.GetTickCount() - userInfo.ClientTick > 5000)
                        {
                            AccountCreate(ref userInfo, sData);
                        }
                        else
                        {
                            _logger.Information("[超速操作] 创建帐号/" + userInfo.UserIPaddr);
                        }
                    }
                    break;
                case Grobal2.CM_CHANGEPASSWORD:
                    if (string.IsNullOrEmpty(userInfo.Account))
                    {
                        if (HUtil32.GetTickCount() - userInfo.ClientTick > 5000)
                        {
                            userInfo.ClientTick = HUtil32.GetTickCount();
                            AccountChangePassword(userInfo, sData);
                        }
                        else
                        {
                            _logger.Information("[超速操作] 修改密码 /" + userInfo.UserIPaddr);
                        }
                    }
                    else
                    {
                        userInfo.Account = string.Empty;
                    }
                    break;
                case Grobal2.CM_UPDATEUSER:
                    if (HUtil32.GetTickCount() - userInfo.ClientTick > 5000)
                    {
                        userInfo.ClientTick = HUtil32.GetTickCount();
                        AccountUpdateUserInfo(userInfo, sData);
                    }
                    else
                    {
                        _logger.Information("[超速操作] 更新帐号 /" + userInfo.UserIPaddr);
                    }
                    break;
                case Grobal2.CM_GETBACKPASSWORD:
                    if (HUtil32.GetTickCount() - userInfo.ClientTick > 5000)
                    {
                        userInfo.ClientTick = HUtil32.GetTickCount();
                        AccountGetBackPassword(userInfo, sData);
                    }
                    else
                    {
                        _logger.Information("[超速操作] 找回密码 /" + userInfo.UserIPaddr);
                    }
                    break;
            }
        }

        /// <summary>
        /// 账号登陆
        /// </summary>
        private void AccountLogin(Config config, UserInfo userInfo, string sData)
        {
            var sLoginId = string.Empty;
            UserEntry userEntry = null;
            var nIdCostIndex = -1;
            var nIpCostIndex = -1;
            AccountRecord accountRecord = null;
            try
            {
                var sPassword = HUtil32.GetValidStr3(EDCode.DeCodeString(sData), ref sLoginId, new[] { "/" });
                var nCode = 0;
                var boNeedUpdate = false;
                var accountIndex = _accountStorage.Index(sLoginId);
                if (accountIndex >= 0 && _accountStorage.Get(accountIndex, ref accountRecord) >= 0)
                {
                    if (accountRecord.nErrorCount < 5 || HUtil32.GetTickCount() - accountRecord.dwActionTick > 60000)
                    {
                        if (accountRecord.UserEntry.sPassword == sPassword)
                        {
                            accountRecord.nErrorCount = 0;
                            if (accountRecord.UserEntry.sUserName == "" || accountRecord.UserEntryAdd.sQuiz2 == "")
                            {
                                userEntry = accountRecord.UserEntry;
                                boNeedUpdate = true;
                            }
                            accountRecord.Header.CreateDate = userInfo.dtDateTime;
                            nCode = 1;
                        }
                        else
                        {
                            accountRecord.nErrorCount++;
                            accountRecord.dwActionTick = HUtil32.GetTickCount();
                            nCode = -1;
                        }
                        _accountStorage.Update(accountIndex, ref accountRecord);
                    }
                    else
                    {
                        nCode = -2;
                        accountRecord.dwActionTick = HUtil32.GetTickCount();
                        _accountStorage.Update(accountIndex, ref accountRecord);
                    }
                }
                if (nCode == 1 && IsLogin(config, sLoginId))
                {
                    SessionKick(config, sLoginId);
                    nCode = -3;
                }
                ClientMesaagePacket defMsg;
                if (boNeedUpdate)
                {
                    defMsg = Grobal2.MakeDefaultMsg(Grobal2.SM_NEEDUPDATE_ACCOUNT, 0, 0, 0, 0);
                    SendGateMsg(userInfo.Socket, userInfo.SockIndex, EDCode.EncodeMessage(defMsg) + EDCode.EncodeBuffer(userEntry));
                }
                if (nCode == 1)
                {
                    userInfo.Account = sLoginId;
                    userInfo.SessionID = LsShare.GetSessionId();
                    userInfo.SelServer = false;
                    if (config.AccountCostList.ContainsKey(userInfo.Account))
                    {
                        nIdCostIndex = config.AccountCostList[userInfo.Account];
                    }
                    if (config.IPaddrCostList.ContainsKey(userInfo.UserIPaddr))
                    {
                        nIpCostIndex = config.IPaddrCostList[userInfo.UserIPaddr];
                    }
                    var nIdCost = 0;
                    var nIpCost = 0;
                    if (nIdCostIndex >= 0)
                    {
                        nIdCost = nIdCostIndex;//Config.AccountCostList[nIDCostIndex];
                    }
                    if (nIpCostIndex >= 0)
                    {
                        nIpCost = nIpCostIndex;//Config.IPaddrCostList[nIPCostIndex];
                    }
                    if (nIdCost >= 0 || nIpCost >= 0)
                    {
                        userInfo.boPayCost = true;
                    }
                    else
                    {
                        userInfo.boPayCost = false;
                    }

                    /*var RemainDays = 0;
                    var RemainIpDays = 0;
                    var RemainHours = 0;
                    var RemainIpHours = 0;
                    var st = DateTime.Now;
                    var nCurrentTime = GetDay(st.Year, st.Month, st.Day);
                    RemainDays = userInfo.dwValidUntil - nCurrentTime + 1;
                    RemainIpDays = userInfo.dwIpValidUntil - nCurrentTime + 1;
                    RemainHours = (userInfo.dwSeconds + 1) / 3600;
                    RemainIpHours = (userInfo.dwIpSeconds + 1) / 3600;
                    if (RemainDays < 0) 
                      RemainDays = 0;
                    if (RemainIpDays < 0) 
                      RemainIpDays = 0;
                    if (RemainHours < 0) 
                      RemainHours = 0;
                    if (RemainIpHours < 0) 
                      RemainIpHours = 0;*/

                    userInfo.IDDay = HUtil32.LoWord(nIdCost);
                    userInfo.IDHour = HUtil32.HiWord(nIdCost);
                    userInfo.IPDay = HUtil32.LoWord(nIpCost);
                    userInfo.IPHour = HUtil32.HiWord(nIpCost);
                    if (!userInfo.boPayCost)
                    {
                        defMsg = Grobal2.MakeDefaultMsg(Grobal2.SM_PASSOK_SELECTSERVER, 0, 0, 0, config.ServerNameList.Count);
                    }
                    else
                    {
                        defMsg = Grobal2.MakeDefaultMsg(Grobal2.SM_PASSOK_SELECTSERVER, nIdCost, HUtil32.LoWord(nIpCost), HUtil32.HiWord(nIpCost), config.ServerNameList.Count);
                    }
                    var sServerName = GetServerListInfo();
                    SendGateMsg(userInfo.Socket, userInfo.SockIndex, EDCode.EncodeMessage(defMsg) + EDCode.EncodeString(sServerName));
                    SessionAdd(config, userInfo.Account, userInfo.UserIPaddr, userInfo.SessionID, userInfo.boPayCost, false);
                }
                else
                {
                    defMsg = Grobal2.MakeDefaultMsg(Grobal2.SM_PASSWD_FAIL, nCode, 0, 0, 0);
                    SendGateMsg(userInfo.Socket, userInfo.SockIndex, EDCode.EncodeMessage(defMsg));
                }
            }
            catch (Exception ex)
            {
                _logger.Information("[Exception] LoginService.LoginUser");
                _logger.Information(ex.StackTrace);
            }
        }

        private long GetDay(int iYear, int iMonth, int iDay)
        {
            int[] MONTH_DAY = new int[13] { 0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            const int DAYTOYEAR1999 = 730119;
            if (iYear > 0)
            {
                int iTotalDay;
                int iStartYear;
                if (iYear > 1999)
                {
                    iStartYear = 2000;
                    iTotalDay = DAYTOYEAR1999;
                }
                else
                {
                    iStartYear = 1;
                    iTotalDay = 0;
                }

                for (int i = iStartYear; i < iYear; i++)
                {
                    int iTemp;
                    if (i % 400 == 0)
                    {
                        iTemp = 366;
                    }
                    else if (i % 100 == 0)
                    {
                        iTemp = 365;
                    }
                    else if (i % 4 == 0)
                    {
                        iTemp = 366;
                    }
                    else
                    {
                        iTemp = 365;
                    }
                    iTotalDay += iTemp;
                }

                bool bYun;
                if (iYear % 400 == 0)
                {
                    bYun = true;
                }
                else if (iYear % 100 == 0)
                {
                    bYun = false;
                }
                else if (iYear % 4 == 0)
                {
                    bYun = true;
                }
                else
                {
                    bYun = false;
                }

                if (iMonth >= 1 && iMonth <= 12)
                {
                    for (int i = 1; i < iMonth; i++)
                    {
                        iTotalDay += MONTH_DAY[i];
                        if (i == 2 && !bYun)
                        {
                            iTotalDay = iTotalDay - 1;
                        }
                    }
                }

                int MaxDay = MONTH_DAY[iMonth];
                if (iMonth == 2 && !bYun)
                {
                    MaxDay = 28;
                }
                if (iDay >= 1 && iDay <= MaxDay)
                {
                    iTotalDay = iTotalDay + iDay;
                }

                return iTotalDay;
            }

            return 0;
        }

        /// <summary>
        /// 账号注册
        /// </summary>
        private void AccountCreate(ref UserInfo userInfo, string sData)
        {
            var success = false;
            const string sAddNewuserFail = "[新建帐号失败] {0}/{1}";
            try
            {
                if (string.IsNullOrEmpty(sData) || sData.Length < 333)
                {
                    _logger.Information("[新建账号失败] 数据包为空或数据包长度异常");
                    return;
                }
                var accountStrSize = (byte)Math.Ceiling((decimal)(UserEntry.Size * 4) / 3);
                var ueBuff = EDCode.DecodeBuffer(sData[..accountStrSize]);
                var uaBuff = EDCode.DecodeBuffer(sData[accountStrSize..]);
                var accountBuff = new byte[ueBuff.Length + uaBuff.Length];
                Buffer.BlockCopy(ueBuff, 0, accountBuff, 0, ueBuff.Length);
                Buffer.BlockCopy(uaBuff, 0, accountBuff, ueBuff.Length, uaBuff.Length);
                var userFullEntry = Packets.ToPacket<UserFullEntry>(accountBuff);
                var nErrCode = -1;
                if (LsShare.CheckAccountName(userFullEntry.UserEntry.sAccount))
                {
                    success = true;
                }
                if (success)
                {
                    var n10 = _accountStorage.Index(userFullEntry.UserEntry.sAccount);
                    if (n10 <= 0)
                    {
                        var accountRecord = new AccountRecord();
                        accountRecord.UserEntry = userFullEntry.UserEntry;
                        accountRecord.UserEntryAdd = userFullEntry.UserEntryAdd;
                        if (!string.IsNullOrEmpty(userFullEntry.UserEntry.sAccount))
                        {
                            if (_accountStorage.Add(ref accountRecord))
                            {
                                nErrCode = 1;
                            }
                        }
                    }
                    else
                    {
                        nErrCode = 0;
                    }
                }
                else
                {
                    _logger.Information(string.Format(sAddNewuserFail, userFullEntry.UserEntry.sAccount, userFullEntry.UserEntryAdd.sQuiz2));
                }
                ClientMesaagePacket defMsg;
                if (nErrCode == 1)
                {
                    defMsg = Grobal2.MakeDefaultMsg(Grobal2.SM_NEWID_SUCCESS, 0, 0, 0, 0);
                }
                else
                {
                    defMsg = Grobal2.MakeDefaultMsg(Grobal2.SM_NEWID_FAIL, nErrCode, 0, 0, 0);
                }
                SendGateMsg(userInfo.Socket, userInfo.SockIndex, EDCode.EncodeMessage(defMsg));
            }
            catch (Exception ex)
            {
                _logger.LogDebug("[Exception] LoginsService.AccountCreate");
                _logger.Information(ex.StackTrace);
            }
            finally
            {
                userInfo.ClientTick = HUtil32.GetTickCount();
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        private void AccountChangePassword(UserInfo userInfo, string sData)
        {
            var sLoginId = string.Empty;
            var sOldPassword = string.Empty;
            ClientMesaagePacket defMsg;
            AccountRecord accountRecord = null;
            try
            {
                var sMsg = EDCode.DeCodeString(sData);
                sMsg = HUtil32.GetValidStr3(sMsg, ref sLoginId, new[] { "\09", "\t" });
                var sNewPassword = HUtil32.GetValidStr3(sMsg, ref sOldPassword, new[] { "\09", "\t" });
                var nCode = 0;
                if (sNewPassword.Length >= 3)
                {
                    var n10 = _accountStorage.Index(sLoginId);
                    if (n10 >= 0 && _accountStorage.Get(n10, ref accountRecord) >= 0)
                    {
                        if (accountRecord.nErrorCount < 5 || HUtil32.GetTickCount() - accountRecord.dwActionTick > 180000)
                        {
                            if (accountRecord.UserEntry.sPassword == sOldPassword)
                            {
                                accountRecord.nErrorCount = 0;
                                accountRecord.UserEntry.sPassword = sNewPassword;
                                nCode = 1;
                            }
                            else
                            {
                                accountRecord.nErrorCount++;
                                accountRecord.dwActionTick = HUtil32.GetTickCount();
                                nCode = -1;
                            }
                            _accountStorage.Update(n10, ref accountRecord);
                        }
                        else
                        {
                            nCode = -2;
                            if (HUtil32.GetTickCount() < accountRecord.dwActionTick)
                            {
                                accountRecord.dwActionTick = HUtil32.GetTickCount();
                                _accountStorage.Update(n10, ref accountRecord);
                            }
                        }
                    }
                }
                if (nCode == 1)
                {
                    defMsg = Grobal2.MakeDefaultMsg(Grobal2.SM_CHGPASSWD_SUCCESS, 0, 0, 0, 0);
                }
                else
                {
                    defMsg = Grobal2.MakeDefaultMsg(Grobal2.SM_CHGPASSWD_FAIL, nCode, 0, 0, 0);
                }
                SendGateMsg(userInfo.Socket, userInfo.SockIndex, EDCode.EncodeMessage(defMsg));
            }
            catch (Exception ex)
            {
                _logger.Information("[Exception] LoginService.ChangePassword");
                _logger.Information(ex.StackTrace);
            }
        }

        /// <summary>
        /// 选择服务器
        /// </summary>
        private void AccountSelectServer(Config config, UserInfo userInfo, string sData)
        {
            ClientMesaagePacket defMsg;
            bool boPayCost;
            var sSelGateIp = string.Empty;
            var nSelGatePort = 0;
            const string sSelServerMsg = "Server: {0}/{1}-{2}:{3}";
            var sServerName = EDCode.DeCodeString(sData);
            if (!string.IsNullOrEmpty(userInfo.Account) && !string.IsNullOrEmpty(sServerName) && IsLogin(config, userInfo.SessionID))
            {
                GetSelGateInfo(config, sServerName, config.sGateIPaddr, ref sSelGateIp, ref nSelGatePort);
                if (sSelGateIp != "" && nSelGatePort > 0)
                {
                    if (config.boDynamicIPMode)
                    {
                        sSelGateIp = userInfo.GateIPaddr;
                    }
                    _logger.LogDebug(string.Format(sSelServerMsg, sServerName, config.sGateIPaddr, sSelGateIp, nSelGatePort));
                    userInfo.SelServer = true;
                    boPayCost = false;
                    var nPayMode = 5;
                    if (userInfo.IDHour > 0)
                    {
                        nPayMode = 2;
                    }
                    if (userInfo.IPHour > 0)
                    {
                        nPayMode = 4;
                    }
                    if (userInfo.IPDay > 0)
                    {
                        nPayMode = 3;
                    }
                    if (userInfo.IDDay > 0)
                    {
                        nPayMode = 1;
                    }
                    if (_masSocService.IsNotUserFull(sServerName))
                    {
                        SessionUpdate(config, userInfo.SessionID, sServerName, boPayCost);
                        _masSocService.SendServerMsg(Grobal2.SS_OPENSESSION, sServerName, userInfo.Account + "/" + userInfo.SessionID + "/" + (userInfo.boPayCost ? 1 : 0) + "/" + nPayMode + "/" + userInfo.UserIPaddr);
                        defMsg = Grobal2.MakeDefaultMsg(Grobal2.SM_SELECTSERVER_OK, userInfo.SessionID, 0, 0, 0);
                        SendGateMsg(userInfo.Socket, userInfo.SockIndex, EDCode.EncodeMessage(defMsg) + EDCode.EncodeString(sSelGateIp + "/" + nSelGatePort + "/" + userInfo.SessionID));
                    }
                    else
                    {
                        userInfo.SelServer = false;
                        SessionDel(config, userInfo.SessionID);
                        defMsg = Grobal2.MakeDefaultMsg(Grobal2.SM_STARTFAIL, 0, 0, 0, 0);
                        SendGateMsg(userInfo.Socket, userInfo.SockIndex, EDCode.EncodeMessage(defMsg));
                    }
                }
            }
        }

        /// <summary>
        /// 更新账号信息
        /// </summary>
        private void AccountUpdateUserInfo(UserInfo userInfo, string sData)
        {
            AccountRecord accountRecord = null;
            ClientMesaagePacket defMsg;
            try
            {
                if (string.IsNullOrEmpty(sData))
                {
                    _logger.Information("[新建账号失败,数据包为空].");
                    return;
                }
                var deBuffer = EDCode.DecodeBuffer(sData);
                UserFullEntry userFullEntry = Packets.ToPacket<UserFullEntry>(deBuffer);
                var nCode = -1;
                if (userInfo.Account == userFullEntry.UserEntry.sAccount && LsShare.CheckAccountName(userFullEntry.UserEntry.sAccount))
                {
                    var accountIndex = _accountStorage.Index(userFullEntry.UserEntry.sAccount);
                    if (accountIndex >= 0)
                    {
                        if (_accountStorage.Get(accountIndex, ref accountRecord) >= 0)
                        {
                            accountRecord.UserEntry = userFullEntry.UserEntry;
                            accountRecord.UserEntryAdd = userFullEntry.UserEntryAdd;
                            _accountStorage.Update(accountIndex, ref accountRecord);
                            nCode = 1;
                        }
                    }
                    else
                    {
                        nCode = 0;
                    }
                }
                if (nCode == 1)
                {
                    defMsg = Grobal2.MakeDefaultMsg(Grobal2.SM_UPDATEID_SUCCESS, 0, 0, 0, 0);
                }
                else
                {
                    defMsg = Grobal2.MakeDefaultMsg(Grobal2.SM_UPDATEID_FAIL, nCode, 0, 0, 0);
                }
                SendGateMsg(userInfo.Socket, userInfo.SockIndex, EDCode.EncodeMessage(defMsg));
            }
            catch (Exception ex)
            {
                _logger.Information("[Exception] LoginService.UpdateUserInfo");
                _logger.Information(ex.StackTrace);
            }
        }

        /// <summary>
        /// 找回密码
        /// </summary>
        private void AccountGetBackPassword(UserInfo userInfo, string sData)
        {
            var sAccount = string.Empty;
            var sQuest1 = string.Empty;
            var sAnswer1 = string.Empty;
            var sQuest2 = string.Empty;
            var sAnswer2 = string.Empty;
            var sPassword = string.Empty;
            var sBirthDay = string.Empty;
            ClientMesaagePacket defMsg;
            AccountRecord accountRecord = null;
            var sMsg = EDCode.DeCodeString(sData);
            sMsg = HUtil32.GetValidStr3(sMsg, ref sAccount, new[] { "\09" });
            sMsg = HUtil32.GetValidStr3(sMsg, ref sQuest1, new[] { "\09" });
            sMsg = HUtil32.GetValidStr3(sMsg, ref sAnswer1, new[] { "\09" });
            sMsg = HUtil32.GetValidStr3(sMsg, ref sQuest2, new[] { "\09" });
            sMsg = HUtil32.GetValidStr3(sMsg, ref sAnswer2, new[] { "\09" });
            sMsg = HUtil32.GetValidStr3(sMsg, ref sBirthDay, new[] { "\09" });
            var nCode = 0;
            if (!string.IsNullOrEmpty(sAccount))
            {
                var nIndex = _accountStorage.Index(sAccount);
                if (nIndex >= 0 && _accountStorage.Get(nIndex, ref accountRecord) >= 0)
                {
                    if (accountRecord.nErrorCount < 5 || HUtil32.GetTickCount() - accountRecord.dwActionTick > 180000)
                    {
                        nCode = -1;
                        if (accountRecord.UserEntry.sQuiz == sQuest1)
                        {
                            nCode = -3;
                            if (accountRecord.UserEntry.sAnswer == sAnswer1)
                            {
                                if (accountRecord.UserEntryAdd.sBirthDay == sBirthDay)
                                {
                                    nCode = 1;
                                }
                            }
                        }
                        if (nCode != 1)
                        {
                            if (accountRecord.UserEntryAdd.sQuiz2 == sQuest2)
                            {
                                nCode = -3;
                                if (accountRecord.UserEntryAdd.sAnswer2 == sAnswer2)
                                {
                                    if (accountRecord.UserEntryAdd.sBirthDay == sBirthDay)
                                    {
                                        nCode = 1;
                                    }
                                }
                            }
                        }
                        if (nCode == 1)
                        {
                            sPassword = accountRecord.UserEntry.sPassword;
                        }
                        else
                        {
                            accountRecord.nErrorCount++;
                            accountRecord.dwActionTick = HUtil32.GetTickCount();
                            _accountStorage.Update(nIndex, ref accountRecord);
                        }
                    }
                    else
                    {
                        nCode = -2;
                        if (HUtil32.GetTickCount() < accountRecord.dwActionTick)
                        {
                            accountRecord.dwActionTick = HUtil32.GetTickCount();
                            _accountStorage.Update(nIndex, ref accountRecord);
                        }
                    }
                }
            }
            if (nCode == 1)
            {
                defMsg = Grobal2.MakeDefaultMsg(Grobal2.SM_GETBACKPASSWD_SUCCESS, 0, 0, 0, 0);
                SendGateMsg(userInfo.Socket, userInfo.SockIndex, EDCode.EncodeMessage(defMsg) + EDCode.EncodeString(sPassword));
            }
            else
            {
                defMsg = Grobal2.MakeDefaultMsg(Grobal2.SM_GETBACKPASSWD_FAIL, nCode, 0, 0, 0);
                SendGateMsg(userInfo.Socket, userInfo.SockIndex, EDCode.EncodeMessage(defMsg));
            }
        }

        private void AccountCheckProtocol(UserInfo userInfo, int nDate)
        {
            ClientMesaagePacket defMsg;
            if (nDate < LsShare.VersionDate)
            {
                defMsg = Grobal2.MakeDefaultMsg(Grobal2.SM_CERTIFICATION_FAIL, 0, 0, 0, 0);
            }
            else
            {
                defMsg = Grobal2.MakeDefaultMsg(Grobal2.SM_CERTIFICATION_SUCCESS, 0, 0, 0, 0);
                userInfo.nVersionDate = nDate;
                userInfo.boCertificationOK = true;
            }
            SendGateMsg(userInfo.Socket, userInfo.SockIndex, EDCode.EncodeMessage(defMsg));
        }

        private bool IsLogin(Config config, int nSessionId)
        {
            var result = false;
            for (var i = 0; i < config.SessionList.Count; i++)
            {
                if (config.SessionList[i].nSessionID == nSessionId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        private bool IsLogin(Config config, string sLoginId)
        {
            var result = false;
            for (var i = 0; i < config.SessionList.Count; i++)
            {
                if (config.SessionList[i].sAccount == sLoginId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// 剔除会话
        /// </summary>
        private void SessionKick(Config config, string sLoginId)
        {
            TConnInfo connInfo;
            for (var i = 0; i < config.SessionList.Count; i++)
            {
                connInfo = config.SessionList[i];
                if (connInfo.sAccount == sLoginId && !connInfo.boKicked)
                {
                    _masSocService.SendServerMsg(Grobal2.SS_CLOSESESSION, connInfo.sServerName, connInfo.sAccount + "/" + connInfo.nSessionID);
                    connInfo.dwKickTick = HUtil32.GetTickCount();
                    connInfo.boKicked = true;
                }
            }
        }

        private void SessionUpdate(Config config, int nSessionId, string sServerName, bool boPayCost)
        {
            for (var i = 0; i < config.SessionList.Count; i++)
            {
                var connInfo = config.SessionList[i];
                if (connInfo.nSessionID == nSessionId)
                {
                    connInfo.sServerName = sServerName;
                    connInfo.bo11 = boPayCost;
                    break;
                }
            }
        }

        private void SessionAdd(Config config, string sAccount, string sIPaddr, int nSessionId, bool boPayCost, bool bo11)
        {
            var connInfo = new TConnInfo();
            connInfo.sAccount = sAccount;
            connInfo.sIPaddr = sIPaddr;
            connInfo.nSessionID = nSessionId;
            connInfo.boPayCost = boPayCost;
            connInfo.bo11 = bo11;
            connInfo.dwKickTick = HUtil32.GetTickCount();
            connInfo.dwStartTick = HUtil32.GetTickCount();
            connInfo.boKicked = false;
            config.SessionList.Add(connInfo);
        }

        private void SendGateMsg(Socket socket, string sSockIndex, string sMsg)
        {
            if (socket.Connected)
            {
                //var sSendMsg = "%" + sSockIndex + "/#" + sMsg + "!$";
                //Socket.SendText(sSendMsg);
                var packet = new LoginSvrPacket();
                packet.ConnectionId = sSockIndex;
                packet.ClientPacket = HUtil32.GetBytes("#" + sMsg + "!$");
                socket.SendBuffer(packet.GetBuffer());
            }
        }

        private void KickUser(Config config, ref UserInfo userInfo)
        {
            const string sKickMsg = "Kick: {0}";
            for (var i = 0; i < LsShare.Gates.Count; i++)
            {
                var gateInfo = LsShare.Gates[i];
                for (var j = 0; j < gateInfo.UserList.Count; j++)
                {
                    var user = gateInfo.UserList[j];
                    if (user == userInfo)
                    {
                        _logger.LogDebug(string.Format(sKickMsg, userInfo.UserIPaddr));
                        SendGateKickMsg(gateInfo.Socket, userInfo.SockIndex);
                        userInfo = null;
                        gateInfo.UserList.RemoveAt(j);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// 获取角色网关信息
        /// </summary>
        private void GetSelGateInfo(Config config, string sServerName, string sIPaddr, ref string sSelGateIp, ref int nSelGatePort)
        {
            int nGateIdx;
            int nGateCount;
            int nSelIdx;
            bool boSelected;
            try
            {
                sSelGateIp = "";
                nSelGatePort = 0;
                for (var i = 0; i < config.nRouteCount; i++)
                {
                    if (config.boDynamicIPMode || (config.GateRoute[i].sServerName == sServerName && config.GateRoute[i].sPublicAddr == sIPaddr))
                    {
                        nGateCount = 0;
                        nGateIdx = 0;
                        while (true)
                        {
                            if (config.GateRoute[i].Gate[nGateIdx].sIPaddr != "" && config.GateRoute[i].Gate[nGateIdx].boEnable)
                            {
                                nGateCount++;
                            }
                            nGateIdx++;
                            if (nGateIdx >= 10)
                            {
                                break;
                            }
                        }
                        if (nGateCount <= 0)
                        {
                            break;
                        }
                        nSelIdx = config.GateRoute[i].nSelIdx;
                        boSelected = false;
                        for (nGateIdx = nSelIdx + 1; nGateIdx <= 9; nGateIdx++)
                        {
                            if (config.GateRoute[i].Gate[nGateIdx].sIPaddr != "" && config.GateRoute[i].Gate[nGateIdx].boEnable)
                            {
                                config.GateRoute[i].nSelIdx = nGateIdx;
                                boSelected = true;
                                break;
                            }
                        }
                        if (!boSelected)
                        {
                            for (nGateIdx = 0; nGateIdx < nSelIdx; nGateIdx++)
                            {
                                if (config.GateRoute[i].Gate[nGateIdx].sIPaddr != "" && config.GateRoute[i].Gate[nGateIdx].boEnable)
                                {
                                    config.GateRoute[i].nSelIdx = nGateIdx;
                                    break;
                                }
                            }
                        }
                        nSelIdx = config.GateRoute[i].nSelIdx;
                        sSelGateIp = config.GateRoute[i].Gate[nSelIdx].sIPaddr;
                        nSelGatePort = config.GateRoute[i].Gate[nSelIdx].nPort;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Information("[Exception] LoginService.GetSelGateInfo");
                _logger.Information(ex.StackTrace);
            }
        }

        /// <summary>
        /// 获取服务器信息
        /// </summary>
        /// <returns></returns>
        private string GetServerListInfo()
        {
            var result = string.Empty;
            var sServerInfo = string.Empty;
            var config = _configManager.Config;
            try
            {
                for (var i = 0; i < config.ServerNameList.Count; i++)
                {
                    var sServerName = config.ServerNameList[i];
                    if (!string.IsNullOrEmpty(sServerName))
                    {
                        sServerInfo = sServerInfo + sServerName + "/" + _masSocService.GetServerStatus(sServerName) + "/";
                    }
                }
                result = sServerInfo;
            }
            catch
            {
                _logger.Information("[Exception] LoginService.GetServerListInfo");
            }
            return result;
        }

        private void SendGateKickMsg(Socket socket, string sSockIndex)
        {
            var sSendMsg = $"%+-{sSockIndex}$";
            socket.SendText(sSendMsg);
        }

        private void SessionDel(Config config, int nSessionId)
        {
            for (var i = 0; i < config.SessionList.Count; i++)
            {
                if (config.SessionList[i].nSessionID == nSessionId)
                {
                    config.SessionList[i] = null;
                    config.SessionList.RemoveAt(i);
                    break;
                }
            }
        }
    }
}