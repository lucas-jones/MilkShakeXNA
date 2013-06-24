using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace MilkShakeFramework.Tools.Debug.Watch
{
    public class WatchEntry
    {
        public object Object { get; private set; }
        public FieldInfo FieldInfo { get; private set; }

        public WatchEntry(object _object, FieldInfo _fieldInfo)
        {
            Object = _object;
            FieldInfo = _fieldInfo;
        }

        public object GetValue()
        {
            return FieldInfo.GetValue(Object);
        }

        public void SetValue(object value)
        {
            FieldInfo.SetValue(Object, value);
        }
    }
}
