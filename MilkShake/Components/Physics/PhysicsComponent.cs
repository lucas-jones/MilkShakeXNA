using System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Scenes.Components;
using MilkShakeFramework.Tools.Physics;
using FarseerPhysics;
using FarseerPhysics.DebugViews;

namespace MilkShakeFramework.Components.Physics
{
    public class PhysicsComponent : SceneComponent
    {
        public static PhysicsComponent GetInstance()
        {
            return SceneManager.CurrentScene.ComponentManager.GetComponent<PhysicsComponent>(); ;
        }

        private World mWorld;
        private DebugViewXNA mDebugView;

        private Matrix mProjection;
        private Matrix mView;

        private Vector2 mGravity;
        private bool mCameraRotationGravity;

        public PhysicsComponent(Vector2 aGravity)
        {
            ConvertUnits.SetDisplayUnitToSimUnitRatio(24f);

            //lucas smells
            mGravity = aGravity;
            Multiplier = 1;
            mWorld = new World(mGravity);

            mDebugView = new DebugViewXNA(mWorld);
            mDebugView.DefaultShapeColor = Color.White;
            mDebugView.SleepingShapeColor = Color.LightGray;

            mDebugView.LoadContent(MilkShake.Graphics, MilkShake.ConentManager);

            // 
            Settings.AllowSleep = false;
        }

        public override void FixUp()
        {
            base.FixUp();

            Scene.Listener.Update += new UpdateEvent(Update);
            Scene.Listener.PostDraw[DrawLayer.First] += new DrawEvent(Draw);
        }

        public override void Update(GameTime gameTime)
        {
            if (mCameraRotationGravity) World.Gravity = GravityFromCameraAngle(mGravity);

            // [Update Physics]
            float elapsedTime = Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f));
            mWorld.Step(elapsedTime * Multiplier);
            Console.WriteLine(Multiplier);
        }

        public override void Draw()
        {
            mView = GetViewMatrix();
            mProjection = GetProjectionMatrix();
            mDebugView.RenderDebugData(ref mProjection, ref mView);
        }

        private Matrix GetViewMatrix()
        {
            Matrix view = Matrix.Identity;

            float xTranslation = -1 * ConvertUnits.ToSimUnits(Scene.Camera.Position.X + Globals.ScreenWidthCenter);
            float yTranslation = -1 * ConvertUnits.ToSimUnits(Scene.Camera.Position.Y + Globals.ScreenHeightCenter);
            Vector3 translationVector = new Vector3(xTranslation, yTranslation, 0f);
            view = Matrix.Identity;
            view.Translation = translationVector;
            view *= Matrix.CreateRotationZ(MathHelper.ToRadians(Scene.Camera.Rotation));
            return view;
        }

        private Matrix GetProjectionMatrix()
        {
            Matrix projection = Matrix.Identity;

            float zoom = Scene.Camera.Zoom;
            float width = (1f / zoom) * ConvertUnits.ToSimUnits(Globals.ScreenWidth);
            float height = (-1f / zoom) * ConvertUnits.ToSimUnits(Globals.ScreenHeight);
            float zNearPlane = 0f;
            float zFarPlane = 1000000f;

            projection = Matrix.CreateOrthographic(width, height, zNearPlane, zFarPlane);

            return projection;
        }

        private Vector2 GravityFromCameraAngle(Vector2 _gravity)
        {
            float cameraRotation = MathHelper.ToRadians(Scene.Camera.Rotation);

            Vector2 newGravity = new Vector2(_gravity.X * (float)Math.Cos(cameraRotation) + _gravity.Y * (float)Math.Sin(cameraRotation),
                                             _gravity.X * (float)Math.Sin(cameraRotation) + _gravity.Y * (float)Math.Cos(cameraRotation));


            return newGravity;
        }

        public World World { get { return mWorld; } }
        public bool CameraRotationGravity { get { return mCameraRotationGravity; } set { mCameraRotationGravity = value; } }
        public bool DrawDebug { get { return mDebugView.Enabled; } set { mDebugView.Enabled = value; } }
        public float Multiplier { get; set; }
    }
}