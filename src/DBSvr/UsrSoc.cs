using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using SystemModule;
using SystemModule.Common;
using SystemModule.Packages;
using SystemModule.Sockets;

namespace DBSvr
{
    public class TFrmUserSoc
    {
        private long dwKeepAliveTick = 0;
        private object CS_GateSession = null;
        private int n2DC = 0;
        private int n2E0 = 0;
        private int n2E4 = 0;
        private IList<TGateInfo> GateList = null;
        private TGateInfo CurGate = null;
        private IList<string> MapList = null;
        private readonly THumDB HumDB;

        public TFrmUserSoc()
        {

        }

        public void UserSocketClientConnect(Object Sender, Socket Socket)
        {
            //TGateInfo GateInfo;
            //string sIPaddr;
            //sIPaddr = Socket.RemoteAddress;
            //if (!DBShare.CheckServerIP(sIPaddr))
            //{
            //    DBShare.OutMainMessage("�Ƿ���������: " + sIPaddr);
            //    Socket.Close;
            //    return;
            //}
            //if (!DBShare.boOpenDBBusy)
            //{
            //    GateInfo = new TGateInfo();
            //    GateInfo.Socket = Socket;
            //    GateInfo.sGateaddr = sIPaddr;
            //    GateInfo.sText = "";
            //    GateInfo.UserList = new List<TUserInfo>();
            //    GateInfo.dwTick10 = HUtil32.GetTickCount();
            //    GateInfo.nGateID = DBShare.GetGateID(sIPaddr);
            //    GateList.Add(GateInfo);
            //}
            //else
            //{
            //    Socket.Close;
            //}
        }

        public void UserSocketClientDisconnect(Object Sender, Socket Socket)
        {
            TGateInfo GateInfo;
            TUserInfo UserInfo;
            for (var i = 0; i < GateList.Count; i++)
            {
                GateInfo = GateList[i];
                if (GateInfo != null)
                {
                    for (var ii = 0; ii < GateInfo.UserList.Count; ii++)
                    {
                        UserInfo = GateInfo.UserList[ii];
                        UserInfo = null;
                    }
                    GateInfo.UserList = null;
                }
                GateList.RemoveAt(i);
                break;
            }
        }

        public void UserSocketClientError(Object Sender)
        {
            
        }

        public void UserSocketClientRead(Object Sender, Socket Socket)
        {
            string sReviceMsg;
            TGateInfo GateInfo;
            for (var i = 0; i < GateList.Count; i++)
            {
                GateInfo = GateList[i];
                if (GateInfo.Socket == Socket)
                {
                    CurGate = GateInfo;
                    //sReviceMsg = Socket.ReceiveText;
                    //GateInfo.sText = GateInfo.sText + sReviceMsg;
                    if (GateInfo.sText.Length < 81920)
                    {
                        if (GateInfo.sText.IndexOf("$", StringComparison.OrdinalIgnoreCase) > 1)
                        {
                            ProcessGateMsg(ref GateInfo);
                        }
                    }
                    else
                    {
                        GateInfo.sText = "";
                    }
                }
            }
        }

        public void FormCreate(System.Object Sender, System.EventArgs _e1)
        {
            //CS_GateSession = new TCriticalSection();
            //GateList = new ArrayList();
            //MapList = new ArrayList();
            //UserSocket.Port = DBShare.g_nGatePort;
            //UserSocket.Address = DBShare.g_sGateAddr;
            //UserSocket.Active = true;
            LoadServerInfo();
            LoadChrNameList("DenyChrName.txt");
            LoadClearMakeIndexList("ClearMakeIndex.txt");
        }

        public void FormDestroy(Object Sender)
        {
            TGateInfo GateInfo;
            TUserInfo UserInfo;
            for (var i = 0; i < GateList.Count; i++)
            {
                GateInfo = GateList[i];
                if (GateInfo != null)
                {
                    //for (var ii = 0; ii < GateInfo.UserList.Count; ii++)
                    //{
                    //    UserInfo = GateInfo.UserList[ii];
                    //    this.Dispose(UserInfo);
                    //}
                    //GateInfo.UserList.Free;
                }
                GateList.RemoveAt(i);
                break;
            }
            //GateList.Free;
            //MapList.Free;
            //CS_GateSession.Free;
        }

        public void Timer1Timer(System.Object Sender, System.EventArgs _e1)
        {
            int n8 = DBShare.g_nQueryChrCount + DBShare.nHackerNewChrCount + DBShare.nHackerDelChrCount + DBShare.nHackerSelChrCount + DBShare.n4ADC1C + DBShare.n4ADC20 + DBShare.n4ADC24 + DBShare.n4ADC28;
            if (DBShare.n4ADBB8 != n8)
            {
                DBShare.n4ADBB8 = n8;
                DBShare.OutMainMessage("H-QyChr=" + (DBShare.g_nQueryChrCount).ToString() + " " + "H-NwChr=" + (DBShare.nHackerNewChrCount).ToString() + " " + "H-DlChr=" + (DBShare.nHackerDelChrCount).ToString() + " " + "Dubl-Sl=" + (DBShare.nHackerSelChrCount).ToString() + " " + "H-Er-P1=" + (DBShare.n4ADC1C).ToString() + " " + "Dubl-P2=" + (DBShare.n4ADC20).ToString() + " " + "Dubl-P3=" + (DBShare.n4ADC24).ToString() + " " + "Dubl-P4=" + (DBShare.n4ADC28).ToString());
            }
        }

        public int GetUserCount()
        {
            int result;
            TGateInfo GateInfo;
            int nUserCount = 0;
            for (var i = 0; i < GateList.Count; i++)
            {
                GateInfo = GateList[i];
                nUserCount += GateInfo.UserList.Count;
            }
            result = nUserCount;
            return result;
        }

        private bool NewChrData(string sChrName, int nSex, int nJob, int nHair)
        {
            bool result = false;
            THumDataInfo ChrRecord;
            try
            {
                if (HumDB.Open() && (HumDB.Index(sChrName) == -1))
                {
                    ChrRecord = new THumDataInfo();
                    ChrRecord.Header = new TRecordHeader();
                    ChrRecord.Data = new THumInfoData();
                    ChrRecord.Header.sName = sChrName;
                    ChrRecord.Data.sChrName = sChrName;
                    ChrRecord.Data.btSex = (byte)nSex;
                    ChrRecord.Data.btJob = (byte)nJob;
                    ChrRecord.Data.btHair = (byte)nHair;
                    HumDB.Add(ref ChrRecord);
                    result = true;
                }
            }
            finally
            {
                HumDB.Close();
            }
            return result;
        }

        private void LoadServerInfo()
        {
            StringList LoadList;
            int nRouteIdx;
            int nGateIdx;
            int nServerIndex;
            string sLineText = string.Empty;
            string sSelGateIPaddr = string.Empty;
            string sGameGateIPaddr = string.Empty;
            string sGameGate = string.Empty;
            string sGameGatePort = string.Empty;
            string sMapName = string.Empty;
            string sMapInfo = string.Empty;
            string sServerIndex = string.Empty;
            FileStream Conf;
            try
            {
                LoadList = new StringList();
                LoadList.LoadFromFile(DBShare.sGateConfFileName);
                nRouteIdx = 0;
                nGateIdx = 0;
                for (var i = 0; i < LoadList.Count; i++)
                {
                    sLineText = LoadList[i].Trim();
                    if ((sLineText != "") && (sLineText[0] != ';'))
                    {
                        sGameGate = HUtil32.GetValidStr3(sLineText, ref sSelGateIPaddr, new string[] { " ", "\09" });
                        if ((sGameGate == "") || (sSelGateIPaddr == ""))
                        {
                            continue;
                        }
                        DBShare.g_RouteInfo[nRouteIdx].sSelGateIP = sSelGateIPaddr.Trim();
                        DBShare.g_RouteInfo[nRouteIdx].nGateCount = 0;
                        nGateIdx = 0;
                        while ((sGameGate != ""))
                        {
                            sGameGate = HUtil32.GetValidStr3(sGameGate, ref sGameGateIPaddr, new string[] { " ", "\09" });
                            sGameGate = HUtil32.GetValidStr3(sGameGate, ref sGameGatePort, new string[] { " ", "\09" });
                            DBShare.g_RouteInfo[nRouteIdx].sGameGateIP[nGateIdx] = sGameGateIPaddr.Trim();
                            DBShare.g_RouteInfo[nRouteIdx].nGameGatePort[nGateIdx] = HUtil32.Str_ToInt(sGameGatePort, 0);
                            nGateIdx++;
                        }
                        DBShare.g_RouteInfo[nRouteIdx].nGateCount = nGateIdx;
                        nRouteIdx++;
                    }
                }
                //Conf = new FileStream(DBShare.sConfFileName);
                //DBShare.sMapFile = Conf.ReadString("Setup", "MapFile", DBShare.sMapFile);
                //Conf.Free;
                MapList.Clear();
                if (File.Exists(DBShare.sMapFile))
                {
                    LoadList.Clear();
                    LoadList.LoadFromFile(DBShare.sMapFile);
                    for (var i = 0; i < LoadList.Count; i++)
                    {
                        sLineText = LoadList[i];
                        if ((sLineText != "") && (sLineText[0] == '['))
                        {
                            sLineText = HUtil32.ArrestStringEx(sLineText, "[", "]", ref sMapName);
                            sMapInfo = HUtil32.GetValidStr3(sMapName, ref sMapName, new string[] { " ", "\09" });
                            sServerIndex = HUtil32.GetValidStr3(sMapInfo, ref sMapInfo, new string[] { " ", "\09" }).Trim();
                            nServerIndex = HUtil32.Str_ToInt(sServerIndex, 0);
                            //MapList.Add(sMapName, ((nServerIndex) as Object));
                        }
                    }
                }
                //LoadList.Free;
            }
            finally
            {
            }
        }

        private bool LoadChrNameList(string sFileName)
        {
            bool result = false;
            int i;
            if (File.Exists(sFileName))
            {
                DBShare.DenyChrNameList.LoadFromFile(sFileName);
                i = 0;
                while (true)
                {
                    if (DBShare.DenyChrNameList.Count <= i)
                    {
                        break;
                    }
                    if (DBShare.DenyChrNameList[i].Trim() == "")
                    {
                        DBShare.DenyChrNameList.RemoveAt(i);
                        continue;
                    }
                    i++;
                }
                result = true;
            }
            return result;
        }

        private bool LoadClearMakeIndexList(string sFileName)
        {
            bool result = false;
            int i;
            int nIndex;
            string sLineText;
            if (File.Exists(sFileName))
            {
                DBShare.g_ClearMakeIndex.LoadFromFile(sFileName);
                i = 0;
                while (true)
                {
                    if (DBShare.g_ClearMakeIndex.Count <= i)
                    {
                        break;
                    }
                    sLineText = DBShare.g_ClearMakeIndex[i];
                    nIndex = HUtil32.Str_ToInt(sLineText, -1);
                    if (nIndex < 0)
                    {
                        DBShare.g_ClearMakeIndex.RemoveAt(i);
                        continue;
                    }
                    DBShare.g_ClearMakeIndex[i] = nIndex.ToString();
                    i++;
                }
                result = true;
            }
            return result;
        }

        private void ProcessGateMsg(ref TGateInfo GateInfo)
        {
            string s0C = string.Empty;
            string s10 = string.Empty;
            char s19;
            int i;
            TUserInfo UserInfo;
            while (true)
            {
                if (GateInfo.sText.IndexOf("$", StringComparison.OrdinalIgnoreCase) <= 0)
                {
                    break;
                }
                GateInfo.sText = HUtil32.ArrestStringEx(GateInfo.sText, "%", "$", ref s10);
                if (s10 != "")
                {
                    s19 = s10[1];
                    s10 = s10.Substring(2 - 1, s10.Length - 1);
                    switch (s19)
                    {
                        case '-':
                            SendKeepAlivePacket(GateInfo.Socket);
                            dwKeepAliveTick = HUtil32.GetTickCount();
                            break;
                        case 'A':
                            s10 = HUtil32.GetValidStr3(s10, ref s0C, new string[] { "/" });
                            for (i = 0; i < GateInfo.UserList.Count; i++)
                            {
                                UserInfo = GateInfo.UserList[i];
                                if (UserInfo != null)
                                {
                                    if (UserInfo.sConnID == s0C)
                                    {
                                        UserInfo.s2C = UserInfo.s2C + s10;
                                        if (s10.IndexOf("!", StringComparison.OrdinalIgnoreCase) < 1)
                                        {
                                            continue;
                                        }
                                        ProcessUserMsg(ref UserInfo);
                                        break;
                                    }
                                }
                            }
                            break;
                        case 'O':
                            s10 = HUtil32.GetValidStr3(s10, ref s0C, new string[] { "/" });
                            OpenUser(s0C, s10, ref GateInfo);
                            break;
                        case 'X':
                            CloseUser(s10, ref GateInfo);
                            break;
                    }
                }
            }
        }

        private void SendKeepAlivePacket(Socket Socket)
        {
            if (Socket.Connected)
            { 
                Socket.SendText("%++$");
            }
        }

        private void ProcessUserMsg(ref TUserInfo UserInfo)
        {
            string s10 = string.Empty;
            int nC;
            nC = 0;
            while (true)
            {
                if (HUtil32.TagCount(UserInfo.s2C, '!') <= 0)
                {
                    break;
                }
                UserInfo.s2C = HUtil32.ArrestStringEx(UserInfo.s2C, "#", "!", ref s10);
                if (s10 != "")
                {
                    s10 = s10.Substring(2 - 1, s10.Length - 1);
                    if (s10.Length >= Grobal2.DEFBLOCKSIZE)
                    {
                        DeCodeUserMsg(s10, ref UserInfo);
                    }
                    else
                    {
                        DBShare.n4ADC20++;
                    }
                }
                else
                {
                    DBShare.n4ADC1C++;
                    if (nC >= 1)
                    {
                        UserInfo.s2C = "";
                    }
                    nC++;
                }
            }
        }

        private void OpenUser(string sID, string sIP, ref TGateInfo GateInfo)
        {
            TUserInfo UserInfo;
            string sUserIPaddr = string.Empty;
            string sGateIPaddr = string.Empty;
            sGateIPaddr = HUtil32.GetValidStr3(sIP, ref sUserIPaddr, new string[] { "/" });
            for (var i = 0; i < GateInfo.UserList.Count; i++)
            {
                UserInfo = GateInfo.UserList[i];
                if ((UserInfo != null) && (UserInfo.sConnID == sID))
                {
                    return;
                }
            }
            UserInfo = new TUserInfo();
            UserInfo.sAccount = "";
            UserInfo.sUserIPaddr = sUserIPaddr;
            UserInfo.sGateIPaddr = sGateIPaddr;
            UserInfo.sConnID = sID;
            UserInfo.nSessionID = 0;
            UserInfo.Socket = GateInfo.Socket;
            UserInfo.s2C = "";
            UserInfo.dwTick34 = HUtil32.GetTickCount();
            UserInfo.dwChrTick = HUtil32.GetTickCount();
            UserInfo.boChrSelected = false;
            UserInfo.boChrQueryed = false;
            UserInfo.nSelGateID = GateInfo.nGateID;
            GateInfo.UserList.Add(UserInfo);
        }

        private void CloseUser(string sID, ref TGateInfo GateInfo)
        {
            TUserInfo UserInfo;
            for (var i = 0; i < GateInfo.UserList.Count; i++)
            {
                UserInfo = GateInfo.UserList[i];
                if ((UserInfo != null) && (UserInfo.sConnID == sID))
                {
                    if (!IDSocCli.FrmIDSoc.GetGlobaSessionStatus(UserInfo.nSessionID))
                    {
                        IDSocCli.FrmIDSoc.SendSocketMsg(Grobal2.SS_SOFTOUTSESSION, UserInfo.sAccount + "/" + (UserInfo.nSessionID).ToString());
                        IDSocCli.FrmIDSoc.CloseSession(UserInfo.sAccount, UserInfo.nSessionID);
                    }
                    UserInfo = null;
                    GateInfo.UserList.RemoveAt(i);
                    break;
                }
            }
        }

        private void DeCodeUserMsg(string sData, ref TUserInfo UserInfo)
        {
            string sDefMsg;
            string s18;
            TDefaultMessage Msg;
            sDefMsg = sData.Substring(1 - 1, Grobal2.DEFBLOCKSIZE);
            s18 = sData.Substring(Grobal2.DEFBLOCKSIZE + 1 - 1, sData.Length - Grobal2.DEFBLOCKSIZE);
            Msg = EDcode.DecodeMessage(sDefMsg);
            switch (Msg.Ident)
            {
                case Grobal2.CM_QUERYCHR:
                    if (!UserInfo.boChrQueryed || ((HUtil32.GetTickCount() - UserInfo.dwChrTick) > 200))
                    {
                        UserInfo.dwChrTick = HUtil32.GetTickCount();
                        if (QueryChr(s18, ref UserInfo))
                        {
                            UserInfo.boChrQueryed = true;
                        }
                    }
                    else
                    {
                        DBShare.g_nQueryChrCount++;
                        DBShare.OutMainMessage("[Hacker Attack] QUERYCHR " + UserInfo.sUserIPaddr);
                    }
                    break;
                case Grobal2.CM_NEWCHR:
                    if ((HUtil32.GetTickCount() - UserInfo.dwChrTick) > 1000)
                    {
                        UserInfo.dwChrTick = HUtil32.GetTickCount();
                        if ((UserInfo.sAccount != "") && IDSocCli.FrmIDSoc.CheckSession(UserInfo.sAccount, UserInfo.sUserIPaddr, UserInfo.nSessionID))
                        {
                            NewChr(s18, ref UserInfo);
                            UserInfo.boChrQueryed = false;
                        }
                        else
                        {
                            OutOfConnect(UserInfo);
                        }
                    }
                    else
                    {
                        DBShare.nHackerNewChrCount++;
                        DBShare.OutMainMessage("[Hacker Attack] NEWCHR " + UserInfo.sAccount + "/" + UserInfo.sUserIPaddr);
                    }
                    break;
                case Grobal2.CM_DELCHR:
                    if ((HUtil32.GetTickCount() - UserInfo.dwChrTick) > 1000)
                    {
                        UserInfo.dwChrTick = HUtil32.GetTickCount();
                        if ((UserInfo.sAccount != "") && IDSocCli.FrmIDSoc.CheckSession(UserInfo.sAccount, UserInfo.sUserIPaddr, UserInfo.nSessionID))
                        {
                            DelChr(s18, ref UserInfo);
                            UserInfo.boChrQueryed = false;
                        }
                        else
                        {
                            OutOfConnect(UserInfo);
                        }
                    }
                    else
                    {
                        DBShare.nHackerDelChrCount++;
                        DBShare.OutMainMessage("[Hacker Attack] DELCHR " + UserInfo.sAccount + "/" + UserInfo.sUserIPaddr);
                    }
                    break;
                case Grobal2.CM_SELCHR:
                    if (!UserInfo.boChrQueryed)
                    {
                        if ((UserInfo.sAccount != "") && IDSocCli.FrmIDSoc.CheckSession(UserInfo.sAccount, UserInfo.sUserIPaddr, UserInfo.nSessionID))
                        {
                            if (SelectChr(s18, ref UserInfo))
                            {
                                UserInfo.boChrSelected = true;
                            }
                        }
                        else
                        {
                            OutOfConnect(UserInfo);
                        }
                    }
                    else
                    {
                        DBShare.nHackerSelChrCount++;
                        DBShare.OutMainMessage("Double send SELCHR " + UserInfo.sAccount + "/" + UserInfo.sUserIPaddr);
                    }
                    break;
                default:
                    DBShare.n4ADC24++;
                    break;
            }
        }

        private bool QueryChr(string sData, ref TUserInfo UserInfo)
        {
            bool result;
            string sAccount = string.Empty;
            string sSessionID = string.Empty;
            int nSessionID;
            int nChrCount;
            ArrayList ChrList;
            int I;
            int nIndex;
            THumDataInfo ChrRecord = null;
            THumInfo HumRecord = null;
            //TQuickID QuickID;
            byte btSex;
            string sChrName = string.Empty;
            string sJob = string.Empty;
            string sHair = string.Empty;
            string sLevel = string.Empty;
            string s40 = string.Empty;
            result = false;
            sSessionID = HUtil32.GetValidStr3(EDcode.DeCodeString(sData), ref sAccount, new string[] { "/" });
            nSessionID = HUtil32.Str_ToInt(sSessionID, -2);
            UserInfo.nSessionID = nSessionID;
            nChrCount = 0;
            if (IDSocCli.FrmIDSoc.CheckSession(sAccount, UserInfo.sUserIPaddr, nSessionID))
            {
                IDSocCli.FrmIDSoc.SetGlobaSessionNoPlay(nSessionID);
                UserInfo.sAccount = sAccount;
                ChrList = new ArrayList();
                try
                {
                    //if (HumDB.Open() && (HumDB.FindByAccount(sAccount, ref ChrList) >= 0))
                    //{
                    //    try
                    //    {
                    //        if (HumDB.OpenEx())
                    //        {
                    //            for (I = 0; I < ChrList.Count; I++)
                    //            {
                    //                QuickID = ((TQuickID)(ChrList.Values[I]));
                    //                if (QuickID.nSelectID != UserInfo.nSelGateID) // ���ѡ��ID����,������
                    //                {
                    //                    continue;
                    //                }
                    //                if (HumDB.GetBy(QuickID.nIndex, ref HumRecord) && !HumRecord.boDeleted)
                    //                {
                    //                    sChrName = QuickID.sChrName;
                    //                    nIndex = HumDB.Index(sChrName);
                    //                    if ((nIndex < 0) || (nChrCount >= 2))
                    //                    {
                    //                        continue;
                    //                    }
                    //                    if (HumDB.Get(nIndex, ref ChrRecord) >= 0)
                    //                    {
                    //                        btSex = ChrRecord.Data.btSex;
                    //                        sJob = (ChrRecord.Data.btJob).ToString();
                    //                        sHair = (ChrRecord.Data.btHair).ToString();
                    //                        sLevel = (ChrRecord.Data.Abil.Level).ToString();
                    //                        if (HumRecord.boSelected)
                    //                        {
                    //                            s40 = s40 + "*";
                    //                        }
                    //                        s40 = s40 + sChrName + "/" + sJob + "/" + sHair + "/" + sLevel + "/" + (btSex).ToString() + "/";
                    //                        nChrCount++;
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //    finally
                    //    {
                    //        HumDB.Close();
                    //    }
                    //}
                }
                finally
                {
                    HumDB.Close();
                }
                //ChrList.Free;
                SendUserSocket(UserInfo.Socket, UserInfo.sConnID, EDcode.EncodeMessage(Grobal2.MakeDefaultMsg(Grobal2.SM_QUERYCHR, nChrCount, 0, 1, 0)) + EDcode.EncodeString(s40));
            }
            else
            {
                SendUserSocket(UserInfo.Socket, UserInfo.sConnID, EDcode.EncodeMessage(Grobal2.MakeDefaultMsg(Grobal2.SM_QUERYCHR_FAIL, nChrCount, 0, 1, 0)));
                CloseUser(UserInfo.sConnID, ref CurGate);
            }
            return result;
        }

        private void OutOfConnect(TUserInfo UserInfo)
        {
            TDefaultMessage Msg;
            string sMsg;
            Msg = Grobal2.MakeDefaultMsg(Grobal2.SM_OUTOFCONNECTION, 0, 0, 0, 0);
            sMsg = EDcode.EncodeMessage(Msg);
            SendUserSocket(UserInfo.Socket, sMsg, UserInfo.sConnID);
        }

        public int DelChr_snametolevel(string sName)
        {
            int result;
            int nIndex;
            THumDataInfo ChrRecord = null;
            result = 0;
            try
            {
                if (HumDB.OpenEx())
                {
                    nIndex = HumDB.Index(sName);
                    if (nIndex >= 0)
                    {
                        HumDB.Get(nIndex, ref ChrRecord);
                        result = ChrRecord.Data.Abil.Level;
                    }
                }
            }
            finally
            {
                HumDB.Close();
            }
            return result;
        }

        private void DelChr(string sData, ref TUserInfo UserInfo)
        {
            TDefaultMessage Msg;
            string sMsg;
            int n10;
            THumInfo HumRecord = null;
            int nckr;
            int nIndex;
            var sChrName = EDcode.DeCodeString(sData);
            var boCheck = false;
            try
            {
                if (HumDB.Open())
                {
                    n10 = HumDB.Index(sChrName);
                    //if (n10 >= 0)
                    //{
                    //    HumDB.Get(n10, ref HumRecord);
                    //    nIndex = DelChr_snametolevel(sChrName);
                    //    if (HumRecord.sAccount == UserInfo.sAccount)
                    //    {
                    //        if (nIndex < DBShare.nDELMaxLevel)
                    //        {
                    //            HumRecord.boDeleted = true;
                    //            HumRecord.dModDate = DateTime.Now;
                    //            boCheck = HumDB.Update(n10, ref HumRecord);
                    //        }
                    //        else
                    //        {
                    //            nckr = 1;
                    //        }
                    //    }
                    //}
                }
            }
            finally
            {
                HumDB.Close();
            }
            if (boCheck)
            {
                Msg = Grobal2.MakeDefaultMsg(Grobal2.SM_DELCHR_SUCCESS, 0, 0, 0, 0);
            }
            else
            {
                Msg = Grobal2.MakeDefaultMsg(Grobal2.SM_DELCHR_FAIL, 0, 0, 0, 0);
            }
            sMsg = EDcode.EncodeMessage(Msg);
            SendUserSocket(UserInfo.Socket, UserInfo.sConnID, sMsg);
        }

        private void NewChr(string sData, ref TUserInfo UserInfo)
        {
            string Data = string.Empty;
            string sAccount = string.Empty;
            string sChrName = string.Empty;
            string sHair = string.Empty;
            string sJob = string.Empty;
            string sSex = string.Empty;
            TDefaultMessage Msg;
            string sMsg;
            THumInfo HumRecord;
            int i;
            var nCode = -1;
            Data = EDcode.DeCodeString(sData);
            Data = HUtil32.GetValidStr3(Data, ref sAccount, new string[] { "/" });
            Data = HUtil32.GetValidStr3(Data, ref sChrName, new string[] { "/" });
            Data = HUtil32.GetValidStr3(Data, ref sHair, new string[] { "/" });
            Data = HUtil32.GetValidStr3(Data, ref sJob, new string[] { "/" });
            Data = HUtil32.GetValidStr3(Data, ref sSex, new string[] { "/" });
            if (Data.Trim() != "")
            {
                nCode = 0;
            }
            sChrName = sChrName.Trim();
            if (sChrName.Length < 3)
            {
                nCode = 0;
            }
            if (DBShare.g_boEnglishNames && !HUtil32.IsEnglishStr(sChrName))
            {
                nCode = 0;
            }
            if (!CheckDenyChrName(sChrName))
            {
                nCode = 2;
            }
            if (!DBShare.boDenyChrName)
            {
                if (!DBShare.CheckChrName(sChrName))
                {
                    nCode = 0;
                }
                for (i = 0; i <= sChrName.Length; i++)
                {
                    if ((sChrName[i] == '?') || (sChrName[i] == ' ') || (sChrName[i] == '/') || (sChrName[i] == '@') || (sChrName[i] == '?') || (sChrName[i] == '\'') ||
                        (sChrName[i] == '\'') || (sChrName[i] == '\\') || (sChrName[i] == '.') || (sChrName[i] == ',') || (sChrName[i] == ':') || (sChrName[i] == ';') ||
                        (sChrName[i] == '`') || (sChrName[i] == '~') || (sChrName[i] == '!') || (sChrName[i] == '#') || (sChrName[i] == '$') || (sChrName[i] == '%') ||
                        (sChrName[i] == '^') || (sChrName[i] == '&') || (sChrName[i] == '*') || (sChrName[i] == '(') || (sChrName[i] == ')') || (sChrName[i] == '-') ||
                        (sChrName[i] == '_') || (sChrName[i] == '+') || (sChrName[i] == '=') || (sChrName[i] == '|') || (sChrName[i] == '[') || (sChrName[i] == '{') ||
                        (sChrName[i] == ']') || (sChrName[i] == '}'))
                    {
                        nCode = 0;
                    }
                }
            }
            if (nCode == -1)
            {
                try
                {
                    HumDB.__Lock();
                    if (HumDB.Index(sChrName) >= 0)
                    {
                        nCode = 2;
                    }
                }
                finally
                {
                    HumDB.UnLock();
                }
                try
                {
                    if (HumDB.Open())
                    {
                        //if (HumDB.ChrCountOfAccount(sAccount) < 2)
                        //{
                        //    HumRecord = new THumInfo();
                        //    HumRecord.sChrName = sChrName;
                        //    HumRecord.sAccount = sAccount;
                        //    HumRecord.boDeleted = false;
                        //    HumRecord.btCount = 0;
                        //    HumRecord.Header.sName = sChrName;
                        //    HumRecord.Header.nSelectID = UserInfo.nSelGateID;
                        //    if (HumRecord.Header.sName != "")
                        //    {
                        //        if (!HumDB.Add(ref HumRecord))
                        //        {
                        //            nCode = 2;
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    nCode = 3;
                        //}
                    }
                }
                finally
                {
                    HumDB.Close();
                }
                if (nCode == -1)
                {
                    if (NewChrData(sChrName, HUtil32.Str_ToInt(sSex, 0), HUtil32.Str_ToInt(sJob, 0), HUtil32.Str_ToInt(sHair, 0)))
                    {
                        nCode = 1;
                    }
                }
                else
                {
                    DBSMain.FrmDBSrv.DelHum(sChrName);
                    nCode = 4;
                }
            }
            if (nCode == 1)
            {
                Msg = Grobal2.MakeDefaultMsg(Grobal2.SM_NEWCHR_SUCCESS, 0, 0, 0, 0);
            }
            else
            {
                Msg = Grobal2.MakeDefaultMsg(Grobal2.SM_NEWCHR_FAIL, nCode, 0, 0, 0);
            }
            sMsg = EDcode.EncodeMessage(Msg);
            SendUserSocket(UserInfo.Socket, UserInfo.sConnID, sMsg);
        }

        private bool SelectChr(string sData, ref TUserInfo UserInfo)
        {
            string sAccount = string.Empty;
            ArrayList ChrList;
            THumInfo HumRecord = null;
            int I;
            int nIndex;
            int nMapIndex;
            //TQuickID QuickID;
            THumDataInfo ChrRecord = null;
            string sCurMap = string.Empty;
            bool boDataOK;
            string sDefMsg = string.Empty;
            string sRouteMsg = string.Empty;
            string sRouteIP = string.Empty;
            int nRoutePort = 0;
            var result = false;
            var sChrName = HUtil32.GetValidStr3(EDcode.DeCodeString(sData), ref sAccount, new string[] { "/" });
            boDataOK = false;
            if (UserInfo.sAccount == sAccount)
            {
                try
                {
                    //if (HumDB.Open())
                    //{
                    //    ChrList = new ArrayList();
                    //    if (HumDB.FindByAccount(sAccount, ref ChrList) >= 0)
                    //    {
                    //        for (I = 0; I < ChrList.Count; I++)
                    //        {
                    //            QuickID = ((TQuickID)(ChrList.Values[I]));
                    //            nIndex = QuickID.nIndex;
                    //            if (HumDB.GetBy(nIndex, ref HumRecord))
                    //            {
                    //                if (HumRecord.sChrName == sChrName)
                    //                {
                    //                    HumRecord.boSelected = true;
                    //                    HumDB.UpdateBy(nIndex, ref HumRecord);
                    //                }
                    //                else
                    //                {
                    //                    if (HumRecord.boSelected)
                    //                    {
                    //                        HumRecord.boSelected = false;
                    //                        HumDB.UpdateBy(nIndex, ref HumRecord);
                    //                    }
                    //                }
                    //            }
                    //        }
                    //    }
                    //    //ChrList.Free;
                    //}
                }
                finally
                {
                    HumDB.Close();
                }
                try
                {
                    if (HumDB.OpenEx())
                    {
                        nIndex = HumDB.Index(sChrName);
                        if (nIndex >= 0)
                        {
                            HumDB.Get(nIndex, ref ChrRecord);
                            sCurMap = ChrRecord.Data.sCurMap;
                            boDataOK = true;
                        }
                    }
                }
                finally
                {
                    HumDB.Close();
                }
            }
            if (boDataOK)
            {
                nMapIndex = GetMapIndex(sCurMap);
                sDefMsg = EDcode.EncodeMessage(Grobal2.MakeDefaultMsg(Grobal2.SM_STARTPLAY, 0, 0, 0, 0));
                sRouteIP = GateRouteIP(CurGate.sGateaddr, ref nRoutePort);
                if (DBShare.g_boDynamicIPMode)// ʹ�ö�̬IP
                {
                    sRouteIP = UserInfo.sGateIPaddr;
                }
                sRouteMsg = EDcode.EncodeString(sRouteIP + "/" + (nRoutePort + nMapIndex).ToString());
                SendUserSocket(UserInfo.Socket, UserInfo.sConnID, sDefMsg + sRouteMsg);
                IDSocCli.FrmIDSoc.SetGlobaSessionPlay(UserInfo.nSessionID);
                result = true;
            }
            else
            {
                SendUserSocket(UserInfo.Socket, UserInfo.sConnID, EDcode.EncodeMessage(Grobal2.MakeDefaultMsg(Grobal2.SM_STARTFAIL, 0, 0, 0, 0)));
            }
            return result;
        }

        private int GateRoutePort(string sGateIP)
        {
            return 7200;
        }

        private string GateRouteIP_GetRoute(TRouteInfo RouteInfo, ref int nGatePort)
        {
            var nGateIndex = (new System.Random(RouteInfo.nGateCount)).Next();
            var result = RouteInfo.sGameGateIP[nGateIndex];
            nGatePort = RouteInfo.nGameGatePort[nGateIndex];
            return result;
        }

        private string GateRouteIP(string sGateIP, ref int nPort)
        {
            string result = string.Empty;
            TRouteInfo RouteInfo;
            nPort = 0;
            for (var i = DBShare.g_RouteInfo.GetLowerBound(0); i <= DBShare.g_RouteInfo.GetUpperBound(0); i++)
            {
                RouteInfo = DBShare.g_RouteInfo[i];
                if (RouteInfo.sSelGateIP == sGateIP)
                {
                    result = GateRouteIP_GetRoute(RouteInfo, ref nPort);
                    break;
                }
            }
            return result;
        }

        private int GetMapIndex(string sMap)
        {
            int result = 0;
            for (var i = 0; i < MapList.Count; i++)
            {
                if (MapList[i] == sMap)
                {
                    //result = MapList.Values[i];
                    break;
                }
            }
            return result;
        }

        private void SendUserSocket(Socket Socket, string sSessionID, string sSendMsg)
        {
            Socket.SendText("%" + sSessionID + "/#" + sSendMsg + "!$");
        }

        private bool CheckDenyChrName(string sChrName)
        {
            bool result= true;
            for (var i = 0; i < DBShare.DenyChrNameList.Count; i++)
            {
                if (string.Compare((sChrName).ToLower(), (DBShare.DenyChrNameList[i]).ToLower(), StringComparison.OrdinalIgnoreCase) == 0)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

    } 
}

namespace DBSvr
{
    public class UsrSoc
    {
        public static TFrmUserSoc FrmUserSoc = null;
    } 
}

