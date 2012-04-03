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
        private SamplerState mSamplerState;

        public RenderManager(Scene scene) : base(scene)
        {
            mSpriteBatch = new SpriteBatch(MilkShake.Graphics);
            mSamplerState = SamplerState.LinearClamp;
        }

        public void SetRenderTarget(RenderTarget2D renderTarget)
        {
            MilkShake.Graphics.SetRenderTarget(renderTarget);
        }

        public void Begin()
        {
            mSpriteBatch.Begin(SpriteSortMode.Immediate,
                               BlendState.AlphaBlend,
                               SamplerState,
                               DepthStencilState.None,
                               RasterizerState.CullNone,
                               null,
                               Scene.Camera.Matrix);
        }

        public void RawDraw(Vector2 position, Texture2D texture, int width, int height)
        {
            mSpriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, width, height), new Rectangle(0, 0, texture.Width, texture.Height), Color.White);
        }

        public void Draw(Vector2 position, Texture2D texture, int width, int height)
        {
            mSpriteBatch.Draw(texture, new Rectangle((int)position.X - (int)cameraOffset().X, (int)position.Y - (int)cameraOffset().Y, width, height), new Rectangle(0, 0, texture.Width, texture.Height), Color.White);
        }

        public void End()
        {
            mSpriteBatch.End();
        }

        public void RawBegin()
        {
            mSpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState, DepthStencilState.None, RasterizerState.CullNone);
        }

        public Vector2 cameraOffset()
        {
            return Scene.Camera.Transform;
        }

        public SamplerState SamplerState { get { return mSamplerState; } set { mSamplerState = value; } }
    }
}
