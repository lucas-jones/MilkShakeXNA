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
    public class SpriteSheetRenderer : Renderer
    {
        private Texture2D mTexture2D;
        private string mURL;

        public SpriteSheetRenderer(string url)
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

        public void Draw(Vector2 position, int width, int height, Rectangle source, bool flipped, Color color, float rotation, Vector2 origin, Vector2 scale)
        {
            SpriteEffects effects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

            RenderManager.Draw(position, Texture, width, height, source, effects, color, rotation, origin, scale);
        }

        public Texture2D Texture { get { return mTexture2D; } }
        public string URL { get { return mURL; } }
    }
}
