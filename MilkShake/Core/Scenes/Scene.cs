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

namespace MilkShakeFramework.Core.Scenes
{
    public delegate void EntityEvent(Entity node);

    public class Scene : GameEntity
    {
        private RenderManager mRenderManager;
        private CameraManager mCameraManager;
        private LoadManager mLoadManager;

        private RenderTarget2D mRenderTarget;
        private int mRenderWidth, mRenderHeight;
        private int mWidth, mHeight;

        public Scene()
        {
            SetScene(this);

            mLoadManager = new LoadManager(this);
            mCameraManager = new CameraManager(this);
            mRenderManager = new RenderManager(this);
        }

        public override void Setup()
        {
            mWidth = (mWidth == 0) ? Globals.ScreenWidth : mWidth;
            mHeight = (mHeight == 0) ? Globals.ScreenHeight : mHeight;

            mRenderWidth = (mRenderWidth == 0) ? Globals.ScreenWidth : mRenderWidth;
            mRenderHeight = (mRenderHeight == 0) ? Globals.ScreenHeight : mRenderHeight;

            mRenderTarget = new RenderTarget2D(MilkShake.Graphics, mRenderWidth, mRenderHeight); // Setup

            base.Setup();
        }

        public override void Update(GameTime gameTime)
        {
            mLoadManager.Update();
            mCameraManager.Update(gameTime);
            base.Update(gameTime);
        }

        // [Events]
        public event EntityEvent OnEntityAdded;
        public event EntityEvent OnEntityRemoved;

        public void EntityAdded(Entity entity)
        {
            if (OnEntityAdded != null) OnEntityAdded(entity);
        }

        public void EntityRemoved(Entity entity)
        {
            if (OnEntityRemoved != null) OnEntityRemoved(entity);
        }

        public override void Draw()
        {
            RenderScene(); // Draws to Target Renderer
            DrawScene(); // Draws Scene on screen
        }

        public void RenderScene()
        {
            RenderManager.SetRenderTarget(mRenderTarget);
            RenderManager.Begin();
            base.Draw();
            RenderManager.End();
            RenderManager.SetRenderTarget(null);
        }

        private void DrawScene()
        {
            RenderManager.RawBegin();
            RenderManager.RawDraw(Position, mRenderTarget, mWidth, mHeight);
            RenderManager.End();
        }

        // [Public]
        public CameraManager CameraManager { get { return mCameraManager; } }
        public LoadManager ContentManager { get { return mLoadManager; } }

        public Camera Camera { get { return mCameraManager.CurrentCamera; } }
        public RenderManager RenderManager { get { return mRenderManager; } }

        public int RenderWidth { get { return mRenderWidth; } set { mRenderWidth = value; } }
        public int RenderHeight { get { return mRenderHeight; } set { mRenderHeight = value; } }

        public int Width { get { return mWidth; } set { mWidth = value; } }
        public int Height { get { return mHeight; } set { mHeight = value; } }
        
        internal void LoadScene()
        {
            Load(ContentManager);
        }
    }
}
