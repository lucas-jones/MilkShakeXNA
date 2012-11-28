using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Core.Game.Components.Water
{
    public class WaterModifier : GameEntity
    {
        public override void SetParent(ITreeNode parent)
        {
            if (!(parent is Water)) throw new Exception("Parent must be of type Water");

            base.SetParent(parent);
        }

        public Water Water { get { return (Water)Parent; } }
    }
}
