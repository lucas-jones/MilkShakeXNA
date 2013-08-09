using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MilkShakeFramework.Tools.Debug.Watch
{
    public class WatchedManager
    {
        public static List<WatchEntry> Entries = new List<WatchEntry>();

        public static void PrintEntries()
        {
            foreach (WatchEntry entry in Entries)
            {
                Console.WriteLine("[" + entry.FieldInfo.FieldType.Name + "] " + entry.FieldInfo.Name + " = " + entry.GetValue());
            }
        }

        public static void SetValue(string key, object value)
        {
            Entries.First(e => e.FieldInfo.Name == key).SetValue(value);
        }

        public static void Scan(object obj)
        {
            // Find all [Watched] fields
            List<FieldInfo> fieldList = obj.GetType().GetFields().Where(p => p.IsDefined(typeof(Watched), false)).ToList();

            foreach (FieldInfo field in fieldList)
            {
                Entries.Add(new WatchEntry(obj, field));
            }
        }
    }
}
