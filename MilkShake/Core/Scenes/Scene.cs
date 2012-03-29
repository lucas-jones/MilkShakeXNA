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
    public class Scene : GameEntity
    {
        private RenderManager mRenderManager;
        private CameraManager mCameraManager;
        private LoadManager mLoadManager; // Content manager should be able to load content on fly/ingame
        private bool mIsLoaded;

        public Scene() { }
        public virtual void Setup() { }

        public virtual void LoadScene() { Load(mLoadManager); mIsLoaded = true; } // Loads all init content

        public virtual void FixUp() { } // After loading is complete
        public virtual void Draw() { }
        public virtual void Update(GameTime gameTime) { }

        // [Public]
        public CameraManager CameraManager { get { return mCameraManager; } }
        public LoadManager ContentManager { get { return mLoadManager; } }
        public bool IsLoaded { get { return mIsLoaded; } }
        public Camera Camera { get { return mCameraManager.CurrentCamera; } }
    }
}
