using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework.Core.Scenes;

namespace MilkShakeFramework.Core.Content
{
    public class LoadManager : SceneComponent
    {
        private Queue<Entity> mLoadQueue;
        private bool mSceneLoaded;

        public LoadManager(Scene scene) : base(scene)
        {
            mLoadQueue = new Queue<Entity>();
            mSceneLoaded = false;

            Scene.OnEntityAdded += new EntityEvent(SceneOnEntityAdded);
        }

        private void SceneOnEntityAdded(Entity node)
        {
            mLoadQueue.Enqueue(node);
        }

        public void LoadScene()
        {
            while(mLoadQueue.Count > 0)
            {
                LoadEntity(mLoadQueue.Dequeue());
            }

            mSceneLoaded = true;
        }

        private void OnDemandLoad()
        {
            LoadEntity(mLoadQueue.Dequeue());
        }

        public void Update()
        {
            if (mLoadQueue.Count > 0) OnDemandLoad();
        }

        private void LoadEntity(Entity entity)
        {
            entity.Load(this);
            entity.FixUp();
        }

        // [Public]
        public bool IsLoaded { get { return mSceneLoaded; } }
    }
}
