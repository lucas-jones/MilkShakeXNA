using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Factories;
using MilkShakeFramework.Components.Physics;
using MilkShakeFramework.Core.Scenes.Components;
using FarseerPhysics.Dynamics;
using MilkShakeFramework.Tools.Physics;
using Microsoft.Xna.Framework;
using FarseerPhysics;

namespace MilkShakeFramework.Core.Game.Components.Water.Modify
{
    public class TriggerWaterModifier : WaterModifier
    {
        private Body _triggerBox;

        public override void FixUp()
        {
            base.FixUp();

            // Create collision box for detecting objects hitting the water
            _triggerBox = BodyFactory.CreateRectangle(PhysicsComponent.GetInstance().World, ConvertUnits.ToSimUnits(Water.Width),
                                                                                 ConvertUnits.ToSimUnits(Water.Height), 1);

            _triggerBox.Position = ConvertUnits.ToSimUnits(Water.Position + new Vector2(Water.Width / 2, Water.Height / 2));

            _triggerBox.UserData = "Water";
            _triggerBox.BodyType = BodyType.Static;
            _triggerBox.IsSensor = true;
        }
    }
}
