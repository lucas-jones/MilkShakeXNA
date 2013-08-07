using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Krypton;

namespace MilkShakeFramework.Components.Lighting.Hulls
{
    public class CircleHull : AbstractHull
    {
        public float Radius { get; protected set; }
        public int Sides { get; protected set; }

        public CircleHull(float radius, int sides)
        {
            Radius = radius;
            Sides = sides;
        }

        protected override ShadowHull GenerateShadowHull()
        {
            return ShadowHull.CreateCircle(Radius, Sides);
        }
    }
}
