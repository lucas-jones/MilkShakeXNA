using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Components.Scripting
{
    public class ScriptFunctions
    {
        [LUAFunction]
        public static void trace(object _object)
        {
            if (_object == null) _object = "Null";
            Console.WriteLine("[LUA] " + _object.ToString());
        }

        [LUAFunction]
        public static double Sin(double a)
        {
            return Math.Sin(a);
        }

        [LUAFunction]
        public static double Cos(double a)
        {
            return Math.Cos(a);
        }
    }
}
