using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Core.Game.Components.UI
{
    public class HGroup : GameEntity
    {
        private int _gap;

        public HGroup(int gap = 0)
        {
            _gap = gap;

           
        }

        public override void FixUp()
        {
            base.FixUp();

            int currentX = 0;

            foreach (GameEntity child in Nodes)
            {
                child.X = WorldPosition.X + currentX;
                child.Y = WorldPosition.Y;
                currentX += _gap + child.BoundingBox.Width;
            }
        }
    }
}
