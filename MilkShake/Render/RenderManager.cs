using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Scenes;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Scenes.Components;
using MilkShakeFramework.IO.Input.Devices;

namespace MilkShakeFramework.Render
{
    public class RenderManager : SceneComponent
    {
        private SpriteBatch _spriteBatch;
        private SamplerState _samplerState;        

        public RenderManager(Scene scene) : base(scene)
        {
            _spriteBatch = new SpriteBatch(MilkShake.Graphics);
            _samplerState = SamplerState;
        }

        public void SetRenderTarget(RenderTarget2D renderTarget)
        {
            MilkShake.Graphics.SetRenderTarget(renderTarget);
        }

        public void Begin(BlendState blendState)
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate,
                               blendState,
                               SamplerState,
                               DepthStencilState.None,
                               RasterizerState.CullNone,
                               null,
                               (!IsRawDraw) ? Scene.Camera.Matrix : Matrix.Identity);
        }

        public void Begin(Effect effect = null)
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate,
                               BlendState.AlphaBlend,
                               SamplerState,
                               DepthStencilState.None,
                               RasterizerState.CullNone,
                               effect,
                               (!IsRawDraw) ? Scene.Camera.Matrix : Matrix.Identity);
        }

        public void Begin(Effect effect, BlendState blend)
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate,
                               blend,
                               SamplerState,
                               DepthStencilState.None,
                               RasterizerState.CullNone,
                               effect,
                               (!IsRawDraw) ? Scene.Camera.Matrix : Matrix.Identity);
        }

        public void Begin(Matrix matrix)
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive,
                                          SamplerState,
                                          DepthStencilState.None,
                                          RasterizerState.CullNone,
                                          null,
                                          (!IsRawDraw) ? matrix : Matrix.Identity);
        }


        public void RawDraw(Vector2 position, Texture2D texture, int width, int height, Color color)
        {
            _spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, width, height), new Rectangle(0, 0, texture.Width, texture.Height), color);
        }


        public void Draw(Vector2 position, Texture2D texture, int width, int height, float rotation, Vector2 origin, Color color, float scaleX = 1, float scaleY = 1)
        {
            _spriteBatch.Draw(texture, new Rectangle((int)position.X - (int)cameraOffset().X, (int)position.Y - (int)cameraOffset().Y, (int)(width * scaleX), (int)(height * scaleY)), new Rectangle(0, 0, texture.Width, texture.Height), color, rotation, origin, SpriteEffects.None, 1);
        }

        public void RawDraw(Vector2 position, Texture2D texture, int width, int height, float rotation, Vector2 origin, Color color, float scaleX = 1, float scaleY = 1)
        {
            _spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)(width * scaleX), (int)(height * scaleY)), new Rectangle(0, 0, texture.Width, texture.Height), color, rotation, origin, SpriteEffects.None, 1);
        }

        public void Draw(Vector2 position, Texture2D texture, int width, int height, Rectangle sourceRectangle)
        {
            _spriteBatch.Draw(texture, new Rectangle((int)position.X - (int)cameraOffset().X, (int)position.Y - (int)cameraOffset().Y, width, height), sourceRectangle, Color.White);
        }

        public void Draw(Vector2 position, Texture2D texture, int width, int height, Rectangle sourceRectangle, SpriteEffects spriteEffects)
        {
            _spriteBatch.Draw(texture, new Vector2((int)position.X - (int)cameraOffset().X, (int)position.Y - (int)cameraOffset().Y), sourceRectangle, Color.White, 0, Vector2.Zero, Vector2.One, spriteEffects, 0);
        }

        public void Draw(Vector2 position, Texture2D texture, int width, int height, Rectangle sourceRectangle, SpriteEffects spriteEffects, Color color, float rotation, Vector2 origin, Vector2 scale)
        {
            _spriteBatch.Draw(texture, new Vector2((int)position.X - (int)cameraOffset().X, (int)position.Y - (int)cameraOffset().Y), sourceRectangle, color, rotation, origin, scale, spriteEffects, 1);
        }

        public void RawDraw(Vector2 position, Texture2D texture, int width, int height, Rectangle sourceRectangle, SpriteEffects spriteEffects, Color color, float rotation, Vector2 origin, Vector2 scale)
        {
            _spriteBatch.Draw(texture, new Vector2((int)position.X - (int)cameraOffset().X, (int)position.Y - (int)cameraOffset().Y), sourceRectangle, color, rotation, origin, scale, spriteEffects, 1);
        }

        public void End()
        {
            _spriteBatch.End();
        }

        public void RawBegin()
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Matrix.Identity);
        }

        public Vector2 cameraOffset()
        {
            return Scene.Camera.Transform;
        }

        public SpriteBatch SpriteBatch { get { return _spriteBatch; } }
        public SamplerState SamplerState { get { return _samplerState; } set { _samplerState = value; } }

        public bool IsRawDraw { get; set; }
    }
}
