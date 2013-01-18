using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Core.Game.Components.Misc
{
    public class Layer : GameEntity
    {
        public Layer() : base() { }
        public Layer(List<GameEntity> nodes) : base() { nodes.ForEach(node => AddNode(node)); }

        public GameEntity this[String key]
        {
            get
            {
                return (GameEntity)GetNodeByName(key);
            }
        }
    }
}
