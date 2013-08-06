using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework;
using Microsoft.Xna.Framework;

namespace Samples.Scenes.Demos
{
    public class Demo3Scene : DemoScene
    {
        public Demo3Scene() : base("", "")
        {
            GameEntity milkshakeLogo = new GameEntity()
            {
                Position = Globals.ScreenCenter
            };

            milkshakeLogo.AddNode(new Sprite("Scenes//Global//MilkShakeLight")
            {
                Origin = new Vector2(270, 290)
            });

            milkshakeLogo.AddNode(new Sprite("Scenes//Global//MilkShakeLogo")
            {
                AutoCenter = true
            });

            AddNode(milkshakeLogo);
            
            AddNode(new Sprite("Scenes//Global//Gunter")
            {
                Position = new Vector2(1000, 550)
            });
        }
    }
}
