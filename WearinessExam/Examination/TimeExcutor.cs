using System;
using System.Windows.Forms;

namespace WearinessExam.Examination
{
    /// <summary>
    /// Class TimeExcutor
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TimeExcutor<T>
    {
        /// <summary>
        /// Delegate DrawCurve
        /// </summary>
        /// <param name="valueList">The value list.</param>
        /// <param name="restTime">The rest time.</param>
        public delegate void DrawCurve(T valueList, int restTime);
        /// <summary>
        /// Delegate ExcuteCompleted
        /// </summary>
        public delegate void ExcuteCompleted();
        /// <summary>
        /// The timer
        /// </summary>
        private Timer m_Timer = new Timer();
        /// <summary>
        /// The total time
        /// </summary>
        private int m_TotalTime = 0;
        /// <summary>
        /// The times
        /// </summary>
        private int m_Times = 0;
        /// <summary>
        /// Occurs when [draw].
        /// </summary>
        public event DrawCurve Draw;
        /// <summary>
        /// Occurs when [excute completed handle].
        /// </summary>
        public event ExcuteCompleted ExcuteCompletedHandle;
        /// <summary>
        /// The time in milliseconds.
        /// </summary>
        private const int TIME_IN_MILLISECONDS = 1000;
        /// <summary>
        /// The m_ is success
        /// </summary>
        protected bool m_IsSuccess = false;
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeExcutor{T}"/> class.
        /// </summary>
        /// <param name="time">The time.</param>
        /// <param name="interval">The interval.</param>
        public TimeExcutor(int time, int interval)
        {
            m_TotalTime = time;
            m_Timer.Interval = interval;
            m_Timer.Tick += new EventHandler(Timer_Tick);
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public bool Start()
        {
            if (m_Timer.Enabled)
            {

                m_Timer.Stop();
                m_Timer.Enabled = false;
            }
            m_Times = 0;
            bool result = true;
            if (result = StartExam())
            {
                m_Timer.Start();
            }
            return result;
        }

        /// <summary>
        /// Stars the exam.
        /// </summary>
        protected virtual bool StartExam()
        {
            return true;
        }
        /// <summary>
        /// Stops the exam.
        /// </summary>
        protected virtual bool StopExam()
        {
            return true;
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public bool Stop()
        {
            bool result = true;
            m_Times = 0;
            if (m_Timer.Enabled)
            {
                m_Timer.Stop();
                m_Timer.Enabled = false;

                result=StopExam();
            }
            return result;
        }

        /// <summary>
        /// Excutes the exam.
        /// </summary>
        /// <param name="valueList">The value list.</param>
        /// <returns><c>true</c> if success, <c>false</c> otherwise</returns>
        protected virtual bool ExcuteExam(out T valueList)
        {
            valueList = default(T);
            return true;
        }

        /// <summary>
        /// Handles the Tick event of the Timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            T valueList;
            m_IsSuccess = ExcuteExam(out valueList);
            m_Times++;
            if (m_IsSuccess)
            {
                if (Draw != null)
                {
                    Draw(valueList, m_TotalTime - m_Timer.Interval * m_Times / TIME_IN_MILLISECONDS);
                }
            }
            if (CheckStop())
            {
                Stop();
                ExcuteCompletedHandle();
            }
        }

        /// <summary>
        /// Checks the timer to stop.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise</returns>
        protected virtual bool CheckStop()
        {
            return m_TotalTime <= m_Timer.Interval * m_Times / TIME_IN_MILLISECONDS;
        }

    }
}
