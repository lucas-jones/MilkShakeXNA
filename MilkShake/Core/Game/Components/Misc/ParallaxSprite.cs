using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Core.Game.Components.Misc
{
    public class ParallaxSprite : Sprite
    {
        private Vector2 mOriginalPosition;
        private Vector2 mSpeed;

        public ParallaxSprite(string url, float xSpeed = 0.5f, float ySpeed = 1) : base(url)
        {
            mSpeed = new Vector2(xSpeed, ySpeed);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Position = mOriginalPosition + (Scene.Camera.Position * mSpeed);

            base.Update(gameTime);
        }

        public override float X
        {
            get { return base.X; }
            set { mOriginalPosition.X = value; base.X = value; }
        }

        public override float Y
        {
            get { return base.Y; }
            set { mOriginalPosition.Y = value; base.Y = value; }
        }

        public Vector2 Speed { get { return mSpeed; } set { mSpeed = value; } }
    }
}
