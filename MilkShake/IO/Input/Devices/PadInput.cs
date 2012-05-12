using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.IO.Input.Devices
{
    public class PadInput
    {
        private static GamePadState mCurState;
        private static GamePadState mPrvState;

        public static void UpdateStart()
        {
            mCurState = GamePad.GetState(PlayerIndex.One);
        }

        public static void UpdateEnd()
        {
            mPrvState = mCurState;
        }

        public static bool isButtonDown(Buttons button)
        {
            return mCurState.IsButtonDown(button);
        }

        public static bool isButtonUp(Buttons button)
        {
            return mCurState.IsButtonUp(button);
        }

        public static bool isButtonPressed(Buttons button)
        {
            if (mPrvState.IsButtonUp(button) && mCurState.IsButtonDown(button))
                return true;

            return false;
        }

        public static bool isKeyReleased(Buttons button)
        {
            if (mPrvState.IsButtonDown(button) && mCurState.IsButtonUp(button))
                return true;

            return false;
        }

        public static GamePadThumbSticks ThumbSticks { get { return mCurState.ThumbSticks; } }
        public static GamePadTriggers Triggers { get { return mCurState.Triggers; } }

    }
}
