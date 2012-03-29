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
            AddNode(mImage);
        }
    }
}
