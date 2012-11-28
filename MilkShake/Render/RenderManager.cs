using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Scenes;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Scenes.Components;

namespace MilkShakeFramework.Render
{
    public class RenderManager : SceneComponent
    {
        private SpriteBatch mSpriteBatch;
        private SamplerState mSamplerState;

        public RenderManager(Scene scene) : base(scene)
        {
            mSpriteBatch = new SpriteBatch(MilkShake.Graphics);
            mSamplerState = SamplerState;
        }

        public void SetRenderTarget(RenderTarget2D renderTarget)
        {
            MilkShake.Graphics.SetRenderTarget(renderTarget);
            //MilkShake.GraphicsManager.PreferMultiSampling = true;
        }

        public void Begin()
        {
            mSpriteBatch.Begin(SpriteSortMode.Immediate,
                               BlendState.NonPremultiplied,
                               SamplerState,
                               DepthStencilState.None,
                               RasterizerState.CullNone,
                               null,
                               Scene.Camera.Matrix);
        }

        public void RawDraw(Vector2 position, Texture2D texture, int width, int height, Color color)
        {
            mSpriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, width, height), new Rectangle(0, 0, texture.Width, texture.Height), color);
        }

        public void Draw(Vector2 position, Texture2D texture, int width, int height, float rotation, Vector2 origin, Color color)
        {
            mSpriteBatch.Draw(texture, new Rectangle((int)position.X - (int)cameraOffset().X, (int)position.Y - (int)cameraOffset().Y, width, height), new Rectangle(0, 0, texture.Width, texture.Height), color, rotation, origin, SpriteEffects.None, 1);
        }

        public void Draw(Vector2 position, Texture2D texture, int width, int height, Rectangle sourceRectangle)
        {
            mSpriteBatch.Draw(texture, new Vector2((int)position.X - (int)cameraOffset().X, (int)position.Y - (int)cameraOffset().Y), sourceRectangle, Color.White);
        }

        public void Draw(Vector2 position, Texture2D texture, int width, int height, Rectangle sourceRectangle, SpriteEffects spriteEffects)
        {
            mSpriteBatch.Draw(texture, new Vector2((int)position.X - (int)cameraOffset().X, (int)position.Y - (int)cameraOffset().Y), sourceRectangle, Color.White, 0, Vector2.Zero, Vector2.One, spriteEffects, 0);
        }

        public void End()
        {
            mSpriteBatch.End();
        }

        public void RawBegin()
        {
            mSpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState, DepthStencilState.None, RasterizerState.CullNone);
        }

        public Vector2 cameraOffset()
        {
            return Scene.Camera.Transform;
        }

        public SpriteBatch SpriteBatch { get { return mSpriteBatch; } }
        public SamplerState SamplerState { get { return mSamplerState; } set { mSamplerState = value; } }
    }
}
