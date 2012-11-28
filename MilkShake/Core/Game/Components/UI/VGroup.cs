using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Core.Game.Components.UI
{
    public class VGroup : GameEntity
    {
        private int _gap;

        public VGroup(int gap = 0)
        {
            _gap = gap;
        }

        public override void Update(GameTime gameTime)
        {
            int currentY = 0;

            foreach (Sprite child in Nodes)
            {
                child.Position = WorldPosition + new Vector2(0, currentY);

                currentY += _gap + child.Height;
            }

            base.Update(gameTime);
        }
    }
}
