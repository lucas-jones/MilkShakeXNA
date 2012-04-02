using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Scenes;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Render
{
    public class RenderManager : SceneComponent
    {
        private SpriteBatch mSpriteBatch;

        public RenderManager(Scene scene) : base(scene)
        {
            mSpriteBatch = new SpriteBatch(MilkShake.Graphics);
        }

        public void Begin()
        {
            mSpriteBatch.Begin(SpriteSortMode.Immediate,
                               BlendState.AlphaBlend,
                               SamplerState.LinearClamp,
                               DepthStencilState.None,
                               RasterizerState.CullNone,
                               null,
                               Scene.Camera.Matrix);
        }

        public void Draw(Vector2 position, Texture2D texture, int width, int height)
        {
            mSpriteBatch.Draw(texture, new Rectangle((int)position.X - (int)cameraOffset().X, (int)position.Y - (int)cameraOffset().Y, width, height), new Rectangle(0, 0, texture.Width, texture.Height), Color.White);
        }

        public void End()
        {
            mSpriteBatch.End();
        }

        public Vector2 cameraOffset()
        {
            return Scene.Camera.Transform;
        }
    }
}
