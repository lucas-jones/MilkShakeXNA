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

        public void Load()
        {
            foreach (Entity entity in mLoadQueue)
            {
                entity.Load(this);
                entity.FixUp();
            }

            mSceneLoaded = true;
        }

        // [Public]
        public bool IsLoaded { get { return mSceneLoaded; } }
    }
}
