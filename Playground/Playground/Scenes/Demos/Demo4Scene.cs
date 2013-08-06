using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Components.Physics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using MilkShakeFramework.Tools.Physics;
using MilkShakeFramework;
using MilkShakeFramework.IO.Input.Devices;
using Microsoft.Xna.Framework.Input;
using MilkShakeFramework.Components.Lighting;
using MilkShakeFramework.Components.Lighting.Lights;
using MilkShakeFramework.Core.Game;

namespace Samples.Scenes.Demos
{
    public class Demo4Scene : DemoScene
    {
        private PointLight light;

        public Demo4Scene() : base("", "")
        {
            ClearColor = Color.Tomato;
            //ComponentManager.AddComponent(new PhysicsComponent(new Vector2(0, 32)) { DrawDebug = true });
            ComponentManager.AddComponent(new LightingComponent());

            AddNode(new Sprite("Scenes//Global//background"));
        }

        public override void FixUp()
        {
            base.FixUp();

            light = new PointLight(500);

            AddNode(light);
            /*
            Body floor = BodyFactory.CreateRectangle(PhysicsComponent.GetInstance().World, 20, 0.5f, 1);
            floor.Position = ConvertUnits.ToSimUnits(Globals.ScreenCenter);*/
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            /*
            if (PadInput.GetPad(PlayerIndex.One).isButtonPressed(Buttons.A))
            {
                Body body = BodyFactory.CreateCircle(PhysicsComponent.GetInstance().World, 0.5f, 2);
                body.Position = ConvertUnits.ToSimUnits(Globals.ScreenCenter - new Vector2((float)Globals.Random.Next(-10, 10), 200));
                body.Restitution = 0.8f;
                body.BodyType = BodyType.Dynamic;
            }
            */
            light.Position += PadInput.GetPad(PlayerIndex.One).ThumbSticks.Left;

            if (MouseInput.isRightClicked())
            {
                AddNode(new PointLight(250) { Position = MouseInput.Position, Color = Color.Blue, Intensity = 1 });
            }
        }
    }
}
