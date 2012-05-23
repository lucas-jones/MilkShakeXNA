using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Scenes.Components;
using MilkShakeFramework.Core.Scenes;

namespace MilkShakeFramework.Components.Effects
{
    public class EffectsComponent : SceneComponent
    {
        public EffectsComponent(Scene scene) : base(scene)
        {
  
        }

        public void AddEffect(Effect effect)
        {
            // Clean up, Nodes needed to be added to stage to fixup/load
            Scene.AddNode(effect);
        }
    }
}
