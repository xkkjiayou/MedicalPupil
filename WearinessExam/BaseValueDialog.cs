using MedicalSys.DataAccessor;
using MedicalSys.MSCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WearinessExam.DAO;
using WearinessExam.DO;
using WearinessExam.Utility;

namespace WearinessExam
{
    public partial class BaseValueDialog : MedicalSys.MSCommon.BaseForm
    {

        #region 变量
        /// <summary>
        /// 受测员记录的主键
        /// </summary>
        public int PatientKey { get; set; }

        /// <summary>
        /// 初始的基础值
        /// </summary>
        private BaseValue OriginalBaseValue { get; set; }

        /// <summary>
        /// 系统默认的基础值
        /// </summary>
        private BaseValue DefaultBaseValue { get; set; }

        /// <summary>
        /// 检测值列表（平均值作为基础值）
        /// </summary>
        public IList<BaseValue> BaseValueList { get; set; }

        /// <summary>
        /// The new mode flag
        /// </summary>
        private bool m_IsNewMode = false;

        /// <summary>
        /// The save status
        /// </summary>
        private bool m_SaveStatus = false;

        /// <summary>
        /// The base value DAO
        /// </summary>
        private BaseValueDAO m_BaseValueDao = new BaseValueDAO();

        #endregion  变量

        public BaseValueDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// BaseValueDialog
        /// </summary>
        /// <param name="patientKey">PatientKey</param>
        public BaseValueDialog(int patientKey)
        {
            InitializeComponent();

            this.PatientKey = patientKey;
            // 查找受测员信息
            Patient patient = FindPatient(patientKey);

            // 根据查找到的受测员信息更新画面
            if (patient != null)
            {
                this.lblId.Text = patient.ID;
                this.lblName.Text = patient.Name;
                this.lblSex.Text = patient.Sex == 2 ? "女" : "男";
                this.lblBirthday.Text = patient.Birth.ToString("yyyy年MM月dd日");
                this.lblCompany.Text = patient.Unit;
            }

            // 查找受测员的基础值
            OriginalBaseValue = FindBaseValue(patientKey);
            if (OriginalBaseValue != null)
            {
                // 更新画面的基础值信息
                this.dblInputCFF.Text = OriginalBaseValue.CFF.ToString();
            }
            else
            {
                m_IsNewMode = true;
                // 从配置文件取得默认的基础值
                DefaultBaseValue = BaseValueHelper.GetDefaultBaseValue();
                // 保存一份原始值
                OriginalBaseValue = new BaseValue();
                // 更新画面的基础值信息
                this.dblInputCFF.Text = DefaultBaseValue.CFF.ToString();
                OriginalBaseValue.CFF = DefaultBaseValue.CFF;
            }

            this.dblInputCFF.KeyPress += Txt_Decimal_KeyPress;
        }


        #region 私有方法
        /// <summary>
        /// Gets the BaseValue info.
        /// </summary>
        /// <param name="patientID">The BaseValue ID.</param>
        /// <returns>BaseValue.</returns>
        private BaseValue FindBaseValue(int patientKey)
        {
            // 根据PatientKey，到Patient表里获取BaseValue信息
            BaseValue baseValue;
            bool success = true;
            success = DataAccessProxy.Execute<BaseValue>(() => { return m_BaseValueDao.GetBaseValue(patientKey); }, this, out baseValue);
            return baseValue;
        }

        /// <summary>
        /// Gets the patient info.
        /// </summary>
        /// <param name="patientID">The patient ID.</param>
        /// <returns>Patient.</returns>
        private Patient FindPatient(int patientKey)
        {
            // 根据PatientID，到Patient表里获取Patient信息
            Patient patient = new Patient();
            PatientDAO patientDao = new PatientDAO();
            List<Patient> patientList = null;
            bool success = true;
            success = DataAccessProxy.Execute<Patient>(() => { return patientDao.GetPatient(patientKey); }, this, out patientList);
            if (success && patientList != null && patientList.Count != 0)
            {
                patient = patientList[0];
            }
            return patient;
        }


        /// <summary>
        /// Gets the BaseValue data.
        /// </summary>
        /// <returns>BaseValue.</returns>
        private BaseValue GetBaseValueData()
        {
            double d = 0d;
            double.TryParse(this.dblInputCFF.Text, out d);

            // 取得画面上的受测员信息
            BaseValue baseValue = new BaseValue()
            {
                primaryKey = (m_IsNewMode ? 0 : OriginalBaseValue.primaryKey),
                Patient_key = this.PatientKey,
                USER_KEY = LoginInfoManager.CurrentUser.primaryKey,
                UPDATE_DATE_TIME = System.DateTime.Now,
                CFF = d,
            };
            return baseValue;
        }

        /// <summary>
        /// Determines whether the specified user is modified.
        /// </summary>
        /// <param name="patient">The BaseValue.</param>
        /// <returns><c>true</c> if the BaseValue is modified; otherwise, <c>false</c>.</returns>
        private bool IsModified(BaseValue baseValue)
        {
            bool result = false;

            result = !baseValue.CFF.Equals(OriginalBaseValue.CFF);

            return result;
        }

        /// <summary>
        /// Verifies the input.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        private bool VerifyInput()
        {
            bool result = false;

            result = !string.IsNullOrWhiteSpace(dblInputCFF.Text);
            if (!result)
            {
                dblInputCFF.Focus();
            }

            if (!result)
            {
                // 画面输入项目有任何一项为空时，弹出对话框。
                MessageBox.Show(this, CommonMessage.M0005, string.Empty,
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            return result;
        }
        #endregion 私有方法

        #region 事件
        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            BaseValue baseValue = GetBaseValueData();
            if (!m_IsNewMode)
            {
                // 没有修改直接退出
                if (!IsModified(baseValue))
                {
                    this.Close();
                    return;
                }
            }

            // 检验输入值
            if (VerifyInput())
            {
                bool success = true;

                if (m_IsNewMode)
                {
                    // 原来没有基础值记录，插入记录
                    success = DataAccessProxy.Execute(() => { m_BaseValueDao.Insert(baseValue); }, this);
                }
                else
                {
                    // 已经有基础值记录，更新记录
                    success = DataAccessProxy.Execute(() => { m_BaseValueDao.Update(baseValue); }, this);
                }

                if (success)
                {
                    m_SaveStatus = true;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// 默认值按钮点击事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void btnSetDefault_Click(object sender, EventArgs e)
        {
            if (DefaultBaseValue == null)
            {
                DefaultBaseValue = BaseValueHelper.GetDefaultBaseValue();
            }
            // 更新画面的基础值为默认值
            this.dblInputCFF.Text = DefaultBaseValue.CFF.ToString();
        }

        /// <summary>
        /// 平均值按钮点击事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void btnSetAverage_Click(object sender, EventArgs e)
        {
            // 如果检测值列表为空，则点击按钮后返回
            if (BaseValueList == null)
            {
                return;
            }

            int count = BaseValueList.Count;
            if (count <= 0)
            {
                return;
            }

            // 定义变量并初始化
            double totalCFF = 0d;

            // 分别求和
            for (int i = 0; i < count; i++)
            {
                totalCFF += BaseValueList[i].CFF;
            }

            // 分别求平均值，并更新画面
            this.dblInputCFF.Text = (totalCFF / count).ToString();
        }

        /// <summary>
        /// 窗体Closing事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void BaseValueDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_SaveStatus)
            {
                this.DialogResult = DialogResult.OK;
            }
            else if (IsModified(GetBaseValueData()) && MessageBox.Show(this, ConstMessage.MESSAGE_M1203, string.Empty,
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                // 数据有修改，用户选择No，取消关闭窗体
                e.Cancel = true;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        //// <summary>
        /// 控制只能输入整数或小数
        /// (小数位最多位4位，小数位可以自己修改)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txt_Decimal_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!(((e.KeyChar >= '0') && (e.KeyChar <= '9')) || e.KeyChar <= 31))
            {
                if (e.KeyChar == '.')
                {
                    if (((TextBox)sender).Text.Trim().IndexOf('.') > -1)
                        e.Handled = true;
                }
                else
                    e.Handled = true;
            }
            else
            {
                if (e.KeyChar <= 31)
                {
                    e.Handled = false;
                }
                else if (((TextBox)sender).Text.Trim().IndexOf('.') > -1)
                {
                    if (((TextBox)sender).Text.Trim().Substring(((TextBox)sender).Text.Trim().IndexOf('.') + 1).Length >= 2)
                        e.Handled = true;
                }
            }
        }
        #endregion 事件
    }
}
