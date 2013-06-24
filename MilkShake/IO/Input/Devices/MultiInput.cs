using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.IO.Input.Devices
{
    public class MultiInput
    {
        public static bool IsPressed(Buttons button, Keys key, PlayerIndex index = PlayerIndex.One)
        {
            return PadInput.GetPad(index).isButtonPressed(button) || KeyboardInput.isKeyPressed(key);
        }
    }
}
