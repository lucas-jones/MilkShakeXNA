using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Core.Cameras
{
    public class Camera : GameEntity
    {
        public const float MAX_ZOOM = 2;
        public const float MIN_ZOOM = 0.193f;

        private Vector2 mOffset;
        private float mZoom;

        private Rectangle mViewBox;
        private Matrix mMatrix;

        public Camera()
        {
            mOffset = Vector2.Zero;
            mZoom = 1;

            mViewBox = new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            mMatrix = Matrix.CreateTranslation(-new Vector3(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2, 0)) *
                      Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                      Matrix.CreateTranslation(new Vector3(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2, 0));

            UpdateViewBox();
        }

        private void UpdateViewBox()
        {
            Vector2 inverseOffset = Vector2.Transform(Vector2.Zero, Matrix.Invert(Matrix)) + Position;

            mViewBox = new Rectangle((int)inverseOffset.X, (int)inverseOffset.Y, (int)(Globals.ScreenWidth * (1 / Zoom)), (int)(Globals.ScreenHeight * (1 / Zoom)));
        }

        public float Zoom { get { return mZoom; } set { mZoom = value; } }
        public Vector2 Offset { get { return mOffset; } }
        public Rectangle ViewBox { get { return mViewBox; } }
        public Vector2 Transform { get { return (Position + Offset); } }
        public Matrix Matrix { get { return mMatrix; } }

        public override Vector2 WorldPosition { get { return Position + Offset; } }
    }
}
