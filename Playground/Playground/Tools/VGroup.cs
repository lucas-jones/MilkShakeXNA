using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework;

namespace Playground.Tools
{
    public class VGroup : GameEntity
    {

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            int currentY = 0;

            foreach (Sprite child in Nodes)
            {
                child.Position = WorldPosition + new Vector2(0, currentY);

                currentY += 5 + child.Height; //Gap
            }
            
            base.Update(gameTime);
        }

 

    }
}
