﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.IO.Input.Devices;

namespace MilkShakeFramework.IO.Input
{
    public class InputManager
    {
        public static void UpdateStart()
        {
            MouseInput.UpdateStart();
            KeyboardInput.UpdateStart();
            PadInput.UpdateStart();
        }

        public static void UpdateEnd()
        {
            MouseInput.UpdateEnd();
            KeyboardInput.UpdateEnd();
            PadInput.UpdateEnd();
        }
    }
}
