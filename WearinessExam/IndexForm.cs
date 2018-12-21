using MedicalSys.Framework;
using MedicalSys.MSCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WearinessExam
{
    public partial class IndexForm : Form
    {
        private IMSLogger logger = LogFactory.GetLogger();

        public IndexForm()
        {
            InitializeComponent();

            if(LoginInfoManager.IsChecked)
            {
                this.imageButton1.Visible = false;
            }
            // 获取屏幕大小
            Rectangle rect = Screen.GetWorkingArea(this);
            panel1.Location = new Point((rect.Width - panel1.Width) / 2, (rect.Height - panel1.Height) / 2 + 100);
            btnExit.Location = new Point(rect.Width - btnExit.Width - 150, rect.Height - 200);
            //label1.Location = new Point(150, rect.Height - 100);
            this.Shown += IndexForm_Shown;
        }

        void IndexForm_Shown(object sender, EventArgs e)
        {
            //string result = "";
            //WearinessExam.Examination.DeviceFacade.DeviceSelfCheck(ref result);
            //label1.Text = result;

            DeviceCheck();
        }


        /// <summary>
        /// Devices the check.
        /// </summary>
        private void DeviceCheck()
        {
            if (!LoginInfoManager.IsChecked)
            {
                SelfCheckWindow checkWindow = new SelfCheckWindow();
                if (checkWindow.ShowDialog(this) != System.Windows.Forms.DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams paras = base.CreateParams;
                paras.ExStyle |= 0x02000000;
                return paras;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void imageButton1_Click(object sender, EventArgs e)
        {
            WearinessDetectForm wearinessDetectForm = new WearinessDetectForm();
            wearinessDetectForm.ShowDialog(this);
        }

        private void imageButton5_Click(object sender, EventArgs e)
        {
            SystemSettingsForm settingsForm = new SystemSettingsForm();
            settingsForm.ShowDialog(this);
        }

        private void imageButton3_Click(object sender, EventArgs e)
        {
            PatientMgmtForm patientMgmtForm = new PatientMgmtForm();
            patientMgmtForm.Init();
            logger.Info("访问受测员信息管理画面.");
            patientMgmtForm.ShowDialog(this);
        }

        private void imageButton4_Click(object sender, EventArgs e)
        {
            UserMgmtForm userMgmtForm = new UserMgmtForm();
            userMgmtForm.Initial();
            logger.Info("访问用户信息管理画面.");
            userMgmtForm.ShowDialog(this);
        }

        private void imageButton7_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog(this);
        }

        private void imageButton2_Click(object sender, EventArgs e)
        {
            ExamInfoWindow examInfoWindow = new ExamInfoWindow();
            examInfoWindow.ShowDialog(this);
        }

        private void imageButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
