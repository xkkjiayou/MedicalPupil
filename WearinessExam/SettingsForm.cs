using MedicalSys.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WearinessExam.Examination;

namespace WearinessExam
{
    public partial class SettingsForm : MedicalSys.MSCommon.BaseForm
    {
        public SettingsForm()
        {
            InitializeComponent();

            initData();
        }

        public string ExamScenario { get; set; }
        public string ExamSettings { get; set; }


        private void initData()
        {
            var config = IniSettingConfig.GetInstance();
            comboBox1.SelectedIndex = config.BrightPointColour - 1;
            comboBox2.SelectedIndex = config.BrightBlackRatio - 1;
            comboBox3.SelectedIndex = config.BrightPointIntensity;
            comboBox4.SelectedIndex = config.BackgroundIntensity;
            comboBox5.Text = config.ExamScenario;
            if (config.StimulateLightColour == 5)
            {
                comboBox6.SelectedIndex = 3;
            }
            else
            {
                comboBox6.SelectedIndex = config.StimulateLightColour - 1;
            }
            trackBar1.Value = config.StimulateTime;
            comboBox7.SelectedIndex = config.StimulateStrength;
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            var lightSetting = new LightSetting();
            lightSetting.colorLight = comboBox1.SelectedIndex + 1;
            lightSetting.Liangheibi = comboBox2.SelectedIndex + 1;
            lightSetting.LDLiangdu = comboBox3.SelectedIndex;
            lightSetting.BJLiangdu = comboBox4.SelectedIndex;
            if(comboBox6.SelectedIndex==3)
            {
                lightSetting.StimulateColor = 5;
            }
            else
            {
                lightSetting.StimulateColor = comboBox6.SelectedIndex + 1;
            }
            lightSetting.StimulateTime = trackBar1.Value;
            lightSetting.StimulateStrength = (int)comboBox7.SelectedIndex;

            ExamDataCenter.LightSetting = lightSetting;
            this.ExamScenario = comboBox5.Text;

            this.ExamSettings = string.Format("亮点:{0} 亮黑比:{1} 光强:{2} 背景光强:{3} 刺激光源:{4} 时长:{5}ms 强度:{6}", 
                comboBox1.Text, comboBox2.Text, comboBox3.Text, comboBox4.Text, comboBox6.Text, trackBar1.Value, comboBox7.Text);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            this.label10.Text = trackBar1.Value.ToString();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                trackBar1.Value = 200;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                trackBar1.Value = 400;
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                trackBar1.Value = 2000;
            }
        }
    }
}
