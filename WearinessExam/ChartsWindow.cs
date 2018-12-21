using MedicalSys.MSCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace WearinessExam
{
    public partial class ChartsWindow : MedicalSys.MSCommon.BaseForm
    {

        #region 常量
        /// <summary>
        /// The Output folder
        /// </summary>
        private const string OUT_FOLDER = "output";
        /// <summary>
        /// The JPG image extention
        /// </summary>
        public const string IMAGE_EXTENTION_JPG = ".JPG";
        /// <summary>
        /// The BMP image extention
        /// </summary>
        public const string IMAGE_EXTENTION_BMP = ".BMP";
        /// <summary>
        /// The PNG image extention
        /// </summary>
        public const string IMAGE_EXTENTION_PNG = ".PNG";
        #endregion 常量

        #region 曲线图上的Label
        private const string CURVE_LABEL_CFF2 = "高到低";
        private const string CURVE_LABEL_CFF1 = "低到高";
        private const string TITLE_CFF = "闪光融合频率曲线";
        private const string TITLE_CFF_X = "T (s)";
        private const string TITLE_CFF_Y = "f (Hz)";
        private const string CURVE_LABEL_PCL = "PCL（左眼）";
        private const string CURVE_LABEL_PCL2 = "PCL（右眼）";
        private const string TITLE_PCL = "瞳孔对光反应曲线";
        private const string TITLE_PCL_X = "T(ms)";
        private const string TITLE_PCL_Y = "d (mm)";
        #endregion

        public ChartsWindow()
        {
            InitializeComponent();

            InitializeCurveGraphics();
        }

        #region 函数
        /// <summary>
        /// Initializes the curve graphics.
        /// </summary>
        private void InitializeCurveGraphics()
        {
            //CFF曲线
            cgCFF.SetTitle(TITLE_CFF, TITLE_CFF_X, TITLE_CFF_Y);
            cgCFF.SetXScale(0, 30);
            cgCFF.SetYScale(0, 60);
            cgCFF.SetStep(0.1, 1);
            cgCFF.SetSampleTime(100, 30);
            cgCFF.SetUnitToMs(1000);
            //CFF1曲线
            LineItemData itemDataCFF1 = new LineItemData();
            itemDataCFF1.Color = Color.Blue;
            itemDataCFF1.fun = null;
            itemDataCFF1.Name = CURVE_LABEL_CFF1;
            itemDataCFF1.SymbolType = ZedGraph.SymbolType.None;
            cgCFF.AddLineItemData(itemDataCFF1);
            //CFF2曲线
            LineItemData itemDataCFF2 = new LineItemData();
            itemDataCFF2.Color = Color.Green;
            itemDataCFF2.fun = null;
            itemDataCFF2.Name = CURVE_LABEL_CFF2;
            itemDataCFF2.SymbolType = ZedGraph.SymbolType.None;
            cgCFF.AddLineItemData(itemDataCFF2);
            cgCFF.Initial();
            //PCL曲线
            cgPCL.SetTitle(TITLE_PCL, TITLE_PCL_X, TITLE_PCL_Y);
            cgPCL.SetXScale(0, 5000);
            cgPCL.SetYScale(0, 10);
            cgPCL.SetStep(100, 500);
            cgPCL.SetSampleTime(3.333, 5000);
            cgPCL.SetUnitToMs(1);
            cgPCL.IsShowPointValues = true;
            //PCL曲线(左眼)
            LineItemData itemLeftDataPCL = new LineItemData();
            itemLeftDataPCL.Color = Color.Blue;
            itemLeftDataPCL.fun = null;
            itemLeftDataPCL.Name = CURVE_LABEL_PCL;
            itemLeftDataPCL.SymbolType = ZedGraph.SymbolType.None;
            cgPCL.AddLineItemData(itemLeftDataPCL);
            //PCL曲线(右眼)
            //LineItemData itemRightDataPCL = new LineItemData();
            //itemRightDataPCL.Color = Color.Green;
            //itemRightDataPCL.fun = null;
            //itemRightDataPCL.Name = CURVE_LABEL_PCL2;
            //itemRightDataPCL.SymbolType = ZedGraph.SymbolType.None;
            //cgPCL.AddLineItemData(itemRightDataPCL);
        }

        /// <summary>
        /// Updates the curve.
        /// </summary>
        /// <param name="cff1Data">The CFF1 data.</param>
        /// <param name="cff2Data">The CFF2 data.</param>
        /// <param name="pdLeftData">The left pd data.</param>
        /// <param name="pdRightData">The right pd data.</param>
        public void UpdateCurve(double[] cff1Data, double[] cff2Data, double[] pdLeftData, double[] pdRightData)
        {
            //清空曲线数据
            cgCFF.InitalCurveLists();
            cgPCL.InitalCurveLists();

            //如果CFF1已经检测完成，显示CFF1曲线
            if (cff1Data != null)
            {
                cgCFF.AddData(CURVE_LABEL_CFF1, cff1Data);
            }

            //如果CFF2已经检测完成，显示CFF2曲线
            if (cff2Data != null)
            {
                cgCFF.AddData(CURVE_LABEL_CFF2, cff2Data);
            }

            //如果PCL已经检测完成，显示PCL曲线
            if (pdLeftData != null)
            {
                cgPCL.AddData(CURVE_LABEL_PCL, pdLeftData);
            }
            if (pdRightData != null)
            {
                cgPCL.AddData(CURVE_LABEL_PCL2, pdRightData);
            }
        }

        /// <summary>
        /// Draws the lines.
        /// </summary>
        /// <param name="cff1">CFF低到高</param>
        /// <param name="cff2">CFF高到低</param>
        /// <param name="sl">眼扫视潜伏期值</param>
        /// <param name="sv">眼扫视速度</param>
        /// <param name="pcl">对光反应潜伏期值</param>
        /// <param name="pmd">最小瞳孔直径值</param>
        public void DrawLines(double cff1, double cff2, double pcl1, double pcl2, double pmd1, double pmd2)
        {
            // CFF低到高
            cgCFF.DrawPannelXLine(cff1);
            // CFF高到低
            cgCFF.DrawPannelXLine(cff2);
            // 瞳孔对光反应曲线——潜伏期值(左眼)
            cgPCL.DrawPannelYLine(pcl1, Color.Blue);
            // 瞳孔对光反应曲线——最小瞳孔直径值(左眼)
            cgPCL.DrawPannelXLine(pmd1, Color.Blue);
            // 瞳孔对光反应曲线——潜伏期值(右眼)
            //cgPCL.DrawPannelYLine(pcl2, Color.Green);
            // 瞳孔对光反应曲线——最小瞳孔直径值(右眼)
            //cgPCL.DrawPannelXLine(pmd2, Color.Green);
        }

        public byte[] GetCffImageData()
        {
            return GetImageData(cgCFF);
        }

        public byte[] GetPupilExamImageData()
        {
            return GetImageData(cgPCL);
        }

        private byte[] GetImageData(ZedGraphControl cg)
        {
            byte[] byteImage;
            using (Bitmap image = cg.GraphPane.GetImage(60 * 15, 28 * 15, 1))
            {
                MemoryStream ms = null;
                try
                {

                    ms = new MemoryStream();
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    byteImage = new Byte[ms.Length];
                    byteImage = ms.ToArray();
                    return byteImage;
                }
                catch (ArgumentNullException ex)
                {
                    throw ex;
                }
                finally
                {
                    ms.Close();
                }
            }
        }

        /// <summary>
        /// Gets the image format.
        /// </summary>
        /// <param name="extention">The extention.</param>
        /// <returns>System.Drawing.Imaging.ImageFormat.</returns>
        public System.Drawing.Imaging.ImageFormat GetImageFormat(string extention)
        {
            System.Drawing.Imaging.ImageFormat imageformat = null;
            string upperExtention = extention.ToUpper();
            // 根据扩展名设置图片格式
            if (upperExtention == IMAGE_EXTENTION_JPG)
            {
                imageformat = System.Drawing.Imaging.ImageFormat.Jpeg;
            }
            else if (upperExtention == IMAGE_EXTENTION_PNG)
            {
                imageformat = System.Drawing.Imaging.ImageFormat.Png;
            }
            else if (upperExtention == IMAGE_EXTENTION_BMP)
            {
                imageformat = System.Drawing.Imaging.ImageFormat.Bmp;
            }

            return imageformat;
        }
        #endregion 函数

        #region 事件
        /// <summary>
        /// Handles the Click event of the SaveImage button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            saveFileDialog.AddExtension = true;
            saveFileDialog.CheckPathExists = true;
            saveFileDialog.InitialDirectory = Path.Combine(Application.StartupPath, OUT_FOLDER);

            saveFileDialog.SupportMultiDottedExtensions = false;
            // 皮肤控件影响
            //saveFileDialog.Title = "保存图片";
            saveFileDialog.AddExtension = true;
            saveFileDialog.Filter = "Bimap Image|*.bmp" + "|JPEG Image|*.jpg" + "|PNG Image|*.png";

            if (saveFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                ZedGraphControl zedGraphControl = cgCFF;

                // 保存当前选项卡的图片
                using (Bitmap image = zedGraphControl.GraphPane.GetImage())
                {
                    System.Drawing.Imaging.ImageFormat imageFormat = GetImageFormat(Path.GetExtension(saveFileDialog.FileName));

                    image.Save(saveFileDialog.FileName, imageFormat);
                }
            }
        }
        #endregion 事件
    }
}
