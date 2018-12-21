using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MedicalSys.MSCommon;
using WearinessExam.DAO;
using WearinessExam.DO;
using ZedGraph;

namespace WearinessExam
{
    public partial class ViewChartsForm : MedicalSys.MSCommon.BaseForm
    {

        private GraphPane mPane;
        private DataTable _examDataCFF = null;
        private DataTable _examDataPID = null;
        private BaseValue _baseValue = null;
        private int _patientKey;

        public ViewChartsForm()
        {
            InitializeComponent();
        }

        public ViewChartsForm(int patientKey)
        {
            InitializeComponent();

            _patientKey = patientKey;
            this.Load += ViewChartsForm_Load;

            mPane = zedGraphControl1.GraphPane;
            mPane.XAxis.Title.Text = "日期";   //X轴标题
            mPane.YAxis.Title.Text = "值"; //Y轴标题
            // X坐标时间显示格式
            mPane.XAxis.Type = AxisType.Date;
            mPane.XAxis.CrossAuto = true;
            mPane.XAxis.Scale.Format = "yyyy-MM-dd";
            mPane.AxisChange();
            // 坐标点时间显示格式
            zedGraphControl1.PointDateFormat = "yyyy-MM-dd";
        }

        private void ViewChartsForm_Load(object sender, EventArgs e)
        {
            getData(_patientKey);
            rdbCFF.Checked = true;
        }

        private void getData(int patientKey)
        {
            //读取检查数据
            ExamInfoDAO examDao = new ExamInfoDAO();
            DataAccessProxy.Execute<DataTable>(() => { return examDao.GetExamDataForBaseValue("PID", patientKey); }, this, out _examDataPID);
            DataAccessProxy.Execute<DataTable>(() => { return examDao.GetExamDataForBaseValue("CFF", patientKey); }, this, out _examDataCFF);
            BaseValueDAO baseValueDao = new BaseValueDAO();
            _baseValue = baseValueDao.GetBaseValue(patientKey);
        }

        private void SetXScale(int min, int max)
        {
            mPane.YAxis.Scale.Min = min;
            mPane.YAxis.Scale.Max = max;
        }

        private void SetStep(double minor, double major)
        {
            mPane.YAxis.Scale.MinorStep = minor;
            mPane.YAxis.Scale.MajorStep = major;
        }

        private void clearCharts()
        {
            // 清空绘制区
            mPane.CurveList.Clear();
            mPane.GraphObjList.Clear();
            zedGraphControl1.Refresh();
            //X坐标时间显示格式
            mPane.XAxis.Type = AxisType.Date;
            mPane.XAxis.CrossAuto = true;
            mPane.XAxis.Scale.Format = "yyyy-MM-dd";
            mPane.AxisChange();
            //坐标点时间显示格式
            zedGraphControl1.PointDateFormat = "yyyy-MM-dd";
        }

        private void DrawPannelXLines(double y, SymbolType type,Color color)
        {
            RollingPointPairList list = new RollingPointPairList(2);
            LineItem curve = mPane.AddCurve("", list, color, type);
            curve.AddPoint(mPane.XAxis.Scale.Min, y);
            curve.AddPoint(mPane.XAxis.Scale.Max, y);

            // Force a redraw
            zedGraphControl1.Invalidate();
        }



        private void updateChart(string indicatorName, double baseValue,double upperLimit, double lowerLimit)
        {
            PointPairList listUpper = new PointPairList();
            PointPairList listLower = new PointPairList();
            PointPairList listNormal = new PointPairList();
            PointPairList list = new PointPairList();
            foreach (DataRow dr in _examDataCFF.Rows)
            {
                DateTime dtExam = (DateTime)dr["EXAM_DATE_TIME"];
                double x = (double)new XDate(dtExam.Year, dtExam.Month, dtExam.Day);
                double y = 0d;
                if (dr[indicatorName]!= DBNull.Value)
                {
                    y = Convert.ToDouble(dr[indicatorName]);
                }
                
                if (y > upperLimit)
                {
                    listUpper.Add(x, y);
                }
                else if (y < lowerLimit)
                {
                    listLower.Add(x, y);
                }
                else
                {
                    listNormal.Add(x, y);
                }

                list.Add(x, y);
            }

            mPane.XAxis.Scale.Min = list[list.Count - 1].X + 1;
            mPane.XAxis.Scale.Max = list[0].X - 1;
            zedGraphControl1.AxisChange();

            // 绘制偏差范围外的点 大于2.5%
            LineItem curveUpper = mPane.AddCurve("", listUpper, Color.Red, SymbolType.Triangle);
            curveUpper.Line.IsVisible = false;
            zedGraphControl1.Invalidate();
            // 绘制偏差范围外的点 小于2.5%
            LineItem curveLower = mPane.AddCurve("", listLower, Color.Red, SymbolType.TriangleDown);
            curveLower.Line.IsVisible = false;
            zedGraphControl1.Invalidate();
            // 绘制偏差范围内的点
            LineItem curveNormal = mPane.AddCurve("", listNormal, Color.Blue, SymbolType.Star);
            curveNormal.Line.IsVisible = false;
            zedGraphControl1.Invalidate();

            // 绘制基础值
            DrawPannelXLines(baseValue, SymbolType.None,Color.Blue);
            // 绘制基础值上偏差线 1+2.5%
            DrawPannelXLines(upperLimit, SymbolType.HDash, Color.Green);
            // 绘制基础值下偏差线 1-2.5%
            DrawPannelXLines(lowerLimit, SymbolType.HDash, Color.Green);
            
            zedGraphControl1.AxisChange();
        }

        private void rdbCFF_CheckedChanged(object sender, EventArgs e)
        {
            double upperLimit = Math.Round(_baseValue.CFF * (1d + 0.025d), 2);
            double lowerLimit = Math.Round(_baseValue.CFF * (1d - 0.025d), 2);

            clearCharts();
            mPane.Title.Text = "CFF";//标题
            updateChart("CFF", _baseValue.CFF, upperLimit, lowerLimit);
        }

        private void rdbPID_CheckedChanged(object sender, EventArgs e)
        {
            double upperLimit = Math.Round(_baseValue.PID * (1d + 0.025d), 2);
            double lowerLimit = Math.Round(_baseValue.PID * (1d - 0.025d), 2);

            clearCharts();
            mPane.Title.Text = "PID";//标题
            updateChart("PID", _baseValue.PID, upperLimit, lowerLimit);
        }

        private void rdbPMD_CheckedChanged(object sender, EventArgs e)
        {
            double upperLimit = Math.Round(_baseValue.PMD * (1d + 0.025d), 2);
            double lowerLimit = Math.Round(_baseValue.PMD * (1d - 0.025d), 2);

            clearCharts();
            mPane.Title.Text = "PMD";//标题
            updateChart("PMD", _baseValue.PMD, upperLimit, lowerLimit);
        }

        private void rdbPCV_CheckedChanged(object sender, EventArgs e)
        {
            double upperLimit = Math.Round(_baseValue.PCV * (1d + 0.025d), 2);
            double lowerLimit = Math.Round(_baseValue.PCV * (1d - 0.025d), 2);

            clearCharts();
            mPane.Title.Text = "PCV";//标题
            updateChart("PCV", _baseValue.PCV, upperLimit, lowerLimit);
        }

        private void rdbPCL_CheckedChanged(object sender, EventArgs e)
        {
            double upperLimit = Math.Round(_baseValue.PCL * (1d + 0.025d), 2);
            double lowerLimit = Math.Round(_baseValue.PCL * (1d - 0.025d), 2);

            clearCharts();
            mPane.Title.Text = "PCL";//标题
            updateChart("PCL", _baseValue.PCL, upperLimit, lowerLimit);
        }

        private void rdbPCT_CheckedChanged(object sender, EventArgs e)
        {
            double upperLimit = Math.Round(_baseValue.PCT * (1d + 0.025d), 2);
            double lowerLimit = Math.Round(_baseValue.PCT * (1d - 0.025d), 2);

            clearCharts();
            mPane.Title.Text = "PCT";//标题
            updateChart("PCT", _baseValue.PCT, upperLimit, lowerLimit);
        }

        private void rdbPCR_CheckedChanged(object sender, EventArgs e)
        {
            double upperLimit = Math.Round(_baseValue.PCR * (1d + 0.025d), 2);
            double lowerLimit = Math.Round(_baseValue.PCR * (1d - 0.025d), 2);

            clearCharts();
            mPane.Title.Text = "PCR";//标题
            updateChart("PCR", _baseValue.PCR, upperLimit, lowerLimit);
        }

        private void rdbPCA_CheckedChanged(object sender, EventArgs e)
        {
            double upperLimit = Math.Round(_baseValue.PCA * (1d + 0.025d), 2);
            double lowerLimit = Math.Round(_baseValue.PCA * (1d - 0.025d), 2);

            clearCharts();
            mPane.Title.Text = "PCA";//标题
            updateChart("PCA", _baseValue.PCA, upperLimit, lowerLimit);
        }

        private void rdbPCD_CheckedChanged(object sender, EventArgs e)
        {
            double upperLimit = Math.Round(_baseValue.PCD * (1d + 0.025d), 2);
            double lowerLimit = Math.Round(_baseValue.PCD * (1d - 0.025d), 2);

            clearCharts();
            mPane.Title.Text = "PCD";//标题
            updateChart("PCD", _baseValue.PCD, upperLimit, lowerLimit);
        }
    }
}
