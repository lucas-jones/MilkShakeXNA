using System;
using MilkShakeFramework.Render;
using MilkShakeFramework.Core.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.IO.Input.Devices;

using MilkShakeFramework.Components.Effects;

namespace MilkShakeFramework.Core.Game.Components.Distortion
{
    //
    // DistortionLayer!!!!! Add entitys to it
    // This as an entity = multiple layers of distortion on different depths
    // Which entitys can get added to for saving
    //
    public class DistortionEffect : PostProcessingEffect
    {
        private Image _image;
        private SpriteBatch spriteBatch;
        private RenderTarget2D distortionMap;
        private RenderTarget2D targetMap;
        private Texture2D testImage;

        private bool _showDistortionMap;

        Effect distortEffect;
        EffectTechnique distortTechnique;
        private const float blurAmount = 20f;

        public DistortionEffect()
            : base()
        {
            //_image = new Image("bla");
            testImage = MilkShake.ConentManager.Load<Texture2D>("w1K9D");


        }

        public override void Load(Content.LoadManager content)
        {
            base.Load(content);

            distortEffect = MilkShake.ConentManager.Load<Effect>("Distort");
            distortTechnique = distortEffect.Techniques["Distort"];

            spriteBatch = new SpriteBatch(MilkShake.Graphics);


            PresentationParameters pp = MilkShake.Graphics.PresentationParameters;
            int width = pp.BackBufferWidth;
            int height = pp.BackBufferHeight;
            SurfaceFormat format = pp.BackBufferFormat;
            DepthFormat depthFormat = pp.DepthStencilFormat;

            distortionMap = new RenderTarget2D(MilkShake.Graphics, width, height, false, format, depthFormat);
            targetMap = new RenderTarget2D(MilkShake.Graphics, width, height, false, format, depthFormat);

            targetX = Globals.Random.Next(0, 0);
        }

        int targetX = 0;

        float currentHeight = 0;
        public override void Draw()
        {
            Awesmedraw();
            //base.Draw();
        }

        public void Awesmedraw()
        {
            Scene.RenderManager.End();
            // Render Displacement Map
            MilkShake.Graphics.SetRenderTarget(distortionMap);
            MilkShake.Graphics.Clear(new Color(128, 128, 0));
            //MilkShake.Graphics.Clear(Color.Transparent);

            currentHeight += 10;

            if (currentHeight > 720) currentHeight -= 720;
            // Needed?
            MilkShake.Graphics.DepthStencilState = DepthStencilState.Default;

            Scene.RenderManager.Begin();

            Color col = Color.White;


            float numabh = MathHelper.Clamp(((float)MouseInput.Y / 720), 0, 1);
            Console.WriteLine(numabh);
            col.A = (byte)(numabh * 255);

            base.Draw();

            Scene.RenderManager.Draw(new Vector2(getMultipleOfTwo(targetX), getMultipleOfTwo((int)currentHeight)), testImage, testImage.Width - 1, 720, 0, Vector2.Zero, col);
            Scene.RenderManager.Draw(new Vector2(getMultipleOfTwo(targetX), getMultipleOfTwo((int)currentHeight) - 720), testImage, testImage.Width - 1, 720, 0, Vector2.Zero, col);
            Scene.RenderManager.End();


            // Re-Render Screen
            MilkShake.Graphics.SetRenderTarget(targetMap);

            MilkShake.Graphics.Textures[1] = distortionMap;
            MilkShake.Graphics.SamplerStates[1] = SamplerState.PointClamp;
            Viewport viewport = MilkShake.Graphics.Viewport;
            distortEffect.CurrentTechnique = distortTechnique;

            DrawFullscreenQuad(Scene.RenderTarget, viewport.Width, viewport.Height, distortEffect);
            if (KeyboardInput.isKeyDown(Microsoft.Xna.Framework.Input.Keys.F1)) DrawFullscreenQuad(distortionMap, viewport.Width, viewport.Height, null);

            MilkShake.Graphics.SetRenderTarget(Scene.RenderTarget);
            // [Debug] Render distortion map
            if (_showDistortionMap || true)
            {
                Scene.RenderManager.RawBegin();
                Scene.RenderManager.RawDraw(Vector2.Zero, targetMap, 1280, 720, Color.White);
                Scene.RenderManager.End();
            }

            Scene.RenderManager.Begin();
        }

        private int getMultipleOfTwo(int a)
        {
            int factor = 2;
            int nearestMultiple = (int)Math.Round((a / (double)factor)) * factor;

            return nearestMultiple;
        }

        void DrawFullscreenQuad(Texture2D texture, int width, int height, Effect effect)
        {
            spriteBatch.Begin(0, BlendState.NonPremultiplied, null, null, null, effect);
            spriteBatch.Draw(texture, new Rectangle(0, 0, width, height),Scene.Color);
            spriteBatch.End();
        }

        public bool ShowDistortionMap { get { return _showDistortionMap; } set { _showDistortionMap = value; } }
    }
}