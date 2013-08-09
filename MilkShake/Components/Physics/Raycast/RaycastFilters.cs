using System;
using FarseerPhysics.Dynamics;

namespace MilkShakeFramework.Components.Physics.Raycast
{
    public class RaycastFilters
    {
        public static Func<Fixture, Boolean> OnlyFixture(Fixture fixture)
        {
            return (compareFix) => compareFix == fixture;
        }

        public static Func<Fixture, Boolean> OnlyBodyType(BodyType bodyType)
        {
            return (compareFix) => compareFix.Body.BodyType == bodyType;
        }
    }
}
