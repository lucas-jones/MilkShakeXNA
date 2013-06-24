using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Content;

namespace MilkShakeFramework.Core.Game
{
    public class Entity : TreeNode
    {
        private Scene mScene;
        private Boolean mIsLoaded;

        public Entity() { }

        public override void AddNode(INode node, int index = -1)
        {
            if (node is Entity)
            {
                Entity entity = (node as Entity);

                if (mScene != null)
                {
                    entity.SetScene(mScene);
                }
                else
                {
                    // [ToDo] Should AddNodes be allowed in contructor.. This causes an issue where the Scene is not set untill after it created;
                    // so the entity never calls OnEntityAdded -> Scene never finds out about this eneity.

                    // Maybe parent isn't added to scene!

                    //throw new Exception("Added node in constructor!");
                    Console.WriteLine("Warning: " + entity.Name + " [" + entity.GetType().Name + "] Added nodes before Scene was set!");
                }
            }

            base.AddNode(node, index);

            if (node is Entity)
            {
                if (mScene != null)
                {
                    mScene.Listener.OnEntityAdded(node as Entity);
                }
            }
        }

        public override void RemoveNode(INode gameObject)
        {
            base.RemoveNode(gameObject);

            mScene.Listener.OnEntityRemoved(gameObject as Entity);
        }

        public void SetScene(Scene scene)
        {
            mScene = scene;

            foreach (Entity entity in Nodes) entity.SetScene(scene);
        }

        public virtual void Setup()
        {
            foreach (Entity entity in Nodes.ToArray()) entity.Setup();
        }

        public virtual void Load(LoadManager content)
        {
            foreach (Entity entity in Nodes) entity.Load(content);
        }

        public virtual void FixUp()
        {
            foreach (Entity entity in Nodes.ToArray()) entity.FixUp();

            mIsLoaded = true;
        }

        public virtual void TearDown()
        {
            foreach (Entity entity in Nodes.ToArray()) entity.TearDown();
        }

        // [Public]
        public Scene Scene { get { return mScene; } }
        public Boolean IsLoaded { get { return mIsLoaded; } set { mIsLoaded = value; } }
    }
}
