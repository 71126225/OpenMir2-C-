using M2Server.Actor;
using M2Server.Maps;

namespace M2Server
{
    /// <summary>
    /// ��ͼ��Ʒ
    /// </summary>
    public class VisibleMapItem
    {
        public short nX;
        public short nY;
        public MapItem MapItem;
        public string sName;
        public ushort wLooks;
        public VisibleFlag VisibleFlag;
    }
}