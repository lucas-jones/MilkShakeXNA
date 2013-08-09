using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Tools.Debug;

namespace MilkShakeFramework.Core.Events
{
    // OLD CODE

    public delegate void MEvent();

    public class EventDispatcher
    {
        private Dictionary<string, EventContainer> mEventContainer = new Dictionary<string, EventContainer>();

        public void AddEventListener(string eventName, MEvent eventFunction, string eventHandler = "Unknown")
        {
            if (!mEventContainer.ContainsKey(eventName)) mEventContainer.Add(eventName, new EventContainer());

            mEventContainer[eventName].Add(new MilkEvent(eventName, eventFunction, eventHandler));
        }

        public void DispatchEvent(string eventName)
        {
            if (!mEventContainer.ContainsKey(eventName)) return;

            mEventContainer[eventName].DispatchEvents();
        }

        public Dictionary<string, EventContainer> EventContainer { get { return mEventContainer; } }
    }

    public class EventContainer
    {
        private List<MilkEvent> mMilkEvents;
        private LogicTimer mLogicTimer;

        public EventContainer()
        {
            mMilkEvents = new List<MilkEvent>();
            mLogicTimer = new LogicTimer();
        }

        public void DispatchEvents()
        {
            mLogicTimer.Start();
            foreach (MilkEvent milkEvent in mMilkEvents)
            {
                milkEvent.Invoke();
            }
            mLogicTimer.End();
        }

        public void Add(MilkEvent aMilkEvent)
        {
            mMilkEvents.Add(aMilkEvent);
        }

        public List<MilkEvent> MilkEvents { get { return mMilkEvents; } }
        public double TimeSpent { get { return mLogicTimer.TimeSpent; } }
    }

    public class MilkEvent
    {
        private string mEventName;
        private Delegate mEventFunction;
        private string mEventhandler;

        // [Debug]
        private LogicTimer mTimer;

        public MilkEvent(string eventName, Delegate eventFunction, string eventHandler)
        {
            mEventName = eventName;
            mEventFunction = eventFunction;
            mEventhandler = eventHandler;

            mTimer = new LogicTimer();
        }

        public void Invoke()
        {
            mTimer.Start();
            mEventFunction.DynamicInvoke();
            mTimer.End();
        }

        public string EventName { get { return mEventName; } }
        public Delegate EventFunction { get { return mEventFunction; } }
        public string EventHandler { get { return mEventhandler; } }

        public double TimeSpent { get { return mTimer.TimeSpent; } }
    }
}
