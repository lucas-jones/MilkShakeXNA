using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Scenes;

namespace MilkShakeFramework.Core.Game
{
    public class GameEntityListener
    {
        public event UpdateEvent Update;

        public void OnUpdate(GameTime gameTime)
        {
            if (Update != null) Update(gameTime);
        }
    }
}
