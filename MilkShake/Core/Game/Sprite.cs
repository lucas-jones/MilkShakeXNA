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
            mImage.Load(content);
            base.Load(content);
        }
    }
}
