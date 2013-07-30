using MilkShakeFramework;
using MilkShakeFramework.Core.Scenes;
using System;
using System.CodeDom.Compiler;
using MilkShakeFramework.Core.Game;
using System.IO;
using MilkShakeFramework.Core;
using MilkShakeFramework.Core.Filters;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.IO.Input.Devices;
using System.Drawing;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Components.Physics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Controllers;
using FarseerPhysics;
using MilkShakeFramework.Core.Game.Components.Misc;
using MilkShakeFramework.Tools.Utils;

namespace Playground
{
    public class Playground : MilkShake
    {
        public Playground() : base(1280, 720) { }

        protected override void Initialize()
        {
            base.Initialize();

            SceneManager.AddScene("BasicScene", new BasicScene());
            SceneManager.ChangeScreen("BasicScene");

            
           
        }
    }

    public class BasicScene : Scene
    {


        public BasicScene() : base()
        {
            ComponentManager.AddComponent(new PhysicsComponent(new Vector2(0, 0)) { DrawDebug = true });

            AddNode(new Sprite("background"));

            AddNode(new PlanetRotate() { Position = Globals.ScreenCenter });
            //AddNode(new Planet());


        }

        

        public override void Setup()
        {
            base.Setup();

            AddNode(new CameraDebugModifier(10));
            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (MouseInput.isLeftDown())
            {
                AddNode(new Astroid() { Position = MouseInput.WorldPosition });
            }

            

            if (MouseInput.isRightClicked())
            {
                AddNode(new Planet() { Position = MouseInput.WorldPosition });
            }
        }
    }

    public class Astroid : GameEntity
    {
        public Body _body;

        public Astroid()
        {            
            _body = BodyFactory.CreateCircle(PhysicsComponent.GetInstance().World, 0.5f, 100);
            _body.Position = ConvertUnits.ToSimUnits(MouseInput.WorldPosition);
            _body.BodyType = BodyType.Dynamic;

            _body.ApplyLinearImpulse(new Vector2(1000, 1000));
        }

        public override void Update(GameTime gameTime)
        {
            _body.LinearDamping = (KeyboardInput.isKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift)) ? 10 : 1;

            base.Update(gameTime);
        }
    }

    public class PlanetRotate : Planet
    {
        float _angle = 0;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _angle += 0.1f;

            _body.Position = ConvertUnits.ToSimUnits(Globals.ScreenCenter + MathUtils.AngleToVector2(_angle, 100));
            gravityControler.Points[0] = _body.Position;
        }
    }

    public class Planet : GameEntity
    {
        public GravityController gravityControler;
        public Body _body;

        public Planet()
        {
            MilkShake.Game.IsMouseVisible = true;
        }

        public override void Setup()
        {
            // [Gravity Controller]
            gravityControler = new GravityController(700, ConvertUnits.ToSimUnits(2000), 0);
            gravityControler.Points.Add(ConvertUnits.ToSimUnits(WorldPosition));
            PhysicsComponent.GetInstance().World.AddController(gravityControler);

            // [Body]
            _body = BodyFactory.CreateCircle(PhysicsComponent.GetInstance().World, 11.8f, 100);
            _body.Position = ConvertUnits.ToSimUnits(Position);
            _body.BodyType = BodyType.Static;

            base.Setup();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

           // _body.ApplyTorque(100);
        }
    }
}

