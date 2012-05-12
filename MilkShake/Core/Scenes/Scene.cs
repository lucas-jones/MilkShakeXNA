using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Cameras;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Render;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.DebugViews;
using FarseerPhysics;
using Krypton;
using MilkShakeFramework.Tools.Physics;
using MilkShakeFramework.Core.Scenes.Components;
using MilkShakeFramework.IO.Input.Devices;

namespace MilkShakeFramework.Core.Scenes
{

    public class Scene : GameEntity
    {
        private SceneListener mSceneListener;

        private RenderManager mRenderManager;
        private CameraManager mCameraManager;
        private LoadManager mLoadManager;   

        private SceneComponentManager mComponentManager;

        private RenderTarget2D mRenderTarget;
        private int mRenderWidth, mRenderHeight;
        private int mWidth, mHeight;

        public Scene()
        {
            SetScene(this);

            mSceneListener = new SceneListener();

            mLoadManager = new LoadManager(this);
            mCameraManager = new CameraManager(this);
            mRenderManager = new RenderManager(this);

            mComponentManager = new SceneComponentManager();

            
            ConvertUnits.SetDisplayUnitToSimUnitRatio(24f);
            
        }

        public override void Setup()
        {
            mWidth = SetValueOrDefault(mWidth, Globals.ScreenWidth );
            mHeight = SetValueOrDefault(mHeight, Globals.ScreenHeight);
            mRenderWidth = SetValueOrDefault(mRenderWidth, Globals.ScreenWidth);
            mRenderHeight = SetValueOrDefault(mRenderHeight, Globals.ScreenHeight);

            mRenderTarget = new RenderTarget2D(MilkShake.Graphics, mRenderWidth, mRenderHeight); // Setup

            base.Setup();
        }

        internal void LoadScene()
        {
            Load(ContentManager);
            mLoadManager.SceneLoaded();     
        }

        public override void Update(GameTime gameTime)
        {
            mLoadManager.Update();
            mCameraManager.Update(gameTime);
            Listener.OnUpdate(gameTime);

            base.Update(gameTime);
        }

        public override void Draw()
        {
            RenderScene(); // Draws to Target Renderer
            DrawScene();   // Draws Scene on screen            
        }

        public void RenderScene()
        {
            RenderManager.SetRenderTarget(mRenderTarget);
            mSceneListener.OnPreDraw();
            RenderManager.Begin();            
            base.Draw();
            RenderManager.End();
            mSceneListener.OnPostDraw();
            RenderManager.SetRenderTarget(null);
        }

        private void DrawScene()
        {
            RenderManager.RawBegin();
            RenderManager.RawDraw(Position, mRenderTarget, mWidth, mHeight);
            RenderManager.End();
        }

        // [Public]
        public SceneListener Listener { get { return mSceneListener; } }
        
        public CameraManager CameraManager { get { return mCameraManager; } }
        public LoadManager ContentManager { get { return mLoadManager; } }



        // Component
        public SceneComponentManager ComponentManager { get { return mComponentManager; } }

        public Camera Camera { get { return mCameraManager.CurrentCamera; } }
        public RenderManager RenderManager { get { return mRenderManager; } }

        public int RenderWidth { get { return mRenderWidth; } set { mRenderWidth = value; } }
        public int RenderHeight { get { return mRenderHeight; } set { mRenderHeight = value; } }

        public int Width { get { return mWidth; } set { mWidth = value; } }
        public int Height { get { return mHeight; } set { mHeight = value; } }
    }
}
