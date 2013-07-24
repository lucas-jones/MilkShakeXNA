using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Render;
using MilkShakeFramework.Core.Content;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Tools.Maths;

namespace MilkShakeFramework.Core.Game
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

    public class Sprite : GameEntity
    {
        private ImageRenderer mImage;
        private int mWidth, mHeight;
        private float mRotation;
        private Vector2 mOrigin;
        private Vector2 mScale;
        private Color mColor;
        private bool mAutoCenter;
        private bool mVisible;
        
        public Sprite(string url, bool fromStream = false)
        {
            Name = url.Split('/')[url.Split('/').Length - 1];

            mImage = new ImageRenderer(url) { FromStream = fromStream };
            mColor = Color.White;
            mScale = new Vector2(1, 1);
            mAutoCenter = false;
            mVisible = true;

            AddNode(mImage);
        }

        public override void Setup()
        {

            
            base.Setup();
        }

        public override void Load(LoadManager content)
        {
            base.Load(content);

            if (mImage.Texture == null)
            {
                mImage.SetParent(this);
                mImage.SetScene(Scene);
                mImage.Load(null);
                mImage.FixUp();
            }

            mWidth = SetValueOrDefault(mWidth, mImage.Texture.Width);
            mHeight = SetValueOrDefault(mHeight, mImage.Texture.Height);
        }

        public override void FixUp()
        {
            base.FixUp();

            if(AutoCenter)
            {
                Origin = new Vector2(Width / 2, Height / 2);
            }
        }      

        public override void Draw()
        {
            if (Visible)
            {
                base.Draw();
                
                mImage.Draw(WorldPosition, mWidth, mHeight, mRotation, mOrigin, mColor, mScale.X, mScale.Y);
            }
        }

        public ImageRenderer Image { get { return mImage; } set { mImage = value; } }
        public Color Color { get { return mColor; } set { mColor = value; } }
        public float Alpha { get { return mColor.A; } set { mColor.A = (byte)(value * 255); } }

        public int Width { get { return mWidth; } set { mWidth = value; } }
        public int Height { get { return mHeight; } set { mHeight = value; } }

        public Vector2 Origin { get { return mOrigin; } set { mOrigin = value; } }
        public float Rotation { get { return mRotation; } set { mRotation = value; } }
        public Vector2 Scale { get { return mScale; } set { mScale = value; } }
        public bool AutoCenter { get { return mAutoCenter; } set { mAutoCenter = value; } }
        public bool Visible { get { return mVisible; } set { mVisible = value; } }

        public override Tools.Maths.RotatedRectangle BoundingBox
        {
            get
            {
                return new RotatedRectangle(new Rectangle((int)WorldPosition.X - (int)((Width * Scale.X) / 2), (int)WorldPosition.Y - (int)((Height * Scale.Y) / 2), (int)(Width * Scale.X), (int)(Height * Scale.Y)), (Rotation));
            }
        }

        //public override Rectangle BoundingBox { get { return new Rectangle((int)((WorldPosition.X)) - (int)((Width * Scale.X) / 2), (int)((WorldPosition.Y)) - (int)((Height * Scale.Y) / 2), (int)(Width * Scale.X), (int)(Height * Scale.Y)); } }
    }
}
