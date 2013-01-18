using System;

namespace MilkShakeFramework.Tools.Debug
{
    public class LogicTimer
    {
        private DateTime _startTime;
        private DateTime mEndTime;
        private double mTimeSpent;

        public void Start()
        {
            _startTime = DateTime.Now;
        }

        public void End()
        {
            mEndTime = DateTime.Now;
            mTimeSpent = (mEndTime - _startTime).Milliseconds;
        }

        public double TimeSpent { get { return mTimeSpent; } }
    }
}
