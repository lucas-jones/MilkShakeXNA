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
        public event GameEntityEvent Setup;
        public event GameEntityEvent Load;
        public event GameEntityEvent Fixup;

        public void OnUpdate(GameTime gameTime)
        {
            if (Update != null) Update(gameTime);
        }

        public void OnSetup()
        {
            if (Setup != null) Setup();
        }

        public void OnLoad()
        {
            if (Load != null) Load();
        }

        public void OnFixup()
        {
            if (Fixup != null) Fixup();
        }



    }
}
