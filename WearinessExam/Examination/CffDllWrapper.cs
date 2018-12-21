using System;
using System.Runtime.InteropServices;

namespace WearinessExam.Examination
{
    class CffDllWrapper
    {
        // 人眼瞳孔参数
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct Eye_Pupil
        {
            //x方向人眼瞳孔质心坐标
            public float x;

            //y方向人眼瞳孔质心坐标
            public float y;

            //瞳孔直径水平方起始点x方向坐标
            public int StartP_X;

            //瞳孔直径水平方起始点y方向坐标
            public int StartP_Y;

            //瞳孔直径水平方终止点x方向坐标
            public int EndP_X;

            //瞳孔直径水平方终止点y方向坐标            
            public int EndP_Y;

            //瞳孔直径数据
            public float pupil_D;
        }

        //// 初始化相机
        //[DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        //public static extern bool initCamera();

        //// 采集图像
        //[DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        //public static extern IntPtr grabImage();

        //[DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        //public static extern IntPtr startCapture();

        //[DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        //public static extern IntPtr stopCapture();

        //[DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        //public static extern Eye_Pupil getLeftEyePupil();

        //[DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        //public static extern Eye_Pupil getRightEyePupil();

        //// 断开相机
        //[DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        //public static extern bool disconnectCamera();

        //// 获取相机错误类型
        //[DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        //public static extern int getErrorNum();


        // 串口自动连接，从注册表当中获得当前可用的串口号
        [DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool getComPort();

        // 通信一次
        [DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OnCommunicate();

        // 判断是否通信
        [DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool IsCommflag();

        // 关闭串口，同时也关闭关联线程
        [DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern bool Close();

        // 清除发送缓冲区
        [DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void ClearOutputBuffer();

        // 清除接受缓冲区
        [DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void ClearInputBuffer();

        [DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void QueRen_But(int colour, int rate1, int rate2, int Liangheibi, int LDLiangdu);

        [DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void QueRen_But1(int BJLiangdu, int BJColor, int BJTime, int mode);

        [DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void QueRen_But2(int HWLianghen);

        [DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void Check_But();

        [DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void ReadString(byte[] szBuffer, int dwBufferLength, int dwWaitTime = 20);


        //相机帧率设置（由控制板触发）
        [DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void camerTrigger(int triggerAcquisition, int camerFrame);

        //亮度精调
        [DllImport("CffTest.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
        public static extern void setupLight(int setType, int lightValue);


    }
}
