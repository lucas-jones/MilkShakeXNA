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

        public override void AddNode(INode node)
        {
            if (node is Entity)
            {
                Entity entity = (node as Entity);

                entity.SetScene(mScene);
                if(mScene != null) mScene.EntityAdded(entity);
            }

            base.AddNode(node);
        }

        public void SetScene(Scene scene)
        {
            mScene = scene;

            foreach (Entity entity in Nodes) entity.SetScene(scene);
        }

        public virtual void Setup()
        {
            foreach (Entity entity in Nodes) entity.Setup();
        }

        public virtual void Load(LoadManager content)
        {
            foreach (Entity entity in Nodes) entity.Load(content);
        }

        public virtual void FixUp()
        {
            foreach (Entity entity in Nodes) entity.FixUp();
        }

        // [Public]
        public Scene Scene { get { return mScene; } }
        public Boolean IsLoaded { get { return mIsLoaded; } }
    }
}
