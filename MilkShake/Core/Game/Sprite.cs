using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Render;
using MilkShakeFramework.Core.Content;

namespace MilkShakeFramework.Core.Game
{
    public class Sprite : GameEntity
    {
        private Image mImage;
        private int mWidth, mHeight;

        public Sprite(string url)
        {
            mImage = new Image(url);
        }

        public override void Setup()
        {
            AddNode(mImage);

            base.Setup();
        }

        public override void Load(LoadManager content)
        {
            base.Load(content);

            mWidth = SetValueOrDefault(mWidth, mImage.Texture.Width);
            mHeight = SetValueOrDefault(mHeight, mImage.Texture.Height);
        }

        public override void FixUp()
        {
            base.FixUp();
        }

        public override void Draw()
        {
            mImage.Draw(WorldPosition, mWidth, mHeight);

            base.Draw();
        }

        public int Width { get { return mWidth; } set { mWidth = value; } }
        public int Height { get { return mHeight; } set { mHeight = value; } }
    }
}
