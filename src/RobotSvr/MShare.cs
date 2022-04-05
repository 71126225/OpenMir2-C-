using System;
using System.Collections;
using System.Drawing;
using System.IO;
using SystemModule;

namespace RobotSvr
{
    public class MShare
    {
        public static THumTitle[] g_Titles;
        public static THumTitle g_ActiveTitle = null;
        public static THumTitle[] g_hTitles;
        public static THumTitle g_hActiveTitle = null;
#if DEBUG_LOGIN
        public static byte g_fWZLFirst = 7;
#else
        public static byte g_fWZLFirst = 7;
#endif
        public static bool g_boLogon = false;
        public static bool g_fGoAttack = false;
        // g_QueryWinBottomTick: Longword;
        // g_fGetRenderBottom: Boolean = False;
        public static int g_nDragonRageStateIndex = 0;
        public static int AAX = 26 + 14;
        public static int LMX = 30;
        public static int LMY = 26;
        public static int VIEWWIDTH = 8;
        public static Rectangle g_SkidAD_Rect = null;
        public static Rectangle g_SkidAD_Rect2 = null;
        public static Rectangle G_RC_SQUENGINER = null;
        public static Rectangle G_RC_IMEMODE = null;
        // ====================================��Ʒ====================================
        public static byte g_BuildBotTex = 0;
        public static byte g_WinBottomType = 0;
        public static bool g_Windowed = false;
        // g_pkeywords: PString = nil;
        public static bool g_boAutoPickUp = true;
        public static bool g_boPickUpAll = false;
        public static int g_ptItems_Pos = -1;
        public static int g_ptItems_Type = 0;
        public static TCnHashTableSmall g_ItemsFilter_All = null;
        public static TCnHashTableSmall g_ItemsFilter_All_Def = null;
        public static ArrayList g_ItemsFilter_Dress = null;
        public static ArrayList g_ItemsFilter_Weapon = null;
        public static ArrayList g_ItemsFilter_Headgear = null;
        public static ArrayList g_ItemsFilter_Drug = null;
        public static ArrayList g_ItemsFilter_Other = null;
        public static ArrayList g_xMapDescList = null;
        public static ArrayList g_xCurMapDescList = null;
        public static byte[] g_pWsockAddr = new byte[4 + 1];
        // g_dwImgThreadId: Longword = 0;
        // g_hImagesThread: THandle = INVALID_HANDLE_VALUE;
        public static int g_nMagicRange = 8;
        public static int g_TileMapOffSetX = 9;
        public static int g_TileMapOffSetY = 9;
        public static byte g_btMyEnergy = 0;
        public static byte g_btMyLuck = 0;
        public static TItemShine g_tiOKShow =
    {0, 0};
        public static TItemShine g_tiFailShow =
    {0, 0};
        public static TItemShine g_tiOKShow2 =
    {0, 0};
        public static TItemShine g_tiFailShow2 =
    {0, 0};
        public static TItemShine g_spOKShow2 =
    {0, 0};
        public static TItemShine g_spFailShow2 =
    {0, 0};
        public static string g_tiHintStr1 = "";
        public static string g_tiHintStr2 = "";
        public static TMovingItem[] g_TIItems = new TMovingItem[1 + 1];
        public static string g_spHintStr1 = "";
        public static string g_spHintStr2 = "";
        public static TMovingItem[] g_spItems = new TMovingItem[1 + 1];
        public static int g_SkidAD_Count = 0;
        public static int g_SkidAD_Count2 = 0;
        public static string g_lastHeroSel = String.Empty;
        public static THeroInfo[] g_heros;
        public static byte g_ItemWear = 0;
        public static byte g_ShowSuite = 0;
        public static byte g_ShowSuite2 = 0;
        public static byte g_ShowSuite3 = 0;
        public static byte g_SuiteSpSkill = 0;
        public static int g_SuiteIdx = -1;
        public static ArrayList g_SuiteItemsList = null;
        public static ArrayList g_TitlesList = null;
        public static byte g_btSellType = 0;
        public static bool g_showgamegoldinfo = false;
        public static bool SSE_AVAILABLE = false;
        public static int g_lWavMaxVol = 68;
        // -100;
        public static long g_uDressEffectTick = 0;
        public static long g_sDressEffectTick = 0;
        public static long g_hDressEffectTick = 0;
        public static int g_uDressEffectIdx = 0;
        public static int g_sDressEffectIdx = 0;
        public static int g_hDressEffectIdx = 0;
        public static long g_uWeaponEffectTick = 0;
        public static long g_sWeaponEffectTick = 0;
        public static long g_hWeaponEffectTick = 0;
        public static int g_uWeaponEffectIdx = 0;
        public static int g_sWeaponEffectIdx = 0;
        public static int g_hWeaponEffectIdx = 0;
        public static int g_ChatWindowLines = 12;
        public static bool g_LoadBeltConfig = false;
        public static bool g_BeltMode = true;
        public static int g_BeltPositionX = 408;
        public static int g_BeltPositionY = 487;
        public static int g_dwActorLimit = 5;
        public static int g_nProcActorIDx = 0;
        public static bool g_boPointFlash = false;
        public static long g_PointFlashTick = 0;
        public static bool g_boHPointFlash = false;
        public static long g_HPointFlashTick = 0;
        public static TVenationInfo[] g_VenationInfos = {
    {0, 0} ,
    {0, 0} ,
    {0, 0} ,
    {0, 0} };
        // ������Ϣ
        public static TVenationInfo[] g_hVenationInfos = {
    {0, 0} ,
    {0, 0} ,
    {0, 0} ,
    {0, 0} };
        // ������Ϣ
        public static bool g_NextSeriesSkill = false;
        public static long g_dwSeriesSkillReadyTick = 0;
        public static int g_nCurrentMagic = 888;
        public static int g_nCurrentMagic2 = 888;
        public static long g_SendFireSerieSkillTick = 0;
        public static long g_IPointLessHintTick = 0;
        public static long g_MPLessHintTick = 0;
        public static int g_SeriesSkillStep = 0;
        public static bool g_SeriesSkillFire_100 = false;
        public static bool g_SeriesSkillFire = false;
        public static bool g_SeriesSkillReady = false;
        public static bool g_SeriesSkillReadyFlash = false;
        // g_TempMagicArr            : array[0..3] of TTempSeriesSkillA;
        public static byte[] g_TempSeriesSkillArr;
        public static byte[] g_HTempSeriesSkillArr;
        public static byte[] g_SeriesSkillArr = new byte[3 + 1];
        // TSeriesSkill;
        public static ArrayList g_SeriesSkillSelList = null;
        public static ArrayList g_hSeriesSkillSelList = null;
        public static long g_dwAutoTecTick = 0;
        public static long g_dwAutoTecHeroTick = 0;
        // g_ProcOnIdleTick: Longword;
        // g_boProcMessagePacket: Boolean = False;
        public static long g_dwProcMessagePacket = 0;
        public static long g_ProcNowTick = 0;
        public static bool g_ProcCanFill = true;
        // g_ProcOnDrawTick: Longword;
        public static long g_ProcOnDrawTick_Effect = 0;
        public static long g_ProcOnDrawTick_Effect2 = 0;
        // g_ProcCanDraw: Boolean;
        // g_ProcCanDraw_Effect      : Boolean;
        // g_ProcCanDraw_Effect2     : Boolean;
        public static long g_dwImgMgrTick = 0;
        public static int g_nImgMgrIdx = 0;
        // ProcImagesCS              : TRTLCriticalSection;
        public static TRTLCriticalSection ProcMsgCS = null;
        public static TRTLCriticalSection ThreadCS = null;
        public static bool g_bIMGBusy = false;
        // g_dwCurrentTick           : PLongWord;
        public static long g_dwThreadTick = 0;
        public static long g_rtime = 0;
        public static long g_dwLastThreadTick = 0;
        public static bool g_boExchgPoison = false;
        public static bool g_boCheckTakeOffPoison = false;
        public static int g_Angle = 0;
        public static bool g_ShowMiniMapXY = false;
        public static bool g_DrawingMiniMap = false;
        public static bool g_DrawMiniBlend = false;
        // g_MiniMapRC: TRect;
        public static bool g_boTakeOnPos = true;
        public static bool g_boHeroTakeOnPos = true;
        public static bool g_boQueryDynCode = false;
        public static bool g_boQuerySelChar = false;
        // g_pRcHeader: pTRcHeader;
        // g_bLoginKey: PBoolean;
        // g_pbInitSock: PBoolean;
        public static bool g_pbRecallHero = false;
        // g_dwCheckTick             : LongWord = 0;
        public static int g_dwCheckCount = 0;
        public static int g_nBookPath = 0;
        public static int g_nBookPage = 0;
        public static int g_HillMerchant = 0;
        public static string g_sBookLabel = "";
        public static int g_MaxExpFilter = 2000;
        public static bool g_boDrawLevelRank = false;
        public static THeroLevelRank[] g_HeroLevelRanks;
        public static THumanLevelRank[] g_HumanLevelRanks;
        public static string[] g_UnBindItems = { "����ѩ˪", "����ҩ", "ǿЧ̫��ˮ", "ǿЧ��ҩ", "ǿЧħ��ҩ", "��ҩ(С��)", "ħ��ҩ(С��)", "��ҩ(����)", "ħ��ҩ(����)", "�������Ѿ�", "������;�", "�سǾ�", "�л�سǾ�" };
        public static string g_sLogoText = "LegendSoft";
        public static string g_sGoldName = "���";
        public static string g_sGameGoldName = "Ԫ��";
        public static string g_sGamePointName = "�ݵ�";
        public static string g_sWarriorName = "��ʿ";
        // ְҵ����
        public static string g_sWizardName = "ħ��ʦ";
        // ְҵ����
        public static string g_sTaoistName = "��ʿ";
        // ְҵ����
        public static string g_sUnKnowName = "δ֪";
        public static string g_sMainParam1 = String.Empty;
        // ��ȡ���ò���
        public static string g_sMainParam2 = String.Empty;
        // ��ȡ���ò���
        public static string g_sMainParam3 = String.Empty;
        // ��ȡ���ò���
        public static string g_sMainParam4 = String.Empty;
        // ��ȡ���ò���
        public static string g_sMainParam5 = String.Empty;
        // ��ȡ���ò���
        public static string g_sMainParam6 = String.Empty;
        // ��ȡ���ò���
        public static bool g_boCanDraw = true;
        public static bool g_boInitialize = false;
        public static int g_nInitializePer = 0;
        public static bool g_boQueryExit = false;
        public static string g_FontName = String.Empty;
        public static int g_FontSize = 0;
        public static byte[] g_PowerBlock = { 0x55, 0x8B, 0xEC, 0x83, 0xC4, 0xE8, 0x89, 0x55, 0xF8, 0x89, 0x45, 0xFC, 0xC7, 0x45, 0xEC, 0xE8, 0x03, 0x00, 0x00, 0xC7, 0x45, 0xE8, 0x64, 0x00, 0x00, 0x00, 0xDB, 0x45, 0xEC, 0xDB, 0x45, 0xE8, 0xDE, 0xF9, 0xDB, 0x45, 0xFC, 0xDE, 0xC9, 0xDD, 0x5D, 0xF0, 0x9B, 0x8B, 0x45, 0xF8, 0x8B, 0x00, 0x8B, 0x55, 0xF8, 0x89, 0x02, 0xDD, 0x45, 0xF0, 0x8B, 0xE5, 0x5D, 0xC3, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        public static byte[] g_PowerBlock1 = { 0x55, 0x8B, 0xEC, 0x83, 0xC4, 0xE8, 0x89, 0x55, 0xF8, 0x89, 0x45, 0xFC, 0xC7, 0x45, 0xEC, 0x64, 0x00, 0x00, 0x00, 0xC7, 0x45, 0xE8, 0x64, 0x00, 0x00, 0x00, 0xDB, 0x45, 0xEC, 0xDB, 0x45, 0xE8, 0xDE, 0xF9, 0xDB, 0x45, 0xFC, 0xDE, 0xC9, 0xDD, 0x5D, 0xF0, 0x9B, 0x8B, 0x45, 0xF8, 0x8B, 0x00, 0x8B, 0x55, 0xF8, 0x89, 0x02, 0xDD, 0x45, 0xF0, 0x8B, 0xE5, 0x5D, 0xC3, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        // g_RegInfo                 : TRegInfo;
        public static string g_sServerName = String.Empty;
        // ��������ʾ����
        public static string g_sServerMiniName = String.Empty;
        // ����������
        public static string g_psServerAddr = String.Empty;
        public static int g_pnServerPort = 0;
        public static string g_sSelChrAddr = String.Empty;
        public static int g_nSelChrPort = 0;
        public static string g_sRunServerAddr = String.Empty;
        public static int g_nRunServerPort = 0;
        public static int g_nTopDrawPos = 0;
        public static int g_nLeftDrawPos = 0;
        public static bool g_boSendLogin = false;
        // �Ƿ��͵�¼��Ϣ
        public static bool g_boServerConnected = false;
        public static bool g_SoftClosed = false;
        // С����Ϸ���
        public static TChrAction g_ChrAction;
        public static TConnectionStep g_ConnectionStep;
        // g_boSound                 : Boolean;
        // g_boBGSound               : Boolean;
        // g_MainFont: string = '����';
        // g_FontArr: array[0..MAXFONT - 1] of string = (
        // '����',
        // '������',
        // '����',
        // '����',
        // 'Courier New',
        // 'Arial',
        // 'MS Sans Serif',
        // 'Microsoft Sans Serif'
        // );
        // g_OldTime: Longword;
        // g_nCurFont: Integer = 0;
        // g_sCurFontName: string = '����';
        public static bool g_boFullScreen = false;
        public static byte g_btMP3Volume = 70;
        public static byte g_btSoundVolume = 70;
        public static bool g_boBGSound = true;
        public static bool g_boSound = true;
        public static bool g_DlgInitialize = false;
        // g_HintSurface_W: TDirectDrawSurface;
        // g_BotSurface: TDirectDrawSurface;
        public static bool g_boFirstActive = true;
        public static bool g_boFirstTime = false;
        public static string g_sMapTitle = String.Empty;
        public static int g_nLastMapMusic = -1;
        public static ArrayList g_SendSayList = null;
        public static int g_SendSayListIdx = 0;
        public static ArrayList g_ServerList = null;
        public static THStringList g_GroupMembers = null;
        public static ArrayList g_SoundList = null;
        public static ArrayList BGMusicList = null;
        public static long g_DxFontsMgrTick = 0;
        public static TClientMagic[,] g_MagicArr = new TClientMagic[2 + 1, 255 + 1];
        public static ArrayList g_MagicList = null;
#if SERIESSKILL
        public static ArrayList g_MagicList2 = null;
        public static ArrayList g_hMagicList2 = null;
#endif // SERIESSKILL
        public static ArrayList g_IPMagicList = null;
        public static ArrayList g_HeroMagicList = null;
        public static ArrayList g_HeroIPMagicList = null;
        public static ArrayList[] g_ShopListArr = new ArrayList[5 + 1];
        public static ArrayList g_SaveItemList = null;
        public static ArrayList g_MenuItemList = null;
        public static ArrayList g_DropedItemList = null;
        public static ArrayList g_ChangeFaceReadyList = null;
        public static ArrayList g_FreeActorList = null;
        public static int g_PoisonIndex = 0;
        public static int g_nBonusPoint = 0;
        public static int g_nSaveBonusPoint = 0;
        public static TNakedAbility g_BonusTick = null;
        public static TNakedAbility g_BonusAbil = null;
        public static TNakedAbility g_NakedAbil = null;
        public static TNakedAbility g_BonusAbilChg = null;
        public static string g_sGuildName = String.Empty;
        public static string g_sGuildRankName = String.Empty;
        public static long g_dwLatestJoinAttackTick = 0;
        // ���ħ������ʱ��
        public static long g_dwLastAttackTick = 0;
        // ��󹥻�ʱ��(������������ħ������)
        public static long g_dwLastMoveTick = 0;
        // ����ƶ�ʱ��
        public static long g_dwLatestSpellTick = 0;
        // ���ħ������ʱ��
        public static long g_dwLatestFireHitTick = 0;
        // ����л𹥻�ʱ��
        public static long g_dwLatestSLonHitTick = 0;
        // ����л𹥻�ʱ��
        public static long g_dwLatestTwinHitTick = 0;
        public static long g_dwLatestPursueHitTick = 0;
        public static long g_dwLatestRushHitTick = 0;
        public static long g_dwLatestSmiteHitTick = 0;
        public static long g_dwLatestSmiteLongHitTick = 0;
        public static long g_dwLatestSmiteLongHitTick2 = 0;
        public static long g_dwLatestSmiteLongHitTick3 = 0;
        public static long g_dwLatestSmiteWideHitTick = 0;
        public static long g_dwLatestSmiteWideHitTick2 = 0;
        public static long g_dwLatestRushRushTick = 0;
        // ����ƶ�ʱ��
        // g_dwLatestStruckTick      : LongWord; //�������ʱ��
        // g_dwLatestHitTick         : LongWord; //���������ʱ��(�������ƹ���״̬�����˳���Ϸ)
        // g_dwLatestMagicTick       : LongWord; //����ħ��ʱ��(�������ƹ���״̬�����˳���Ϸ)
        public static long g_dwMagicDelayTime = 0;
        public static long g_dwMagicPKDelayTime = 0;
        public static int g_nMouseCurrX = 0;
        // ������ڵ�ͼλ������X
        public static int g_nMouseCurrY = 0;
        // ������ڵ�ͼλ������Y
        public static int g_nMouseX = 0;
        // ���������Ļλ������X
        public static int g_nMouseY = 0;
        // ���������Ļλ������Y
        public static int g_nTargetX = 0;
        // Ŀ������
        public static int g_nTargetY = 0;
        // Ŀ������
        public static TActor g_TargetCret = null;
        public static TActor g_FocusCret = null;
        public static TActor g_MagicTarget = null;
        public static Link g_APQueue = null;
        public static ArrayList g_APPathList = null;
        public static double[,] g_APPass;
        // array[0..MAXX * 3, 0..MAXY * 3] of DWORD;
        public static TActor g_APTagget = null;
        // /////////////////////////////
        public static long g_APRunTick = 0;
        public static long g_APRunTick2 = 0;
        public static TDropItem g_AutoPicupItem = null;
        public static int g_nAPStatus = 0;
        public static bool g_boAPAutoMove = false;
        public static bool g_boLongHit = false;
        public static string g_sAPStr = String.Empty;
        public static int g_boAPXPAttack = 0;
        public static long m_dwSpellTick = 0;
        public static long m_dwRecallTick = 0;
        public static long m_dwDoubluSCTick = 0;
        public static long m_dwPoisonTick = 0;
        public static int m_btMagPassTh = 0;
        public static int g_nTagCount = 0;
        public static long m_dwTargetFocusTick = 0;
        public static THStringList g_APPickUpList = null;
        public static THStringList g_APMobList = null;
        public static THStringList g_ItemDesc = null;
        public static int g_AttackInvTime = 900;
        public static TActor g_AttackTarget = null;
        public static long g_dwSearchEnemyTick = 0;
        public static bool g_boAllowJointAttack = false;
        // ////////////////////////////////////////
        public static byte g_nAPReLogon = 0;
        public static bool g_nAPrlRecallHero = false;
        public static bool g_nAPSendSelChr = false;
        public static bool g_nAPSendNotice = false;
        public static long g_nAPReLogonWaitTick = 0;
        public static int g_nAPReLogonWaitTime = 10 * 1000;
        public static int g_ApLastSelect = 0;
        public static int g_nOverAPZone = 0;
        public static int g_nOverAPZone2 = 0;
        public static bool g_APGoBack = false;
        public static bool g_APGoBack2 = false;
        public static int g_APStep = -1;
        public static int g_APStep2 = -1;
        public static Point[] g_APMapPath;
        public static Point[] g_APMapPath2;
        public static Point g_APLastPoint = null;
        public static Point g_APLastPoint2 = null;
        public static bool g_nApMiniMap = false;
        public static long g_dwBlinkTime = 0;
        public static bool g_boViewBlink = false;
        // g_boAttackSlow            : Boolean;  //��������ʱ����������.
        // g_boAttackFast            : Byte = 0;
        // g_boMoveSlow              : Boolean;  //���ز���ʱ��������
        // g_nMoveSlowLevel          : Integer;
        public static bool g_boMapMoving = false;
        public static bool g_boMapMovingWait = false;
        public static bool g_boCheckBadMapMode = false;
        public static bool g_boCheckSpeedHackDisplay = false;
        public static bool g_boViewMiniMap = false;
        // �Ƿ����С��ͼ Ĭ��ΪTrue
        public static int g_nViewMinMapLv = 0;
        // С��ͼ��ʾ�ȼ�
        public static int g_nMiniMapIndex = 0;
        // С��ͼ�������
        public static int g_nMiniMapX = 0;
        // Сͼ���ָ������X
        public static int g_nMiniMapY = 0;
        // Сͼ���ָ������Y
        // NPC ���
        public static int g_nCurMerchant = 0;
        // NPC��Ի���
        public static int g_nCurMerchantFaceIdx = 0;
        // Development 2019-01-14
        public static int g_nMDlgX = 0;
        public static int g_nMDlgY = 0;
        public static int g_nStallX = 0;
        public static int g_nStallY = 0;
        public static long g_dwChangeGroupModeTick = 0;
        public static long g_dwDealActionTick = 0;
        public static long g_dwQueryMsgTick = 0;
        public static int g_nDupSelection = 0;
        public static bool g_boAllowGroup = false;
        // ������Ϣ���
        public static int g_nMySpeedPoint = 0;
        // ����
        public static int g_nMyHitPoint = 0;
        // ׼ȷ
        public static int g_nMyAntiPoison = 0;
        // ħ�����
        public static int g_nMyPoisonRecover = 0;
        // �ж��ָ�
        public static int g_nMyHealthRecover = 0;
        // �����ָ�
        public static int g_nMySpellRecover = 0;
        // ħ���ָ�
        public static int g_nMyAntiMagic = 0;
        // ħ�����
        public static int g_nMyHungryState = 0;
        // ����״̬
        public static int g_nMyIPowerRecover = 0;
        // �ж��ָ�
        public static int g_nMyAddDamage = 0;
        public static int g_nMyDecDamage = 0;
        // g_nMyGameDiamd            : Integer = 0;
        // g_nMyGameGird             : Integer = 0;
        // g_nMyGameGold             : Integer = 0;
        public static int g_nHeroSpeedPoint = 0;
        // ����
        public static int g_nHeroHitPoint = 0;
        // ׼ȷ
        public static int g_nHeroAntiPoison = 0;
        // ħ�����
        public static int g_nHeroPoisonRecover = 0;
        // �ж��ָ�
        public static int g_nHeroHealthRecover = 0;
        // �����ָ�
        public static int g_nHeroSpellRecover = 0;
        // ħ���ָ�
        public static int g_nHeroAntiMagic = 0;
        // ħ�����
        public static int g_nHeroHungryState = 0;
        // ����״̬
        public static int g_nHeroBagSize = 40;
        public static int g_nHeroIPowerRecover = 0;
        public static int g_nHeroAddDamage = 0;
        public static int g_nHeroDecDamage = 0;
        public static short g_wAvailIDDay = 0;
        public static short g_wAvailIDHour = 0;
        public static short g_wAvailIPDay = 0;
        public static short g_wAvailIPHour = 0;
        public static THumActor g_MySelf = null;
        public static THumActor g_MyDrawActor = null;
        public static string g_sAttackMode = "";
        public static string sAttackModeOfAll = "[ȫ�幥��ģʽ]";
        public static string sAttackModeOfPeaceful = "[��ƽ����ģʽ]";
        public static string sAttackModeOfDear = "[���޹���ģʽ]";
        public static string sAttackModeOfMaster = "[ʦͽ����ģʽ]";
        public static string sAttackModeOfGroup = "[���鹥��ģʽ]";
        public static string sAttackModeOfGuild = "[�лṥ��ģʽ]";
        public static string sAttackModeOfRedWhite = "[�ƶ񹥻�ģʽ]";
        public static int g_RIWhere = 0;
        public static TMovingItem[] g_RefineItems = new TMovingItem[2 + 1];
        public static int g_BuildAcusesStep = 0;
        public static int g_BuildAcusesProc = 0;
        public static long g_BuildAcusesProcTick = 0;
        public static int g_BuildAcusesSuc = -1;
        public static int g_BuildAcusesSucFrame = 0;
        public static long g_BuildAcusesSucFrameTick = 0;
        public static long g_BuildAcusesFrameTick = 0;
        public static int g_BuildAcusesFrame = 0;
        public static int g_BuildAcusesRate = 0;
        public static TMovingItem[] g_BuildAcuses = new TMovingItem[7 + 1];
        public static int g_BAFirstShape = -1;
        public static TClientItem[] g_tui = new TClientItem[13 + 1];
        public static TClientItem[] g_UseItems = new TClientItem[Grobal2.U_FASHION + 1];
        public static TClientItem[] g_HeroUseItems = new TClientItem[Grobal2.U_FASHION + 1];
        public static TUserStateInfo UserState1 = null;
        public static TItemShine g_detectItemShine = null;
        public static TItemShine[] UserState1Shine = new TItemShine[Grobal2.U_FASHION + 1];
        public static TItemShine[] g_UseItemsShine = new TItemShine[Grobal2.U_FASHION + 1];
        public static TItemShine[] g_HeroUseItemsShine = new TItemShine[Grobal2.U_FASHION + 1];
        public static TClientItem[] g_ItemArr = new TClientItem[MAXBAGITEMCL - 1 + 1];
        public static TClientItem[] g_HeroItemArr = new TClientItem[MAXBAGITEMCL - 1 + 1];
        public static TItemShine[] g_ItemArrShine = new TItemShine[MAXBAGITEMCL - 1 + 1];
        public static TItemShine[] g_HeroItemArrShine = new TItemShine[MAXBAGITEMCL - 1 + 1];
        public static TItemShine[] g_StallItemArrShine = new TItemShine[10 - 1 + 1];
        public static TItemShine[] g_uStallItemArrShine = new TItemShine[10 - 1 + 1];
        public static TItemShine[] g_DealItemsShine = new TItemShine[10 - 1 + 1];
        public static TItemShine[] g_DealRemoteItemsShine = new TItemShine[20 - 1 + 1];
        public static TItemShine g_MovingItemShine = null;
        public static bool g_boBagLoaded = false;
        public static bool g_boServerChanging = false;
        public static int g_nCaptureSerial = 0;
        // ץͼ�ļ������
        // g_nSendCount              : Integer; //���Ͳ�������
        public static int g_nReceiveCount = 0;
        // �ӸĲ���״̬����
        public static int g_nTestSendCount = 0;
        public static int g_nTestReceiveCount = 0;
        public static int g_nSpellCount = 0;
        // ʹ��ħ������
        public static int g_nSpellFailCount = 0;
        // ʹ��ħ��ʧ�ܼ���
        public static int g_nFireCount = 0;
        public static int g_nDebugCount = 0;
        public static int g_nDebugCount1 = 0;
        public static int g_nDebugCount2 = 0;
        // �������
        public static TClientItem g_SellDlgItem = null;
        public static TMovingItem g_TakeBackItemWait = null;
        public static TMovingItem g_SellDlgItemSellWait = null;
        // TClientItem;
        public static TClientItem g_DetectItem = null;
        public static int g_DetectItemMineID = 0;
        public static TClientItem g_DealDlgItem = null;
        public static bool g_boQueryPrice = false;
        public static long g_dwQueryPriceTime = 0;
        public static string g_sSellPriceStr = String.Empty;
        // �������
        public static TClientItem[] g_DealItems = new TClientItem[9 + 1];
        public static bool g_boYbDealing = false;
        public static TClientPS g_YbDealInfo = null;
        public static TClientItem[] g_YbDealItems = new TClientItem[9 + 1];
        public static TClientItem[] g_DealRemoteItems = new TClientItem[19 + 1];
        public static int g_nDealGold = 0;
        public static int g_nDealRemoteGold = 0;
        public static bool g_boDealEnd = false;
        public static string g_sDealWho = String.Empty;
        public static TClientItem g_MouseItem = null;
        public static TClientItem g_MouseStateItem = null;
        public static TClientItem g_HeroMouseStateItem = null;
        public static TClientItem g_MouseUserStateItem = null;
        public static TClientItem g_HeroMouseItem = null;
        public static TShopItem g_ClickShopItem = null;
        public static bool g_boItemMoving = false;
        public static TMovingItem g_MovingItem = null;
        public static TMovingItem g_OpenBoxItem = null;
        public static TMovingItem g_WaitingUseItem = null;
        public static TMovingItem g_WaitingStallItem = null;
        public static TMovingItem g_WaitingDetectItem = null;
        public static TDropItem g_FocusItem = null;
        public static TDropItem g_FocusItem2 = null;
        public static bool g_boOpenStallSystem = true;
        public static bool g_boAutoLongAttack = true;
        public static bool g_boAutoSay = true;
        public static bool g_boMutiHero = true;
        public static bool g_boSkill_114_MP = false;
        public static bool g_boSkill_68_MP = false;
        public static int g_nDayBright = 0;
        public static int g_nAreaStateValue = 0;
        public static bool g_boNoDarkness = false;
        public static int g_nRunReadyCount = 0;
        public static bool g_boLastViewFog = false;
#if VIEWFOG
        public static bool g_boViewFog = true;
    // �Ƿ���ʾ�ڰ�
        public static bool g_boForceNotViewFog = true;
    // ������
#else
        public static bool g_boViewFog = false;
        // �Ƿ���ʾ�ڰ�
        public static bool g_boForceNotViewFog = true;
        // ������
#endif // VIEWFOG
        public static TClientItem g_EatingItem = null;
        public static long g_dwEatTime = 0;
        // timeout...
        public static long g_dwHeroEatTime = 0;
        public static long g_dwDizzyDelayStart = 0;
        public static long g_dwDizzyDelayTime = 0;
        public static bool g_boDoFadeOut = false;
        // �����䰵
        public static bool g_boDoFadeIn = false;
        // �ɰ�����
        public static int g_nFadeIndex = 0;
        public static bool g_boDoFastFadeOut = false;
        public static bool g_boAutoDig = false;
        public static bool g_boAutoSit = false;
        // �Զ�����
        public static bool g_boSelectMyself = false;
        // ����Ƿ�ָ���Լ�
        // ��Ϸ�ٶȼ����ر���
        public static long g_dwFirstServerTime = 0;
        public static long g_dwFirstClientTime = 0;
        // ServerTimeGap: int64;
        public static int g_nTimeFakeDetectCount = 0;
        // g_dwSHGetCount            : PLongWord;
        // g_dwSHGetTime             : LongWord;
        // g_dwSHTimerTime           : LongWord;
        // g_nSHFakeCount            : Integer;  //�������ٶ��쳣�������������4������ʾ�ٶȲ��ȶ�
        public static long g_dwLatestClientTime2 = 0;
        public static long g_dwFirstClientTimerTime = 0;
        // timer �ð�
        public static long g_dwLatestClientTimerTime = 0;
        public static long g_dwFirstClientGetTime = 0;
        // gettickcount �ð�
        public static long g_dwLatestClientGetTime = 0;
        public static int g_nTimeFakeDetectSum = 0;
        public static int g_nTimeFakeDetectTimer = 0;
        public static long g_dwLastestClientGetTime = 0;
        // ��ҹ��ܱ�����ʼ
        public static long g_dwDropItemFlashTime = 5 * 1000;
        // ������Ʒ��ʱ����
        public static int g_nHitTime = 1400;
        // �������ʱ����  0820
        public static int g_nItemSpeed = 60;
        public static long g_dwSpellTime = 500;
        // ħ�������ʱ��
        public static bool g_boHero = true;
        public static bool g_boOpenAutoPlay = true;
        public static TColorEffect g_DeathColorEffect = WIL.TColorEffect.ceRed;
        // ������ɫ  Development 2018-12-29
        public static bool g_boClientCanSet = true;
        public static int g_nEatIteminvTime = 200;
        public static bool g_boCanRunSafeZone = true;
        public static bool g_boCanRunHuman = true;
        public static bool g_boCanRunMon = true;
        public static bool g_boCanRunNpc = true;
        public static bool g_boCanRunAllInWarZone = false;
        public static bool g_boCanStartRun = true;
        // �Ƿ�����������
        public static bool g_boParalyCanRun = false;
        // ����Ƿ������
        public static bool g_boParalyCanWalk = false;
        // ����Ƿ������
        public static bool g_boParalyCanHit = false;
        // ����Ƿ���Թ���
        public static bool g_boParalyCanSpell = false;
        // ����Ƿ����ħ��
        public static bool g_boShowRedHPLable = true;
        // ��ʾѪ��
        public static bool g_boShowHPNumber = true;
        // ��ʾѪ������
        public static bool g_boShowJobLevel = true;
        // ��ʾְҵ�ȼ�
        public static bool g_boDuraAlert = true;
        // ��Ʒ�־þ���
        public static bool g_boMagicLock = true;
        // ħ������
        public static bool g_boSpeedRate = false;
        public static bool g_boSpeedRateShow = false;
        // g_boAutoPuckUpItem        : Boolean = False;
        public static bool g_boShowHumanInfo = true;
        public static bool g_boShowMonsterInfo = false;
        public static bool g_boShowNpcInfo = false;
        // ��ҹ��ܱ�������
        public static bool g_boQuickPickup = false;
        public static long g_dwAutoPickupTick = 0;
        /// <summary>
        /// �Զ�����Ʒ���
        /// </summary>
        public static long g_dwAutoPickupTime = 100;
        public static TActor g_MagicLockActor = null;
        public static bool g_boNextTimePowerHit = false;
        public static bool g_boCanLongHit = false;
        public static bool g_boCanWideHit = false;
        public static bool g_boCanCrsHit = false;
        public static bool g_boCanStnHit = false;
        public static bool g_boNextTimeFireHit = false;
        public static bool g_boNextTimeTwinHit = false;
        public static bool g_boNextTimePursueHit = false;
        public static bool g_boNextTimeRushHit = false;
        public static bool g_boNextTimeSmiteHit = false;
        public static bool g_boNextTimeSmiteLongHit = false;
        public static bool g_boNextTimeSmiteLongHit2 = false;
        public static bool g_boNextTimeSmiteLongHit3 = false;
        public static bool g_boNextTimeSmiteWideHit = false;
        public static bool g_boNextTimeSmiteWideHit2 = false;
        public static bool g_boCanSLonHit = false;
        public static bool g_boCanSquHit = false;
        public static THStringList g_ShowItemList = null;
        public static bool g_boDrawTileMap = true;
        public static bool g_boDrawDropItem = true;
        public static int g_nTestX = 71;
        public static int g_nTestY = 212;
        public static int g_nSquHitPoint = 0;
        public static int g_nMaxSquHitPoint = 0;
        public static bool g_boConfigLoaded = false;
        public static byte g_dwCollectExpLv = 0;
        public static bool g_boCollectStateShine = false;
        public static int g_nCollectStateShine = 0;
        public static long g_dwCollectStateShineTick = 0;
        public static long g_dwCollectStateShineTick2 = 0;
        public static long g_dwCollectExp = 0;
        public static long g_dwCollectExpMax = 1;
        public static bool g_boCollectExpShine = false;
        public static int g_boCollectExpShineCount = 0;
        public static long g_dwCollectExpShineTick = 0;
        public static long g_dwCollectIpExp = 0;
        public static long g_dwCollectIpExpMax = 1;
        public static bool g_ReSelChr = false;
        public static bool ShouldUnloadEnglishKeyboardLayout = false;
        public static string LocalModName_Shift = ModName_Shift;
        public static string LocalModName_Ctrl = ModName_Ctrl;
        public static string LocalModName_Alt = ModName_Alt;
        public static string LocalModName_Win = ModName_Win;
        public static int[] g_FSResolutionWidth = { 800, 1024, 1280, 1280, 1366, 1440, 1600, 1680, 1920 };// ���Էֱ��ʿ��
        public static int[] g_FSResolutionHeight = { 600, 768, 800, 1024, 768, 900, 900, 1050, 1080 };// ���Էֱ��ʸ߶�
        public static byte g_FScreenMode = 0;
        public static int g_FScreenWidth = SCREENWIDTH;
        public static int g_FScreenHeight = SCREENHEIGHT;
        public static bool g_boClientUI = false;
        public const string REG_SETUP_PATH = "SOFTWARE\\Jason\\Mir2\\Setup";
        public const string REG_SETUP_BITDEPTH = "BitDepth";
        public const string REG_SETUP_DISPLAY = "DisplaySize";
        public const string REG_SETUP_WINDOWS = "Window";
        public const string REG_SETUP_MP3VOLUME = "MusicVolume";
        public const string REG_SETUP_SOUNDVOLUME = "SoundVolume";
        public const string REG_SETUP_MP3OPEN = "MusicOpen";
        public const string REG_SETUP_SOUNDOPEN = "SoundOpen";
        public const int MAXX = 40;
        // SCREENWIDTH div 20;
        public const int MAXY = 30;
        // SCREENWIDTH div 20;
        public const int LONGHEIGHT_IMAGE = 35;
        public const int FLASHBASE = 410;
        public const int SOFFX = 0;
        public const int SOFFY = 0;
        public const int HEALTHBAR_BLACK = 0;
        // HEALTHBAR_RED = 1;
        public const int BARWIDTH = 30;
        public const int BARHEIGHT = 2;
        public const int MAXSYSLINE = 8;
        public const int BOTTOMBOARD = 1;
        public const int AREASTATEICONBASE = 150;
        public const int g_WinBottomRetry = 45;
        // ------------
        public const bool NEWHINTSYS = true;
        // MIR2EX = True;
        public const int NPC_CILCK_INVTIME = 500;
        public const int MAXITEMBOX_WIDTH = 177;
        public const int MAXMAGICLV = 3;
        public const int RUNLOGINCODE = 0;
        public const int CLIENT_VERSION_NUMBER = 120020522;
        public const int STDCLIENT = 0;
        public const int RMCLIENT = 46;
        public const int CLIENTTYPE = RMCLIENT;
        public const int CUSTOMLIBFILE = 0;
        public const int DEBUG = 0;
        public const int LOGICALMAPUNIT = 30;
        // 1108 40;
        public const int HUMWINEFFECTTICK = 200;
        public const int WINLEFT = 100;
        // ������� ͼƬ�ز����������Ļ�ڵĳߴ�Ϊ100
        public const int WINTOP = 100;
        // ���嶥�� ͼƬ�ز����ڶ�����Ļ�ڵĳߴ�Ϊ100
        public const int MINIMAPSIZE = 200;
        // �����ͼ���
        public const int DEFAULTCURSOR = 0;
        // ϵͳĬ�Ϲ��
        public const int IMAGECURSOR = 1;
        // ͼ�ι��
        public const int USECURSOR = DEFAULTCURSOR;
        // ʹ��ʲô���͵Ĺ��
        public const int MAXBAGITEMCL = 52;
        public const int MAXFONT = 8;
        public const int ENEMYCOLOR = 69;
        public const int HERO_MIIDX_OFFSET = 5000;
        public const int SAVE_MIIDX_OFFSET = HERO_MIIDX_OFFSET + 500;
        public const int STALL_MIIDX_OFFSET = HERO_MIIDX_OFFSET + 500 + 50;
        public const int DETECT_MIIDX_OFFSET = HERO_MIIDX_OFFSET + 500 + 50 + 10 + 1;
        public const int MSGMUCH = 2;
        public static string[] g_sHumAttr = { "��", "ľ", "ˮ", "��", "��" };
        public static string[] g_DBStateStrArr = { "װ", "ʱ", "״", "��", "��", "��", "��" };
        public static string[] g_DBStateStrArrW = { "��", "װ", "̬", "��", "��", "��", "��" };
        public static string[] g_DBStateStrArrUS = { "װ", "ʱ", "��" };
        public static string[] g_DBStateStrArrUSW = { "��", "װ", "��" };
        public static string[] g_DBStateStrArr2 = { "״", "��", "��", "��", "��" };
        public static string[] g_DBStateStrArr2W = { "̬", "��", "��", "��", "��" };
        public static string[] g_slegend = { "", "������", "����ѫ��", "��������", "����֮��", "", "���滤��", "", "����֮��", "", "��������", "����֮ѥ", "", "�������" };
        public const int MAX_GC_GENERAL = 16;
        public static Rectangle[] g_ptGeneral = new Rectangle[20] {
    {35 + 000, 70 + 23 * 0, 35 + 000 + 72 + 18, 70 + 23 * 0 + 16} ,
    {35 + 000, 70 + 23 * 1, 35 + 000 + 72 + 18, 70 + 23 * 1 + 16} ,
    {35 + 000, 70 + 23 * 2, 35 + 000 + 78 + 18, 70 + 23 * 2 + 16} ,
    {35 + 000, 70 + 23 * 3, 35 + 000 + 96, 70 + 23 * 3 + 16} ,
    {35 + 120, 70 + 23 * 0, 35 + 120 + 72 + 30, 70 + 23 * 0 + 16} ,
    {35 + 120, 70 + 23 * 1, 35 + 120 + 72, 70 + 23 * 1 + 16} ,
    {35 + 120, 70 + 23 * 2, 35 + 120 + 72 + 18, 70 + 23 * 2 + 16} ,
    {35 + 120, 70 + 23 * 3, 35 + 120 + 72, 70 + 23 * 3 + 16} ,
    {35 + 120, 70 + 23 * 4, 35 + 120 + 72 + 18, 70 + 23 * 4 + 16} ,
    {35 + 240, 70 + 23 * 0, 35 + 240 + 72, 70 + 23 * 0 + 16} ,
    {35 + 240, 70 + 23 * 1, 35 + 240 + 72, 70 + 23 * 1 + 16} ,
    {35 + 240, 70 + 23 * 2, 35 + 240 + 48, 70 + 23 * 2 + 16} ,
    {35 + 240, 70 + 23 * 3, 35 + 240 + 72, 70 + 23 * 3 + 16} ,
    {35 + 240, 70 + 23 * 4, 35 + 240 + 72, 70 + 23 * 4 + 16} ,
    {35 + 240, 70 + 23 * 5, 35 + 240 + 72, 70 + 23 * 5 + 16} ,
    {35 + 120, 70 + 23 * 5, 35 + 120 + 72, 70 + 23 * 5 + 16} ,
    {35 + 000, 70 + 23 * 5, 35 + 000 + 96, 70 + 23 * 5 + 16} };
        public static bool[] g_gcGeneral = { true, true, false, true, true, true, false, true, false, true, true, true, true, false, false, true, true };
        public static Color[] g_clGeneral = { System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver };
        public static Color[] g_clGeneralDef = { System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver };
        // ====================================Protect====================================
        public const int MAX_GC_PROTECT = 11;
        public static Rectangle[] g_ptProtect = new Rectangle[]{
    {35 + 000, 70 + 24 * 0, 35 + 000 + 20, 70 + 24 * 0 + 16} ,
    {35 + 000, 70 + 24 * 1, 35 + 000 + 20, 70 + 24 * 1 + 16} ,
    {35 + 000, 70 + 24 * 2, 35 + 000 + 20, 70 + 24 * 2 + 16} ,
    {35 + 000, 70 + 24 * 3, 35 + 000 + 20, 70 + 24 * 3 + 16} ,
    {35 + 000, 70 + 24 * 4, 35 + 000 + 20, 70 + 24 * 4 + 16} ,
    {35 + 000, 70 + 24 * 5, 35 + 000 + 20, 70 + 24 * 5 + 16} ,
    {35 + 000, 70 + 24 * 6, 35 + 000 + 72, 70 + 24 * 6 + 16} ,
    {35 + 180, 70 + 24 * 0, 35 + 180 + 20, 70 + 24 * 0 + 16} ,
    {35 + 180, 70 + 24 * 1, 35 + 180 + 20, 70 + 24 * 1 + 16} ,
    {35 + 180, 70 + 24 * 3, 35 + 180 + 20, 70 + 24 * 3 + 16} ,
    {35 + 180, 70 + 24 * 5, 35 + 180 + 20, 70 + 24 * 5 + 16} ,
    {35 + 180, 70 + 24 * 6, 35 + 180 + 20, 70 + 24 * 6 + 16} };
        // 0
        // 1
        // 2
        // 3
        // 4
        // 5
        // 6
        // 7
        // 8
        // 9
        // 10
        public static string[] g_caProtect = { "HP               ����", "MP               ����", "", "HP               ����", "", "HP               ����", "��������", "HP               ����", "MP               ����", "HP               ����", "HP", "MP��������ʹ������ҩƷ" };
        // shape = 2
        // shape = 1
        // shape = 3
        // shape = 5
        public static string[] g_sRenewBooks = { "������;�", "�������Ѿ�", "�سǾ�", "�л�سǾ�", "���ش���ʯ", "���洫��ʯ", "�������ʯ", "", "", "", "", "" };
        public static bool[] g_gcProtect = { false, false, false, false, false, false, false, true, true, true, false, true };
        public static int[] g_gnProtectPercent = { 10, 10, 10, 10, 10, 10, 0, 88, 88, 88, 20, 00 };
        public static int[] g_gnProtectTime = { 4000, 4000, 4000, 4000, 4000, 4000, 4000, 4000, 4000, 1000, 1000, 1000 };
        public static Color[] g_clProtect = { System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Lime };
        // ====================================Tec====================================
        public const int MAX_GC_TEC = 14;
        // 0
        // 1
        // 2
        // 3
        // 4
        // 5
        // 6
        // 7
        // 8
        // 9
        // 0
        // 1
        // 2
        // 3
        // 4
        // 5
        // 6
        // 7
        // 8
        // 9
        public static string[] g_HintTec = { "��ѡ�������������ɱ", "��ѡ����������ܰ���", "��ѡ����Զ������һ𽣷�", "��ѡ����Զ��������ս���", "��ѡ����Զ�����ħ����", "��ѡ����Ӣ�۽��Զ�����ħ����", "��ѡ�����ʿ���Զ�ʹ��������", "", "", "��ѡ����Զ�������������", "��ѡ����Զ����и�λ��ɱ", "��ѡ����Զ����۶Ͽ�ն", "��ѡ����Ӣ�۽���ʹ���������\\�������֮�����PK", "��ѡ����Զ����ۿ���ն", "��ѡ���ʩչħ�������������ʱ�����Զ��ܽ�Ŀ�겢�ͷ�ħ��" };
        // 0
        // 1
        // 2
        // 3
        // 4
        // 5
        // 6
        // 7
        // 8
        // 9
        public static string[] g_caTec = { "������ɱ", "���ܰ���", "�Զ��һ�", "���ս���", "�Զ�����", "��������(Ӣ��)", "�Զ�����", "ʱ����", "", "�Զ�����", "��λ��ɱ", "�Զ��Ͽ�ն", "Ӣ�����������", "�Զ�����ն", "�Զ�����ħ������" };
        public static string[] g_sMagics = { "������", "������", "�����", "ʩ����", "��ɱ����", "���ܻ�", "������", "�����Ӱ", "�׵���", "�׵���", "�׵���", "�׵���", "�׵���", "����ն", "����ն" };
        public const int g_gnTecPracticeKey = 0;
        public static bool[] g_gcTec = { true, true, true, true, true, true, false, false, false, false, false, false, false, true, false };
        public static int[] g_gnTecTime = { 0, 0, 0, 0, 0, 0, 0, 0, 4000, 0, 0, 0, 0, 0, 0 };
        public static Color[] g_clTec = { System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver };
        // ====================================Assistant====================================
        public const int MAX_GC_ASS = 6;
        // 0
        // 1
        // 2
        // 3
        // 4
        // 5
        public static Rectangle[] g_ptAss = {
    {35 + 000, 70 + 24 * 0, 35 + 000 + 142, 70 + 24 * 0 + 16} ,
    {35 + 000, 70 + 24 * 1, 35 + 000 + 72, 70 + 24 * 1 + 16} ,
    {35 + 000, 70 + 24 * 2, 35 + 000 + 72, 70 + 24 * 2 + 16} ,
    {35 + 000, 70 + 24 * 3, 35 + 000 + 72, 70 + 24 * 3 + 16} ,
    {35 + 000, 70 + 24 * 4, 35 + 000 + 72, 70 + 24 * 4 + 16} ,
    {35 + 000, 70 + 24 * 5, 35 + 000 + 120, 70 + 24 * 5 + 16} ,
    {35 + 000, 70 + 24 * 6, 35 + 000 + 120, 70 + 24 * 6 + 16} };
        // 0
        // 1
        // 2
        // 3
        // 4
        public static string[] g_HintAss = { "", "", "", "", "", "�����Լ��༭Ҫ��ʾ��ʰȡ����Ʒ������\\�˹��ܺ󣬽��滻�� [��Ʒ] ѡ�������", "" };
        // 0
        // 1
        // 2
        // 3
        // 4
        public static string[] g_caAss = { "�����һ�(Ctrl+Alt+X)", "��ҩ����س�", "��ҩ����س�", "��������س�", "������ʱ�س�", "�Զ���Ʒ����(��ѡ�༭)", "�Զ���ֹ���(��ѡ�༭)" };
        public static bool[] g_gcAss = { false, false, false, false, false, false, false };
        public static Color[] g_clAss = { System.Drawing.Color.Lime, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver };
        // ====================================HotKey====================================
        public const int MAX_GC_HOTKEY = 8;
        // 0
        // 2
        // 3
        // 4
        // 5
        // 6
        // 7
        // 8
        public static string[] g_caHotkey = { "�����Զ���ݼ�", "�ٻ�Ӣ��", "Ӣ�۹���Ŀ��", "ʹ�úϻ�����", "Ӣ�۹���ģʽ", "Ӣ���ػ�ģʽ", "�л�����ģʽ", "�л�С��ͼ", "�ͷ�����" };
        public static bool[] g_gcHotkey = { false, false, false, false, false, false, false, false, false };
        public static int[] g_gnHotkey = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public static Color[] g_clHotkey = { System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Lime };
        public const int MAX_GC_ITEMS = 7;
        public const Rectangle g_ptItemsA =
    {25 + 194, 68 + 18 * 7 + 23, 25 + 194 + 80, 68 + 18 * 7 + 16 + 23};
        public const Rectangle g_ptAutoPickUp =
    {25 + 267, 68 + 18 * 7 + 23, 25 + 267 + 80, 68 + 18 * 7 + 16 + 23};
        // 0
        // 1
        // 2
        // 3
        // 4
        // 5
        public static TCItemRule[] g_caItems = { null, null, null, null, null, null, null, null };
        public static TCItemRule[] g_caItems2 = { null, null, null, null, null, null, null, null };
        public static Color[] g_clItems = { System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver, System.Drawing.Color.Silver };
        public const int MAX_SERIESSKILL_POINT = 4;
        public const int g_HitSpeedRate = 0;
        public const int g_MagSpeedRate = 0;
        public const int g_MoveSpeedRate = 0;
        public const bool g_boFlashMission = false;
        public const bool g_boNewMission = false;
        // ������
        public const long g_dwNewMission = 0;
        // 11
        // 21
        // 31
        // ��ʨ�Ӻ�
        // �����
        // 41
        // 51
        // 59
        // 67
        // 68
        // 69
        // 70
        // 71
        // 72
        // 73
        // 74
        // 75
        // 100
        // 101
        // 102
        // 103
        // 104
        // 105
        // 106
        // 107
        // 108
        // 109
        // 110
        // 111
        public static string[] g_asSkillDesc = { "��������ħ������һö����\\����Ŀ��", "�ͷž���֮���ָ��Լ�����\\���˵�����", "�������Ĺ���������", "ͨ���뾫��֮����ͨ������\\���ս��ʱ��������", "��������ħ������һö���\\�򹥻�Ŀ��", "�������ҩ�ۿ���ָ��ĳ��\\Ŀ���ж�", "����ʱ�л�����ɴ���˺�", "����ߵ��˻��߹����ƿ�", "��ǰ�ӳ�һ�»���ǽ��ʹ��\\�������ڵĵ����ܵ��˺�", "����һ����磬ʹֱ������\\�е����ܵ��˺�", "�ӿ����ٻ�һ���׵繥������", "��λʩչ������ʹ�����ܵ�\\����˺�", "������֮�������ڻ�����ϣ�\\Զ�̹���Ŀ��", "ʹ�û������߷�Χ���ѷ�\\��ħ��������", "ʹ�û������߷�Χ���ѷ�\\�ķ�����", "�������������еĹ��޲���\\�ƶ��򹥻�Ȧ�����", "ʹ�û�����ӵ�����ٻ�\\���ã��ֵ��ٻ����ܵ����˺�", "��������Χ�ͷž���֮��ʹ\\�����޷������Ĵ���", "ͨ������ͷž���֮������\\�����ط�Χ�ڵ���", "ͨ��������ʹ����̱����\\��������ʹ�����Ϊ��ʵ������", "����ǿ��ħ�����ҿռ䣬��\\���ﵽ�������Ŀ�ĵķ���", "�ڵ����ϲ������棬ʹ̤��\\�ĵ����ܵ��˺�", "�������ȵĻ��棬ʹ������\\���ڵĵ����ܵ��˺�", "�ܹ�������һ��ǿ�����׹�\\�籩���˺�����Χ����ߵĵ���", "ʹ�þ�����ͬʱ����������\\����Χ�ĵ���", "�ٻ����鸽�������ϣ���\\�����ǿ���Ķ����˺�", "�ü��ѵ���ײ�������ײ\\���ϰ��ｫ����Լ�����˺�", "ʹ�þ������鿴Ŀ������", "�ָ��Լ�����Χ������ҵ�\\����", "ʹ�û�����ٻ�һֻǿ����\\����Ϊ�Լ������", "ʹ������ħ������һ��ħ��\\�ܼ���ʩ�����ܵ����˺�", "�л���һ��ɱ����������", "�ٻ�ǿ���ı���ѩ��ʹ����\\�����ڵĵ����ܵ��˺�", "����ѷ������еĸ��ֶ�", "", "����������ɱ�����Ŀ�꣬\\��һ������ʹ�Է���ʱʯ��", "�ٻ�ǿ�����׵磬ʹ����\\�����ڵĵ����ܵ��˺�", "�������ҩ�ۿ���ָ��ĳ��\\�����ڵ�Ŀ���ж�", "���ض�����ʿԶ�̹�������", "ʹ�þ�����ͬʱ��������\\������Χ�ĵ���", "ʹ�þ���������㽫����\\��Χ������ʱʯ��", "ʹ�þ�����ɴ�������ǰ\\��������ĵ�������", "�ٻ��׵��鸽�������ϣ�\\�Ӷ����ǿ���Ķ����˺���\\��һ������ʹ�������", "�����޴��ħ������ͬʱ\\���������һ�����˺�", "��ȡ�Է�һ����MP��ͬʱ\\�����޴��ħ���˺�", "", "�ٻ��������ۻ����յĻ�������", "һ���ڹ���������������\\����Χ�Ĺ�������Է��������", "����ѷ������еĸ����ж�״̬", "˲�������Լ��ľ�����", "쫷���", "������", "Ѫ��", "������", "", "�������۳��Σ�˲�仯��\\һ����Ӱ��ͻϮ��ǰ�ĵ���", "��ʹ��������˺�ͬʱ��ȡ\\�Է�����ֵ", "�ٻ�һ�����ҵĻ��꣬ʹ\\���������ڵĵ����ܵ��˺�", "", "���������֮������˫��֮\\�⣬�Ե�����������˺�", "������û������ƺ�⻷��\\���ܣ��Ե�����������˺�", "�ٻ�����֮��������֮�У�\\�Ե�����������˺�", "�ٻ��ɻ����󣬶Ե������\\�����˺�", "���ȵ������붳���ı�Ϣ��\\��ϳ�����֮����������˲��", "�ٻ��������ۻ����յĻ�������", "���������Ķ��㣬�û���һ��\\�޽���������������ص�����", "", "", "", "���Լ��������ٻ������", "Զ�����ܻ���ﵽ����ǰ��", "ʹ�÷��������ƶ���ָ��λ��", "", "", "�����������ͶԷ�����\\����˺�", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "����������ײ����Ŀ�꣬\\����ʹ����˵�ͬʱ������˺�", "���ҿ������ӽ��ؿ���\\���������Ե���Ŀ������˺�", "�����ػ�������ɾ޴��˺���\\Զ�̹������������ڵ�\\����Ŀ������˺�", "��ɨǧ����������֮����\\��Χ������������Ϊ���ģ�\\��5*5��Χ������˺�", "���������Ͱ������һ����\\Զ�̹������Ե���Ŀ������˺�", "Ծ��󷢳�ǿ�ҵ�ħ��������\\Զ�̹������Ե���Ŀ������˺�", "�����ػ������ѵ����γɱ��̡�\\��Χ��������Ŀ��Ϊ���ģ�\\��5*5��Χ����ɳ����˺�", "�˺��ǳ��ֲ���˫��������\\Զ�̹������Ե���Ŀ������˺�", "�ų�ʥ�޶�Ŀ�귢�𹥻���\\Զ�̹������Ե���Ŀ������˺�", "˫���������Ƴ������ƹ������ˡ�\\Զ�̹������Ե���Ŀ������˺�", "���������мܵ���������\\Զ�̹������Ե���Ŀ������˺�", "���뷢�����ͬ�顣\\��Χ��������Ŀ��Ϊ���ģ�\\��5*5��Χ������˺�", "��Χ��������Ŀ��Ϊ���ģ�\\��3*3��Χ����ɳ����˺�", "�����ػ�������ɾ޴��˺�\\Զ�̹��������Ĳ��ڵ�\\����Ŀ������˺�", "�����ػ�������ɾ޴��˺�\\Զ�̹���������Ļ�ڵ�\\Ŀ����ɾ޴��˺�", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
        public const int WH_KEYBOARD_LL = 13;
        public const int LLKHF_ALTDOWN = 0x20;
        // Windows 2000/XP multimedia keys (adapted from winuser.h and renamed to avoid potential conflicts)
        // See also: http://msdn.microsoft.com/library/default.asp?url=/library/en-us/winui/winui/WindowsUserInterface/UserInput/VirtualKeyCodes.asp
        public const int _VK_BROWSER_BACK = 0xA6;
        // Browser Back key
        public const int _VK_BROWSER_FORWARD = 0xA7;
        // Browser Forward key
        public const int _VK_BROWSER_REFRESH = 0xA8;
        // Browser Refresh key
        public const int _VK_BROWSER_STOP = 0xA9;
        // Browser Stop key
        public const int _VK_BROWSER_SEARCH = 0xAA;
        // Browser Search key
        public const int _VK_BROWSER_FAVORITES = 0xAB;
        // Browser Favorites key
        public const int _VK_BROWSER_HOME = 0xAC;
        // Browser Start and Home key
        public const int _VK_VOLUME_MUTE = 0xAD;
        // Volume Mute key
        public const int _VK_VOLUME_DOWN = 0xAE;
        // Volume Down key
        public const int _VK_VOLUME_UP = 0xAF;
        // Volume Up key
        public const int _VK_MEDIA_NEXT_TRACK = 0xB0;
        // Next Track key
        public const int _VK_MEDIA_PREV_TRACK = 0xB1;
        // Previous Track key
        public const int _VK_MEDIA_STOP = 0xB2;
        // Stop Media key
        public const int _VK_MEDIA_PLAY_PAUSE = 0xB3;
        // Play/Pause Media key
        public const int _VK_LAUNCH_MAIL = 0xB4;
        // Start Mail key
        public const int _VK_LAUNCH_MEDIA_SELECT = 0xB5;
        // Select Media key
        public const int _VK_LAUNCH_APP1 = 0xB6;
        // Start Application 1 key
        public const int _VK_LAUNCH_APP2 = 0xB7;
        // Start Application 2 key
        // Self-invented names for the extended keys
        public const string NAME_VK_BROWSER_BACK = "Browser Back";
        public const string NAME_VK_BROWSER_FORWARD = "Browser Forward";
        public const string NAME_VK_BROWSER_REFRESH = "Browser Refresh";
        public const string NAME_VK_BROWSER_STOP = "Browser Stop";
        public const string NAME_VK_BROWSER_SEARCH = "Browser Search";
        public const string NAME_VK_BROWSER_FAVORITES = "Browser Favorites";
        public const string NAME_VK_BROWSER_HOME = "Browser Start/Home";
        public const string NAME_VK_VOLUME_MUTE = "Volume Mute";
        public const string NAME_VK_VOLUME_DOWN = "Volume Down";
        public const string NAME_VK_VOLUME_UP = "Volume Up";
        public const string NAME_VK_MEDIA_NEXT_TRACK = "Next Track";
        public const string NAME_VK_MEDIA_PREV_TRACK = "Previous Track";
        public const string NAME_VK_MEDIA_STOP = "Stop Media";
        public const string NAME_VK_MEDIA_PLAY_PAUSE = "Play/Pause Media";
        public const string NAME_VK_LAUNCH_MAIL = "Start Mail";
        public const string NAME_VK_LAUNCH_MEDIA_SELECT = "Select Media";
        public const string NAME_VK_LAUNCH_APP1 = "Start Application 1";
        public const string NAME_VK_LAUNCH_APP2 = "Start Application 2";
        public const string CONFIGFILE = "Config\\%s.ini";
        // *******************************************************************************
        public const string g_affiche0 = "��Ϸ��Ч�ѹرգ�";
        public const string g_affiche1 = "������Ϸ����";
        public const string g_affiche2 = "���Ʋ�����Ϸ �ܾ�������Ϸ ע�����ұ��� ������ƭ�ϵ� �ʶ���Ϸ����";
        public const string g_affiche3 = "������Ϸ���� ������ʱ�� ���ܽ������� ��������Ĳ� Ӫ���г����";
        public const string mmsyst = "winmm.dll";
        public const string kernel32 = "kernel32.dll";
        public const string HotKeyAtomPrefix = "HotKeyManagerHotKey";
        public const string ModName_Shift = "Shift";
        public const string ModName_Ctrl = "Ctrl";
        public const string ModName_Alt = "Alt";
        public const string ModName_Win = "Win";
        public const int VK2_SHIFT = 32;
        public const int VK2_CONTROL = 64;
        public const int VK2_ALT = 128;
        public const int VK2_WIN = 256;
        public const int SCREENWIDTH = 800;
        public const int SCREENHEIGHT = 600;
        public static string[] g_levelstring = { "һ", "��", "��", "��", "��", "��", "��", "��" };

        public string GetMySelfStringVar_GetVarTextFieldFunc(string VarText)
        {
            string result;
            result = "";
            // ת��Ϊ��д
            VarText = VarText.UpperCase(VarText);
            if (VarText == "$SERVERNAME")
            {
                // ѡ���ɫ�����ǩ����
                result = MShare.g_sServerName;
            }
            else if (VarText == "$MAP")
            {
                // ��ͼ����
                result = MShare.g_sMapTitle;
            }
            else if (VarText == "$LOCALTIME")
            {
                // ����ʱ��
                result = FormatDateTime("HH:MM:SS", DateTime.Now);
            }
            else if (VarText == "$MAPAREASTATE")
            {
                // ��ǰλ������״̬
                if ((MShare.g_nAreaStateValue & 8) != 0)
                {
                    result = "8";
                }
                else if ((MShare.g_nAreaStateValue & 4) != 0)
                {
                    result = "��������";
                }
                else if ((MShare.g_nAreaStateValue & 2) != 0)
                {
                    result = "��ȫ����";
                }
                else if ((MShare.g_nAreaStateValue & 1) != 0)
                {
                    result = "��������";
                }
            }
            if ((ClMain.SelectChrScene != null))
            {
                if (VarText == "$SELECTCHRNAME1")
                {
                    result = ClMain.SelectChrScene.ChrArr[0].UserChr.Name;
                }
                else if (VarText == "$SELECTCHRLEVEL1")
                {
                    if (ClMain.SelectChrScene.ChrArr[0].UserChr.Name != "")
                    {
                        result = ClMain.SelectChrScene.ChrArr[0].UserChr.Level.ToString();
                    }
                }
                else if (VarText == "$SELECTCHRJOB1")
                {
                    if (ClMain.SelectChrScene.ChrArr[0].UserChr.Name != "")
                    {
                        result = MShare.GetJobName(ClMain.SelectChrScene.ChrArr[0].UserChr.Job);
                    }
                }
                else if (VarText == "$SELECTCHRNAME2")
                {
                    result = ClMain.SelectChrScene.ChrArr[1].UserChr.Name;
                }
                else if (VarText == "$SELECTCHRLEVEL2")
                {
                    if (ClMain.SelectChrScene.ChrArr[1].UserChr.Name != "")
                    {
                        result = ClMain.SelectChrScene.ChrArr[1].UserChr.Level.ToString();
                    }
                }
                else if (VarText == "$SELECTCHRJOB2")
                {
                    if (ClMain.SelectChrScene.ChrArr[1].UserChr.Name != "")
                    {
                        result = MShare.GetJobName(ClMain.SelectChrScene.ChrArr[1].UserChr.Job);
                    }
                }
            }
            // ������Ϣ
            if (MShare.g_MySelf != null)
            {
                // �������Ա�ǩ����
                if (VarText == "$USERNAME")
                {
                    result = MShare.g_MySelf.m_sUserName;
                }
                else if (VarText == "$HP")
                {
                    result = MShare.g_MySelf.m_Abil.HP.ToString();
                }
                else if (VarText == "$MP")
                {
                    result = MShare.g_MySelf.m_Abil.MP.ToString();
                }
                else if (VarText == "$MAXHP")
                {
                    result = MShare.g_MySelf.m_Abil.MaxHP.ToString();
                }
                else if (VarText == "$MAXMP")
                {
                    result = MShare.g_MySelf.m_Abil.MaxMP.ToString();
                }
                else if (VarText == "$X")
                {
                    result = MShare.g_MySelf.m_nCurrX.ToString();
                }
                else if (VarText == "$Y")
                {
                    result = MShare.g_MySelf.m_nCurrY.ToString();
                }
                else if (VarText == "$LEVEL")
                {
                    result = MShare.g_MySelf.m_Abil.Level.ToString();
                }
            }
            // Ӣ����Ϣ
            if ((MShare.g_MySelf != null) && (MShare.g_MySelf.m_HeroObject != null))
            {
                if (VarText == "$HEROGLORY")
                {
                    result = MShare.g_MySelf.m_HeroObject.m_wGloryPoint.ToString();
                }
            }
            return result;
        }

        // �Զ���UI�ַ����ı��¼�
        public static void GetMySelfStringVar(ref string Text)
        {
            string S;
            string sVarText;
            int nPos;
            int nPos2;
            string ShowText;
            if (Text == "")
            {
                return;
            }
            S = Text;
            nPos = S.IndexOf("<");
            if (nPos > 0)
            {
                ShowText = S.Substring(1 - 1, nPos - 1);
                nPos2 = S.IndexOf(">");
                sVarText = S.Substring(nPos + 1 - 1, nPos2 - nPos - 1);
                ShowText = ShowText + GetMySelfStringVar_GetVarTextFieldFunc(sVarText);
                S = S.Substring(nPos2 + 1 - 1, 255);
                while (true)
                {
                    nPos = S.IndexOf("<");
                    nPos2 = S.IndexOf(">");
                    ShowText = ShowText + S.Substring(1 - 1, nPos - 1);
                    if (nPos + nPos2 > 0)
                    {
                        sVarText = S.Substring(nPos + 1 - 1, nPos2 - nPos - 1);
                        ShowText = ShowText + GetMySelfStringVar_GetVarTextFieldFunc(sVarText);
                        S = S.Substring(nPos2 + 1 - 1, 255);
                    }
                    else
                    {
                        break;
                    }
                }
                Text = ShowText + S;
            }
        }

        // �õ���ͼ�ļ������Զ���·��
        public static string GetMapDirAndName(string sFileName)
        {
            string result;
            if (File.Exists(WMFile.Units.WMFile.MAPDIRNAME + sFileName + ".map"))
            {
                result = WMFile.Units.WMFile.MAPDIRNAME + sFileName + ".map";
            }
            else
            {
                result = WMFile.Units.WMFile.OLDMAPDIRNAME + sFileName + ".map";
            }
            return result;
        }

        public static void ShowMsg(string Str)
        {
            ClMain.DScreen.AddChatBoardString(Str, System.Drawing.Color.White, System.Drawing.Color.Black);
        }

        public static void LoadMapDesc()
        {
            int i;
            string szFileName;
            string szLine;
            ArrayList xsl;
            string szMapTitle;
            string szPointX;
            string szPointY;
            string szPlaceName;
            string szColor;
            string szFullMap;
            int nPointX;
            int nPointY;
            int nFullMap;
            Color nColor;
            TMapDescInfo pMapDescInfo;
            szFileName = ".\\data\\MapDesc2.dat";
            if (File.Exists(szFileName))
            {
                xsl = new ArrayList();
                xsl.LoadFromFile(szFileName);
                for (i = 0; i < xsl.Count; i++)
                {
                    szLine = xsl[i];
                    if ((szLine == "") || (szLine[1] == ";"))
                    {
                        continue;
                    }
                    szLine = HGEGUI.Units.HGEGUI.GetValidStr3(szLine, ref szMapTitle, new string[] { "," });
                    szLine = HGEGUI.Units.HGEGUI.GetValidStr3(szLine, ref szPointX, new string[] { "," });
                    szLine = HGEGUI.Units.HGEGUI.GetValidStr3(szLine, ref szPointY, new string[] { "," });
                    szLine = HGEGUI.Units.HGEGUI.GetValidStr3(szLine, ref szPlaceName, new string[] { "," });
                    szLine = HGEGUI.Units.HGEGUI.GetValidStr3(szLine, ref szColor, new string[] { "," });
                    szLine = HGEGUI.Units.HGEGUI.GetValidStr3(szLine, ref szFullMap, new string[] { "," });
                    nPointX = HUtil32.Str_ToInt(szPointX, -1);
                    nPointY = HUtil32.Str_ToInt(szPointY, -1);
                    nColor = Convert.ToInt32(szColor);
                    nFullMap = HUtil32.Str_ToInt(szFullMap, -1);
                    if ((szPlaceName != "") && (szMapTitle != "") && (nPointX >= 0) && (nPointY >= 0) && (nFullMap >= 0))
                    {
                        pMapDescInfo = new TMapDescInfo();
                        pMapDescInfo.szMapTitle = szMapTitle;
                        pMapDescInfo.szPlaceName = szPlaceName;
                        pMapDescInfo.nPointX = nPointX;
                        pMapDescInfo.nPointY = nPointY;
                        pMapDescInfo.nColor = nColor;
                        pMapDescInfo.nFullMap = nFullMap;
                        g_xMapDescList.Add(szMapTitle, ((pMapDescInfo) as Object));
                    }
                }
                xsl.Free;
            }
        }

        public static int GetTickCount()
        {
            return SystemModule.HUtil32.GetTickCount(); ;
        }

        // stdcall;
        public static bool IsDetectItem(int idx)
        {
            bool result;
            result = idx == DETECT_MIIDX_OFFSET;
            return result;
        }

        public static bool IsBagItem(int idx)
        {
            bool result;
            result = idx >= 6 && idx <= Grobal2.MAXBAGITEM - 1;
            return result;
        }

        public static bool IsEquItem(int idx)
        {
            bool result;
            int sel;
            result = false;
            if (idx < 0)
            {
                sel = -(idx + 1);
                result = sel >= 0 && sel <= Grobal2.U_FASHION;
            }
            return result;
        }

        public static bool IsStorageItem(int idx)
        {
            bool result;
            result = (idx >= SAVE_MIIDX_OFFSET) && (idx < SAVE_MIIDX_OFFSET + 46);
            return result;
        }

        public static bool IsStallItem(int idx)
        {
            bool result;
            result = (idx >= STALL_MIIDX_OFFSET) && (idx < STALL_MIIDX_OFFSET + 10);
            return result;
        }

        public static void ResetSeriesSkillVar()
        {
            g_nCurrentMagic = 888;
            g_nCurrentMagic2 = 888;
            g_SeriesSkillStep = 0;
            g_SeriesSkillFire = false;
            g_SeriesSkillFire_100 = false;
            g_SeriesSkillReady = false;
            g_NextSeriesSkill = false;
            //FillChar(g_VenationInfos);            //FillChar(g_TempSeriesSkillArr);            //FillChar(g_HTempSeriesSkillArr);            //FillChar(g_SeriesSkillArr);        }
        }

        public static int GetSeriesSkillIcon(int id)
        {
            int result;
            result = -1;
            switch (id)
            {
                case 100:
                    result = 950;
                    break;
                case 101:
                    result = 952;
                    break;
                case 102:
                    result = 956;
                    break;
                case 103:
                    result = 954;
                    break;
                case 104:
                    result = 942;
                    break;
                case 105:
                    result = 946;
                    break;
                case 106:
                    result = 940;
                    break;
                case 107:
                    result = 944;
                    break;
                case 108:
                    result = 934;
                    break;
                case 109:
                    result = 936;
                    break;
                case 110:
                    result = 932;
                    break;
                case 111:
                    result = 930;
                    break;
                case 112:
                    result = 944;
                    break;
            }
            return result;
        }

        public static void CheckSpeedCount(int Count)
        {
            g_dwCheckCount++;
            if (g_dwCheckCount > Count)
            {
                g_dwCheckCount = 0;
                // g_ModuleDetect.FCheckTick := 0;
            }
        }

        // procedure SaveUserConfig(sUserName: string);
        public static bool IsPersentHP(int nMin, int nMax)
        {
            bool result;
            result = false;
            if (nMax != 0)
            {
                // or (nMax - nMin > 1500)
                result = (Math.Round((nMin / nMax) * 100) < g_gnProtectPercent[0]);
            }
            return result;
        }

        public static bool IsPersentMP(int nMin, int nMax)
        {
            bool result;
            result = false;
            if (nMax != 0)
            {
                // or (nMax - nMin > 1500)
                result = (Math.Round((nMin / nMax) * 100) < g_gnProtectPercent[1]);
            }
            return result;
        }

        public static bool IsPersentSpc(int nMin, int nMax)
        {
            bool result;
            result = false;
            if (nMax != 0)
            {
                // or (nMax - nMin > 6000)
                result = (Math.Round((nMin / nMax) * 100) < g_gnProtectPercent[3]);
            }
            return result;
        }

        public static bool IsPersentBook(int nMin, int nMax)
        {
            bool result;
            result = false;
            if (nMax != 0)
            {
                // or (nMax - nMin > 6000)
                result = (Math.Round((nMin / nMax) * 100) < g_gnProtectPercent[5]);
            }
            return result;
        }

        public static bool IsPersentHPHero(int nMin, int nMax)
        {
            bool result;
            result = false;
            if (nMax != 0)
            {
                // or (nMax - nMin > 1500)
                result = (Math.Round((nMin / nMax) * 100) < g_gnProtectPercent[7]);
            }
            return result;
        }

        public static bool IsPersentMPHero(int nMin, int nMax)
        {
            bool result;
            result = false;
            if (nMax != 0)
            {
                // or (nMax - nMin > 1500)
                result = (Math.Round((nMin / nMax) * 100) < g_gnProtectPercent[8]);
            }
            return result;
        }

        public static bool IsPersentSpcHero(int nMin, int nMax)
        {
            bool result;
            result = false;
            if (nMax != 0)
            {
                // or (nMax - nMin > 6000)
                result = (Math.Round((nMin / nMax) * 100) < g_gnProtectPercent[9]);
            }
            return result;
        }

        // ȡ��ְҵ����
        // 0 ��ʿ
        // 1 ħ��ʦ
        // 2 ��ʿ
        public static string GetJobName(int nJob)
        {
            string result;
            result = "";
            switch (nJob)
            {
                case 0:
                    result = g_sWarriorName;
                    break;
                case 1:
                    result = g_sWizardName;
                    break;
                case 2:
                    result = g_sTaoistName;
                    break;
                default:
                    result = g_sUnKnowName;
                    break;
            }
            return result;
        }

        // procedure ClearShowItemList();
        public static string GetItemType(TItemType ItemType)
        {
            string result;
            switch (ItemType)
            {
                case TItemType.i_HPDurg:
                    result = "��ҩ";
                    break;
                case TItemType.i_MPDurg:
                    result = "ħ��ҩ";
                    break;
                case TItemType.i_HPMPDurg:
                    result = "�߼�ҩ";
                    break;
                case TItemType.i_OtherDurg:
                    result = "����ҩƷ";
                    break;
                case TItemType.i_Weapon:
                    result = "����";
                    break;
                case TItemType.i_Dress:
                    result = "�·�";
                    break;
                case TItemType.i_Helmet:
                    result = "ͷ��";
                    break;
                case TItemType.i_Necklace:
                    result = "����";
                    break;
                case TItemType.i_Armring:
                    result = "����";
                    break;
                case TItemType.i_Ring:
                    result = "��ָ";
                    break;
                case TItemType.i_Belt:
                    result = "����";
                    break;
                case TItemType.i_Boots:
                    result = "Ь��";
                    break;
                case TItemType.i_Charm:
                    result = "��ʯ";
                    break;
                case TItemType.i_Book:
                    result = "������";
                    break;
                case TItemType.i_PosionDurg:
                    result = "��ҩ";
                    break;
                case TItemType.i_UseItem:
                    result = "����Ʒ";
                    break;
                case TItemType.i_Scroll:
                    result = "����";
                    break;
                case TItemType.i_Stone:
                    result = "��ʯ";
                    break;
                case TItemType.i_Gold:
                    result = "���";
                    break;
                case TItemType.i_Other:
                    result = "����";
                    break;
            }
            return result;
        }

        public static bool GetItemShowFilter(string sItemName)
        {
            bool result;
            result = g_ShowItemList.IndexOf(sItemName) > -1;
            return result;
        }

        public static void LoadUserConfig(string sUserName)
        {
            //FileStream ini;
            //FileStream ini2;
            //string sFileName;
            //ArrayList Strings;
            //int i;
            //int no;
            //string sn;
            //string so;
            //sFileName = ".\\Config\\" + g_sServerName + "." + sUserName + ".Set";
            //ini = new FileStream(sFileName);
            //// base
            //g_gcGeneral[0] = ini.ReadBool("Basic", "ShowActorName", g_gcGeneral[0]);
            //g_gcGeneral[1] = ini.ReadBool("Basic", "DuraWarning", g_gcGeneral[1]);
            //g_gcGeneral[2] = ini.ReadBool("Basic", "AutoAttack", g_gcGeneral[2]);
            //g_gcGeneral[3] = ini.ReadBool("Basic", "ShowExpFilter", g_gcGeneral[3]);
            //g_MaxExpFilter = ini.ReadInteger("Basic", "ShowExpFilterMax", g_MaxExpFilter);
            //g_gcGeneral[4] = ini.ReadBool("Basic", "ShowDropItems", g_gcGeneral[4]);
            //g_gcGeneral[5] = ini.ReadBool("Basic", "ShowDropItemsFilter", g_gcGeneral[5]);
            //g_gcGeneral[6] = ini.ReadBool("Basic", "ShowHumanWing", g_gcGeneral[6]);
            //g_boAutoPickUp = ini.ReadBool("Basic", "AutoPickUp", g_boAutoPickUp);
            //g_gcGeneral[7] = ini.ReadBool("Basic", "AutoPickUpFilter", g_gcGeneral[7]);
            //g_boPickUpAll = ini.ReadBool("Basic", "PickUpAllItem", g_boPickUpAll);
            //g_gcGeneral[8] = ini.ReadBool("Basic", "HideDeathBody", g_gcGeneral[8]);
            //g_gcGeneral[9] = ini.ReadBool("Basic", "AutoFixItem", g_gcGeneral[9]);
            //g_gcGeneral[10] = ini.ReadBool("Basic", "ShakeScreen", g_gcGeneral[10]);
            //g_gcGeneral[13] = ini.ReadBool("Basic", "StruckShow", g_gcGeneral[13]);
            //g_gcGeneral[15] = ini.ReadBool("Basic", "HideStruck", g_gcGeneral[15]);
            //g_gcGeneral[14] = ini.ReadBool("Basic", "CompareItems", g_gcGeneral[14]);
            //ini2 = new FileStream(".\\lscfg.ini");
            //g_gcGeneral[11] = ini2.ReadBool("Setup", "EffectSound", g_gcGeneral[11]);
            //g_gcGeneral[12] = ini2.ReadBool("Setup", "EffectBKGSound", g_gcGeneral[12]);
            //g_lWavMaxVol = ini2.ReadInteger("Setup", "EffectSoundLevel", g_lWavMaxVol);
            //ini2.Free;
            //g_HitSpeedRate = ini.ReadInteger("Basic", "HitSpeedRate", g_HitSpeedRate);
            //g_MagSpeedRate = ini.ReadInteger("Basic", "MagSpeedRate", g_MagSpeedRate);
            //g_MoveSpeedRate = ini.ReadInteger("Basic", "MoveSpeedRate", g_MoveSpeedRate);
            //// Protect
            //g_gcProtect[0] = ini.ReadBool("Protect", "RenewHPIsAuto", g_gcProtect[0]);
            //g_gcProtect[1] = ini.ReadBool("Protect", "RenewMPIsAuto", g_gcProtect[1]);
            //g_gcProtect[3] = ini.ReadBool("Protect", "RenewSpecialIsAuto", g_gcProtect[3]);
            //g_gcProtect[5] = ini.ReadBool("Protect", "RenewBookIsAuto", g_gcProtect[5]);
            //g_gcProtect[7] = ini.ReadBool("Protect", "HeroRenewHPIsAuto", g_gcProtect[7]);
            //g_gcProtect[8] = ini.ReadBool("Protect", "HeroRenewMPIsAuto", g_gcProtect[8]);
            //g_gcProtect[9] = ini.ReadBool("Protect", "HeroRenewSpecialIsAuto", g_gcProtect[9]);
            //g_gcProtect[10] = ini.ReadBool("Protect", "HeroSidestep", g_gcProtect[10]);
            //g_gcProtect[11] = ini.ReadBool("Protect", "RenewSpecialIsAuto_MP", g_gcProtect[11]);
            //g_gnProtectTime[0] = ini.ReadInteger("Protect", "RenewHPTime", g_gnProtectTime[0]);
            //g_gnProtectTime[1] = ini.ReadInteger("Protect", "RenewMPTime", g_gnProtectTime[1]);
            //g_gnProtectTime[3] = ini.ReadInteger("Protect", "RenewSpecialTime", g_gnProtectTime[3]);
            //g_gnProtectTime[5] = ini.ReadInteger("Protect", "RenewBookTime", g_gnProtectTime[5]);
            //g_gnProtectTime[7] = ini.ReadInteger("Protect", "HeroRenewHPTime", g_gnProtectTime[7]);
            //g_gnProtectTime[8] = ini.ReadInteger("Protect", "HeroRenewMPTime", g_gnProtectTime[8]);
            //g_gnProtectTime[9] = ini.ReadInteger("Protect", "HeroRenewSpecialTime", g_gnProtectTime[9]);
            //g_gnProtectPercent[0] = ini.ReadInteger("Protect", "RenewHPPercent", g_gnProtectPercent[0]);
            //g_gnProtectPercent[1] = ini.ReadInteger("Protect", "RenewMPPercent", g_gnProtectPercent[1]);
            //g_gnProtectPercent[3] = ini.ReadInteger("Protect", "RenewSpecialPercent", g_gnProtectPercent[3]);
            //g_gnProtectPercent[7] = ini.ReadInteger("Protect", "HeroRenewHPPercent", g_gnProtectPercent[7]);
            //g_gnProtectPercent[8] = ini.ReadInteger("Protect", "HeroRenewMPPercent", g_gnProtectPercent[8]);
            //g_gnProtectPercent[9] = ini.ReadInteger("Protect", "HeroRenewSpecialPercent", g_gnProtectPercent[9]);
            //g_gnProtectPercent[10] = ini.ReadInteger("Protect", "HeroPerSidestep", g_gnProtectPercent[10]);
            //g_gnProtectPercent[5] = ini.ReadInteger("Protect", "RenewBookPercent", g_gnProtectPercent[5]);
            //g_gnProtectPercent[6] = ini.ReadInteger("Protect", "RenewBookNowBookIndex", g_gnProtectPercent[6]);
            //ClMain.frmMain.SendClientMessage(Grobal2.CM_HEROSIDESTEP, HUtil32.MakeLong(((int)g_gcProtect[10]), g_gnProtectPercent[10]), 0, 0, 0);
            //g_gcTec[0] = ini.ReadBool("Tec", "SmartLongHit", g_gcTec[0]);
            //g_gcTec[10] = ini.ReadBool("Tec", "SmartLongHit2", g_gcTec[10]);
            //g_gcTec[11] = ini.ReadBool("Tec", "SmartSLongHit", g_gcTec[11]);
            //g_gcTec[1] = ini.ReadBool("Tec", "SmartWideHit", g_gcTec[1]);
            //g_gcTec[2] = ini.ReadBool("Tec", "SmartFireHit", g_gcTec[2]);
            //g_gcTec[3] = ini.ReadBool("Tec", "SmartPureHit", g_gcTec[3]);
            //g_gcTec[4] = ini.ReadBool("Tec", "SmartShield", g_gcTec[4]);
            //g_gcTec[5] = ini.ReadBool("Tec", "SmartShieldHero", g_gcTec[5]);
            //g_gcTec[6] = ini.ReadBool("Tec", "SmartTransparence", g_gcTec[6]);
            //g_gcTec[9] = ini.ReadBool("Tec", "SmartThunderHit", g_gcTec[9]);
            //g_gcTec[7] = ini.ReadBool("AutoPractice", "PracticeIsAuto", g_gcTec[7]);
            //g_gnTecTime[8] = ini.ReadInteger("AutoPractice", "PracticeTime", g_gnTecTime[8]);
            //g_gnTecPracticeKey = ini.ReadInteger("AutoPractice", "PracticeKey", g_gnTecPracticeKey);
            //g_gcTec[12] = ini.ReadBool("Tec", "HeroSeriesSkillFilter", g_gcTec[12]);
            //g_gcTec[13] = ini.ReadBool("Tec", "SLongHit", g_gcTec[13]);
            //g_gcTec[14] = ini.ReadBool("Tec", "SmartGoMagic", g_gcTec[14]);
            //ClMain.frmMain.SendClientMessage(Grobal2.CM_HEROSERIESSKILLCONFIG, HUtil32.MakeLong(((int)g_gcTec[12]), 0), 0, 0, 0);
            //g_gcHotkey[0] = ini.ReadBool("Hotkey", "UseHotkey", g_gcHotkey[0]);
            //FrmDlg.DEHeroCallHero.SetOfHotKey(ini.ReadInteger("Hotkey", "HeroCallHero", 0));
            //FrmDlg.DEHeroSetAttackState.SetOfHotKey(ini.ReadInteger("Hotkey", "HeroSetAttackState", 0));
            //FrmDlg.DEHeroSetGuard.SetOfHotKey(ini.ReadInteger("Hotkey", "HeroSetGuard", 0));
            //FrmDlg.DEHeroSetTarget.SetOfHotKey(ini.ReadInteger("Hotkey", "HeroSetTarget", 0));
            //FrmDlg.DEHeroUnionHit.SetOfHotKey(ini.ReadInteger("Hotkey", "HeroUnionHit", 0));
            //FrmDlg.DESwitchAttackMode.SetOfHotKey(ini.ReadInteger("Hotkey", "SwitchAttackMode", 0));
            //FrmDlg.DESwitchMiniMap.SetOfHotKey(ini.ReadInteger("Hotkey", "SwitchMiniMap", 0));
            //FrmDlg.DxEditSSkill.SetOfHotKey(ini.ReadInteger("Hotkey", "SerieSkill", 0));
            //g_ShowItemList.LoadFromFile(".\\Data\\Filter.dat");
            //// ============================================================================
            //// g_gcAss[0] := ini.ReadBool('Ass', '0', g_gcAss[0]);
            //g_gcAss[1] = ini.ReadBool("Ass", "1", g_gcAss[1]);
            //g_gcAss[2] = ini.ReadBool("Ass", "2", g_gcAss[2]);
            //g_gcAss[3] = ini.ReadBool("Ass", "3", g_gcAss[3]);
            //g_gcAss[4] = ini.ReadBool("Ass", "4", g_gcAss[4]);
            //g_gcAss[5] = ini.ReadBool("Ass", "5", g_gcAss[5]);
            //g_gcAss[6] = ini.ReadBool("Ass", "6", g_gcAss[6]);
            //g_APPickUpList.Clear();
            //g_APMobList.Clear();
            //Strings = new ArrayList();
            //if (g_gcAss[5])
            //{
            //    sFileName = ".\\Config\\" + g_sServerName + "." + sUserName + ".ItemFilterEx.txt";
            //    if (File.Exists(sFileName))
            //    {
            //        Strings.LoadFromFile(sFileName);
            //    }
            //    else
            //    {
            //        Strings.SaveToFile(sFileName);
            //    }
            //    for (i = 0; i < Strings.Count; i++)
            //    {
            //        if ((Strings[i] == "") || (Strings[i][1] == ";"))
            //        {
            //            continue;
            //        }
            //        so = HGEGUI.Units.HGEGUI.GetValidStr3(Strings[i], ref sn, new string[] { ",", " ", "\09" });
            //        no = ((int)so != "");
            //        g_APPickUpList.Add(sn, ((no) as Object));
            //    }
            //}
            //if (g_gcAss[6])
            //{
            //    sFileName = ".\\Config\\" + g_sServerName + "." + sUserName + ".MonFilter.txt";
            //    if (File.Exists(sFileName))
            //    {
            //        Strings.LoadFromFile(sFileName);
            //    }
            //    else
            //    {
            //        Strings.SaveToFile(sFileName);
            //    }
            //    for (i = 0; i < Strings.Count; i++)
            //    {
            //        if ((Strings[i] == "") || (Strings[i][1] == ";"))
            //        {
            //            continue;
            //        }
            //        // , nil
            //        g_APMobList.Add(Strings[i]);
            //    }
            //}
        }

        public static void LoadItemDesc()
        {
            const string fItemDesc = ".\\data\\ItemDesc.dat";
            int i;
            string Name;
            string desc;
            string ps;
            ArrayList temp;
            // g_ItemDesc
            if (File.Exists(fItemDesc))
            {
                temp = new ArrayList();
                temp.LoadFromFile(fItemDesc);
                for (i = 0; i < temp.Count; i++)
                {
                    if (temp[i] == "")
                    {
                        continue;
                    }
                    desc = HGEGUI.Units.HGEGUI.GetValidStr3(temp[i], ref Name, new string[] { "=" });
                    desc = desc.Replace("\\", "");
                    ps = new string();
                    ps = desc;
                    if ((Name != "") && (desc != ""))
                    {
                        // g_ItemDesc.Put(name, TObject(ps));
                        g_ItemDesc.Add(Name, ((ps) as Object));
                    }
                }
                temp.Free;
            }
        }

        public static int GetLevelColor(byte iLevel)
        {
            int result;
            switch (iLevel)
            {
                case 0:
                    result = 0x00FFFFFF;
                    break;
                case 1:
                    result = 0x004AD663;
                    break;
                case 2:
                    result = 0x00E9A000;
                    break;
                case 3:
                    result = 0x00FF35B1;
                    break;
                case 4:
                    result = 0x000061EB;
                    break;
                case 5:
                    result = 0x005CF4FF;
                    break;
                case 15:
                    result = Color.Gray.ToArgb();
                    break;
                default:
                    result = 0x005CF4FF;
                    break;
            }
            return result;
        }

        public static void LoadItemFilter()
        {
            int i;
            int n;
            string s;
            string s0;
            string s1;
            string s2;
            string s3;
            string s4;
            string fn;
            ArrayList ls;
            TCItemRule p;
            TCItemRule p2;
            fn = ".\\Data\\lsDefaultItemFilter.txt";
            if (File.Exists(fn))
            {
                ls = new ArrayList();
                ls.LoadFromFile(fn);
                for (i = 0; i < ls.Count; i++)
                {
                    s = ls[i];
                    if (s == "")
                    {
                        continue;
                    }
                    s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s0, new string[] { "," });
                    s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s1, new string[] { "," });
                    s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s2, new string[] { "," });
                    s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s3, new string[] { "," });
                    s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s4, new string[] { "," });
                    p = new TCItemRule();
                    p.Name = s0;
                    p.rare = s2 == "1";
                    p.pick = s3 == "1";
                    p.Show = s4 == "1";
                    g_ItemsFilter_All.Put(s0, ((p) as Object));
                    p2 = new TCItemRule();
                    p2 = p;
                    g_ItemsFilter_All_Def.Put(s0, ((p2) as Object));
                    n = Convert.ToInt32(s1);
                    switch (n)
                    {
                        case 0:
                            g_ItemsFilter_Dress.Add(s0, ((p) as Object));
                            break;
                        case 1:
                            g_ItemsFilter_Weapon.Add(s0, ((p) as Object));
                            break;
                        case 2:
                            g_ItemsFilter_Headgear.Add(s0, ((p) as Object));
                            break;
                        case 3:
                            g_ItemsFilter_Drug.Add(s0, ((p) as Object));
                            break;
                        default:
                            g_ItemsFilter_Other.Add(s0, ((p) as Object));
                            break;
                            // ��װ
                    }
                }
                ls.Free;
            }
        }

        public static void LoadItemFilter2()
        {
            int i;
            string s;
            string s0;
            string s2;
            string s3;
            string s4;
            string fn;
            ArrayList ls;
            TCItemRule p;
            TCItemRule p2;
            bool b2;
            bool b3;
            bool b4;
            fn = ".\\Config\\" + g_sServerName + "." + ClMain.frmMain.m_sCharName + ".ItemFilter.txt";
            // DScreen.AddChatBoardString(fn, clWhite, clBlue);
            if (File.Exists(fn))
            {
                // DScreen.AddChatBoardString('1', clWhite, clBlue);
                ls = new ArrayList();
                ls.LoadFromFile(fn);
                for (i = 0; i < ls.Count; i++)
                {
                    s = ls[i];
                    if (s == "")
                    {
                        continue;
                    }
                    s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s0, new string[] { "," });
                    s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s2, new string[] { "," });
                    s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s3, new string[] { "," });
                    s = HGEGUI.Units.HGEGUI.GetValidStr3(s, ref s4, new string[] { "," });
                    p = ((TCItemRule)(g_ItemsFilter_All_Def.GetValues(s0)));
                    if (p != null)
                    {
                        // DScreen.AddChatBoardString('2', clWhite, clBlue);
                        b2 = s2 == "1";
                        b3 = s3 == "1";
                        b4 = s4 == "1";
                        if ((b2 != p.rare) || (b3 != p.pick) || (b4 != p.Show))
                        {
                            // DScreen.AddChatBoardString('3', clWhite, clBlue);
                            p2 = ((TCItemRule)(g_ItemsFilter_All.GetValues(s0)));
                            if (p2 != null)
                            {
                                // DScreen.AddChatBoardString('4', clWhite, clBlue);
                                p2.rare = b2;
                                p2.pick = b3;
                                p2.Show = b4;
                            }
                        }
                    }
                }
                ls.Free;
            }
        }

        public static void SaveItemFilter()
        {
            // �˳���������
            int i;
            ArrayList ls;
            TCItemRule p;
            TCItemRule p2;
            string fn;
            fn = ".\\Config\\" + g_sServerName + "." + ClMain.frmMain.m_sCharName + ".ItemFilter.txt";
            ls = new ArrayList();
            for (i = 0; i < g_ItemsFilter_All.Count; i++)
            {
                p = ((TCItemRule)(g_ItemsFilter_All.GetValues(g_ItemsFilter_All.Keys[i])));
                p2 = ((TCItemRule)(g_ItemsFilter_All_Def.GetValues(g_ItemsFilter_All_Def.Keys[i])));
                if (p.Name == p2.Name)
                {
                    if ((p.rare != p2.rare) || (p.pick != p2.pick) || (p.Show != p2.Show))
                    {
                        ls.Add(string.Format("%s,%d,%d,%d", new byte[] { p.Name, ((byte)p.rare), ((byte)p.pick), ((byte)p.Show) }));
                    }
                }
            }
            if (ls.Count > 0)
            {
                ls.SaveToFile(fn);
            }
            ls.Free;
        }

        public static TClientSuiteItems getSuiteHint(ref int idx, string s, byte gender)
        {
            TClientSuiteItems result;
            int i;
            TClientSuiteItems p;
            result = null;
            if ((idx > 12) || (idx < 0))
            {
                return result;
            }
            for (i = 0; i < g_SuiteItemsList.Count; i++)
            {
                p = g_SuiteItemsList[i];
                if (((p.asSuiteName[0] == "") || (gender == p.Gender)) && ((s).ToLower().CompareTo((p.asSuiteName[idx]).ToLower()) == 0))
                {
                    result = p;
                    break;
                }
            }
            idx = -1;
            return result;
        }

        public static int GetItemWhere(TClientItem clientItem)
        {
            int result;
            result = -1;
            if (clientItem.Item.Name == "")
            {
                return result;
            }
            switch (clientItem.Item.ItemtdMode)
            {
                case 10:
                case 11:
                    result = Grobal2.U_DRESS;
                    break;
                case 5:
                case 6:
                    result = Grobal2.U_WEAPON;
                    break;
                case 30:
                    result = Grobal2.U_RIGHTHAND;
                    break;
                case 19:
                case 20:
                case 21:
                    result = Grobal2.U_NECKLACE;
                    break;
                case 15:
                    result = Grobal2.U_HELMET;
                    break;
                case 16:
                    break;
                case 24:
                case 26:
                    result = Grobal2.U_ARMRINGL;
                    break;
                case 22:
                case 23:
                    result = Grobal2.U_RINGL;
                    break;
                case 25:
                    result = Grobal2.U_BUJUK;
                    break;
                case 27:
                    result = Grobal2.U_BELT;
                    break;
                case 28:
                    result = Grobal2.U_BOOTS;
                    break;
                case 7:
                case 29:
                    result = Grobal2.U_CHARM;
                    break;
            }
            return result;
        }

        public static bool GetSecretAbil(TClientItem CurrMouseItem)
        {
            bool result;
            int i;
            int start;
            byte adv;
            byte cnt;
            string s;
            result = false;
            if (!(new ArrayList(new int[] { 5, 6, 10, 15, 26 }).Contains(CurrMous.Item.Item.StdMode)))
            {
                return result;
            }
            return result;
        }

        public static void InitClientItems()
        {
            //FillChar(g_MagicArr);            
            //FillChar(g_TakeBackItemWait);           
            //FillChar(g_UseItems);           
            //FillChar(g_ItemArr);           
            //FillChar(g_HeroUseItems);        
            //FillChar(g_HeroItemArr);     
            //FillChar(g_RefineItems);     
            //FillChar(g_BuildAcuses);     
            //FillChar(g_DetectItem);    
            //FillChar(g_TIItems);        
            //FillChar(g_spItems);     
            //FillChar(g_ItemArr);        
            //FillChar(g_HeroItemArr);  
            //FillChar(g_DealItems);       
            //FillChar(g_YbDealItems);         
            //FillChar(g_DealRemoteItems);
        }

        public static byte GetTIHintString1(int idx, TClientItem ci, string iname)
        {
            byte result;
            result = 0;
            g_tiHintStr1 = "";
            //switch (idx)
            //{
            //    case 0:
            //        g_tiHintStr1 = "���ղ����µ������챦�����ϴ�����ʮ���ˣ����������������٣�����Ҫ������װ�����������ϰɣ�";
            //        FrmDlg.DBTIbtn1.btnState = tdisable;
            //        FrmDlg.DBTIbtn1.Caption = "��ͨ����";
            //        FrmDlg.DBTIbtn2.btnState = tdisable;
            //        FrmDlg.DBTIbtn2.Caption = "�߼�����";
            //        break;
            //    case 1:
            //        if ((ci == null) || (ci.s.Name == ""))
            //        {
            //            return result;
            //        }
            //        if (ci.Item.Eva.EvaTimesMax == 0)
            //        {
            //            g_tiHintStr1 = "��־�˲��ɼ�������Ʒ���Ǽ������˵ģ��㻻һ���ɡ�";
            //            FrmDlg.DBTIbtn1.btnState = tdisable;
            //            FrmDlg.DBTIbtn1.Caption = "��ͨ����";
            //            FrmDlg.DBTIbtn2.btnState = tdisable;
            //            FrmDlg.DBTIbtn2.Caption = "�߼�����";
            //            return result;
            //        }
            //        if (ci.Item.Eva.EvaTimes < ci.Item.Eva.EvaTimesMax)
            //        {
            //            if (FrmDlg.DWTI.tag == 1)
            //            {
            //                switch (ci.Item.Eva.EvaTimes)
            //                {
            //                    case 0:
            //                        g_tiHintStr1 = "��һ�μ�������Ҫһ��һ���������ᣬ���ȥ�ռ�һ���ɣ�";
            //                        FrmDlg.DBTIbtn1.btnState = tnor;
            //                        FrmDlg.DBTIbtn1.Caption = "��ͨһ��";
            //                        FrmDlg.DBTIbtn2.btnState = tnor;
            //                        FrmDlg.DBTIbtn2.Caption = "�߼�һ��";
            //                        break;
            //                    case 1:
            //                        g_tiHintStr1 = "�ڶ��μ�������Ҫһ�������������ᣬ���ȥ�ռ�һ���ɣ�";
            //                        FrmDlg.DBTIbtn1.btnState = tnor;
            //                        FrmDlg.DBTIbtn1.Caption = "��ͨ����";
            //                        FrmDlg.DBTIbtn2.btnState = tnor;
            //                        FrmDlg.DBTIbtn2.Caption = "�߼�����";
            //                        break;
            //                    case 2:
            //                        g_tiHintStr1 = "�����μ�������Ҫһ�������������ᣬ���ȥ�ռ�һ���ɣ�";
            //                        FrmDlg.DBTIbtn1.btnState = tnor;
            //                        FrmDlg.DBTIbtn1.Caption = "��ͨ����";
            //                        FrmDlg.DBTIbtn2.btnState = tnor;
            //                        FrmDlg.DBTIbtn2.Caption = "�߼�����";
            //                        break;
            //                    default:
            //                        g_tiHintStr1 = "����Ҫһ�������������������������װ����";
            //                        FrmDlg.DBTIbtn1.btnState = tnor;
            //                        FrmDlg.DBTIbtn1.Caption = "��ͨ����";
            //                        FrmDlg.DBTIbtn2.btnState = tnor;
            //                        FrmDlg.DBTIbtn2.Caption = "�߼�����";
            //                        break;
            //                }
            //            }
            //            else if (FrmDlg.DWTI.tag == 2)
            //            {
            //                FrmDlg.DBTIbtn1.btnState = tnor;
            //                FrmDlg.DBTIbtn1.Caption = "����";
            //            }
            //            result = ci.Item.Eva.EvaTimes;
            //        }
            //        else
            //        {
            //            g_tiHintStr1 = string.Format("������%s�Ѿ������ټ����ˡ�", new string[] { ci.s.Name });
            //            FrmDlg.DBTIbtn1.btnState = tdisable;
            //            FrmDlg.DBTIbtn1.Caption = "��ͨ����";
            //            FrmDlg.DBTIbtn2.btnState = tdisable;
            //            FrmDlg.DBTIbtn2.Caption = "�߼�����";
            //        }
            //        break;
            //    case 2:
            //        g_tiHintStr1 = string.Format("������������������Ѿ����㷢��������%s��Ǳ�ܡ�", new string[] { iname });
            //        break;
            //    case 3:
            //        g_tiHintStr1 = string.Format("������������������Ѿ����㷢��������%s������Ǳ�ܡ�", new string[] { iname });
            //        break;
            //    case 4:
            //        g_tiHintStr1 = string.Format("��%s��Ȼû�ܷ��ָ����Ǳ�ܣ�������ӵ�и�Ӧ����������ڵ�����������", new string[] { iname });
            //        break;
            //    case 5:
            //        g_tiHintStr1 = string.Format("�Ҳ�û�ܴ�������%s�Ϸ��ָ����Ǳ�ܡ��㲻Ҫ��ɥ���һ�������Ĳ�����", new string[] { iname });
            //        break;
            //    case 6:
            //        g_tiHintStr1 = string.Format("�Ҳ�û�ܴ�������%s�Ϸ��ָ����Ǳ�ܡ�", new string[] { iname });
            //        break;
            //    case 7:
            //        g_tiHintStr1 = string.Format("�Ҳ�û�ܴ�������%s�Ϸ��ָ����Ǳ�ܡ���ı����Ѿ����ɼ�����", new string[] { iname });
            //        break;
            //    case 8:
            //        g_tiHintStr1 = "��ȱ�ٱ�����߾��ᡣ";
            //        break;
            //    case 9:
            //        g_tiHintStr1 = string.Format("��ϲ��ı��ﱻ����Ϊ����װ����������%s��", new string[] { iname });
            //        break;
            //    case 10:
            //        g_tiHintStr1 = "������Ʒ����򲻴��ڣ�";
            //        break;
            //    case 11:
            //        g_tiHintStr1 = string.Format("������%s�����Լ�����", new string[] { iname });
            //        break;
            //    case 12:
            //        FrmDlg.DBTIbtn1.btnState = tdisable;
            //        FrmDlg.DBTIbtn2.btnState = tdisable;
            //        g_tiHintStr1 = string.Format("����Ŀǰ��������%sֻ���ȼ����������ˡ�", new string[] { iname });
            //        break;
            //    case 30:
            //        g_tiHintStr1 = "�����������򲻴��ڣ�";
            //        break;
            //    case 31:
            //        g_tiHintStr1 = string.Format("����Ҫһ��%s���������ᣬ��ľ��᲻����Ҫ��", new string[] { iname });
            //        break;
            //    case 32:
            //        g_tiHintStr1 = string.Format("�߼�����ʧ�ܣ����%s��ʧ�ˣ�", new string[] { iname });
            //        break;
            //    case 33:
            //        g_tiHintStr1 = string.Format("������û��%s�����ݣ��߼�����ʧ�ܣ�", new string[] { iname });
            //        break;
            //}
            return result;
        }

        public static byte GetTIHintString1(int idx)
        {
            return GetTIHintString1(idx, null);
        }

        public static byte GetTIHintString1(int idx, TClientItem ci)
        {
            return GetTIHintString1(idx, ci, "");
        }

        public static byte GetTIHintString2(int idx, TClientItem ci, string iname)
        {
            byte result;
            result = 0;
            /*g_tiHintStr1 = "";
            switch (idx)
            {
                case 0:
                    g_tiHintStr1 = "����㲻ϲ���Ѿ��������˵ı������԰������ң���ƽ����ղظ��ֱ���һ����һ��һģһ����û��������װ����Ϊ������";
                    FrmDlg.DBTIbtn1.btnState = tdisable;
                    break;
                case 1:
                    g_tiHintStr1 = string.Format("���%s������ȥ����������������û�м������ĸ���%s�������һ�ѣ�Ҫ���Ļ�����Ҫ����һ�����˷���", new string[] { ci.s.Name, ci.s.Name });
                    FrmDlg.DBTIbtn1.btnState = tnor;
                    break;
                case 2:
                    g_tiHintStr1 = string.Format("���Ѿ�������һ��û��������%s������ԭ����%sû������֮ǰ��һģһ���ģ�", new string[] { iname, iname });
                    break;
                case 3:
                    g_tiHintStr1 = "ȱ�ٱ������ϡ�";
                    break;
                case 4:
                    g_tiHintStr1 = string.Format("������%s��û�м�������", new string[] { iname });
                    break;
                case 5:
                    g_tiHintStr1 = "���ϲ����ϣ���������˷���";
                    break;
                case 6:
                    g_tiHintStr1 = "����Ʒ��ֻ�ܷż������ı����Ķ��������ϣ����Ѿ������Ż���İ����ˡ�";
                    break;
                case 7:
                    g_tiHintStr1 = "����Ʒ��ֻ�ܷ����˷�����Ķ��������ϣ����Ѿ������Ż���İ����ˡ�";
                    break;
                case 8:
                    g_tiHintStr1 = "�������ʧ�ܡ�";
                    break;
            }*/
            return result;
        }

        public static byte GetTIHintString2(int idx)
        {
            return GetTIHintString2(idx, null);
        }

        public static byte GetTIHintString2(int idx, TClientItem ci)
        {
            return GetTIHintString2(idx, ci, "");
        }

        public static byte GetSpHintString1(int idx, TClientItem ci, string iname)
        {
            byte result;
            result = 0;
            /*g_spHintStr1 = "";
            switch (idx)
            {
                case 0:
                    g_spHintStr1 = "����Ը����˹������ؾ��ᣬҲ�����Լ��������ؾ��������������������ԡ�";
                    break;
                case 1:
                    g_spHintStr1 = "��ν������ʧ�ܣ��������ֵ�����ؾ���" + "�ĵȼ��������ȹ��Ϳ��ܵ��½��ʧ�ܣ���" + "Ҫʧ�����ٽ������ɡ�";
                    break;
                case 2:
                    g_spHintStr1 = "�Ҳ���������Ʒ�����";
                    break;
                case 3:
                    FrmDlg.DBSP.btnState = tdisable;
                    g_spHintStr1 = "û�пɼ�������������";
                    break;
                case 4:
                    g_spHintStr1 = "װ�����������ؽ��Ҫ��";
                    break;
                case 5:
                    g_spHintStr1 = "�������Ͳ�����";
                    break;
                case 6:
                    g_spHintStr1 = "����ȼ�������";
                    break;
                case 7:
                    g_spHintStr1 = "�������ؾ���İ������Ѿ�����������һ����������";
                    break;
                case 10:
                    g_spHintStr1 = "���ؾ��������ɹ���";
                    break;
                case 11:
                    g_spHintStr1 = "����������ҵ�ʧ���ˣ���������Ϊ�����" + "�ؽ�����ܵȼ��������ߣ���������������" + "����ȼ�̫����";
                    break;
                case 12:
                    g_spHintStr1 = "�Ҳ�����Ƥ��";
                    break;
                case 13:
                    g_spHintStr1 = "�������Ƥ��";
                    break;
                case 14:
                    g_spHintStr1 = "����ֵ������";
                    break;
                case 15:
                    g_spHintStr1 = "û�н�����ܣ�����ʧ�ܡ�";
                    break;
            }*/
            return result;
        }

        public static byte GetSpHintString1(int idx)
        {
            return GetSpHintString1(idx, null);
        }

        public static byte GetSpHintString1(int idx, TClientItem ci)
        {
            return GetSpHintString1(idx, ci, "");
        }

        public static byte GetSpHintString2(int idx, TClientItem ci, string iname)
        {
            byte result;
            result = 0;
            g_spHintStr1 = "";
            switch (idx)
            {
                case 0:
                    g_spHintStr1 = "����԰���Լ������ĵû�����ļ�������д�����ؾ����ϣ������Ļ����Ϳ��԰��������˽���������ԡ�";
                    break;
            }
            return result;
        }

        public static byte GetSpHintString2(int idx)
        {
            return GetSpHintString2(idx, null);
        }

        public static byte GetSpHintString2(int idx, TClientItem ci)
        {
            return GetSpHintString2(idx, ci, "");
        }

        public static void AutoPutOntiBooks()
        {
            int i;
            TClientItem cu;
            //if ((g_TIItems[0].Item.Item.Name != "") && (g_TIItems[0].Item.Item.Eva.EvaTimesMax > 0) && (g_TIItems[0].Item.Item.Eva.EvaTimes < g_TIItems[0].Item.Item.Eva.EvaTimesMax) && ((g_TIItems[1].Item.Item.Name == "") || (g_TIItems[1].Item.Item.ItemtdMode != 56) || !(g_TIItems[1].Item.Item.Itemhape >= 1 && g_TIItems[1].Item.Item.Itemhape <= 3) || (g_TIItems[1].Item.Item.Itemhape != g_TIItems[0].Item.Item.Eva.EvaTimes + 1)))
            //{
            //    for (i = MAXBAGITEMCL - 1; i >= 6; i--)
            //    {
            //        if ((g_ItemArr[i].Item.Name != "") && (g_ItemArr[i].Item.ItemtdMode == 56) && (g_ItemArr[i].Item.Itemhape == g_TIItems[0].Item.Eva.EvaTimes + 1))
            //        {
            //            if (g_TIItems[1].Item.Item.Name != "")
            //            {
            //                cu = g_TIItems[1].Item;
            //                g_TIItems[1].Item = g_ItemArr[i];
            //                g_TIItems[1].Index = i;
            //                g_ItemArr[i] = cu;
            //            }
            //            else
            //            {
            //                g_TIItems[1].Item = g_ItemArr[i];
            //                g_TIItems[1].Index = i;
            //                g_ItemArr[i].Item.Name = "";
            //            }
            //            break;
            //        }
            //    }
            //}
        }

        public static void AutoPutOntiSecretBooks()
        {
            int i;
            TClientItem cu;
            //if (FrmDlg.DWSP.Visible && (FrmDlg.DWSP.tag == 1) && (g_spItems[0].Item.Item.Name != "") && (g_spItems[0].Item.Item.Eva.EvaTimesMax > 0) && ((g_spItems[1].Item.Item.Name == "") || (g_spItems[1].Item.Item.ItemtdMode != 56) || (g_spItems[1].Item.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             s.Shape != 0)))
            //{
            //    for (i = MAXBAGITEMCL - 1; i >= 6; i--)
            //    {
            //        if ((g_ItemArr[i].Item.Name != "") && (g_ItemArr[i].Item.StdMode == 56) && (g_ItemArr[i].Item.Shape == 0))
            //        {
            //            if (g_spItems[1].Item.Item.Name != "")
            //            {
            //                cu = g_spItems[1].Item;
            //                g_spItems[1].Item = g_ItemArr[i];
            //                g_spItems[1].Index = i;
            //                g_ItemArr[i] = cu;
            //            }
            //            else
            //            {
            //                g_spItems[1].Item = g_ItemArr[i];
            //                g_spItems[1].Index = i;
            //                g_ItemArr[i].Item.Name = "";
            //            }
            //            break;
            //        }
            //    }
            //}
        }

        public static void AutoPutOntiCharms()
        {
            int i;
            TClientItem cu;
            //if ((g_TIItems[0].Item.Item.Name != "") && (g_TIItems[0].Item.Item.Eva.EvaTimesMax > 0) && (g_TIItems[0].Item.Item.Eva.EvaTimes > 0) && ((g_TIItems[1].Item.Item.Name == "") || (g_TIItems[1].Item.Item.ItemtdMode != 41) || (g_TIItems[1].Item.Item.Itemhape != 30)))
            //{
            //    for (i = MAXBAGITEMCL - 1; i >= 6; i--)
            //    {
            //        if ((g_ItemArr[i].Item.Name != "") && (g_ItemArr[i].Item.StdMode == 41) && (g_ItemArr[i].Item.Shape == 30))
            //        {
            //            if (g_TIItems[1].Item.Item.Name != "")
            //            {
            //                cu = g_TIItems[1].Item;
            //                g_TIItems[1].Item = g_ItemArr[i];
            //                g_TIItems[1].Index = i;
            //                g_ItemArr[i] = cu;
            //            }
            //            else
            //            {
            //                g_TIItems[1].Item = g_ItemArr[i];
            //                g_TIItems[1].Index = i;
            //                g_ItemArr[i].Item.Name = "";
            //            }
            //            break;
            //        }
            //    }
            //}
        }

        public static bool GetSuiteAbil(int idx, int Shape, ref byte[] sa)
        {
            bool result = false;
            //FillChar(sa);           
            //switch (idx)
            //{
            //    case 1:
            //        result = true;
            //        for (i = TtSuiteAbil.GetLowerBound(0); i <= TtSuiteAbil.GetUpperBound(0); i++ )
            //        {
            //            if ((g_UseItems[i].s.Name != "") && ((g_UseItems[i].s.Shape == Shape) || (g_UseItems[i].s.AniCount == Shape)))
            //            {
            //                sa[i] = 1;
            //            }
            //        }
            //        break;
            //    case 2:
            //        result = true;
            //        for (i = Grobal2.byte.GetLowerBound(0); i <= Grobal2.byte.GetUpperBound(0); i++ )
            //        {
            //            if ((g_HeroUseItems[i].s.Name != "") && ((g_HeroUseItems[i].s.Shape == Shape) || (g_HeroUseItems[i].s.AniCount == Shape)))
            //            {
            //                sa[i] = 1;
            //            }
            //        }
            //        break;
            //    case 3:
            //        result = true;
            //        for (i = Grobal2.byte.GetLowerBound(0); i <= Grobal2.byte.GetUpperBound(0); i++ )
            //        {
            //            if ((UserState1.UseItems[i].s.Name != "") && ((UserState1.UseItems[i].s.Shape == Shape) || (UserState1.UseItems[i].s.AniCount == Shape)))
            //            {
            //                sa[i] = 1;
            //            }
            //        }
            //        break;
            //}
            return result;
        }

        public static void InitScreenConfig()
        {
            // ��Ļ������������-��Χ
            //g_SkidAD_Rect.Left = 5;
            //g_SkidAD_Rect.Top = 7;
            //g_SkidAD_Rect.Right = SCREENWIDTH - 5;
            //g_SkidAD_Rect.Bottom = 7 + 20;
            //g_SkidAD_Rect2.Left = 183;
            //g_SkidAD_Rect2.Top = 6;
            //g_SkidAD_Rect2.Right = SCREENWIDTH - 208;
            //g_SkidAD_Rect2.Bottom = 6 + 20;
            //G_RC_SQUENGINER.Left = 78;
            //G_RC_SQUENGINER.Top = 90;
            //G_RC_SQUENGINER.Right = G_RC_SQUENGINER.Left + 16;
            //G_RC_SQUENGINER.Bottom = G_RC_SQUENGINER.Top + 95;
            //G_RC_IMEMODE.Left = SCREENWIDTH - 270 - 65;
            //G_RC_IMEMODE.Top = 105;
            //G_RC_IMEMODE.Right = G_RC_IMEMODE.Left + 60;
            //G_RC_IMEMODE.Bottom = G_RC_IMEMODE.Top + 9;
        }

        public static bool IsInMyRange(TActor Act)
        {
            bool result;
            result = false;
            if ((Act == null) || (g_MySelf == null))
            {
                return result;
            }
            if ((Math.Abs(Act.m_nCurrX - g_MySelf.m_nCurrX) <= (g_TileMapOffSetX - 2)) && (Math.Abs(Act.m_nCurrY - g_MySelf.m_nCurrY) <= (g_TileMapOffSetY - 1)))
            {
                result = true;
            }
            return result;
        }

        public static bool IsItemInMyRange(int X, int Y)
        {
            bool result;
            result = false;
            if ((g_MySelf == null))
            {
                return result;
            }
            if ((Math.Abs(X - g_MySelf.m_nCurrX) <= HUtil32._MIN(24, g_TileMapOffSetX + 9)) && (Math.Abs(Y - g_MySelf.m_nCurrY) <= HUtil32._MIN(24, (g_TileMapOffSetY + 10))))
            {
                result = true;
            }
            return result;
        }

        public static TClientStdItem GetTitle(int nItemIdx)
        {
            TClientStdItem result;
            result = null;
            nItemIdx -= 1;
            if ((nItemIdx >= 0) && (g_TitlesList.Count > nItemIdx))
            {
                if (((TStdItem)(g_TitlesList[nItemIdx])).Name != "")
                {
                    result = g_TitlesList[nItemIdx];
                }
            }
            return result;
        }

        public void initialization()
        {
            //g_APPass = new double();
            //g_dwThreadTick = new long();
            //g_dwThreadTick = 0;
            //g_pbRecallHero = new bool();
            //g_pbRecallHero = false;
            //InitializeCriticalSection(ProcMsgCS);
            //InitializeCriticalSection(ThreadCS);
            //g_APPickUpList = new THStringList();
            //g_APMobList = new THStringList();
            //g_ItemsFilter_All = new object();
            //g_ItemsFilter_All_Def = new object();
            //g_ItemsFilter_Dress = new object();
            //g_ItemsFilter_Weapon = new object();
            //g_ItemsFilter_Headgear = new object();
            //g_ItemsFilter_Drug = new object();
            //g_ItemsFilter_Other = new object();
            //g_SuiteItemsList = new object();
            //g_TitlesList = new object();
            //g_xMapDescList = new object();
            //g_xCurMapDescList = new object();
        }

        public void finalization()
        {
            //Dispose(g_APPass);
            //DeleteCriticalSection(ProcMsgCS);
            //DeleteCriticalSection(ThreadCS);
            //g_APPickUpList.Free;
            //g_APMobList.Free;
            //g_ItemsFilter_All.Free;
            //g_ItemsFilter_All_Def.Free;
            //g_ItemsFilter_Dress.Free;
            //g_ItemsFilter_Weapon.Free;
            //g_ItemsFilter_Headgear.Free;
            //g_ItemsFilter_Drug.Free;
            //g_ItemsFilter_Other.Free;
            //g_SuiteItemsList.Free;
            //g_xMapDescList.Free;
            //g_xCurMapDescList.Free;
        }

    }

    public struct TVaInfo
    {
        public string cap;
        public Rectangle[] pt1;
        public Rectangle[] pt2;
        public string[] str1;
        public string[] Hint;
    } // end TVaInfo

    public struct TFindNode
    {
        public int X;
        public int Y;
    } // end TFindNode

    public struct Tree
    {
        public int H;
        public int X;
        public int Y;
        public byte Dir;
        public Tree Father;
    }

    public struct Link
    {
        public Tree Node;
        public int F;
        public Link Next;
    }

    public struct TVirusSign
    {
        public int Offset;
        public string CodeSign;
    }

    public struct TMovingItem
    {
        public int Index;
        public TClientItem Item;
    }

    public struct TCleintBox
    {
        public int Index;
        public TClientItem Item;
    }

    public struct TMoveHMShow
    {
        public TDirectDrawSurface Surface;
        public long dwMoveHpTick;
    }

    public struct TShowItem
    {
        public string sItemName;
        public TItemType ItemType;
        public bool boAutoPickup;
        public bool boShowName;
        public int nFColor;
        public int nBColor;
    } // end TShowItem

    public struct TMapDescInfo
    {
        public string szMapTitle;
        public string szPlaceName;
        public int nPointX;
        public int nPointY;
        public Color nColor;
        public int nFullMap;
    } // end TMapDescInfo

    public struct TItemShine
    {
        public int idx;
        public long tick;
    } // end TItemShine

    public struct TSeriesSkill
    {
        public byte wMagid;
        public byte nStep;
        public bool bSpell;
    } // end TSeriesSkill

    public struct TTempSeriesSkillA
    {
        public TClientMagic pm;
        public bool bo;
    } // end TTempSeriesSkillA

    public enum TTimerCommand
    {
        tcSoftClose,
        tcReSelConnect,
        tcFastQueryChr,
        tcQueryItemPrice
    } // end TTimerCommand

    public enum TChrAction
    {
        caWalk,
        caRun,
        caHorseRun,
        caHit,
        caSpell,
        caSitdown
    } // end TChrAction

    public enum TConnectionStep
    {
        cnsIntro,
        cnsLogin,
        cnsSelChr,
        cnsReSelChr,
        cnsPlay
    } // end TConnectionStep

    public enum TItemType
    {
        i_HPDurg,
        i_MPDurg,
        i_HPMPDurg,
        i_OtherDurg,
        i_Weapon,
        i_Dress,
        i_Helmet,
        i_Necklace,
        i_Armring,
        i_Ring,
        i_Belt,
        i_Boots,
        i_Charm,
        i_Book,
        i_PosionDurg,
        i_UseItem,
        i_Scroll,
        i_Stone,
        i_Gold,
        i_Other
    }

    public class TChrMsg
    {
        public int Ident;
        public int X;
        public int Y;
        public int Dir;
        public int State;
        public int Feature;
        public int Saying;
        public int Sound;
        public int dwDelay;
    }

    public class TDropItem
    {
        public int X;
        public int Y;
        public int id;
        public int looks;
        public string Name;
        public int Width;
        public int Height;
        public int FlashTime;
        public int FlashStepTime;
        public int FlashStep;
        public bool BoFlash;
        public bool boNonSuch;
        public bool boPickUp;
        public bool boShowName;
    }
}