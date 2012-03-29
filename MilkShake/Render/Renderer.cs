using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Cameras;
using MilkShakeFramework.Core.Game;

namespace MilkShakeFramework.Render
{
    public class Renderer : Entity
    {
        // [Helpers]
        public RenderManager RenderManager { get { return Scene.RenderManager; } }
        public Camera CurrentCamera { get { return Scene.Camera; } }
    }
}
