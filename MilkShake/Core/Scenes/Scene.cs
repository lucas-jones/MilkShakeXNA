using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Cameras;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Render;

namespace MilkShakeFramework.Core.Scenes
{
    public delegate void EntityEvent(Entity node);

    public class Scene : GameEntity
    {
        private RenderManager mRenderManager;
        private CameraManager mCameraManager;
        private LoadManager mLoadManager;

        public Scene()
        {
            SetScene(this);

            mLoadManager = new LoadManager(this);
            mCameraManager = new CameraManager(this);
            mRenderManager = new RenderManager(this);
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
            RenderManager.Begin();
            base.Draw();
            RenderManager.End();
        }

        // [Public]
        public CameraManager CameraManager { get { return mCameraManager; } }
        public LoadManager ContentManager { get { return mLoadManager; } }

        public Camera Camera { get { return mCameraManager.CurrentCamera; } }
        public RenderManager RenderManager { get { return mRenderManager; } }

        internal void LoadScene()
        {
            Load(ContentManager);
        }
    }
}
