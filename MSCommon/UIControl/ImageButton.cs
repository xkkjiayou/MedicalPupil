using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.CompilerServices;

namespace MedicalSys.MSCommon
{

    public partial class ImageButton : UserControl
    {
        private Image p1;
        private Image p2;
        private Image p3;

        public delegate void ClickEventHandler(object sender, EventArgs e);
        public new event ClickEventHandler Click;

        public ImageButton()
        {
            //base.MouseLeave += new EventHandler(this.ImageButton_MouseLeave);
            //base.Resize += new EventHandler(this.ImageButton_Resize);
            //base.Load += new EventHandler(this.ImageButton_Load);
            //base.MouseEnter += new EventHandler(this.ImageButton_MouseEnter);
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        [Category("重要属性")]
        [Description("按钮默认显示的图片")]
        public Image ButtonImage
        {
            get
            {
                return this.pbImage.Image;
            }
            set
            {
                this.pbImage.Image = value;
                this.p1 = value;
            }
        }
        [Category("重要属性")]
        [Description("按钮中要显示的文本")]
        public string ButtonText
        {
            get
            {
                return this.lblTemp.Text;
            }
            set
            {
                this.lblTemp.Text = value;
            }
        }

        [Category("重要属性")]
        [Description("单击按钮时候显示的图片")]
        public Image MouseClickImage
        {
            get
            {
                return this.p3;
            }
            set
            {
                this.p3 = value;
            }
        }

        [Category("重要属性")]
        [Description("鼠标移动到按钮上时显示的图片")]
        public Image MouseOverImage
        {
            get
            {
                return this.p2;
            }
            set
            {
                this.p2 = value;
            }
        }
        

        public void ImageButton_MouseEnter(object sender, EventArgs e)
        {
            this.pbImage.Image = this.p2;
        }

        public void ImageButton_MouseLeave(object sender, EventArgs e)
        {
            this.pbImage.Image = this.p1;
        }

        private void ImageButton_Resize(object sender, EventArgs e)
        {
            this.lblTemp.Left = (int)Math.Round((double)((((double)this.pbImage.Width) / 2.0) - (((double)this.lblTemp.Width) / 2.0)));
            this.lblTemp.Top = (int)Math.Round((double)((((double)this.pbImage.Height) / 2.0) - (((double)this.lblTemp.Height) / 2.0)));
        }

        private void ImageButton_Load(object sender, EventArgs e)
        {
            this.lblTemp.Parent = this.pbImage;
            this.lblTemp.Width = this.pbImage.Width;
            this.pbImage.Image = this.p1;
            this.lblTemp.Left = (int)Math.Round((double)((((double)this.pbImage.Width) / 2.0) - (((double)this.lblTemp.Width) / 2.0)));
            this.lblTemp.Top = (int)Math.Round((double)((((double)this.pbImage.Height) / 2.0) - (((double)this.lblTemp.Height) / 2.0)));
        }


        //Lable事件
        private void lblTemp_Click(object sender, EventArgs e)
        {
            ClickEventHandler clickEvent = this.Click;
            if (clickEvent != null)
            {
                clickEvent(this, e);
            }
        }

        private void lblTemp_MouseDown(object sender, MouseEventArgs e)
        {
            this.pbImage.Image = this.p3;
        }

        private void lblTemp_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            this.pbImage.Image = this.p2;
        }

        private void lblTemp_MouseHover(object sender, EventArgs e)
        {
            this.pbImage.Image = this.p2;
        }

        private void lblTemp_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            this.pbImage.Image = this.p1;
        }

        private void lblTemp_MouseUp(object sender, MouseEventArgs e)
        {
            this.pbImage.Image = this.p1;
        }

        private void lblTemp_TextChanged(object sender, EventArgs e)
        {
            this.lblTemp.Parent = this.pbImage;
            this.lblTemp.Width = this.pbImage.Width;
            this.lblTemp.Left = (int)Math.Round((double)((((double)this.pbImage.Width) / 2.0) - (((double)this.lblTemp.Width) / 2.0)));
            this.lblTemp.Top = (int)Math.Round((double)((((double)this.pbImage.Height) / 2.0) - (((double)this.lblTemp.Height) / 2.0)));
        }

        //PictureBox事件
        private void pbImage_BackgroundImageChanged(object sender, EventArgs e)
        {
            this.pbImage.Refresh();
        }

        private void pbImage_Click(object sender, EventArgs e)
        {
            ClickEventHandler clickEvent = this.Click;
            if (clickEvent != null)
            {
                clickEvent(this, e);
            }
        }

        private void pbImage_MouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            this.pbImage.Image = this.p3;
        }

        private void pbImage_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            this.pbImage.Image = this.p2;
        }

        private void pbImage_MouseHover(object sender, EventArgs e)
        {
            this.pbImage.Image = this.p2;
        }

        private void pbImage_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            this.pbImage.Image = this.p1;
        }

        private void pbImage_MouseUp(object sender, MouseEventArgs e)
        {
            this.pbImage.Image = this.p1;
        }
    }
}