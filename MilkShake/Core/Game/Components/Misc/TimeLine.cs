using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Scenes;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Core.Game.Components.Misc
{
    public class TimeLine : GameEntity
    {
        private float _maxCycleTime;
        private float _curCycleTime;

        private int _prvPercent;

        private Dictionary<int, List<BasicEvent>> _timeLineEvents;

        public TimeLine(float cycleTime)
        {
            _maxCycleTime = cycleTime;
            _prvPercent = -1;
            _curCycleTime = 0;

            _timeLineEvents = new Dictionary<int, List<BasicEvent>>();
        }

        public void AddEvent(int timePercent, BasicEvent basicEvent)
        {
            if (!_timeLineEvents.ContainsKey(timePercent)) _timeLineEvents.Add(timePercent, new List<BasicEvent>());

            _timeLineEvents[timePercent].Add(basicEvent);
        }

        public override void Update(GameTime gameTime)
        {
            _curCycleTime += gameTime.ElapsedGameTime.Milliseconds;

            //Console.WriteLine(CurrentPercent);

            if (CurrentPercent != _prvPercent)
            {
                if (CurrentPercent == _prvPercent + 1) // Time correctly
                {
                    CallTimeLineEvents(CurrentPercent);
                }
                else // Frame Skip
                {
                    for (int percent = _prvPercent + 1; percent < CurrentPercent; percent++)
                    {
                        CallTimeLineEvents(percent);
                    }
                }

                _prvPercent = CurrentPercent;
            }

            // Reset
            if (_curCycleTime > _maxCycleTime)
            {
                _curCycleTime -= _maxCycleTime;
                _prvPercent = -1;
            }

            base.Update(gameTime);
        }

        private void CallTimeLineEvents(int percent)
        {
            if (_timeLineEvents.ContainsKey(percent))
            {
                _timeLineEvents[percent].ForEach(action => action());
            }
        }

        public float CurrentFraction { get { return _curCycleTime / _maxCycleTime; } }
        public int CurrentPercent { get { return (int)(CurrentFraction * 100); } }
    }
}
