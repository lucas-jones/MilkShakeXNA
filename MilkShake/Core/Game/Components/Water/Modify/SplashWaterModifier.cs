using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using MilkShakeFramework.Components.Physics;
using MilkShakeFramework.Tools.Physics;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Scenes.Components;
using FarseerPhysics.Dynamics.Contacts;

namespace MilkShakeFramework.Core.Game.Components.Water.Modify
{
    public class SplashWaterModifier : WaterModifier
    {
        private float _splashPower;
        private float _splashMinSpeed;

        private Body _splashTrigger;

        public SplashWaterModifier(float splashPower = 1, float splashMinSpeed = 10)
        {
            _splashPower = splashPower;
            _splashMinSpeed = splashMinSpeed;
        }

        public override void FixUp()
        {
            base.FixUp();

            // Create collision box for detecting objects hitting the water
            _splashTrigger = BodyFactory.CreateRectangle(PhysicsComponent.World, ConvertUnits.ToSimUnits(Water.Width),
                                                                                 ConvertUnits.ToSimUnits(10), 0.5f);

            _splashTrigger.Position = ConvertUnits.ToSimUnits(Water.Position + new Vector2(Water.Width / 2, -10));

            _splashTrigger.BodyType = BodyType.Static;
            _splashTrigger.IsSensor = true;
            _splashTrigger.OnCollision += new OnCollisionEventHandler(onBodyHitWater);
            _splashTrigger.UserData = this;
        }

        private bool onBodyHitWater(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            Vector2 collisionPosition = ConvertUnits.ToDisplayUnits(fixtureB.Body.WorldCenter);

            // Check fall speed
            if (fixtureB.Body.LinearVelocity.Y > _splashMinSpeed)
            {
                Water.Splash(collisionPosition.X - Water.Position.X, -_splashPower);
            }

            return false;
        }

        private PhysicsComponent PhysicsComponent { get { return Scene.ComponentManager.GetComponent<PhysicsComponent>(); } }
    }
}
