using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MedicalSys.MSCommon
{
    /// <summary>
    /// Description:自定义窗体样式，实现标题可自定义化
    /// </summary>
    public partial class BaseForm : Form
    {
        private bool moving = false;
        private Point oldMousePosition;

        public new FormBorderStyle FormBorderStyle
        {
            get
            {
                return base.FormBorderStyle;
            }
            set
            {
                base.FormBorderStyle = value;
            }
        }

        #region 隐藏父类的属性，使其不可见

        [Browsable(false)]
        public new string Text
        {
            get
            {
                return titlelabel.Text;
            }
            set
            { }
        }

        [Browsable(false)]
        public new bool ControlBox
        {
            get {
                return false;
            }
            set
            {
                base.ControlBox = false;
            }
        }

        #endregion

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("窗体标题")]
        public string Title
        {
            get { return titlelabel.Text; }
            set { titlelabel.Text = value; }
        }



        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("窗体标题字体样式")]
        public Font TitleFont
        {
            get
            {
                return titlelabel.Font;
            }
            set
            {
                titlelabel.Font = value;
            }
        }


        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("窗体标题字体颜色")]
        public Color TitleColor
        {
            get
            {
                return titlelabel.ForeColor;
            }
            set
            {
                titlelabel.ForeColor = value;
            }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("窗体标题栏背景色")]
        public Color TitleBarBackColor
        {
            get
            {
                return titlepanel.BackColor;
            }
            set
            {
                titlepanel.BackColor = value;
            }
        }


        private void ResetTitlePanel()
        {
            base.ControlBox = false;
            base.Text = null;
            SetToolTip(button1, "关闭");
        }

        private void SetToolTip(Control ctrl, string tip)
        {
            new ToolTip().SetToolTip(ctrl, tip);
        }
        public BaseForm()
        {
            InitializeComponent();
            ResetTitlePanel();
        }

        private void Titlebutton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            switch (btn.Tag.ToString())
            {
                case "close":
                    {
                        this.Close();
                        break;
                    }
                case "max":
                    {
                        if (this.WindowState == FormWindowState.Maximized)
                        {
                            this.WindowState = FormWindowState.Normal;
                        }
                        else
                        {
                            this.WindowState = FormWindowState.Maximized;
                        }
                        break;
                    }
                case "min":
                    {
                        if (this.WindowState != FormWindowState.Minimized)
                        {
                            this.WindowState = FormWindowState.Minimized;
                        }
                        break;
                    }
            }
        }

        private void Titlepanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                return;
            }
            //Titlepanel.Cursor = Cursors.NoMove2D;
            oldMousePosition = e.Location;
            moving = true;
        }

        private void Titlepanel_MouseUp(object sender, MouseEventArgs e)
        {
            //Titlepanel.Cursor = Cursors.Default;
            moving = false;
        }

        private void Titlepanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && moving)
            {
                Point newPosition = new Point(e.Location.X - oldMousePosition.X, e.Location.Y - oldMousePosition.Y);
                this.Location += new Size(newPosition);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
