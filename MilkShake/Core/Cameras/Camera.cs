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
        private static readonly Random random = new Random();

        public const float MAX_ZOOM = 2;
        public const float MIN_ZOOM = 0.193f;

        private Vector2 mOffset;
        private float mRotation;
        private float mZoom;

        private bool mShaking;
        private float mShakeMagnitude;
        private float mShakeDuration;
        private float mShakeTimer;
        private Vector2 mShakeOffset;

        private Rectangle mViewBox;
        private Matrix mMatrix;

        private int mWidth, mHeight;

        public Camera()
        {
            mOffset = Vector2.Zero;
            mZoom = 1;

            mWidth = Globals.ScreenWidth;
            mHeight = Globals.ScreenHeight;

            mViewBox = new Rectangle(0, 0, mWidth, mHeight);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (mShaking)
            {
                // Move our timer ahead based on the elapsed time
                mShakeTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // If we're at the max duration, we're not going to be shaking anymore
                if (mShakeTimer >= mShakeDuration)
                {
                    mShaking = false;
                    mShakeTimer = mShakeDuration;
                    Position = mShakeStoredPosition;
                }

                // Compute our progress in a [0, 1] range
                float progress = mShakeTimer / mShakeDuration;

                // Compute our magnitude based on our maximum value and our progress. This causes
                // the shake to reduce in magnitude as time moves on, giving us a smooth transition
                // back to being stationary. We use progress * progress to have a non-linear fall 
                // off of our magnitude. We could switch that with just progress if we want a linear 
                // fall off.
                float magnitude = mShakeMagnitude * (1f - (progress * progress));

                // Generate a new offset vector with three random values and our magnitude
                mShakeOffset = new Vector2(NextFloat(), NextFloat()) * magnitude;

                // If we're shaking, add our offset to our position and target
                Position = mShakeStoredPosition + mShakeOffset;
            }

            mMatrix = Matrix.CreateTranslation(-new Vector3(mWidth / 2, mHeight / 2, 0)) *
                      Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                      Matrix.CreateRotationZ(MathHelper.ToRadians(mRotation)) *
                      Matrix.CreateTranslation(new Vector3(mWidth / 2, mHeight / 2, 0));

            UpdateViewBox();
        }

        private void UpdateViewBox()
        {
            Vector2 inverseOffset = Vector2.Transform(Vector2.Zero, Matrix.Invert(Matrix)) + Position;

            mViewBox = new Rectangle((int)inverseOffset.X, (int)inverseOffset.Y, (int)(mWidth * (1 / Zoom)), (int)(mHeight * (1 / Zoom)));
        }

        private float NextFloat()
        {
            return (float)random.NextDouble() * 2f - 1f;
        }
        private Vector2 mShakeStoredPosition;
        public void Shake(float magnitude, float duration)
        {
            mShakeStoredPosition = Position;

            // We're now shaking
            mShaking = true;

            // Store our magnitude and duration
            mShakeMagnitude = magnitude;
            mShakeDuration = duration;

            // Reset our timer
            mShakeTimer = 0f;
        }



        public Vector2 ScreenToWorld(Vector2 screenPosition)
        {
            return Vector2.Transform(screenPosition + (Position * Zoom), Matrix.Invert(Matrix));
        }

        public int Width { get { return mWidth; } set { mWidth = value; } }
        public int Height { get { return mHeight; } set { mHeight = value; } }
        public float Zoom { get { return mZoom; } set { mZoom = value; } }
        public float Rotation { get { return mRotation; } set { mRotation = value; } }
        public Vector2 Offset { get { return mOffset; } }
        public Rectangle ViewBox { get { return mViewBox; } }
        public Vector2 Transform { get { return (Position + Offset); } }
        public Matrix Matrix { get { return mMatrix; } }

        public override Vector2 WorldPosition { get { return Position + Offset; } }


    }
}
