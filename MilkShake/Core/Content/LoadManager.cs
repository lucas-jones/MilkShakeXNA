using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Scenes.Components;

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

                     
        }

        public void SceneLoaded()
        {
            mSceneLoaded = true;

            // Start listening to run time loads
            Scene.Listener.EntityAdded += new EntityEvent(SceneOnEntityAdded);   
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

        private void DynamicLoad()
        {
            LoadEntity(mLoadQueue.Dequeue());
        }

        public void Update()
        {
            if (mLoadQueue.Count > 0) DynamicLoad();
        }

        private void LoadEntity(Entity entity)
        {
            //Console.WriteLine("[Dynamic Load] " + entity.GUID);

            if (!entity.IsLoaded)
            {
                entity.Setup();
                entity.Load(this);
                entity.FixUp();
            }
        }

        // [Public]
        public bool IsLoaded { get { return mSceneLoaded; } }

    }
}
