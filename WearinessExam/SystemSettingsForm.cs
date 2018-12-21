using MedicalSys.Framework;
using MedicalSys.MSCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WearinessExam
{
    public partial class SystemSettingsForm : MedicalSys.MSCommon.BaseForm
    {

        #region 变量

        /// <summary>
        /// PrintDocument
        /// </summary>
        private PrintDocument printDoc = new PrintDocument();

        /// <summary>
        /// Logger
        /// </summary>
        private IMSLogger m_Logger = LogFactory.GetLogger();

        /// <summary>
        /// 初始值数组
        /// </summary>
        private string[] OriginalValue;

        /// <summary>
        /// The save status
        /// </summary>
        private bool m_SaveStatus = false;

        /// <summary>
        /// 打印机状态
        /// </summary>
        private enum PrinterStatus
        {
            其他状态 = 1,
            未知,
            空闲,
            正在打印,
            预热,
            停止打印,
            打印中,
            离线
        }
        #endregion 变量

        public SystemSettingsForm()
        {
            InitializeComponent();

            InitialControls();

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

        #region 私有方法
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitialControls()
        {
            // 缓存初始值
            OriginalValue = new string[]
            {
                IniSettingConfig.GetInstance().PrinterName,
                IniSettingConfig.GetInstance().PrinterRegion
            };

            // 初始化打印机列表
            PopulateInstalledPrinters();
            this.cmbInstalledPrinters.Text = OriginalValue[0];

            try
            {
                GetPrinterState(this.cmbInstalledPrinters.Text);
            }
            catch (Exception ex)
            {
                m_Logger.Error(ex.StackTrace);
            }

            // 初始化打印范围RadioButton
            this.radReportAndList.Checked = (OriginalValue[1] == "0");
            this.radList.Checked = (OriginalValue[1] == "1");

        }

        /// <summary>
        /// 向ComboBox添加系统已安装的打印机
        /// </summary>
        private void PopulateInstalledPrinters()
        {
            // Add list of installed printers found to the combo box.
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                cmbInstalledPrinters.Items.Add(PrinterSettings.InstalledPrinters[i]);
            }
        }

        /// <summary>
        /// 获取打印机的状态
        /// </summary>
        /// <param name="PrinterName">打印机名称</param>
        private void GetPrinterState(string PrinterName)
        {
            // 获取打印机的所有属性
            string path = @"win32_printer.DeviceId='" + PrinterName + "'";
            ManagementObject printer = new ManagementObject(path);
            printer.Get();
            // 打印机状态
            PrinterStatus retStatus = (PrinterStatus)Convert.ToInt32(printer.Properties["PrinterStatus"].Value);
            this.lblStatus.Text = retStatus.ToString();
            // 打印机位置
            string strPortName = Convert.ToString(printer.Properties["PortName"].Value);
            this.lblLocation.Text = strPortName;
            // 打印机类型
            string strDriverName = Convert.ToString(printer.Properties["DriverName"].Value);
            this.lblType.Text = strDriverName;
            // 打印机备注
            string strComment = Convert.ToString(printer.Properties["Comment"].Value);
            this.lblDescription.Text = strComment;
        }

        /// <summary>
        /// 打开打印机配置对话框
        /// </summary>
        /// <param name="printerSettings">PrinterSettings</param>
        private void OpenPrinterPropertiesDialog(PrinterSettings printerSettings)
        {
            IntPtr hDevMode = printerSettings.GetHdevmode(printerSettings.DefaultPageSettings);
            IntPtr pDevMode = GlobalLock(hDevMode);
            int sizeNeeded = DocumentProperties(this.Handle, IntPtr.Zero, printerSettings.PrinterName, pDevMode, pDevMode, 0);
            IntPtr devModeData = Marshal.AllocHGlobal(sizeNeeded);
            // 打开打印机配置对话框
            DocumentProperties(this.Handle, IntPtr.Zero, printerSettings.PrinterName, devModeData, pDevMode, 14);
            GlobalUnlock(hDevMode);
            printerSettings.SetHdevmode(devModeData);
            printerSettings.DefaultPageSettings.SetHdevmode(devModeData);
            GlobalFree(hDevMode);
            Marshal.FreeHGlobal(devModeData);
        }

        /// <summary>
        /// Determines whether the specified user is modified.
        /// </summary>
        /// <param name="patient">The BaseValue.</param>
        /// <returns><c>true</c> if the BaseValue is modified; otherwise, <c>false</c>.</returns>
        private bool IsModified()
        {
            bool result = false;

            result = !OriginalValue[0].Equals(this.cmbInstalledPrinters.Text)
               || !OriginalValue[1].Equals(this.radReportAndList.Checked ? "0" : "1");

            return result;
        }

        #endregion 私有方法

        #region 事件

        /// <summary>
        /// 属性按钮点击事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void btnProperty_Click(object sender, EventArgs e)
        {
            string strPrinterName = this.cmbInstalledPrinters.Text;

            if (strPrinterName != null && strPrinterName.Length > 0)
            {
                IntPtr pPrinter = IntPtr.Zero;
                PRINTER_DEFAULTS pDefault = new PRINTER_DEFAULTS()
                {
                    pDatatype = IntPtr.Zero,
                    pDevMode = IntPtr.Zero,
                    DesiredAccess = PRINTER_ALL_ACCESS
                };
                // 调用PrinterProperties前先调用OpenPrinter
                int retVal = OpenPrinter(strPrinterName, out pPrinter, ref pDefault);
                if (retVal == 0)
                {
                    string strErrorMessage = string.Format(ConstMessage.MESSAGE_M1301, strPrinterName);
                    MessageBox.Show(this, strErrorMessage, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    retVal = PrinterProperties(this.Handle, pPrinter);
                    retVal = ClosePrinter(pPrinter);
                }
            }
        }

        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (IsModified())
            {
                // 修改配置文件
                IniSettingConfig.GetInstance().PrinterName = this.cmbInstalledPrinters.Text;
                IniSettingConfig.GetInstance().PrinterRegion = this.radList.Checked ? "1" : "0";

                // 设置保存状态
                m_SaveStatus = true;

                this.Close();
            }
            else
            {
                // 设置保存状态
                m_SaveStatus = true;
                this.Close();
            }
        }

        /// <summary>
        /// 窗体Closing事件
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the event data.</param>
        private void SystemSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_SaveStatus)
            {
                this.DialogResult = DialogResult.OK;
            }
            else if (IsModified() && MessageBox.Show(this, CommonMessage.M0006, string.Empty,
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }
        #endregion 事件

        #region Win32 API

        private const int PRINTER_ACCESS_ADMINISTER = 0x4;
        private const int PRINTER_ACCESS_USE = 0x8;
        private const int STANDARD_RIGHTS_REQUIRED = 0xF0000;
        private const int PRINTER_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | PRINTER_ACCESS_ADMINISTER | PRINTER_ACCESS_USE);

        [StructLayout(LayoutKind.Sequential)]
        private struct PRINTER_DEFAULTS
        {
            public IntPtr pDatatype;
            public IntPtr pDevMode;
            public int DesiredAccess;
        }

        // 显示打印机属性对话框
        [DllImportAttribute("winspool.drv", SetLastError = true)]
        static extern int PrinterProperties(
            IntPtr hwnd,  // handle to parent window
            IntPtr hPrinter); // handle to printer object

        // 打开打印机
        [DllImportAttribute("winspool.drv", SetLastError = true)]
        extern static int OpenPrinter(
            string pPrinterName,   // printer name
            out IntPtr hPrinter,      // handle to printer object
            ref PRINTER_DEFAULTS pDefault);    // handle to default printer object. 

        // 关闭打印机
        [DllImportAttribute("winspool.drv", SetLastError = true)]
        static extern int ClosePrinter(
            IntPtr phPrinter); // handle to printer object

        // 显示打印机配置对话框
        [DllImport("winspool.Drv", EntryPoint = "DocumentPropertiesW", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        static extern int DocumentProperties(IntPtr hwnd, IntPtr hPrinter, [MarshalAs(UnmanagedType.LPWStr)] string pDeviceName, IntPtr pDevModeOutput, IntPtr pDevModeInput, int fMode);

        // Locks a global memory object and returns a pointer to the first byte of the object's memory block.
        [DllImport("kernel32.dll")]
        static extern IntPtr GlobalLock(IntPtr hMem);

        // Unlocks the specified global memory object.
        [DllImport("kernel32.dll")]
        static extern IntPtr GlobalUnlock(IntPtr hMem);

        // Frees the specified global memory object and invalidates its handle.
        [DllImport("kernel32.dll")]
        static extern IntPtr GlobalFree(IntPtr hMem);

        #endregion Win32 API

        private void cmbInstalledPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetPrinterState(this.cmbInstalledPrinters.Text);
        }


    }
}
