using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Tools.Debug
{
    public class LogicTimer
    {
        private DateTime mStartTime;
        private DateTime mEndTime;
        private double mTimeSpent;

        public void Start()
        {
            mStartTime = DateTime.Now;
        }

        public void End()
        {
            mEndTime = DateTime.Now;
            mTimeSpent = (mEndTime - mStartTime).Milliseconds;
        }

        public double TimeSpent { get { return mTimeSpent; } }
    }
}
