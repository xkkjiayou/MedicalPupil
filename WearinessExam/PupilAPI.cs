using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using GxIAPINET;
using System.IO;
using System.Diagnostics;

namespace daheng_xkk
{
    public class PupilAPI
    {
        //相机实体相关变量
        static List<IGXDeviceInfo> listcamerainfo = new List<IGXDeviceInfo>();
        static List<IGXFeatureControl> cameraFeaturelist = new List<IGXFeatureControl>();
        static List<IGXDevice> cameralist = new List<IGXDevice>();

        //采集图像相关变量
        static PupilBMP gbm1 = null;
        static PupilBMP gbm2 = null;
        static List<PupilBMP> pupilBMPList = new List<PupilBMP>();
        static List<IGXStream> streamlist = new List<IGXStream>();

        /**
         * 采集控制相关变量 
         * 0->普通采集 只显示图像
         * 1->瞳孔追踪 显示图像 + 返回坐标追踪数据 + 返回半径追踪数据
         **/
        int calculatewhat = 0;

        /**
         * 刷新相机
         * 获得当前 连接的相机
         * 当数量小于两台的时候，抛出异常，并返回 false
         * 当数量等于两台的时候，连接两台，若成功则返回true，若连接异常，则抛出异常并返回false
         * */
        private int Connect_Num_Camera()
        {
            GxIAPINET.IGXFactory.GetInstance().Init();
            //获取当前PC连接相机数
            listcamerainfo = new List<IGXDeviceInfo>();
            IGXFactory.GetInstance().UpdateAllDeviceList(200, listcamerainfo);
            Console.WriteLine("当前接入了{0}台相机", listcamerainfo.Count);
            return listcamerainfo.Count;

        }


        /**
        * 连接相机，并绑定传输类
        * 若两台均连接成功则返回封装了采集功能 相机采集实体类 列表
        * 若任意一台连接异常，则抛出异常并返回null
        * */
        public List<PupilBMP> Connect_Camera()
        {
            try
            {
                Connect_Num_Camera();
                if (listcamerainfo.Count > 0)
                {
                    int i = 0;
                    IGXDevice camera = null;
                    foreach (IGXDeviceInfo o in listcamerainfo)
                    {
                        camera = IGXFactory.GetInstance().OpenDeviceBySN(o.GetSN(), GX_ACCESS_MODE.GX_ACCESS_EXCLUSIVE);
                        if (camera != null)
                        {
                            //MessageBox.Show("连接设备：SN：" + csn + "成功");
                            cameralist.Add(camera);

                            //拿到相机属性
                            IGXFeatureControl cameraFeature = camera.GetRemoteFeatureControl();
                            cameraFeaturelist.Add(cameraFeature);

                            if (i == 0)
                            {
                                gbm1 = new PupilBMP(cameralist[0]);
                                pupilBMPList.Add(gbm1);

                            }
                            if (i == 1)
                            {
                                gbm2 = new PupilBMP(cameralist[1]);
                                pupilBMPList.Add(gbm2);
                            }
                            i++;
                        }

                    }
                    //MessageBox.Show("相机打开成功");
                    return pupilBMPList;
                }
                else
                {
                    // MessageBox.Show("暂没有设备接入");
                    return null;
                }

            }
            catch (Exception e1)
            {
                MessageBox.Show("连接失败，原因" + e1.Message);
                throw e1;
            }

        }

        /**
           * 初始化相机参数
           * 若两台均初始化成功则返回true
           * 若任意一台初始化异常，则抛出异常并返回false
           * */
        public bool Init_Camera()
        {
            try
            {
                int i = 0;
                foreach (IGXFeatureControl featureControl in cameraFeaturelist)
                {

                    featureControl.GetIntFeature("Height").SetValue(554);
                    featureControl.GetIntFeature("Width").SetValue(912);
                    featureControl.GetFloatFeature("Gain").SetValue(8);
                    if (i == 0)
                    {
                        featureControl.GetIntFeature("OffsetX").SetValue(64);
                        featureControl.GetIntFeature("OffsetY").SetValue(126);
                        //featureControl.GetIntFeature("OffsetX").SetValue(144);
                    }

                    if (i == 1)
                    {
                        featureControl.GetIntFeature("OffsetX").SetValue(144);
                        featureControl.GetIntFeature("OffsetY").SetValue(112);
                        //featureControl.GetIntFeature("OffsetX").SetValue(112);
                    }

                    featureControl.GetFloatFeature("ExposureTime").SetValue(3000);

                    i++;

                }
                if (i >= 2)
                {
                    //MessageBox.Show("初始化成功");
                    return true;
                }
                else
                {
                    throw new Exception("初始化失败，两台相机没有同时接入");
                    return false;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("初始化失败，原因" + e1.Message);
                throw e1;
                return false;
            }

        }


        /**
         * 开启采集
         * 显示图像
         * public void onFramecallback(object obj, IFrameData objIFrameData) 回调函数示例
         * **/
        public bool Start_Capture_Image(CaptureDelegate LeftFun, CaptureDelegate RightFun)
        {
            try
            {
                calculatewhat = 0;
                streamlist = new List<IGXStream>();
                int i = 0;
                foreach (IGXDevice camera in cameralist)
                {
                    //打开相机采集stream流
                    IGXStream cameraStream = camera.OpenStream(0);
                    streamlist.Add(cameraStream);
                    if (i == 0)
                    {
                        cameraStream.RegisterCaptureCallback(camera, LeftFun);
                        Set_Camera_Capture(cameraStream, camera.GetRemoteFeatureControl());
                    }
                    if (i == 1)
                    {
                        cameraStream.RegisterCaptureCallback(camera, RightFun);
                        Set_Camera_Capture(cameraStream, camera.GetRemoteFeatureControl());
                    }
                    i++;
                }
                if (i >= 2)
                {
                    // MessageBox.Show("开启采集成功");
                    return true;
                }
                else
                {
                    throw new Exception("开启采集失败，两台相机没有同时接入");
                    return false;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("开启失败，原因" + e1.Message);
                throw e1;
                return false;
            }

        }

        /**
         * 采集初始化，用于开通采集通道
         * 设置Line3 input
         * 设置Strobe
         * 设置Trigger Mode On
         * */
        //专门开启采集通道
        private void Set_Camera_Capture(IGXStream cameraStream, IGXFeatureControl cameraFeature)
        {
            if (cameraFeature != null)
            {
                cameraStream.SetAcqusitionBufferNumber(10);
                cameraFeature.GetEnumFeature("AcquisitionMode").SetValue("Continuous");
                cameraFeature.GetEnumFeature("TriggerMode").SetValue("On");
                cameraFeature.GetEnumFeature("LineSelector").SetValue("Line3");
                cameraFeature.GetEnumFeature("LineMode").SetValue("Input");
                cameraFeature.GetEnumFeature("TriggerSource").SetValue("Line3");
                cameraFeature.GetEnumFeature("LineSource").SetValue("Strobe");
                cameraStream.StartGrab();
                cameraFeature.GetCommandFeature("AcquisitionStart").Execute();
            }

        }

        /**
         * 停止采集
         * */
        public bool Stop_Camera_Capture()
        {
            try
            {
                int i = 0;
                //在停止采集前，一定要先关闭采集属性！！！
                foreach (IGXFeatureControl featureControl in cameraFeaturelist)
                {
                    featureControl.GetCommandFeature("AcquisitionStop").Execute();
                    

                }
                foreach (IGXStream cameraStream in streamlist)
                {
                    //cameraFeature.GetCommandFeature("AcquisitionStop").Execute();
                    cameraStream.UnregisterCaptureCallback();
                    //停止采集                    
                    cameraStream.FlushQueue();
                    cameraStream.StopGrab();
                    //cameraData.Destroy();采用回调函数以后 就没有这个东西了
                    cameraStream.Close();
                    i++;
                }
                return true;
            }
            catch (Exception e1)
            {

                MessageBox.Show("停止采集失败，原因：" + e1.Message);
                throw e1;
                return false;
            }
        }


        /**
         * 关闭相机
         * */
        public Boolean Disconnect_Camera()
        {
            try
            {
                foreach (IGXDevice camera in cameralist)
                {
                    if (camera != null)
                    {
                        
                        // MessageBox.Show("关闭设备:" + camera.GetDeviceInfo().GetSN() + "成功");
                        camera.Close();
                    }

                }
                foreach (IGXStream cameraStream in streamlist)
                {
                  
                    //cameraFeature.GetCommandFeature("AcquisitionStop").Execute();
                    //停止采集
                    cameraStream.StopGrab();
                    cameraStream.UnregisterCaptureCallback();
                    //cameraData.Destroy();采用回调函数以后 就没有这个东西了
                    cameraStream.Close();
                }
                cameralist.Clear();
                listcamerainfo.Clear();
                cameraFeaturelist.Clear();
                streamlist.Clear();
                return true;
            }
            catch (Exception e1)
            {
                throw e1;
                return false;
            }
        }





    }
}
