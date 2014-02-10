using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Filters.Presets;
using MilkShakeFramework.IO.Input.Devices;

namespace Samples.Scenes.Demo1
{
    public class Demo1Scene : DemoScene
    {
        public const string TITLE = "Sprite Demo";
        public const string DESCRIPTION = "Show the basic adding of sprites";

        private Sprite background;

        private Sprite world;
        private Sprite clouds;
        private DisplayObject planet;

        public Demo1Scene() : base(TITLE, DESCRIPTION)
        {
            background = new Sprite("Scenes//Global//background");

            world = new Sprite("Scenes//Demo1//world")
            {
                AutoCenter = true
            };

            clouds = new Sprite("Scenes//Demo1//clouds")
            {
                Origin = new Vector2(750, 750)
            };
                        
            AddNode(background);
            AddNode(planet = new DisplayObject() { Position = Globals.ScreenCenter });
            planet.AddNode(world);
            planet.AddNode(clouds);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            world.Rotation += MathHelper.ToRadians(0.5f);
            clouds.Rotation += MathHelper.ToRadians(0.2f);

            planet.Scale = new Vector2((720f / MouseInput.X) - 1);
        }
    }
}
