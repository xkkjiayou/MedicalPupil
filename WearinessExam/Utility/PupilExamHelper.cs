using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MedicalSys.Framework;
using WearinessExam.Examination;
using WearinessExam.DO;

namespace WearinessExam.Utility
{
    public class PupilExamHelper
    {

        /// <summary>
        /// 瞳孔对光反应数据处理
        /// </summary>
        /// <param name="pdArray">瞳孔直径数组</param>
        /// <param name="pcvArrayReturn">瞳孔对光反应数据</param>
        /// <returns><c>true</c> if calculate success, <c>false</c> otherwise</returns>
        public static bool CalculatePupilsValue(double[] pdArray, out ExamInfo examInfo)
        {
            examInfo = null;
            if (pdArray == null || pdArray.Length < 1500)
            {
                return false;
            }

            int pointNum = 1500;

            double[] pointX = new double[pointNum];
            double[] pointY = new double[pointNum];
            double[] pointYTemp = new double[pointNum];

            for (int n = 0; n < pointNum; n++)
            {
                pointYTemp[n] = pdArray[n];
                pointY[n] = pointYTemp[n]; //mm
                pointX[n] = 2 / pointNum * n; //ms
            }


            double temp_data;
            int i = 0;
            int j = 0;
            int k = 0;
            double sum15 = 0;
            double housum15 = 0;
            //for (i = 0; i < 45; i++)
            //{
            //    sum15 = sum15 + pointYTemp[i];
            //    housum15 = housum15 + pointYTemp[1499-i];
            //}
            //for (i = 0; i < 45; i++)
            //{
            //    pointYTemp[i] = sum15 / 45.0;
            //    pointYTemp[1499 - i] = housum15 / 45.0;
            //}




            //for (i = 15; i < pointNum - 16; i++)
            //{
            //    temp_data = 0F;
            //    for (j = -15; j <= 15; j++)
            //    {
            //        k = i + j;
            //        temp_data += pointY[k];
            //    }
            //    pointYTemp[i] = temp_data / 31.0;
            //}
            //for (i = 15; i < pointNum - 32; i++)
            //{
            //    temp_data = 0F;
            //    for (j = 0; j <= 31; j++)
            //    {
            //        k = i + j;
            //        temp_data += pointY[k];
            //    }
            //    pointYTemp[i] = temp_data / 32.0;
            //}

            //pdArray = pointYTemp;

            for (i = 0; i <= pointNum - 1; i++)
            {
                pointY[i] = pointYTemp[i];
                pdArray[i] = pointYTemp[i];
            }

            for (i = 0; i < 3; i++)
            {
                pointY[i] = (pointY[i + 5] + pointY[i + 4] + pointY[i + 3] + pointY[i + 2]) / 4;
            }
            pointY[pointNum - 1] = (pointY[pointNum - 1] + pointY[pointNum - 2]) / 2;




            //计算最大、最小直径，并UI显示
            int m = 0;
            double minD = 10000D;
            double maxD = 0D;
            int minDx = 0;
            for (m = 0; m < pointNum; m++) //此处数据为900个
            {
                //double D = pdArray[m];
                double D = pointY[m];
                if (D > maxD)
                {
                    maxD = D;
                }
                if (D < minD)
                {
                    minD = D;
                    minDx = m;
                }
            }

            //计算瞳孔初始直径、瞳孔对光反射潜伏期
            //double[] pointYTemp = new double[pointNum];
            for (m = 4; m < pointNum; m++) //此处数据为900个
            {
                //pointYTemp[m] = (pdArray[m] - pdArray[m - 4 + 1]) / 0.0067D;
                pointYTemp[m] = (pointY[m] - pointY[m - 4 + 1]) / 0.0067D;
            }

            double minP = 10000D;
            //瞳孔变化最大处对应的X坐标
            int pMDX = 0;
            for (m = 5; m < pointNum; m++) //此处数据为900个
            {
                if (pointYTemp[m] < minP)
                {
                    minP = pointYTemp[m];
                    pMDX = m;
                }
            }
            //瞳孔对光反射潜伏期
            int pCL = 0;
            for (m = pMDX; m >= 5; m--)
            {
                if (pointYTemp[m] >= -1)
                {
                    pCL = m;
                    break;
                }
            }

            //瞳孔初始直径
            double pID = 0.0D;
            pID = pdArray[pCL-5];

            //瞳孔收缩速度
            double pCV = 0.0F;
            //瞳孔收缩时间
            double time = (minDx - pCL) * ((double)2 / pointNum); 
            if (time != 0.0D)
            {
                pCV = (minD - pID) / time;
            }

            //瞳孔收缩面积比
            double minA = 3.1415926D * minD * minD;
            double maxA = 3.1415926D * pID * pID;
            double pCA = minA / maxA;
            //瞳孔收缩直径比
            double pCD = minD / pID;
            //瞳孔收缩比例
            double pCR = 0.0F;
            pCR = (pID - minD) / pID * 100;


            examInfo = new ExamInfo();
            examInfo.PCA = Math.Round(pCA, 2);
            examInfo.PCD = Math.Round(pCD, 2);
            //瞳孔对光反射潜伏期
            examInfo.PCL = Math.Round(pCL*3.333, 0);
            //examInfo.PCL = pCL;
            // 瞳孔收缩比例
            examInfo.PCR = Math.Round(pCR, 2);
            // 瞳孔收缩时间
            examInfo.PCT = Math.Round(time, 2);
            // 瞳孔收缩速度
            examInfo.PCV = Math.Round(pCV, 2);
            // 瞳孔初始直径
            examInfo.PID = Math.Round(pID, 2);
            // 瞳孔直径最小值
            examInfo.PMD = Math.Round(minD, 2);
            
            // Pupils react to light
            return true;
        }

        /// <summary>
        /// 计算临界闪光融合频率
        /// </summary>
        /// <param name="CffArray"></param>
        /// <returns></returns>
        public static double CalculateCffValue(double[] CffArray)
        {
            double cff = 0d;

            // 获取到的300个数据，取倒数第一个不为零的数据值作为CFF
            if (CffArray != null && CffArray.Length > 0)
            {
                int count = CffArray.Length;
                for (int i = count - 1; i >= 0; i--)
                {
                    if (CffArray[i] != 0)
                    {
                        cff = CffArray[i];
                        break;
                    }
                }
            }

            return cff;
        }


    }
}
