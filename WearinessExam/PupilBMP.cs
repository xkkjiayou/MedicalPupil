using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using GxIAPINET;
using System.Diagnostics;
using System.Collections;

namespace daheng_xkk
{
    public class PupilBMP
    {
        public ArrayList pointlist = new ArrayList();
        public ArrayList radiuslist = new ArrayList();

        IGXDevice m_objIGXDevice = null;                ///<设备对像
        bool m_bIsColor = false;               ///<是否支持彩色相机
        public byte[] m_byMonoBuffer = null;                ///<黑白相机buffer
        byte[] m_byColorBuffer = null;                ///<彩色相机buffer
        byte[] m_byRawBuffer = null;                ///<用于存储Raw图的Buffer
        int m_nPayloadSize = 0;                   ///<图像数据大小
        public int m_nWidth = 0;                   ///<图像宽度
        public int m_nHeigh = 0;                   ///<图像高度
        Bitmap m_bitmapForSave = null;                ///<bitmap对象,仅供存储图像使用
        const uint PIXEL_FORMATE_BIT = 0x00FF0000;          ///<用于与当前的数据格式进行与运算得到当前的数据位数
        const uint GX_PIXEL_8BIT = 0x00080000;          ///<8位数据图像格式
                                                        ///
        public int stride = 0;
        const int COLORONCOLOR = 3;
        const uint DIB_RGB_COLORS = 0;
        const uint SRCCOPY = 0x00CC0020;
        CWin32Bitmaps.BITMAPINFO m_objBitmapInfo = new CWin32Bitmaps.BITMAPINFO();
        IntPtr m_pBitmapInfo = IntPtr.Zero;


        /// <summary>
        /// 构造函数用于初始化设备对象与PictureBox控件对象
        /// </summary>
        /// <param name="objIGXDevice">设备对象</param>
        /// <param name="objPictureBox">图像显示控件</param>
        public PupilBMP(IGXDevice objIGXDevice)
        {
            m_objIGXDevice = objIGXDevice;
            string strValue = null;
            if (null != objIGXDevice)
            {
                //获得图像原始数据大小、宽度、高度等
                m_nPayloadSize = (int)objIGXDevice.GetRemoteFeatureControl().GetIntFeature("PayloadSize").GetValue();
                m_nWidth = (int)objIGXDevice.GetRemoteFeatureControl().GetIntFeature("Width").GetValue();
                m_nHeigh = (int)objIGXDevice.GetRemoteFeatureControl().GetIntFeature("Height").GetValue();
                //获取是否为彩色相机
                if (objIGXDevice.GetRemoteFeatureControl().IsImplemented("PixelColorFilter"))
                {
                    strValue = objIGXDevice.GetRemoteFeatureControl().GetEnumFeature("PixelColorFilter").GetValue();

                    if ("None" != strValue)
                    {
                        m_bIsColor = true;
                    }
                }
            }

            //申请用于缓存图像数据的buffer
            m_byRawBuffer = new byte[m_nPayloadSize];
            m_byMonoBuffer = new byte[__GetStride(m_nWidth, m_bIsColor) * m_nHeigh];
            m_byColorBuffer = new byte[__GetStride(m_nWidth, m_bIsColor) * m_nHeigh];

            __CreateBitmap(out m_bitmapForSave, m_nWidth, m_nHeigh, m_bIsColor);

            if (m_bIsColor)
            {
                m_objBitmapInfo.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(CWin32Bitmaps.BITMAPINFOHEADER));
                m_objBitmapInfo.bmiHeader.biWidth = m_nWidth;
                m_objBitmapInfo.bmiHeader.biHeight = -m_nHeigh;
                m_objBitmapInfo.bmiHeader.biPlanes = 1;
                m_objBitmapInfo.bmiHeader.biBitCount = 24;
                m_objBitmapInfo.bmiHeader.biCompression = 0;
                m_objBitmapInfo.bmiHeader.biSizeImage = 0;
                m_objBitmapInfo.bmiHeader.biXPelsPerMeter = 0;
                m_objBitmapInfo.bmiHeader.biYPelsPerMeter = 0;
                m_objBitmapInfo.bmiHeader.biClrUsed = 0;
                m_objBitmapInfo.bmiHeader.biClrImportant = 0;
            }
            else
            {
                m_objBitmapInfo.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(CWin32Bitmaps.BITMAPINFOHEADER));
                m_objBitmapInfo.bmiHeader.biWidth = m_nWidth;
                m_objBitmapInfo.bmiHeader.biHeight = -m_nHeigh;
                m_objBitmapInfo.bmiHeader.biPlanes = 1;
                m_objBitmapInfo.bmiHeader.biBitCount = 8;
                m_objBitmapInfo.bmiHeader.biCompression = 0;
                m_objBitmapInfo.bmiHeader.biSizeImage = 0;
                m_objBitmapInfo.bmiHeader.biXPelsPerMeter = 0;
                m_objBitmapInfo.bmiHeader.biYPelsPerMeter = 0;
                m_objBitmapInfo.bmiHeader.biClrUsed = 0;
                m_objBitmapInfo.bmiHeader.biClrImportant = 0;

                m_objBitmapInfo.bmiColors = new CWin32Bitmaps.RGBQUAD[256];
                // 黑白图像需要初始化调色板
                for (int i = 0; i < 256; i++)
                {
                    m_objBitmapInfo.bmiColors[i].rgbBlue = (byte)i;
                    m_objBitmapInfo.bmiColors[i].rgbGreen = (byte)i;
                    m_objBitmapInfo.bmiColors[i].rgbRed = (byte)i;
                    m_objBitmapInfo.bmiColors[i].rgbReserved = (byte)i;
                }
            }
            m_pBitmapInfo = Marshal.AllocHGlobal(2048);
            Marshal.StructureToPtr(m_objBitmapInfo, m_pBitmapInfo, false);
        }

        Point second_loc_point = Point.Empty;
        static int hrange = 250;
        static int wrange = 250;
        static int wend = -10;
        static int wstart = -10;
        static int hstart = -10;
        static int hend = -10;
        //求thresh
        public int getbogupos(IBaseData objIBaseData)
        {
            GX_VALID_BIT_LIST emValidBits = GX_VALID_BIT_LIST.GX_BIT_0_7;
            int bogupos12 = 0;
            int bogupos = 0;
            //检查图像是否改变并更新Buffer
            __UpdateBufferSize(objIBaseData);

            if (null != objIBaseData)
            {
                emValidBits = __GetBestValudBit(objIBaseData.GetPixelFormat());
                if (GX_FRAME_STATUS_LIST.GX_FRAME_STATUS_SUCCESS == objIBaseData.GetStatus())
                {

                    IntPtr pBufferMono = IntPtr.Zero;
                    if (__IsPixelFormat8(objIBaseData.GetPixelFormat()))
                    {
                        pBufferMono = objIBaseData.GetBuffer();
                    }
                    else
                    {
                        pBufferMono = objIBaseData.ConvertToRaw8(emValidBits);
                    }
                    stride = __GetStride(m_nWidth, m_bIsColor);
                    int len = stride * m_nHeigh;
                    Marshal.Copy(pBufferMono, m_byMonoBuffer, 0, len);
                    int i = 0;
                    int j = 0;
                    int[] hist = new int[256];
                    int maxgray = 0;
                    int maxpos = 0;
                    int bogugray = 0;
                    //求阈值峰值
                    // Console.Write("波谷----------");
                    for (i = 0; i < m_nHeigh; i++)
                    {
                        for (j = 0; j < m_nWidth; j++)
                        {
                            int t = m_byMonoBuffer[i * stride + j];
                            hist[t]++;
                            if (hist[t] > maxgray)
                            {
                                if (t <= 50 && t >= 15)
                                {
                                    maxgray = hist[t];
                                    maxpos = t;
                                }
                            }
                        }
                    }
                    //求阈值波谷
                    bogugray = maxgray;
                    //Console.WriteLine("灰度最大值是：" + maxpos + "");
                    for (int k = 5; k < maxpos; k++)
                    {
                        if (hist[k] < bogugray)
                        {
                            bogugray = hist[k];
                            bogupos = k;
                        }
                    }
                    //Console.WriteLine("波峰：" + maxpos+"波谷"+ bogupos);
                    //Console.WriteLine("第一次求波谷" + bogupos + "最大值" + maxpos);
                }


                double radius = 1;
                if (wend == -10)
                {
                    //int  bogupos11 = small_getbogupos(imgbyte, 0, h, 0, w);
                    Point point = getcenterpoint(m_byMonoBuffer, 0, 554, 0, 912, bogupos, out radius);
                    wend = Math.Min(point.X + wrange, m_nWidth);
                    wstart = Math.Max(point.X - wrange, 0);
                    hstart = Math.Max(point.Y - hrange, 0);
                    hend = Math.Min(point.Y + hrange, m_nHeigh);
                }

                //三次精确 次定位
                bogupos12 = small_getbogupos(m_byMonoBuffer, hstart, hend, wstart, wend);

                wend = -10;
                wstart = -10;
                hstart = -10;
                hend = -10;


            }
            return bogupos12;
        }


        public int small_getbogupos(byte[] m_byMonoBuffer, int xstart, int xend, int ystart, int yend )
        {
            int bogupos = 0;
            int[] hist = new int[256];
            int maxgray = 0;
            int maxpos = 0;
            int bogugray = 0;
            //求阈值峰值
            // Console.Write("波谷----------");
            for (int i = xstart; i < xend; i++)
            {
                for (int j = ystart; j < yend; j++)
                {
                    int t = m_byMonoBuffer[i * stride + j];
                    hist[t]++;
                    if (hist[t] > maxgray)
                    {
                        if (t <= 50 && t >= 15)
                        {
                            maxgray = hist[t];
                            maxpos = t;
                        }
                    }
                }
            }
            //求阈值波谷
            bogugray = maxgray;
            //Console.WriteLine("灰度最大值是：" + maxpos + "");
            for (int k = 5; k < maxpos; k++)
            {
                if (hist[k] < bogugray)
                {
                    bogugray = hist[k];
                    bogupos = k;
                }
            }

            //if (bogupos < 8 || bogupos > 18)
            //{
            //    return 0;
            //}
            //Console.WriteLine("求波谷" + bogupos + "最大值" + maxpos);

            return bogupos;
        }


        public Point getdivimg(byte[] imgbyte, int h, int w, out double radius, int bogupos)
        {
            radius = 1;
            int i = 0;
            int j = 0;
            //int[] hist = new int[256];
            //int maxgray = 0;
            //int maxpos = 0;
            //int bogugray = 0;
            try
            {
                //if (bogupos == 0) { 
                //    //求阈值峰值
                //    Console.Write("波谷----------");
                //    for (i = 0; i < h; i++)
                //    {
                //        for (j = 0; j < w; j++)
                //        {
                //            int t = imgbyte[i * stride + j];
                //            hist[t]++;
                //            if (hist[t] > maxgray)
                //            {
                //                maxgray = hist[t];
                //                maxpos = t;
                //            }
                //        }
                //    }
                //    //求阈值波谷
                //    bogugray = maxgray;
                //    for (int k = 12; k < maxpos; k++)
                //    {
                //        if (hist[k] < bogugray)
                //        {
                //            bogugray = hist[k];
                //            bogupos = k;
                //        }
                //    }
                //}
                //初次定位
                //Point point = new Point();
                if (wend == -10)
                {
                    //int  bogupos11 = small_getbogupos(imgbyte, 0, h, 0, w);
                    Point point = getcenterpoint(imgbyte, 0, h, 0, w, bogupos, out radius);
                    //Console.WriteLine("X:" + point.X + "----------------Y:" + point.Y);
                    wend = Math.Min(point.X + wrange, m_nWidth);
                    wstart = Math.Max(point.X - wrange, 0);
                    hstart = Math.Max(point.Y - hrange, 0);
                    hend = Math.Min(point.Y + hrange, m_nHeigh);
                    //return point;
                }
                //二次定位
                //int bogupos12 = small_getbogupos(imgbyte, hstart, hend, wstart, wend);
                second_loc_point = getcenterpoint(imgbyte, hstart, hend, wstart, wend, bogupos, out radius);
                return second_loc_point;

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
                return Point.Empty;
            }
        }

        //求坐标   
        public Point getcenterpoint(byte[] imgbyte, int xstart, int xend, int ystart, int yend, int thresh, out double radius)
        {
            bool firstpoint = false;
            int firstx = 0;
            int firsty = 0;
            int xpossum = 0, ypossum = 0, count = 1;
            for (int i = xstart; i < xend; i++)
            {
                for (int j = ystart; j < yend; j++)
                {
                    //Console.WriteLine(i * bd.Stride + j);
                    int t = imgbyte[i * stride + j];
                    if (t <= thresh)
                    {
                        //if (!firstpoint) {
                        //    firstx = i;
                        //    firsty = j;
                        //    firstpoint = true;

                        //}
                        //用于显示二值化后的图像
                        //imgbyte[i * stride + j] = (byte)0;
                        xpossum += i;
                        ypossum += j;
                        count++;
                    }
                    //else
                    //{

                    //    //用于显示二值化后的图像
                    //    //imgbyte[i * stride + j] = (byte)255;
                    //}
                }
            }
            int yp = ypossum / count;
            int xp = xpossum / count;
            Point p = new Point();
            if (yp > m_nWidth) { yp = m_nWidth; }
            if (xp > m_nHeigh) { xp = m_nHeigh; }
            p.Y = xp;
            p.X = yp;

            //for (int j = ystart; j < yend; j++)
            //{
            //    //Console.WriteLine(i * bd.Stride + j);
            //    int t = imgbyte[xp * stride + j];
            //    if (t <= thresh)
            //    {
            //        if (!firstpoint)
            //        {
            //            firsty = j;
            //            firstpoint = true;
            //            break;

            //        }
            //        //用于显示二值化后的图像
            //        //imgbyte[i * stride + j] = (byte)0;
            //        //xpossum += i;
            //        //ypossum += j;
            //        //count++;
            //    }
            //    //else
            //    //{

            //    //    //用于显示二值化后的图像
            //    //    //imgbyte[i * stride + j] = (byte)255;
            //    //}
            //}

            //radius = Math.Sqrt(Math.Abs(firstx - xp) * Math.Abs(firstx - xp) + Math.Abs(firsty - yp) * Math.Abs(firsty - yp));

            radius = Math.Sqrt(count / 3.14);
            return p;
        }




        /// <summary>
        /// 检查图像是否改变并更新Buffer
        /// </summary>
        /// <param name="objIBaseData">图像数据对象</param>
        private void __UpdateBufferSize(IBaseData objIBaseData)
        {
            if (null != objIBaseData)
            {
                if (__IsCompatible(m_bitmapForSave, m_nWidth, m_nHeigh, m_bIsColor))
                {
                    m_nPayloadSize = (int)objIBaseData.GetPayloadSize();
                    m_nWidth = (int)objIBaseData.GetWidth();
                    m_nHeigh = (int)objIBaseData.GetHeight();
                }
                else
                {
                    m_nPayloadSize = (int)objIBaseData.GetPayloadSize();
                    m_nWidth = (int)objIBaseData.GetWidth();
                    m_nHeigh = (int)objIBaseData.GetHeight();

                    m_byRawBuffer = new byte[m_nPayloadSize];
                    m_byMonoBuffer = new byte[__GetStride(m_nWidth, m_bIsColor) * m_nHeigh];
                    m_byColorBuffer = new byte[__GetStride(m_nWidth, m_bIsColor) * m_nHeigh];

                    //更新BitmapInfo
                    m_objBitmapInfo.bmiHeader.biWidth = m_nWidth;
                    m_objBitmapInfo.bmiHeader.biHeight = m_nHeigh;
                    Marshal.StructureToPtr(m_objBitmapInfo, m_pBitmapInfo, false);
                }
            }
        }



        /// <summary>
        /// 判断PixelFormat是否为8位
        /// </summary>
        /// <param name="emPixelFormatEntry">图像数据格式</param>
        /// <returns>true为8为数据，false为非8位数据</returns>
        private bool __IsPixelFormat8(GX_PIXEL_FORMAT_ENTRY emPixelFormatEntry)
        {
            bool bIsPixelFormat8 = false;
            uint uiPixelFormatEntry = (uint)emPixelFormatEntry;
            if ((uiPixelFormatEntry & PIXEL_FORMATE_BIT) == GX_PIXEL_8BIT)
            {
                bIsPixelFormat8 = true;
            }
            return bIsPixelFormat8;
        }

        /// <summary>
        /// 通过GX_PIXEL_FORMAT_ENTRY获取最优Bit位
        /// </summary>
        /// <param name="em">图像数据格式</param>
        /// <returns>最优Bit位</returns>
        private GX_VALID_BIT_LIST __GetBestValudBit(GX_PIXEL_FORMAT_ENTRY emPixelFormatEntry)
        {
            GX_VALID_BIT_LIST emValidBits = GX_VALID_BIT_LIST.GX_BIT_0_7;
            switch (emPixelFormatEntry)
            {
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO8:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GR8:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_RG8:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GB8:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_BG8:
                    {
                        emValidBits = GX_VALID_BIT_LIST.GX_BIT_0_7;
                        break;
                    }
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO10:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GR10:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_RG10:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GB10:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_BG10:
                    {
                        emValidBits = GX_VALID_BIT_LIST.GX_BIT_2_9;
                        break;
                    }
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO12:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GR12:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_RG12:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GB12:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_BG12:
                    {
                        emValidBits = GX_VALID_BIT_LIST.GX_BIT_4_11;
                        break;
                    }
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO14:
                    {
                        //暂时没有这样的数据格式待升级
                        break;
                    }
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_MONO16:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GR16:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_RG16:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_GB16:
                case GX_PIXEL_FORMAT_ENTRY.GX_PIXEL_FORMAT_BAYER_BG16:
                    {
                        //暂时没有这样的数据格式待升级
                        break;
                    }
                default:
                    break;
            }
            return emValidBits;
        }

        /// <summary>
        /// 获取图像显示格式
        /// </summary>
        /// <param name="bIsColor">是否为彩色相机</param>
        /// <returns>图像的数据格式</returns>
        private PixelFormat __GetFormat(bool bIsColor)
        {
            return bIsColor ? PixelFormat.Format24bppRgb : PixelFormat.Format8bppIndexed;
        }

        /// <summary>
        /// 计算宽度所占的字节数
        /// </summary>
        /// <param name="nWidth">图像宽度</param>
        /// <param name="bIsColor">是否是彩色相机</param>
        /// <returns>图像一行所占的字节数</returns>
        private int __GetStride(int nWidth, bool bIsColor)
        {
            return bIsColor ? nWidth * 3 : nWidth;
        }

        /// <summary>
        /// 判断是否兼容
        /// </summary>
        /// <param name="bitmap">Bitmap对象</param>
        /// <param name="nWidth">图像宽度</param>
        /// <param name="nHeight">图像高度</param>
        /// <param name="bIsColor">是否是彩色相机</param>
        /// <returns>true为一样，false不一样</returns>
        private bool __IsCompatible(Bitmap bitmap, int nWidth, int nHeight, bool bIsColor)
        {
            if (bitmap == null
                || bitmap.Height != nHeight
                || bitmap.Width != nWidth
                || bitmap.PixelFormat != __GetFormat(bIsColor)
             )
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 创建Bitmap
        /// </summary>
        /// <param name="bitmap">Bitmap对象</param>
        /// <param name="nWidth">图像宽度</param>
        /// <param name="nHeight">图像高度</param>
        /// <param name="bIsColor">是否是彩色相机</param>
        private void __CreateBitmap(out Bitmap bitmap, int nWidth, int nHeight, bool bIsColor)
        {
            bitmap = new Bitmap(nWidth, nHeight, __GetFormat(bIsColor));
            if (bitmap.PixelFormat == PixelFormat.Format8bppIndexed)
            {
                ColorPalette colorPalette = bitmap.Palette;
                for (int i = 0; i < 256; i++)
                {
                    colorPalette.Entries[i] = Color.FromArgb(i, i, i);
                }
                bitmap.Palette = colorPalette;
            }
        }

        /// <summary>
        /// 更新和复制图像数据到Bitmap的buffer
        /// </summary>
        /// <param name="bitmap">Bitmap对象</param>
        /// <param name="nWidth">图像宽度</param>
        /// <param name="nHeight">图像高度</param>
        /// <param name="bIsColor">是否是彩色相机</param>
        private void __UpdateBitmap(Bitmap bitmap, byte[] byBuffer, int nWidth, int nHeight, bool bIsColor)
        {
            //给BitmapData加锁
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            //得到一个指向Bitmap的buffer指针
            IntPtr ptrBmp = bmpData.Scan0;
            int nImageStride = __GetStride(m_nWidth, bIsColor);
            //图像宽能够被4整除直接copy
            if (nImageStride == bmpData.Stride)
            {
                Marshal.Copy(byBuffer, 0, ptrBmp, bmpData.Stride * bitmap.Height);
            }
            else//图像宽不能够被4整除按照行copy
            {
                for (int i = 0; i < bitmap.Height; ++i)
                {
                    Marshal.Copy(byBuffer, i * nImageStride, new IntPtr(ptrBmp.ToInt64() + i * bmpData.Stride), m_nWidth);
                }
            }
            //BitmapData解锁
            bitmap.UnlockBits(bmpData);
        }

        public byte[] getByteImg(IBaseData objIBaseData, out Point p, out double radius, int num, int danci_bogupos, int calculatewhat = 0)
        {
            GX_VALID_BIT_LIST emValidBits = GX_VALID_BIT_LIST.GX_BIT_0_7;
            radius = 1;
            p = new Point();
            //检查图像是否改变并更新Buffer
            __UpdateBufferSize(objIBaseData);


            if (null != objIBaseData)
            {
                emValidBits = __GetBestValudBit(objIBaseData.GetPixelFormat());
              
                if (GX_FRAME_STATUS_LIST.GX_FRAME_STATUS_SUCCESS == objIBaseData.GetStatus())
                {

                    IntPtr pBufferMono = IntPtr.Zero;
                    pBufferMono = objIBaseData.GetBuffer();
                    
                    stride = __GetStride(m_nWidth, m_bIsColor);
                    int len = stride * m_nHeigh;
                    Marshal.Copy(pBufferMono, m_byMonoBuffer, 0, len);


                    if (calculatewhat == 2 || calculatewhat == 1)
                    {
                        //Stopwatch sw = new Stopwatch();
                        //sw.Start();
                        //getbogupos(objIBaseData);
                        p = getdivimg(m_byMonoBuffer, m_nHeigh, m_nWidth, out radius, danci_bogupos);
                        //sw.Stop();
                        //Console.WriteLine("time：" + sw.ElapsedMilliseconds.ToString());
                    }
                    return m_byMonoBuffer;

                }
            }
            return m_byMonoBuffer;
        }

        private void __UpdateBitmapForSave(byte[] byBuffer)
        {
            if (__IsCompatible(m_bitmapForSave, m_nWidth, m_nHeigh, m_bIsColor))
            {
                __UpdateBitmap(m_bitmapForSave, byBuffer, m_nWidth, m_nHeigh, m_bIsColor);
            }
            else
            {
                __CreateBitmap(out m_bitmapForSave, m_nWidth, m_nHeigh, m_bIsColor);
                __UpdateBitmap(m_bitmapForSave, byBuffer, m_nWidth, m_nHeigh, m_bIsColor);
            }
        }


    }



}
