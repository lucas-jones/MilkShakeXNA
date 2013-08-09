using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;

namespace MilkShakeFramework.Components.Physics.Raycast
{
    public struct RayCastResult
    {
        public bool Collision;
        public Vector2 CollisionPoint;
        public Vector2 Normal;
        public Fixture Fixture;

        public override string ToString()
        {
            return "{ Collision = " + Collision + " }";
        }
    }
}
