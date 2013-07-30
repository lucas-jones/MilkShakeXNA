using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MilkShakeFramework.IO.Input.Devices;
using Microsoft.Xna.Framework.Input;

namespace MilkShakeFramework.Core.Game.Components.Misc
{
    public class CameraDebugModifier : GameEntity
    {
        private float _speed;
        private Keys _up;
        private Keys _down;
        private Keys _left;
        private Keys _right;

        public CameraDebugModifier(float speed = 1, Keys up = Keys.W, Keys down = Keys.S, Keys left = Keys.A, Keys right = Keys.D)
        {
            _speed = speed;
            _up = up;
            _down = down;
            _left = left;
            _right = right;
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 movement = Vector2.Zero;
            if (KeyboardInput.isKeyDown(_up)) movement.Y -= _speed;
            if (KeyboardInput.isKeyDown(_down)) movement.Y += _speed;
            if (KeyboardInput.isKeyDown(_left)) movement.X -= _speed;
            if (KeyboardInput.isKeyDown(_right)) movement.X += _speed;

            if (KeyboardInput.isKeyDown(Keys.Q)) Scene.Camera.Zoom += 0.01F;
            if (KeyboardInput.isKeyDown(Keys.E)) Scene.Camera.Zoom -= 0.01F;

            if (KeyboardInput.isKeyDown(Keys.LeftShift)) movement *= 2.5f;

            Scene.Camera.Position += movement;
        }
    }
}
