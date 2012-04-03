using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Render;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework;

namespace Playground
{
    public class Tile : GameEntity
    {
        public static Image image; // SpriteSheet
        public static Random random;

        public Tile()
        {
        }

        public override void SetParent(MilkShakeFramework.Core.ITreeNode parent)
        {
            base.SetParent(parent);

            if (image == null)
            {
                image = new Image("Tiles/key");
                Parent.AddNode(image);

                random = new Random();
            }

            offset = random.Next(0, 100);
        }

        

        public override void Draw()
        {
            image.Draw(Position + new Vector2(0, sinval), image.Texture.Width, image.Texture.Height);
            base.Draw();
        }

        int sinval;
        int offset;



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            sinval = (int)(Math.Sin((gameTime.TotalGameTime.TotalSeconds + offset) * 4) * 25);
        }
    }
}
