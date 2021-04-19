using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace M2Server
{
    public class TOSObject
    {
        public byte btType;
        public object CellObj;
        public double dwAddTime;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct TMapHeader
    {
        public short wWidth;
        public short wHeight;
    }

    public struct TMapUnitInfo
    {
        public short wBkImg;
        // 32768 $8000 Ϊ��ֹ�ƶ�����
        public short wMidImg;
        public short wFrImg;
        public byte btDoorIndex;
        // $80 (��¦), ���� �ĺ� �ε���
        public byte btDoorOffset;
        // ���� ���� �׸��� ��� ��ġ, $80 (����/����(�⺻))
        public byte btAniFrame;
        // $80(Draw Alpha) +  ������ ��
        public byte btAniTick;
        public byte btArea;
        // ���� ����
        public byte btLight;
    }

    public class TMapCellinfo
    {
        public byte chFlag;
        public IList<TOSObject> ObjList;
    }
}

