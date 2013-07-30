using MilkShakeFramework.Components.Physics;
using MilkShakeFramework.Tools.Physics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision;
using FarseerPhysics;

namespace MilkShakeFramework.Core.Game.Components.Water.Modify
{
    public class PhysicsWaterModifier : WaterModifier
    {
        private float _fluidDensity;
        private float _linearDragCoefficient;
        private float _rotationalDragCoefficient;
        private Vector2 _waterFlow;
        
       // private BuoyancyController _buoyancyController;

        public PhysicsWaterModifier(float fluidDensity = 1, float linearDragCoefficient = 6, float rotationalDragCoefficient = 0)
        {
            _fluidDensity = fluidDensity;
            _linearDragCoefficient = linearDragCoefficient;
            _rotationalDragCoefficient = rotationalDragCoefficient;
        }

        public override void FixUp()
        {
            base.FixUp();

            _waterFlow = PhysicsComponent.World.Gravity + new Vector2(0, 0); // current

            // Create BuoyancyController for water
            /*
            _buoyancyController = new BuoyancyController(new AABB(ConvertUnits.ToSimUnits(Water.Position +
                                                                                          new Vector2(Water.Width / 2, Water.Height / 2) +
                                                                                          new Vector2(0, 20)),
                                                                  ConvertUnits.ToSimUnits(Water.Width),
                                                                  ConvertUnits.ToSimUnits(Water.Height)),
                                                                  _fluidDensity,
                                                                  _linearDragCoefficient,
                                                                  _rotationalDragCoefficient,
                                                                  _waterFlow);

            PhysicsComponent.World.AddController(_buoyancyController);
             * */
        }

        private PhysicsComponent PhysicsComponent { get { return Scene.ComponentManager.GetComponent<PhysicsComponent>(); } }
    }
}
