using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Render;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Core.Game.Components.Misc
{
    public class ScrollingPattern : GameEntity
    {
        private Image image;

        private Vector2 offset;

        public override void Setup()
        {
            image = new Image("pattern");
            AddNode(image);

            base.Setup();
        }

        public override void FixUp()
        {
            offset = -new Vector2(image.Texture.Width, image.Texture.Height);

            base.FixUp();
        }

        public override void Update(GameTime gameTime)
        {
            offset += new Vector2(0, 0);

            if (offset.X >= 0 && offset.Y >= 0)
            {
                offset = -new Vector2(image.Texture.Width, image.Texture.Height);
            }

            base.Update(gameTime);
        }

        public override void Draw()
        {
            base.Draw();

            int xCount = (Globals.ScreenWidth / image.Texture.Width) + 1;
            int yCount = (Globals.ScreenHeight / image.Texture.Height) + 1;

            for (int x = 0; x < xCount; x++)
            for (int y = 0; y < yCount; y++)
			{
                image.Draw(WorldPosition + new Vector2(x * image.Texture.Width, y * image.Texture.Height) + offset, image.Texture.Width, image.Texture.Height, 0, Vector2.Zero, Color.White);
			}
        }

    }
}
