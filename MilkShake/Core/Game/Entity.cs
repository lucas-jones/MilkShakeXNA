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

        public Entity() { }

        public override void AddNode(INode node)
        {
            base.AddNode(node);

            if (node is Entity)
            {
                Entity entity = (node as Entity);
                entity.SetScene(mScene);
                mScene.EntityAdded(entity);
            }
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
        }

        public virtual void FixUp()
        {

        }

        // [Public]
        public Scene Scene { get { return mScene; } }
    }
}
