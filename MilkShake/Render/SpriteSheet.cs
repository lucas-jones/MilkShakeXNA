using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Render
{
    public class SpriteSheet : Renderer
    {
        private Texture2D mTexture2D;
        private string mURL;

        public SpriteSheet(string url)
        {
            mURL = url;
        }

        public override void Load(LoadManager content)
        {
            mTexture2D = MilkShake.ConentManager.Load<Texture2D>(URL);
        }

        public void Draw(Vector2 position, int width, int height, Rectangle source)
        {
            RenderManager.Draw(position, Texture, width, height, source);
        }

        public void Draw(Vector2 position, int width, int height, Rectangle source, bool flipped)
        {
            SpriteEffects effects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            RenderManager.Draw(position, Texture, width, height, source, effects);
        }

        public Texture2D Texture { get { return mTexture2D; } }
        public string URL { get { return mURL; } }
    }
}
