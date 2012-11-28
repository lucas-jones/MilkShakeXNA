using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;

namespace MilkShakeFramework.Core.Scenes.Components
{
    public class SceneComponent
    {
        private Scene mScene;

        public SceneComponent(Scene aScene)
        {
            mScene = aScene;

            // Add Events
            AddEventListeners();
        }

        private void AddEventListeners()
        {
          //  mScene.OnEntityAdded += new EntityEvent(OnEntityAdded);
        }

        public virtual void OnEntityAdded(Entity node)
        {
        }

        // [Public]
        public Scene Scene { get { return mScene; } }
    }
}
