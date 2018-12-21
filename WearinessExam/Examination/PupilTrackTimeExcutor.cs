using System.Collections.Generic;
using System.Drawing;

namespace WearinessExam.Examination
{
    /// <summary>
    /// Class PupilTrackTimeExcutor
    /// </summary>
    public class PupilTrackTimeExcutor : TimeExcutor<List<Point>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PupilTrackTimeExcutor"/> class.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="interval">The interval.</param>
        public PupilTrackTimeExcutor(int time, int interval)
            : base(time, interval)
        {
        }

        /// <summary>
        /// Excutes the exam.
        /// </summary>
        /// <param name="pointList">The point list.</param>
        /// <returns><c>true</c> if success, <c>false</c> otherwise</returns>
        protected override bool ExcuteExam(out List<Point> pointList)
        {
            //调用DLL 返回数据
            string errorMessage;
            bool result = DeviceFacade.ExecutePupilTrack(out pointList, out errorMessage);
            ErrorMessage = errorMessage;
            return result;
        }

        /// <summary>
        /// Checks the stop.
        /// </summary>
        /// <returns><c>true</c> if check is passed, <c>false</c> otherwise</returns>
        protected override bool CheckStop()
        {
            return base.CheckStop() && !m_IsSuccess;
        }

        /// <summary>
        /// Starts the exam.
        /// </summary>
        protected override bool StartExam()
        {
            return DeviceFacade.StartPupilTrack();
        }

        /// <summary>
        /// Stops the exam.
        /// </summary>
        protected override bool StopExam()
        {
            return DeviceFacade.StopPupilTrack();
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage
        {
            get;
            set;
        }
    }
}
