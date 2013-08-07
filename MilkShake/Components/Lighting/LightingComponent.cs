using Krypton;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Scenes.Components;
using Microsoft.Xna.Framework.Graphics;

namespace MilkShakeFramework.Components.Lighting
{
    public class LightingComponent : SceneComponent
    {
        private KryptonEngine mLight;
        private Matrix mMatrix;
        private LightingMode mLightingMode;
        private RenderTarget2D mRenderTarget;

        public LightingComponent(LightingMode lightingMode = LightingMode.Knockout, LightMapSize mLightMapSize = LightMapSize.Full, int mBluriness = 0)
        {
            mLight = new KryptonEngine(MilkShake.Game, "KryptonEffect");
            mLight.Initialize();
            
            // [Settings]
            mLight.LightMapSize = mLightMapSize;
            mLight.Bluriness = mBluriness;
            mLightingMode = lightingMode;

            mRenderTarget = new RenderTarget2D(MilkShake.Graphics, Globals.ScreenWidth, Globals.ScreenHeight);
        }

        public override void FixUp()
        {
            base.FixUp();

            // [Events]
            Scene.Listener.PreDraw[DrawLayer.First] += new DrawEvent(PreDraw);
            Scene.Listener.PostDraw[DrawLayer.Fourth] += new DrawEvent(PostDraw);
        }

        private void PreDraw()
        {
            GenerateMatrix(); // Cache?

            mLight.Matrix = mMatrix;

            mLight.LightMapPrepare();
        }

        private void PostDraw()
        {
            if (mLightingMode == LightingMode.Knockout) KnockoutDraw();
            if (mLightingMode == LightingMode.Overlay) OverlayDraw();
        }

        private void KnockoutDraw()
        {
            mLight.Draw(null);
        }

        private void OverlayDraw()
        {
            MilkShake.Graphics.SetRenderTarget(mRenderTarget);
            
            mLight.Draw(null);
            MilkShake.Graphics.SetRenderTarget(Scene.RenderTarget);
            
            Scene.RenderManager.Begin(BlendState.Additive);
            Scene.RenderManager.RawDraw(Vector2.Zero, mRenderTarget, Globals.ScreenWidth, Globals.ScreenHeight, Color.White);
            Scene.RenderManager.End();
        }

        private void GenerateMatrix()
        {
            mMatrix = GetViewMatrix() * GetProjectionMatrix();
        }

        private Matrix GetViewMatrix()
        {
            Matrix view = Matrix.Identity;

            float xTranslation = -1 * (Scene.Camera.Position.X);
            float yTranslation = 1 * (Scene.Camera.Position.Y);

            view = Matrix.CreateTranslation(xTranslation, yTranslation, 0) * Matrix.CreateRotationZ(-MathHelper.ToRadians(Scene.Camera.Rotation));

            return view;
        }

        private Matrix GetProjectionMatrix()
        {
            Matrix projection = Matrix.Identity;

            float zoom = Scene.Camera.Zoom;
            float width = (1f / zoom) * Globals.ScreenWidth;
            float height = (1f / zoom) * Globals.ScreenHeight;
            float zNearPlane = 0f;
            float zFarPlane = 1000000f;
            
            projection = Matrix.CreateOrthographic(width, height, zNearPlane, zFarPlane);

            return projection;  

        }

        public KryptonEngine Krypton { get { return mLight; } }
    }
}
