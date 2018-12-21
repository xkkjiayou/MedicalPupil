using MedicalSys.DataAccessor;
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
    public partial class LoginForm : Form
    {
        private IMSLogger logger = LogFactory.GetLogger();

        public LoginForm()
        {
            InitializeComponent();
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //取得输入的登录名和密码
            string userId = this.textBox1.Text.Trim().ToLower();
            string password = this.textBox2.Text;

            //登录名和密码未输入情况弹出错误信息
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show(this, CommonMessage.M0016, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //取得全部用户
            UserDAO dao = new UserDAO();
            List<User> userList;
            bool success = DataAccessProxy.Execute<User>(() => { return dao.GetAll(); }, this, out userList);
            if (success)
            {
                //登录名和密码验证
                foreach (var userItem in userList)
                {
                    if (userItem.LoginID.Equals(userId) && MD5CrypHelper.VerifyMd5Hash(password, userItem.Password)
                        && (userItem.Level != 3))
                    {
                        logger.Info(string.Format("用户:{0}登录系统.",userId));
                        LoginInfoManager.CurrentUser = (User)userItem;
                        //疲劳检测仪系统的情况，记录是否进入数据处理
                        LoginInfoManager.IsChecked = this.checkBox1.Checked;

                        this.textBox1.Text = string.Empty;
                        this.textBox2.Text = string.Empty;

                        IndexForm indexForm = new IndexForm();
                        indexForm.ShowDialog(this);
                        return;
                    }
                }

                //登录名和密码验证未通过时弹出错误信息
                MessageBox.Show(this, CommonMessage.M0004, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }

            if ((Keys)e.KeyChar == Keys.Escape)
            {
                btnExit_Click(sender, e);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

    }
}
