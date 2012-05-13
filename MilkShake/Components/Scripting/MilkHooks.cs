using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuaInterface;

namespace MilkShakeFramework.Components.Scripting
{
    /// <summary>
    /// MilkHooks is a LUA based event system
    /// It has two Librarys* one for standard events
    /// and the other one for custom events.
    /// *Dictionarys :]
    /// </summary>
    public class MilkHooks
    {
        private static Dictionary<string, List<LuaFunction>> Events = new Dictionary<string, List<LuaFunction>>();
        private static Dictionary<string, List<LuaFunction>> CustomEvents = new Dictionary<string, List<LuaFunction>>();

        public static bool debugHooks = false;

        /// <summary>
        /// Clears all Events from the Event Library (Events & Custom Events)
        /// </summary>
        public static void ClearEvents()
        {
            Events = new Dictionary<string, List<LuaFunction>>();
            CustomEvents = new Dictionary<string, List<LuaFunction>>();
        }

        /// <summary>
        /// Hooks a LUA function to a Event
        /// </summary>
        [LUAFunction]
        public static void AddHook(string _Event, LuaFunction _Function)
        {
            if (Events.ContainsKey(_Event))
            {
                Events[_Event].Add(_Function);
            }
            else
            {
                List<LuaFunction> luaFunction = new List<LuaFunction>();
                luaFunction.Add(_Function);
                Events.Add(_Event, luaFunction);
            }

            if (debugHooks)
                Console.WriteLine("[LuaManager] Added new hook: " + _Event);
        }

        /// <summary>
        /// Used for amore specifiic commands such as Cow:Update or Pig:Spawn
        /// </summary>
        [LUAFunction]
        public static void AddObjectHook(string _Object, string _Event, LuaFunction _Function)
        {
            AddHook(_Object + ":" + _Event, _Function);
        }



        public static void RemoveHook(string _Event, LuaFunction _Function)
        {
            if (Events.ContainsKey(_Event.ToString()))
            {
                Events[_Event.ToString()].Remove(_Function);
            }

            if (debugHooks)
                Console.WriteLine("[LuaManager] Removed hook: " + _Event);
        }

        public static void TriggerEvent(string _Event, params object[] _Args)
        {
            // Todo: Remove this shizzle
            try
            {
                if (Events.ContainsKey(_Event.ToString()))
                {
                    foreach (LuaFunction Event in Events[_Event.ToString()].ToArray())
                    {
                        Event.Call(_Args);
                    }
                }
            }
            catch (LuaException _luaError)
            {
                Console.WriteLine("[LuaManager] " + _luaError.ToString());
            }
            catch
            {
            }
        }

        public static string StringResultEvent(string _Event, params object[] _Args)
        {
            try
            {
                if (Events.ContainsKey(_Event.ToString()))
                {
                    return Events[_Event.ToString()][0].Call(_Args)[0].ToString();
                }
            }
            catch (LuaException _luaError)
            {
                Console.WriteLine("[LuaManager] " + _luaError.ToString());
            }
            catch
            {
            }

            // Could return null'd version ResultEvent.Nulled;
            return null;
        }

        public static void TriggerEvent(Object _Object, string _Event, params object[] _Args)
        {
            // Todo: Remove this shizzle
            string EventName = _Object + ":" + _Event.ToString();

            try
            {
                if (Events.ContainsKey(EventName))
                {
                    foreach (LuaFunction Event in Events[EventName].ToArray())
                    {
                        Event.Call(_Args);
                    }
                }
            }
            catch (LuaException _luaError)
            {
                Console.WriteLine("[LuaManager] " + _luaError.ToString());
            }
            catch
            {
            }
        }

        [LUAFunction]
        public static void AddCustomHook(string _Event, LuaFunction _Function)
        {

            if (CustomEvents.ContainsKey(_Event))
            {
                CustomEvents[_Event].Add(_Function);
            }
            else
            {
                List<LuaFunction> luaFunction = new List<LuaFunction>();
                luaFunction.Add(_Function);
                CustomEvents.Add(_Event, luaFunction);
            }

            if (debugHooks)
                Console.WriteLine("[LuaManager] Added new custom hook: " + _Event);
        }


        // Quick Hack up
        [LUAFunction]
        public static void TriggerCustomEvent(string _Event)
        {
            try
            {
                if (CustomEvents.ContainsKey(_Event))
                {
                    foreach (LuaFunction Event in CustomEvents[_Event].ToArray())
                    {
                        Event.Call();
                    }
                }
            }
            catch (LuaException _luaError)
            {
                Console.WriteLine("[LuaManager] " + _luaError.ToString());
            }
            catch
            {
            }
        }

        // Quick Hack up
        [LUAFunction]
        public static void TriggerCustomDataEvent(string _Event, string _Data)
        {
            try
            {
                if (CustomEvents.ContainsKey(_Event))
                {
                    foreach (LuaFunction Event in CustomEvents[_Event].ToArray())
                    {
                        Event.Call(_Data);
                    }
                }
            }
            catch (LuaException _luaError)
            {
                Console.WriteLine("[LuaManager] " + _luaError.ToString());
            }
            catch
            {
            }
        }
    }
}
