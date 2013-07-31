using System;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.IO.Input.Devices;
using MilkShakeFramework.Render;
using MilkShakeFramework.Tools.Maths;

namespace MilkShakeFramework.Components.PostProccessing.Presets
{
    //
    // DistortionLayer!!!!! Add entitys to it
    // This as an entity = multiple layers of distortion on different depths
    // Which entitys can get added to for saving
    //
    public class WaterFall : GameEntity
    {
        private ImageRenderer _waterImage;

        public int XCount { get; set; }
        public int YCount { get; set; }

        public float Speed { get; set; }

        public float ScaleX { get; set; }
        public float ScaleY { get; set; }

        public WaterFall()
        {
            AddNode(_waterImage = new ImageRenderer("Scene//Levels//Images//waterfall_distort"));

            XCount = 1;
            YCount = 2;

            Speed = 10;

            //Width = 241;
            //Height = 48;

            ScaleX = 1;
            ScaleY = 1;
        }

        public override void FixUp()
        {
            base.FixUp();

            
        }

        private float yOffset;

        public override void Update(GameTime gameTime)
        {
            yOffset += Speed;

            if (yOffset > (48 * ScaleY))
            {
                yOffset = 0;
            }

            base.Update(gameTime);
        }

        public override void Draw()
        {
            for (int Y = 0; Y < YCount; Y++)
            {
                Vector2 offset = WorldPosition + new Vector2(0, Y * (48 * ScaleY) + yOffset - ((48 * ScaleY) * (YCount / 2)));

                _waterImage.Draw(offset, _waterImage.Texture.Width, _waterImage.Texture.Height, 0, Vector2.Zero, Color.White, ScaleX, ScaleY);    
            }
        }

        public override RotatedRectangle BoundingBox
        {
            get
            {
                return new RotatedRectangle((int)WorldPosition.X, (int)WorldPosition.Y, (int)(241 * ScaleX), (int)(48 * ScaleY));
            }
        }
    }

    public class DistortionLayer : PostProcessingEffect
    {
        private ImageRenderer _image;
        private SpriteBatch spriteBatch;
        private RenderTarget2D distortionMap;
        private RenderTarget2D targetMap;
        private Texture2D testImage;

        private bool _showDistortionMap;

        Effect distortEffect;
        EffectTechnique distortTechnique;
        private const float blurAmount = 20f;

        public DistortionLayer()  : base()
        {
        }

        public override void Load(Core.Content.LoadManager content)
        {
            base.Load(content);

            distortEffect = MilkShake.ConentManager.Load<Effect>("Scene//Levels//Effects//Distort");
            distortTechnique = distortEffect.Techniques["Distort"];

            spriteBatch = new SpriteBatch(MilkShake.Graphics);

            PresentationParameters pp = MilkShake.Graphics.PresentationParameters;
            int width = pp.BackBufferWidth;
            int height = pp.BackBufferHeight;
            SurfaceFormat format = pp.BackBufferFormat;
            DepthFormat depthFormat = pp.DepthStencilFormat;

            distortionMap = new RenderTarget2D(MilkShake.Graphics, width, height, false, format, depthFormat);
            targetMap = new RenderTarget2D(MilkShake.Graphics, width, height, false, format, depthFormat);

            targetX = 2500;
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

            Scene.RenderManager.Begin();
            base.Draw();
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
                Scene.RenderManager.RawDraw(Vector2.Zero, targetMap, Globals.ScreenWidth, Globals.ScreenHeight, Color.White);
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
            spriteBatch.Draw(texture, new Rectangle(0, 0, width, height),Scene.SceneColor);
            spriteBatch.End();
        }

        public bool ShowDistortionMap { get { return _showDistortionMap; } set { _showDistortionMap = value; } }
    }
}