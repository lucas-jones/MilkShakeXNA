using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Krypton;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Cameras;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Scenes.Components;

namespace MilkShakeFramework.Components.Lighting
{
    public class LightingComponent : SceneComponent
    {
        private KryptonEngine mLight;
        private Matrix mMatrix;

        public LightingComponent(Scene mScene, LightMapSize mLightMapSize = LightMapSize.Full, int mBluriness = 0) : base(mScene)
        {
            mLight = new KryptonEngine(MilkShake.Game, "KryptonEffect");
            mLight.Initialize();
            
            // [Settings]
            mLight.LightMapSize = mLightMapSize;
            mLight.Bluriness = mBluriness;

            // [Events]
            Scene.Listener.PreDraw[DrawLayer.First] += new DrawEvent(PreDraw);
            Scene.Listener.PostDraw[DrawLayer.Fourth] += new DrawEvent(PostDraw);
        }

        private void PreDraw()
        {
            GenerateMatrix(); // Cache?

            mLight.AmbientColor = Color.Transparent;
            
            mLight.SpriteBatchCompatablityEnabled = false;
            mLight.Matrix = mMatrix;

            mLight.LightMapPrepare();
        }

        private void PostDraw()
        {
            mLight.Draw(null);
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

        public KryptonEngine Light { get { return mLight; } }
    }
}
