using System.Collections.Generic;
using System;

namespace WearinessExam.Examination
{
    /// <summary>
    /// Class CFF1TimeExcutor
    /// </summary>
    public class CFF1TimeExcutor : TimeExcutor<List<double>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CFF1TimeExcutor"/> class.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="interval">The interval.</param>
        public CFF1TimeExcutor(int time, int interval)
            : base(time, interval)
        {
        }

        /// <summary>
        /// Excutes the exam.
        /// </summary>
        /// <param name="valueList">The value list.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        protected override bool ExcuteExam(out List<double> valueList)
        {
            //调用DLL 返回数据
            double value = DeviceFacade.GetCFF1Data();
            value = Math.Round(value, 2);
            ExamDataCenter.CFF1Value.Add(value);
            valueList = new List<double>();
            valueList.Add(value);
            return true;
        }

        /// <summary>
        /// Starts the exam.
        /// </summary>
        protected override bool StartExam()
        {
            return DeviceFacade.StartCFF1();
        }
        /// <summary>
        /// Stops the exam.
        /// </summary>
        protected override bool StopExam()
        {
            return DeviceFacade.StopCFF1();
        }
    }
}
