using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Filters.Presets;

namespace Samples.Scenes.Demo1
{
    public class Demo1Scene : DemoScene
    {
        public const string TITLE = "Sprite Demo";
        public const string DESCRIPTION = "Show the basic adding of sprites";

        private Sprite background;
        private Sprite world;
        private Sprite clouds;

        public Demo1Scene() : base(TITLE, DESCRIPTION)
        {
            background = new Sprite("Scenes//Global//background");

            world = new Sprite("Scenes//Demo1//world")
            {
                AutoCenter = true,
                Position = Globals.ScreenCenter,
                Scale = new Vector2(0.25f),
            };

            clouds = new Sprite("Scenes//Demo1//clouds")
            {
                Origin = new Vector2(750, 750),
                Position = Globals.ScreenCenter,
                Scale = new Vector2(0.25f)
            };

            AddNode(background);
            AddNode(world);
            AddNode(clouds);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            world.Rotation += MathHelper.ToRadians(0.5f);
            clouds.Rotation -= MathHelper.ToRadians(0.2f);
        }
    }
}
