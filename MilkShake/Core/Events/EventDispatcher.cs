using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Core.Events
{
    public class MilkEvent
    {
        private string mEventName;
        private Delegate mEventFunction;
        private string mEventhandler;

        public MilkEvent(string eventName, Delegate eventFunction, string eventHandler)
        {
            mEventName = eventName;
            mEventFunction = eventFunction;
            mEventhandler = eventHandler;
        }

        public string EventName { get { return mEventName; } }
        public Delegate EventFunction { get { return mEventFunction; } }
        public string EventHandler { get { return mEventhandler; } }
    }

    public delegate void MEvent();

    public class EventDispatcher
    {
        private Dictionary<string, List<MilkEvent>> mEventContainer = new Dictionary<string,List<MilkEvent>>();

        public void AddEventListener(string eventName, MEvent eventFunction, string eventHandler = "Unknown")
        {
            if (!mEventContainer.ContainsKey(eventName)) mEventContainer.Add(eventName, new List<MilkEvent>());

            mEventContainer[eventName].Add(new MilkEvent(eventName, eventFunction, eventHandler));
        }

        public void DispatchEvent(string eventName)
        {
            if (!mEventContainer.ContainsKey(eventName)) return;

            
            DateTime currentEventTime = DateTime.Now;

            foreach (MilkEvent milkEvent in mEventContainer[eventName])
            {
                DateTime currentTime = DateTime.Now;

                milkEvent.EventFunction.DynamicInvoke();

                TimeSpan elapsed = DateTime.Now - currentTime;
                Console.WriteLine("[DispatchEvent] Handler: " + milkEvent.EventHandler + " " + elapsed.TotalMilliseconds + "ms");
            }
            
            TimeSpan eventElapsed = DateTime.Now - currentEventTime;
            Console.WriteLine("[DispatchEvent] Dispatching Event: " + eventName + " " + eventElapsed.TotalMilliseconds + "ms");
        }
    }
}
