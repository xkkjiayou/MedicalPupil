using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WearinessExam.Examination
{
    class CffFacade
    {

        public bool openCom()
        {
            CffDllWrapper.getComPort();
            CffDllWrapper.OnCommunicate();
            return CffDllWrapper.IsCommflag();
        }

        public void setInfraredLevel(int infraredLevel)
        {
            //红外灯的亮度：0：关闭，1~16 ：分别表示1/16~16/16，即4表示 4/16的亮度
            CffDllWrapper.QueRen_But2(infraredLevel);
        }

        public void setLight(int colorLight, int Liangheibi, int LDLiangdu, int BJLiangdu, int frequencyLight)
        {
            CffDllWrapper.ClearOutputBuffer();
            CffDllWrapper.ClearInputBuffer();

            int rate1, rate2;

            //亮点亮黑比：1~3：1：3，1：1，3：1； 0：关闭
            //int Liangheibi;
            //亮点光亮度：0~6：1，1/2，1/4，1/8，1/16，1/32，1/64，    7：关闭
            //int LDLiangdu;
            //背景光亮度：0~2：1，1/4，1/16；   3：关闭
            //int BJLiangdu;
            //亮点颜色：1~5：红、绿、蓝、黄、白   0：关闭
            //int colorLight;

            //闪光频率
            rate1 = frequencyLight;
            rate2 = (frequencyLight - rate1) * 10;

            //设置亮点
            CffDllWrapper.QueRen_But(colorLight, rate1, rate2, Liangheibi, LDLiangdu);
            //设置背景灯
            //CffDllWrapper.QueRen_But1(BJLiangdu);
        }

        public void initLight()
        {
            // colorLight = 1;Liangheibi = 2; LDLiangdu = 0;BJLiangdu = 3;frequencyLight=30
            setLight(1, 2, 0, 3, 30);
            // 红外灯的亮度
            setInfraredLevel(2);
        }

        public void CFF1(int colorLight, int Liangheibi, int LDLiangdu, int BJLiangdu)
        {
            setLight(colorLight, Liangheibi, LDLiangdu, BJLiangdu, 1);
        }

        public void CFF2(int colorLight, int Liangheibi, int LDLiangdu, int BJLiangdu)
        {
            setLight(colorLight, Liangheibi, LDLiangdu, BJLiangdu, 60);
        }

        public double getFreq()
        {
            CffDllWrapper.ClearOutputBuffer();
            CffDllWrapper.ClearInputBuffer();
            CffDllWrapper.Check_But();

            Thread.Sleep(10);

            byte[] buffer = new byte[10];
            CffDllWrapper.ReadString(buffer, 10);

            double fre1, fre2;
            fre1 = buffer[4];
            fre2 = (double)buffer[5] / 10;
            return fre1 + fre2;
        }

        public void closeLight()
        {
            // colorLight = 0;Liangheibi = 0; LDLiangdu = 3;BJLiangdu = 3;frequencyLight=0
            setLight(0, 0, 3, 3, 0);
            // 红外灯关闭
            setInfraredLevel(0);
        }




    }
}
