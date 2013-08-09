using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework.Tools.Tween;
using Microsoft.Xna.Framework;
using MilkShakeFramework;
using MilkShakeFramework.IO.Input.Devices;
using XNATweener;

namespace Samples.Scenes.Demo1
{
    public class Demo2Scene : DemoScene
    {
        private Sprite background;
        private Sprite penguin;

        public Demo2Scene() : base("Tween Example", "Click to Tween")
        {
            background = new Sprite("Scenes//Global//background");

            penguin = new Sprite("Scenes//Global//Gunter")
            {
                AutoCenter = true,
                Position = Globals.ScreenCenter
            };

            AddNode(background);
            AddNode(penguin);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (MouseInput.isLeftClicked())
            {
                TweenerManager.AddTween(new Vector2Tweener(penguin.Position, MouseInput.WorldPosition, 2, XNATweener.Quartic.EaseInOut), (position) => penguin.Position = position);
            }
        }

        public override void OnEnterSceen()
        {
            MilkShake.Game.IsMouseVisible = true;
        }

        public override void OnExitSceen()
        {
            MilkShake.Game.IsMouseVisible = false;
        }
    }
}
