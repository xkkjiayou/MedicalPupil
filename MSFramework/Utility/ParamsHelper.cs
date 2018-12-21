using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalSys.Framework
{
    public class ParamsHelper
    {
        /// <summary>
        /// 获取亮点颜色
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string getColour(int value)
        {
            string colourName;
            // 亮点颜色：1~5：红、绿、蓝、黄、白   0：关闭
            switch (value)
            {
                case 0:
                    colourName = "关闭";
                    break;
                case 1:
                    colourName = "红";
                    break;
                case 2:
                    colourName = "绿";
                    break;
                case 3:
                    colourName = "蓝";
                    break;
                case 4:
                    colourName = "黄";
                    break;
                case 5:
                    colourName = "白";
                    break;
                default:
                    colourName = "红";
                    break;
            }

            return colourName;
        }

        public string getBrightBlackRatio(int value)
        {
            string brightBlackRatio;
            // 亮黑比: 1~3: 1:3，1:1，3:1  0：关闭

            switch (value)
            {
                case 0:
                    brightBlackRatio = "关闭";
                    break;
                case 1:
                    brightBlackRatio = "1:3";
                    break;
                case 2:
                    brightBlackRatio = "1:1";
                    break;
                case 3:
                    brightBlackRatio = "3:1";
                    break;
                default:
                    brightBlackRatio = "1:3";
                    break;
            }

            return brightBlackRatio;
        }

        public string getBrightPointIntensity(int value)
        {
            string brightPointIntensity;
            // 亮点光亮度：0~6：1，1/2，1/4，1/8，1/16，1/32，1/64，    7：关闭

            switch (value)
            {
                case 0:
                    brightPointIntensity = "1";
                    break;
                case 1:
                    brightPointIntensity = "1/2";
                    break;
                case 2:
                    brightPointIntensity = "1/4";
                    break;
                case 3:
                    brightPointIntensity = "1/8";
                    break;
                case 4:
                    brightPointIntensity = "1/16";
                    break;
                case 5:
                    brightPointIntensity = "1/32";
                    break;
                case 6:
                    brightPointIntensity = "1/64";
                    break;
                case 7:
                    brightPointIntensity = "关闭";
                    break;
                default:
                    brightPointIntensity = "1";
                    break;
            }

            return brightPointIntensity;
        }

        public string getBackgroundIntensity(int value)
        {
            string backgroundIntensity;
            // 背景光亮度：0~2：1，1/4，1/16；   3：全黑

            switch (value)
            {
                case 0:
                    backgroundIntensity = "1";
                    break;
                case 1:
                    backgroundIntensity = "1/4";
                    break;
                case 2:
                    backgroundIntensity = "1/16";
                    break;
                case 3:
                    backgroundIntensity = "全黑";
                    break;
                default:
                    backgroundIntensity = "1";
                    break;
            }

            return backgroundIntensity;
        }

        public string getStimulateLightColour(int value)
        {
            string stimulateLightColour;
            // 刺激光源颜色：1~4：白、红、绿、蓝

            switch (value)
            {
                case 1:
                    stimulateLightColour = "白";
                    break;
                case 2:
                    stimulateLightColour = "红";
                    break;
                case 3:
                    stimulateLightColour = "绿";
                    break;
                case 4:
                    stimulateLightColour = "蓝";
                    break;
                default:
                    stimulateLightColour = "白";
                    break;
            }

            return stimulateLightColour;
        }

    }
}
