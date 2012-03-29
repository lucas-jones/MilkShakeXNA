using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Core.Scenes
{
    public class SceneComponent
    {
        private Scene mScene;

        public SceneComponent(Scene scene)
        {
            mScene = scene;
        }

        // [Public]
        public Scene Scene { get { return mScene; } }
    }
}
