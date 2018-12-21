using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using MedicalSys.Framework;
using daheng_xkk;
using GxIAPINET;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace WearinessExam.Examination
{
    /// <summary>
    /// Class DeviceFacade
    /// </summary>
    public class DeviceFacade
    {
        //开启API 初始化操作
        static PupilAPI api = new PupilAPI();
        static Comm comm = new Comm();
        //建立采集功能实体类，完成初始化
        public static List<PupilBMP> pbmblist = new List<PupilBMP>();
        //建立初次分割阈值
        public static int danci_bogupos_left = 0;
        public static int danci_bogupos_right = 0;
        //建立采集功能 控制变量 
        /*
         * 0->显示图像
         * 1->瞳孔追踪
         * 2->对光反射
         * */
        static int testType = 0;
        public static int TestType
        {
            get
            {
                return testType;
            }
            set
            {
                testType = value;
            }
        }

        public delegate void UpdateLeftImage(Bitmap bitmap);
        public static event UpdateLeftImage updateLeftImage;
        public delegate void UpdateRightImage(Bitmap bitmap);
        public static event UpdateRightImage updateRightImage;

        /// <summary>
        /// The image height
        /// </summary>
        public static int IMAGE_HEIGHT = 340;
        /// <summary>
        /// The iamge width
        /// </summary>
        public static int IMAGE_WIDTH = 1000;
        /// <summary>
        /// The serial communication thread
        /// </summary>
        private static Thread serialCommThread;
        /// <summary>
        /// Thread the serial comm.
        /// </summary>
        /// <param name="pParam">The p param.</param>
        private static void Thread_SerialComm(object pParam)
        {
            try
            {
                //线程用于PC对串口进行访问
                //FatigueDllWrapper.ControlSignalLamp();
            }
            catch (ThreadAbortException ex)
            {
                //m_Logger.Debug(ExceptionHelper.GetExceptionDetailInformation(ex));

            }
            catch (Exception ex)
            {
                m_Logger.Error("调用ControlSignalLamp出错！");
                m_Logger.Error(ExceptionHelper.GetExceptionDetailInformation(ex));
            }
        }
        /// <summary>
        /// The m_ logger
        /// </summary>
        private static IMSLogger m_Logger = LogFactory.GetLogger();
        public static void setLight(int colorLight, int Liangheibi, int LDLiangdu, int frequencyLight)
        {
            comm.ClearOutputBuffer();
            comm.ClearInputBuffer();

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
            comm.QueRen_But(colorLight, rate1, rate2, Liangheibi, LDLiangdu);
            System.Threading.Thread.Sleep(10);
        }

        public static void camerTrigger(int triggerAcquisition, int camerFrame)
        {
            //设置背景灯
            comm.camerTrigger(triggerAcquisition, camerFrame);
        }

        public static void setBgLight(int BJLiangdu, int BJColor, int BJTime, int mode)
        {
            //设置背景灯
            comm.QueRen_But1(BJLiangdu, BJColor, BJTime, mode);
        }

        #region  CFF1
        /// <summary>
        /// The CFF1 status
        /// </summary>
        private static bool m_CFF1Status = false;
        /// <summary>
        /// Starts the CFF1.
        /// </summary>
        /// <returns><c>true</c> if success, <c>false</c> otherwise</returns>
        public static bool StartCFF1()
        {
            bool result = true;
            try
            {
                //setLight(ExamDataCenter.LightSetting.colorLight, ExamDataCenter.LightSetting.Liangheibi, ExamDataCenter.LightSetting.LDLiangdu, ExamDataCenter.LightSetting.BJLiangdu, 4);
                // 初始化CFF低到高

                m_CFF1Status = true;

                //启动检测线程
                serialCommThread = new Thread(new ParameterizedThreadStart(Thread_SerialComm));
                serialCommThread.IsBackground = true;
                serialCommThread.Start();
                m_Logger.Info("CFF1启动!");
            }
            catch (Exception ex)
            {
                m_CFF1Status = false;
                m_Logger.Error("CFF1启动失败！");
                m_Logger.Error(ExceptionHelper.GetExceptionDetailInformation(ex));
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Gets the CFF1 data.
        /// </summary>
        /// <returns>System.Double.</returns>
        public static double GetCFF1Data()
        {

            //调用通讯Dll CFF1检测接口
            // double value = FatigueDllWrapper.Get_RealTime_CFFFrq();

            comm.ClearOutputBuffer();
            comm.ClearInputBuffer();
            comm.Check_But();

            Thread.Sleep(7);

            byte[] buffer = new byte[10];
            comm.ReadString(buffer, 10);

            double fre1, fre2;
            fre1 = buffer[4];
            fre2 = (double)buffer[5] / 10;

            return fre1 + fre2;
        }

        /// <summary>
        /// Stops the CFF1.
        /// </summary>
        /// <returns><c>true</c> if success, <c>false</c> otherwise</returns>
        public static bool StopCFF1()
        {
            bool result = true;
            try
            {
                ////恢复检测类型
                //setLight(0, 0, 3, 3, 0);
                //// 红外灯关闭
                //CffDllWrapper.QueRen_But2(0);
                //结束检测线程
                if (serialCommThread != null)
                {
                    serialCommThread.Join(20);
                    if (serialCommThread.IsAlive)
                    {
                        serialCommThread.Abort();
                    }
                }
                serialCommThread = null;
                m_CFF1Status = false;
                m_Logger.Info("CFF1停止!");
            }
            catch (Exception ex)
            {
                m_Logger.Error("CFF1停止失败！");
                m_Logger.Error(ExceptionHelper.GetExceptionDetailInformation(ex));
                result = false;
            }
            return result;
        }
        #endregion

        #region CFF2
        /// <summary>
        /// The CFF2 status
        /// </summary>
        private static bool m_CFF2Status = false;
        /// <summary>
        /// Starts the CFF2.
        /// </summary>
        /// <returns><c>true</c> if success, <c>false</c> otherwise</returns>
        public static bool StartCFF2()
        {
            bool result = true;
            try
            {
                // 初始化CFF高到低
                //setLight(ExamDataCenter.LightSetting.colorLight, ExamDataCenter.LightSetting.Liangheibi, ExamDataCenter.LightSetting.LDLiangdu, ExamDataCenter.LightSetting.BJLiangdu, 60);

                m_CFF2Status = true;
                //设置检测类型
                //启动检测线程
                serialCommThread = new Thread(new ParameterizedThreadStart(Thread_SerialComm));
                serialCommThread.IsBackground = true;
                serialCommThread.Start();
                m_Logger.Info("CFF2启动！");
            }
            catch (Exception ex)
            {
                m_CFF2Status = false;
                m_Logger.Error("CFF2启动失败！");
                m_Logger.Error(ExceptionHelper.GetExceptionDetailInformation(ex));
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Gets the CFF2 data.
        /// </summary>
        /// <returns>System.Double.</returns>
        public static double GetCFF2Data()
        {
            //调用通讯Dll CFF2数据获得接口
            // double value = FatigueDllWrapper.Get_RealTime_CFFFrq();

            comm.ClearOutputBuffer();
            comm.ClearInputBuffer();
            comm.Check_But();

            Thread.Sleep(7);

            byte[] buffer = new byte[10];
            comm.ReadString(buffer, 10);

            double fre1, fre2;
            fre1 = buffer[4];
            fre2 = (double)buffer[5] / 10;

            return fre1 + fre2;

        }

        /// <summary>
        /// Stops the CFF2.
        /// </summary>
        /// <returns><c>true</c> if success, <c>false</c> otherwise</returns>
        public static bool StopCFF2()
        {
            bool result = true;
            try
            {
                //恢复检测类型
                //FatigueDllWrapper.Set_TestType((int)TestType.PupilTrack);
                //FatigueDllWrapper.InterlockedDecrementFT_SeriCom();
                //结束检测线程
                if (serialCommThread != null)
                {
                    serialCommThread.Join(20);
                    if (serialCommThread.IsAlive)
                    {
                        serialCommThread.Abort();
                    }
                }
                m_CFF2Status = false;
                m_Logger.Info("CFF2停止！");
            }
            catch (Exception ex)
            {
                m_Logger.Error("CFF2停止失败！");
                m_Logger.Error(ExceptionHelper.GetExceptionDetailInformation(ex));
                result = false;
            }
            return result;
        }
        #endregion

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <returns>System.String.</returns>
        private static string GetErrorMessage(int errorCode)
        {
            string errorMessage = string.Empty;
            if (errorCode > 0)
            {
                switch (errorCode)
                {
                    case 41:
                        {
                            errorMessage = "左眼瞳孔追踪失败！";
                            break;
                        }
                    case 42:
                        {
                            errorMessage = "右眼瞳孔追踪失败！";
                            break;
                        }
                    case 43:
                        {
                            errorMessage = "眼扫视测试中，左眼瞳孔追踪失败！";
                            break;
                        }
                    case 44:
                        {
                            errorMessage = "眼扫视测试中，右眼瞳孔追踪失败！";
                            break;
                        }
                    case 45:
                        {
                            errorMessage = "瞳孔对光反应测试中，左眼瞳孔追踪失败！";
                            break;
                        }
                    case 46:
                        {
                            errorMessage = "瞳孔对光反应测试时，右眼瞳孔追踪失败！";
                            break;
                        }
                    default:
                        {
                            errorMessage = "未知设备错误！";
                            break;
                        }
                }
            }

            return errorMessage;
        }


        #region 瞳孔对光反应
        /// <summary>
        /// The pupil exam status
        /// </summary>
        private static bool m_PupilExamStatus = false;
        /// <summary>
        /// The pupil data count
        /// </summary>
        private static int PUPIL_SAMPLE_COUNT = 1500;
        /// <summary>
        /// Starts the pupil exam.
        /// </summary>
        /// <returns><c>true</c> if success, <c>false</c> otherwise</returns>
        public static bool StartPupilExam()
        {
            bool result = true;

            try
            {
                m_PupilExamStatus = true;
                //设置检测类型为瞳孔对光反应
                //FatigueDllWrapper.Set_TestType((int)TestType.PupilExam);
                //瞳孔对光反应
                //FatigueDllWrapper.RPL_Init();
                _leftPdList.Clear();
                _rightPdList.Clear();
                testType = 2;

                m_Logger.Info("瞳孔对光反应测试启动！");
            }
            catch (Exception ex)
            {
                m_PupilExamStatus = false;
                m_Logger.Error("瞳孔对光反应测试启动失败！");
                m_Logger.Error(ExceptionHelper.GetExceptionDetailInformation(ex));
                result = false;
            }
            return result;
        }
        /// <summary>
        /// Gets the pupil exam.
        /// </summary>
        /// <param name="dPupilLeftList">The d pupil left list.</param>
        /// <param name="dPupilRightList">The d pupil right list.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns><c>true</c> if success, <c>false</c> otherwise</returns>
        public static bool GetPupilExam(out double[] dPupilLeftList, out double[] dPupilRightList, out string errorMessage)
        {
            bool result = true;
            dPupilLeftList = null;
            dPupilRightList = null;

            bool runningFlag = true;
            errorMessage = string.Empty;
            try
            {
                while (runningFlag)
                {
                    //获得错误代码
                    int errorCode = 0;
                    if (errorCode > 0)
                    {
                        result = false;
                        errorMessage = GetErrorMessage(errorCode);
                        m_Logger.Error(errorMessage);
                        break;
                    }

                    if (!m_PupilExamStatus)
                    {
                        result = false;
                        break;
                    }
                    //判断检查知否终止，或者已经达到目标采样数
                    if (_leftPdList.Count>= PUPIL_SAMPLE_COUNT && _rightPdList.Count >= PUPIL_SAMPLE_COUNT)
                    {

                        //取出左右眼检测数据并返回
                        //GetArrayData(out pupilLeftList, out pupilRightList, pLeftPupilDList, pRightPupilDList, PUPIL_SAMPLE_COUNT);
                        dPupilLeftList = _leftPdList.ToArray();
                        dPupilRightList = _rightPdList.ToArray();
                        break;
                    }

                    Thread.Sleep(17);
                }
                if (result)
                {
                    m_Logger.Info("瞳孔对光反应获得数据成功！");
                }
            }
            catch (Exception ex)
            {

                m_Logger.Error("瞳孔对光反应获得数据失败！");
                m_Logger.Error(ExceptionHelper.GetExceptionDetailInformation(ex));
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Stops the pupil exam.
        /// </summary>
        /// <returns><c>true</c> if success, <c>false</c> otherwise</returns>
        public static bool StopPupilExam()
        {
            bool result = true;
            try
            {
                testType = 0;
                m_PupilExamStatus = false;
                //FatigueDllWrapper.Set_TestType((int)TestType.PupilTrack);
                m_Logger.Info("瞳孔对光反应停止");
            }
            catch (Exception ex)
            {
                m_Logger.Error("瞳孔对光反应停止失败！");
                m_Logger.Error(ExceptionHelper.GetExceptionDetailInformation(ex));
                result = false;
            }
            return result;
        }

        #endregion


        #region 瞳孔追踪
        /// <summary>
        /// The fatigue test thread
        /// </summary>
        private static Thread fatigueTestThread;

        /// <summary>
        /// The pupil track running flag
        /// </summary>
        private static bool pupilTrackRunningFlag = false;
        /// <summary>
        /// Starts the pupil track.
        /// </summary>
        /// <returns><c>true</c> if success, <c>false</c> otherwise</returns>
        public static bool StartPupilTrack()
        {

            danci_bogupos_left = 0;
            danci_bogupos_right = 0;
            bool result = true;
            testType = 1;
            return result;
        }


        /// <summary>
        /// Stops the pupil track.
        /// </summary>
        /// <returns><c>true</c> if success, <c>false</c> otherwise</returns>
        public static bool StopPupilTrack()
        {
            bool result = true;
            pupilTrackRunningFlag = false;
            TestType = 0;
            m_Logger.Info("停止瞳孔追踪!");

            return result;
        }

        /// <summary>
        /// Executes the pupil track.
        /// </summary>
        /// <param name="postionList">The postion list.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns><c>true</c> if success, <c>false</c> otherwise</returns>
        public static bool ExecutePupilTrack(out List<Point> postionList, out string errorMessage)
        {
            postionList = new List<Point>();
            bool result = false;
            errorMessage = string.Empty;
            try
            {
                //m_Logger.Debug("正在进行瞳孔追踪!");

                if(_isTrackedLeftEye&&_isTrackedRightEye)
                {
                    result = true;
                    //m_Logger.Debug("瞳孔追踪成功!");

                    ////保存人眼位置
                    //postionList.Add(pointLeft);
                    //postionList.Add(pointRight);
                }

            }
            catch (Exception ex)
            {
                m_Logger.Error("瞳孔追踪出错!");
                m_Logger.Error(ExceptionHelper.GetExceptionDetailInformation(ex));
                result = false;
            }
            return result;
        }
        #endregion

        #region 图像捕获


        static int _left_num = 0;
        static bool _isTrackedLeftEye = false;
        static List<double> _leftPdList = new List<double>();

        public static bool TrackedEye { get { return _isTrackedLeftEye && _isTrackedRightEye; } }

        public static void onLeftCallback(object obj, IFrameData objIFrameData)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            // 瞳孔坐标
            Point p = new Point();
            // 瞳孔半径
            double radius = 0;
            // 这个num是否需要计数，运行一次加一？
            _left_num += 1;
            // 这个地方是固定写法？第一次运行需要先计算波谷
            if (TestType !=0 && danci_bogupos_left == 0)
            {
                DeviceFacade.danci_bogupos_left = DeviceFacade.pbmblist[0].getbogupos(objIFrameData);
                //第一次求波谷
                //Console.WriteLine("左眼：" + danci_bogupos_left);
                return;
            }

            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            byte[] leftpupil = pbmblist[0].getByteImg(objIFrameData, out p, out radius, _left_num, danci_bogupos_left, TestType);
            //sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds.ToString());

            if ((_left_num - 1) % 10 == 0 && leftpupil.Length < 600000)
            {
                var leftImageData = new ImageData(p, leftpupil);
                Thread thread = new Thread(new ParameterizedThreadStart(DoTrackLeftEyeImage));
                thread.Start(leftImageData);
            }
            

            if (TestType == 2)
            {
                if (_leftPdList.Count < PUPIL_SAMPLE_COUNT)
                { 
                    _leftPdList.Add(radius * 2 * 0.0257+0.0706);
                    //Console.WriteLine(DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "瞳孔对光反应 _leftPdList:" + _leftPdList.Count.ToString() + "  value:" + (radius * 2).ToString());
                }
            }

            //sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds.ToString());

        }
        public static void DoTrackLeftEyeImage(object obj)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            ImageData imageData = obj as ImageData;
            Point p = imageData._point;
            var bitmap = ConvertToBitmap(imageData._data, 912, 554);

            if (TestType != 0)
            {
                if (bitmap != null)
                {
                    //是否追踪到瞳孔
                    _isTrackedLeftEye = p.X > 0 && p.Y > 0;

                    if (_isTrackedLeftEye)
                    {
                        //创建瞳孔位置曲线
                        Point[] pointListLeftX, pointListLeftY;
                        pointListLeftX = new Point[] { new Point(p.X - 20, p.Y), new Point(p.X + 20, p.Y) };
                        pointListLeftY = new Point[] { new Point(p.X, p.Y - 20), new Point(p.X, p.Y + 20) };

                        Bitmap imageShow = new Bitmap(bitmap);
                        Pen pen = new Pen(Color.Red, 3);
                        //在图像上标记瞳孔位置
                        using (Graphics graphics = Graphics.FromImage(imageShow))
                        {
                            graphics.DrawLines(pen, pointListLeftX);
                            graphics.DrawLines(pen, pointListLeftY);
                        }
                        bitmap.Dispose();
                        bitmap = imageShow;
                    }
                }
            }

            if (updateLeftImage != null)
            {
                updateLeftImage(bitmap);
            }
            //sw.Stop();
            //Console.WriteLine(sw.ElapsedMilliseconds.ToString());
        }

        
        
        static int _right_num = 0;
        static bool _isTrackedRightEye = false;
        static List<double> _rightPdList = new List<double>();
        public static void onRightCallback(object obj, IFrameData objIFrameData)
        {
            // 瞳孔坐标
            Point p = new Point();
            // 瞳孔半径
            double radius = 0;
            // 这个num是否需要计数，运行一次加一？
            _right_num += 1;
            // 这个地方是固定写法？第一次运行需要先计算波谷
            if (TestType != 0 && danci_bogupos_right == 0)
            {
                danci_bogupos_right = pbmblist[1].getbogupos(objIFrameData);
                //Console.WriteLine("右眼："+danci_bogupos_right);
                //第一次求波谷
                return;
            }
            

            byte[] rightpupil = pbmblist[1].getByteImg(objIFrameData, out p, out radius, _right_num, danci_bogupos_right, TestType);

            if ((_right_num - 1) % 10 == 0 && rightpupil.Length<600000)
            {
                var rightImageData = new ImageData(p, rightpupil);
                Thread thread = new Thread(new ParameterizedThreadStart(DoTrackRightEyeImage));
                thread.Start(rightImageData);
            }

            if (TestType == 2)
            {
                if (_rightPdList.Count < PUPIL_SAMPLE_COUNT)
                {
                    _rightPdList.Add(radius * 2 * 0.0241+0.4144);
                    //Console.WriteLine(DateTime.Now.ToString("yyyyMMdd HHmmssfff") + "瞳孔对光反应 _rightPdList" + _rightPdList.Count.ToString() + "  value:" + (radius * 2).ToString());
                }
            }
            //this.Invoke(new Action(() =>
            //{
            //    pbxVideoRight.Image = DeviceFacade.BytesToBitmap(rightpupil);

            //}));
        }

        public static void DoTrackRightEyeImage(object obj)
        {
            ImageData imageData = obj as ImageData;
            Point p = imageData._point;
            var bitmap = ConvertToBitmap(imageData._data, 912, 554);

            if (TestType != 0)
            {
                if (bitmap != null)
                {
                    //是否追踪到瞳孔
                    _isTrackedRightEye = p.X > 0 && p.Y > 0;

                    if (_isTrackedRightEye)
                    {
                        //创建瞳孔位置曲线
                        Point[] pointListRightX, pointListRightY;
                        pointListRightX = new Point[] { new Point(p.X - 20, p.Y), new Point(p.X + 20, p.Y) };
                        pointListRightY = new Point[] { new Point(p.X, p.Y - 20), new Point(p.X, p.Y + 20) };

                        Bitmap imageShow = new Bitmap(bitmap);
                        Pen pen = new Pen(Color.Red, 3);
                        //在图像上标记瞳孔位置
                        using (Graphics graphics = Graphics.FromImage(imageShow))
                        {
                            graphics.DrawLines(pen, pointListRightX);
                            graphics.DrawLines(pen, pointListRightY);
                        }
                        bitmap = imageShow;
                    }
                }
            }

            if (updateRightImage != null)
            {
                updateRightImage(bitmap);
            }
        }



        public static bool StartCaptureDoubleImage()
        {
            bool flag = api.Init_Camera();
            if(!flag)
            {
                m_Logger.Error("初始化摄像头失败。");
            }
            return flag;
        }


        public static void CaptureDoubleImage(CaptureDelegate leftFun, CaptureDelegate rightFun)
        {
            api.Start_Capture_Image(leftFun, rightFun);
        }


        public static void StopCaptureImage()
        {
            api.Stop_Camera_Capture();
            //api.Disconnect_Camera();
        }



        [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(IntPtr hObject);

        /// <summary>
        /// Create and initialize grayscale image
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>Bitmap.</returns>
        public static Bitmap CreateGrayscaleImage(int width, int height)
        {
            Bitmap bmp;
            
            // create new image
            Bitmap grayscaleImage = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
            // set palette to grayscale
            SetGrayscalePalette(grayscaleImage);
            
            //Clone m_GrayscaleImage
            bmp = grayscaleImage.Clone(new Rectangle(0, 0, width, height), PixelFormat.Format8bppIndexed);

            grayscaleImage.Dispose();
            grayscaleImage = null;
            // return new image
            return bmp;
        }
        /// <summary>
        /// Set pallete of the image to grayscale
        /// </summary>
        /// <param name="srcImg">The SRC img.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public static void SetGrayscalePalette(Bitmap srcImg)
        {
            // check pixel format
            if (srcImg.PixelFormat != PixelFormat.Format8bppIndexed)
                throw new ArgumentException();

            // get palette
            ColorPalette cp = srcImg.Palette;
            // init palette
            for (int i = 0; i < 256; i++)
            {
                cp.Entries[i] = Color.FromArgb(i, i, i);
            }
            // set palette back
            srcImg.Palette = cp;
        }

        /// <summary>
        /// The m_ video capture thread
        /// </summary>
        private static Thread m_VideoCaptureThread;
        /// <summary>
        /// Starts the receive data.
        /// </summary>
        /// <returns><c>true</c> if success, <c>false</c> otherwise</returns>
        public static bool StartReceiveData()
        {

            bool result = true;
            try
            {
                //FatigueDllWrapper.InterlockedIncrementFT_GetImage();
                //创建GetImage线程，启动线程
                m_VideoCaptureThread = new Thread(ReceiveData);
                m_VideoCaptureThread.IsBackground = true;
                m_VideoCaptureThread.Start();
                m_Logger.Info("开始接受数据!");
            }
            catch (Exception ex)
            {
                m_Logger.Error("开始接受数据出错!");
                m_Logger.Error(ExceptionHelper.GetExceptionDetailInformation(ex));
                result = false;
            }
            return result;
        }

        public static Bitmap ConvertToBitmap(byte[] rawValues, int width, int height)
        {
            //创建图像大小的灰色图片
            Rectangle rect = new Rectangle(0, 0, width, height);
            Bitmap bitmap = CreateGrayscaleImage(width, height);
            try
            {
                //为图片加锁
                BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
                //获得图片像素首地址
                System.IntPtr dstPtr = bitmapData.Scan0;
                //把buffer中的像素数据copy到dstBitmap
                Marshal.Copy(rawValues, 0, dstPtr, rawValues.Length);
                //释放锁
                bitmap.UnlockBits(bitmapData);
            }
            catch (Exception ex) {
                Console.WriteLine( ex.Message);
            }
            return bitmap;
        }
        
        /// <summary>
        /// Receives the data.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private static void ReceiveData(object obj)
        {
            try
            {
                Thread.Sleep(35);
                //调用DLL获取图像数据
                //int result = FatigueDllWrapper.GetData();
            }
            catch (ThreadAbortException ex)
            {
                //m_Logger.Debug(ExceptionHelper.GetExceptionDetailInformation(ex));
            }
            catch (System.AccessViolationException ex)
            {
                //m_Logger.Debug(ExceptionHelper.GetExceptionDetailInformation(ex));
            }
            catch (Exception ex)
            {
                m_Logger.Error("调用GetData失败!");
                m_Logger.Error(ExceptionHelper.GetExceptionDetailInformation(ex));
            }
        }
        /// <summary>
        /// Stops the receive data.
        /// </summary>
        /// <returns><c>true</c> if success, <c>false</c> otherwise</returns>
        public static bool StopReceiveData()
        {
            bool result = true;
            try
            {
                //if (FatigueDllWrapper.Get_FT_Function() > 0)
                //{
                //    FatigueDllWrapper.InterlockedDecrementFT_Function();
                //}
                //if (FatigueDllWrapper.Get_FT_SeriCom() > 0)
                //{
                //    FatigueDllWrapper.InterlockedDecrementFT_SeriCom();
                //}
                //if (FatigueDllWrapper.Get_FT_GetImage() > 0)
                //{
                //    FatigueDllWrapper.InterlockedDecrementFT_GetImage();
                //}

                //IntPtr synSignal = FatigueDllWrapper.Get_Syn_Signal();
                //关闭Event
                //FatigueDllWrapper.CloseHandle(synSignal);

                if (m_VideoCaptureThread != null)
                {
                    //等待GetData线程结束
                    if (m_VideoCaptureThread.IsAlive)
                    {
                        m_VideoCaptureThread.Join(50);
                    }
                    if (m_VideoCaptureThread.IsAlive)
                    {
                        //结束GetData线程
                        m_VideoCaptureThread.Abort();
                    }
                }
                m_Logger.Info("停止接受数据!");

            }
            catch (Exception ex)
            {
                m_Logger.Error("停止接受数据失败!");
                m_Logger.Error(ExceptionHelper.GetExceptionDetailInformation(ex));

                result = false;
            }
            return result;
        }
        #endregion

        #region 设备启动、停止
        /// <summary>
        /// Closes the devices.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public static bool CloseDevices()
        {
            bool result = true;

            try
            {
                // 调用DLL关闭视标、光源、相机
                //int errorCode = FatigueDllWrapper.DeviceEnd();

                setLight(0, 0, 3, 0);
                setBgLight(3, 0, 0, 0);
                System.Threading.Thread.Sleep(10);
                comm.QueRen_But2(0);
                //bool success = CffDllWrapper.disconnectCamera();
                bool success = api.Disconnect_Camera();
                success = comm.Close() && success;
                if (!success)
                {
                    m_Logger.Error("设备停止失败!");
                    result = false;
                    return result;
                }
                m_Logger.Info("设备停止.");
            }
            catch (Exception ex)
            {
                result = false;
                m_Logger.Error("设备停止失败!");
                m_Logger.Error(ExceptionHelper.GetExceptionDetailInformation(ex));
            }
            return result;
        }

        /// <summary>
        /// Closes the devices.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public static bool SystemProtect()
        {
            bool result = true;

            try
            {
                // 调用DLL关闭视标、光源
                //int errorCode = FatigueDllWrapper.SystemProtect();

                //if (errorCode != 0)
                //{
                //    m_Logger.Error("关闭视标、光源失败!");
                //    result = false;
                //    return result;
                //}
                m_Logger.Info("视标、光源已关闭.");
            }
            catch (Exception ex)
            {
                result = false;
                m_Logger.Error("关闭视标、光源失败!");
                m_Logger.Error(ExceptionHelper.GetExceptionDetailInformation(ex));
            }
            return result;
        }

        /// <summary>
        /// Devices the self check.
        /// </summary>
        /// <param name="statusText">The status text.</param>
        /// <returns><c>true</c> if success, <c>false</c> otherwise</returns>
        public static bool DeviceSelfCheck(ref string statusText)
        {
            bool result = true;
            try
            {
                // 调用DLL启动系统自检流程，完成上位机与下位机的通信及控制测试
                //int errorNo = FatigueDllWrapper.DeviceInit();
                m_Logger.Info("disconnectCamera.");
                //CffDllWrapper.disconnectCamera();
                api.Disconnect_Camera();
                m_Logger.Info("initCamera.");
                pbmblist = api.Connect_Camera();

                bool success = true;
                if(pbmblist == null || pbmblist.Count < 2)
                {
                    success = false;
                }
                //bool success = CffDllWrapper.initCamera();
                //success = api.Init_Camera();

                //CffDllWrapper.Close();
                string portName = IniSettingConfig.GetInstance().ComPortName;
                m_Logger.Info("setComPort " + portName);
                comm.setComPort(portName);
                comm.DataReceived += Comm_DataReceived;
                m_Logger.Info("OnCommunicate.");
                comm.OnCommunicate();
                //byte[] byteArray = System.Text.Encoding.Default.GetBytes("115200,n,8,1");
                //success = CffDllWrapper.Open(5, byteArray) && success;
                m_Logger.Info("IsCommflag.");
                success = comm.IsCommflag && success;
                m_Logger.Info("QueRen_But2.");
                comm.QueRen_But2(5);
                //处理Error
                if (!success)
                {
                    //int errorNo = CffDllWrapper.getErrorNum();
                    statusText = "自检失败!";
                    m_Logger.Error(statusText);
                    result = false;
                }
                else
                {
                    m_Logger.Info("自检成功!");
                }
            }
            catch (Exception ex)
            {
                result = false;

                statusText = "自检失败!";
                m_Logger.Error(statusText);
                m_Logger.Error(ExceptionHelper.GetExceptionDetailInformation(ex));
            }

            return result;
        }

        private static void Comm_DataReceived(byte[] readBuffer)
        {
            Console.WriteLine(Encoding.UTF8.GetString(readBuffer));
            m_Logger.Info(Encoding.UTF8.GetString(readBuffer));
        }

        //public static void ProgramLoopEnd()
        //{
        //    FatigueDllWrapper.ProgramLoopEnd();
        //}

        #endregion

    }
}
