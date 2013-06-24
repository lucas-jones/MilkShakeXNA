using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.IO.Input.Devices;
using Microsoft.Xna.Framework.Input;

namespace MilkShakeFramework.Core.Filters.Presets
{
    public class GaussianBlurFilter : Filter
    {
        private int BLUR_RADIUS = 7;
        private float blurValue = 0f;

        private int radius;
        private float amount;
        private float sigma;
        private float[] kernel;
        private Vector2[] offsetsHoriz;
        private Vector2[] offsetsVert;

        RenderTarget2D renderTargetVert;
        RenderTarget2D renderTargetHori;

        public GaussianBlurFilter(float defaultValue = 0) : base("Scene//Levels//Effects//GaussianBlur")
        {
            blurValue = defaultValue;
        }

        public override void FixUp()
        {
            base.FixUp();

            SetBlur(blurValue);
            ComputeOffsets(Globals.ScreenWidth, Globals.ScreenHeight);


            Effect.CurrentTechnique = Effect.Techniques["GaussianBlur"];
            Parameters["colorMapTexture"].SetValue(Scene.RenderTarget);
            Parameters["weights"].SetValue(kernel);
            Parameters["offsets"].SetValue(offsetsHoriz);

            renderTargetVert = new RenderTarget2D(MilkShake.Graphics, Globals.ScreenWidth, Globals.ScreenHeight);
            renderTargetHori = new RenderTarget2D(MilkShake.Graphics, Globals.ScreenWidth, Globals.ScreenHeight);
        }

        public void SetBlur(float value)
        {
            // Hack T_T
            if (value >= 1) value = 0.999f;
            if (value < 0) value = 0;

            ComputeKernel(BLUR_RADIUS, (BLUR_RADIUS * 2) * (1 - value));
        }

        public override void Begin()
        {
            MilkShake.Graphics.SetRenderTarget(renderTargetVert);
            MilkShake.Graphics.Clear(Color.Transparent);

            
            Parameters["weights"].SetValue(kernel);
            Parameters["offsets"].SetValue(offsetsVert);
            
            base.Begin();
        }

        public override void End()
        {
            base.End();

            MilkShake.Graphics.SetRenderTarget(renderTargetHori);
            MilkShake.Graphics.Clear(Color.Transparent);

            Parameters["weights"].SetValue(kernel);
            Parameters["offsets"].SetValue(offsetsHoriz);

            base.Begin(BlendState.Opaque);

            Scene.RenderManager.RawDraw(Vector2.Zero, renderTargetVert, Globals.ScreenWidth, Globals.ScreenHeight, Color.White);

            base.End();

            MilkShake.Graphics.SetRenderTarget(Scene.RenderTarget);

            Scene.RenderManager.RawDraw(Vector2.Zero, renderTargetHori, Globals.ScreenWidth, Globals.ScreenHeight, Color.White);

            if (KeyboardInput.isKeyDown(Keys.F9))
            {
                Scene.RenderManager.RawDraw(Vector2.Zero, renderTargetVert, Globals.ScreenWidth / 2, Globals.ScreenHeight / 2, Color.White);
                Scene.RenderManager.RawDraw(new Vector2(1280 / 2, 0), renderTargetHori, Globals.ScreenWidth / 2, Globals.ScreenHeight / 2, Color.White);
            }
        }

        public void ComputeKernel(int blurRadius, float blurAmount)
        {
            radius = blurRadius;
            amount = blurAmount;

            kernel = null;
            kernel = new float[radius * 2 + 1];
            sigma = radius / amount;

            float twoSigmaSquare = 2.0f * sigma * sigma;
            float sigmaRoot = (float)Math.Sqrt(twoSigmaSquare * Math.PI);
            float total = 0.0f;
            float distance = 0.0f;
            int index = 0;

            for (int i = -radius; i <= radius; ++i)
            {
                distance = i * i;
                index = i + radius;
                kernel[index] = (float)Math.Exp(-distance / twoSigmaSquare) / sigmaRoot;
                total += kernel[index];
            }

            for (int i = 0; i < kernel.Length; ++i)
                kernel[i] /= total;
        }

        public void     ComputeOffsets(float textureWidth, float textureHeight)
        {
            offsetsHoriz = null;
            offsetsHoriz = new Vector2[radius * 2 + 1];

            offsetsVert = null;
            offsetsVert = new Vector2[radius * 2 + 1];

            int index = 0;
            float xOffset = 1.0f / textureWidth;
            float yOffset = 1.0f / textureHeight;

            for (int i = -radius; i <= radius; ++i)
            {
                index = i + radius;
                offsetsHoriz[index] = new Vector2(i * xOffset, 0.0f);
                offsetsVert[index] = new Vector2(0.0f, i * yOffset);
            }
        }




    }
}
