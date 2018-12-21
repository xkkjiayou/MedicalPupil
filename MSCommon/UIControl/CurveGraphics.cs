using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace MedicalSys.MSCommon
{
    public partial class CurveGraphics : ZedGraph.ZedGraphControl
    {
        private Timer m_Timer = new Timer();
        //sample rate time (ms)
        private double m_SampleTime = 100d;
        //storage time (s)
        private double m_StorageTime = 60d;
        private double m_UnitToMs = 1000d;

        //X Scale Min
        private int m_XScaleMin = 0;
        //X Scale Max
        private int m_XScaleMax = 30;

        //Y Scale Min
        private int m_YScaleMin = 0;
        //Y Scale Max
        private int m_YScaleMax = 30;

        private double m_MinorStep = 1;
        private double m_MajorStep = 5;

        // Starting time in milliseconds
        int tickStart = 0;

        private List<LineItemData> m_LineItemData = new List<LineItemData>();


        public bool IsShowGrid
        {
            get;
            set;
        }
        
       

        public CurveGraphics()
        {
            InitializeComponent();
            this.IsShowContextMenu = false;
            this.IsEnableZoom = false;
        }

        public void AddLineItemData(LineItemData lineItemData)
        {
            lineItemData.ValueList = new List<double>();
            m_LineItemData.Add(lineItemData);
        }

        public void SetXScale(int min, int max)
        {
            m_XScaleMin = min;
            m_XScaleMax = max;
        }
        public void SetYScale(int min, int max)
        {
            m_YScaleMin = min;
            m_YScaleMax = max;
        }
        public void SetUnitToMs(int value)
        {
            m_UnitToMs = value;
        }

        public void SetStep(double minor, double major)
        {
            m_MinorStep = minor;
            m_MajorStep = major;
        }
        public void SetSampleTime(double sampleRateTime, double storageTime)
        {
            m_SampleTime = sampleRateTime;
            m_StorageTime = storageTime;
        }
        public void SetTitle(string title, string xAxisTitle, string yAxisTitle)
        {
            GraphPane.Title.Text = title;
            GraphPane.XAxis.Title.Text = xAxisTitle;
            GraphPane.Title.FontSpec.Family = "SimSun";
            GraphPane.Title.FontSpec.Size = 24f;
            GraphPane.YAxis.Title.Text = yAxisTitle;
        }

        public virtual void Initial()
        {
            // Save 1200 points.  At 50 ms sample rate, this is one minute
            // The RollingPointPairList is an efficient storage class that always
            // keeps a rolling set of point data without needing to shift any data values
            int capcity = Convert.ToInt32(m_StorageTime * m_UnitToMs / m_SampleTime + 10);
            if (GraphPane.CurveList != null)
            {
                GraphPane.CurveList.Clear();
            }

            // Initially, a curve is added with no data points (list is empty)
            // Color is blue, and there will be no symbols
            foreach (LineItemData itemData in m_LineItemData)
            {
                RollingPointPairList list = new RollingPointPairList(capcity);
                LineItem curve = GraphPane.AddCurve(itemData.Name, list, itemData.Color, itemData.SymbolType);
                //// Fill the symbols with white
                curve.Symbol.Fill = new Fill(Color.Blue);
                
                curve.Label.FontSpec = new FontSpec("SimSun", 18f, itemData.Color, false, false, false);
                // Associate this curve with the Y2 axis
                curve.IsY2Axis = false;
                if(itemData.ValueList !=null)
                {
                    itemData.ValueList.Clear();
                }
            }

            // Just manually control the X axis range so it scrolls continuously
            // instead of discrete step-sized jumps
            GraphPane.XAxis.Scale.Min = m_XScaleMin;
            GraphPane.XAxis.Scale.Max = m_XScaleMax;
            GraphPane.XAxis.Scale.MinorStep = m_MinorStep;
            GraphPane.XAxis.Scale.MajorStep = m_MajorStep;

            GraphPane.YAxis.Scale.Min = m_YScaleMin;
            GraphPane.YAxis.Scale.Max = m_YScaleMax;
            //GraphPane.XAxis.MajorGrid.IsVisible = IsShowGrid;
            //GraphPane.XAxis.MinorGrid.IsVisible = IsShowGrid;
            //GraphPane.XAxis.MajorGrid.DashOff = 0.0f;
            //GraphPane.YAxis.MajorGrid.IsVisible = IsShowGrid;
            //GraphPane.YAxis.MajorGrid.DashOff = 0.0f;
            //GraphPane.YAxis.MinorGrid.IsVisible = IsShowGrid;
            // Scale the axes
            this.AxisChange();
            // Force a redraw
            Invalidate();
            
        }
        public void InitialCurveList(string dataName)
        {
            if (GraphPane.CurveList.Count <= 0)
            {
                Initial();

            }

            LineItem curve = GraphPane.CurveList[dataName] as LineItem;
            if (curve != null)
            {

                LineItemData item = m_LineItemData.Find(a => { return a.Name == dataName; });

                item.ValueList.Clear();

                

                IPointListEdit list = curve.Points as IPointListEdit;
                if (list != null)
                {
                    list.Clear();
                }
                // Make sure the Y axis is rescaled to accommodate actual data
                AxisChange();
                // Force a redraw
                Invalidate();
            }

        }

        public void DrawPannelYLine(double x)
        {
            RollingPointPairList list = new RollingPointPairList(2);
             LineItem curve = GraphPane.AddCurve( "", list, Color.Red, SymbolType.None);
             curve.AddPoint(x, m_YScaleMin);
             curve.AddPoint(x, m_YScaleMax);
             // Force a redraw
             Invalidate();
        }

        public void DrawPannelYLine(double x, Color color)
        {
            RollingPointPairList list = new RollingPointPairList(2);
            LineItem curve = GraphPane.AddCurve("", list, color, SymbolType.None);
            curve.AddPoint(x, m_YScaleMin);
            curve.AddPoint(x, m_YScaleMax);
            // Force a redraw
            Invalidate();
        }

        public void DrawPannelXLine(double y)
        {
            RollingPointPairList list = new RollingPointPairList(2);
            LineItem curve = GraphPane.AddCurve("", list, Color.Red, SymbolType.None);
            curve.AddPoint( m_XScaleMin,y);
            curve.AddPoint( m_XScaleMax,y);
            // Force a redraw
            Invalidate();
        }

        public void DrawPannelXLine(double y, Color color)
        {
            RollingPointPairList list = new RollingPointPairList(2);
            LineItem curve = GraphPane.AddCurve("", list, color, SymbolType.None);
            curve.AddPoint(m_XScaleMin, y);
            curve.AddPoint(m_XScaleMax, y);
            // Force a redraw
            Invalidate();
        }

        public void InitalCurveLists()
        {
            if (GraphPane.CurveList.Count <= 0)
            {
                Initial();

            }

            foreach (LineItem curve in GraphPane.CurveList)
            {
                if (curve != null)
                {

                    LineItemData item = m_LineItemData.Find(a => { return a.Name == curve.Label.Text; });
                    if(item != null)
                    {
                        item.ValueList.Clear();
                    }
                    IPointListEdit list = curve.Points as IPointListEdit;
                    if (list != null)
                    {
                        list.Clear();
                    }
                }
            }
            // Make sure the Y axis is rescaled to accommodate actual data
            AxisChange();
            // Force a redraw
            Invalidate();

        }

     

        public void StartTimer()
        {
            StopTimer();

            m_Timer.Tick += new EventHandler(Timer_Tick);
            m_Timer.Interval = 50;
            m_Timer.Enabled = true;
            m_Timer.Start();
            tickStart = Environment.TickCount;
        }

        private void StopTimer()
        {
            if (m_Timer.Enabled)
            {
                m_Timer.Enabled = false;
                m_Timer.Stop();
                m_Timer.Tick -= new EventHandler(Timer_Tick);
            }
        }


        public void AddData(string dataName, IList<double> valueList)
        {
            
            if (GraphPane.CurveList.Count <= 0)
            {
                Initial();

            }
            

            LineItem curve = GraphPane.CurveList[dataName] as LineItem;
            if (curve != null)
            {
                
                LineItemData item = m_LineItemData.Find(a =>{return a.Name==dataName;});
                if (valueList != null)
                {
                    item.ValueList.AddRange(valueList);
                }

                IPointListEdit list = curve.Points as IPointListEdit;
                if (list != null)
                {
                    list.Clear();
                    for (int j = 0; j < item.ValueList.Count; j++)
                    {
                        list.Add(list.Count * m_SampleTime / m_UnitToMs, item.ValueList[j]);
                    }
                }
                // Make sure the Y axis is rescaled to accommodate actual data
                AxisChange();
                // Force a redraw
                Invalidate();
            }
           
            
        }
        public void AddYData(string dataName, IList<double> valueList)
        {

            if (GraphPane.CurveList.Count <= 0)
            {
                Initial();

            }


            LineItem curve = GraphPane.CurveList[dataName] as LineItem;
            if (curve != null)
            {

                LineItemData item = m_LineItemData.Find(a => { return a.Name == dataName; });
                if (valueList != null)
                {
                    item.ValueList.AddRange(valueList);
                }

                IPointListEdit list = curve.Points as IPointListEdit;
                if (list != null)
                {
                    list.Clear();
                    for (int j = 0; j < item.ValueList.Count; j++)
                    {
                        list.Add(5, item.ValueList[j]);
                    }
                }
                // Make sure the Y axis is rescaled to accommodate actual data
                AxisChange();
                // Force a redraw
                Invalidate();
            }


        }
        private GraphPane CopyGraphPane
        {
            get;
            set;

        }

        void Timer_Tick(object sender, EventArgs e)
        {
            
            if (GraphPane.CurveList.Count <= 0)
                return;
            double time = 0;
            //now time
            int currentTime = Environment.TickCount;
            // Time is measured in seconds
            time = (currentTime - tickStart) / 1000.0;
            for (int i = 0; i < m_LineItemData.Count; i++)
            {
                // Get the CurveItem in the graph
                LineItem curve = GraphPane.CurveList[i] as LineItem;
                if (curve == null)
                {
                    continue;
                }

                // Get the PointPairList
                IPointListEdit list = curve.Points as IPointListEdit;
                if (list == null)
                {
                    continue;
                }
                List<double> valueList = new List<double>();

                if (m_LineItemData[i].fun != null)
                {
                    valueList = m_LineItemData[i].fun();
                }

                double timeEvery = time / valueList.Count;

                for (int j = 0; j < valueList.Count; j++)
                {
                    list.Add(tickStart + (j + 1) * timeEvery, valueList[j]);
                }
            }
            tickStart = currentTime;

            // Keep the X scale at a rolling 30 second interval, with one
            // major step between the max X value and the end of the axis
            Scale xScale = GraphPane.XAxis.Scale;
            if (time > xScale.Max - xScale.MajorStep)
            {
                xScale.Max = time + xScale.MajorStep;
                xScale.Min = xScale.Max - m_XScaleMax;
            }
            
            // Make sure the Y axis is rescaled to accommodate actual data
            AxisChange();
            // Force a redraw
            Invalidate();
            
        }

    }




    public class LineItemData
    {
        public string Name
        {
            get;
            set;
        }
        public Color Color
        {
            get;
            set;
        }

        public SymbolType SymbolType
        {
            get;
            set;
        }
        public delegate List<double> GetYValue();
        public GetYValue fun
        {
            get;
            set;
        }

        public List<double> ValueList
        {
            get;
            set;
        }


    }
}
