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
        private static List<PadState> _padStates;

        private static void Boot()
        {
            _padStates = new List<PadState>();

            _padStates.Add(new PadState(PlayerIndex.One));
            _padStates.Add(new PadState(PlayerIndex.Two));
            _padStates.Add(new PadState(PlayerIndex.Three));
            _padStates.Add(new PadState(PlayerIndex.Four));
        }

        public static void UpdateStart()
        {
            if (_padStates == null) Boot();

            _padStates.ForEach(ps => ps.UpdateStart());
        }

        public static void UpdateEnd()
        {
            _padStates.ForEach(ps => ps.UpdateEnd());
        }

        public static PadState GetPad(PlayerIndex index)
        {
            if (_padStates == null) Boot();

            return _padStates.First((pad) => pad.PlayerIndex == index);
        }

        public static List<PadState> GetAllPads()
        {
            if (_padStates == null) Boot();

            return _padStates;
        }
    }

    public class PadState
    {
        private PlayerIndex _index;
        private GamePadState _curState;
        private GamePadState _prvState;

        public PadState(PlayerIndex index)
        {
            _index = index;
        }

        public void UpdateStart()
        {            
            _curState = GamePad.GetState(_index);
        }

        public void UpdateEnd()
        {
            _prvState = _curState;
        }

        public bool isButtonDown(Buttons button)
        {
            return _curState.IsButtonDown(button);
        }

        public bool isButtonUp(Buttons button)
        {
            return _curState.IsButtonUp(button);
        }

        public bool isButtonPressed(Buttons button)
        {
            if (_prvState.IsButtonUp(button) && _curState.IsButtonDown(button))
                return true;

            return false;
        }

        public bool isButtonReleased(Buttons button)
        {
            if (_prvState.IsButtonDown(button) && _curState.IsButtonUp(button))
                return true;

            return false;
        }

        public void Rumble(float left, float right)
        {
            GamePad.SetVibration(PlayerIndex, left, right);
        }

        public PlayerIndex PlayerIndex { get { return _index; } }
        public GamePadThumbSticks ThumbSticks { get { return _curState.ThumbSticks; } }
        public GamePadTriggers Triggers { get { return _curState.Triggers; } }
        public GamePadDPad DPad { get { return _curState.DPad; } }
    }
}
