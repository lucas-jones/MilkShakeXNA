using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Krypton;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Components.Lighting.Hulls
{
    public class ConvexHull : AbstractHull
    {
        public Vector2[] Points;

        public ConvexHull(List<Vector2> points) : this(points.ToArray()) { }

        public ConvexHull(Vector2[] points)
        {
            Points = points;
        }

        protected override ShadowHull GenerateShadowHull()
        {
            return ShadowHull.CreateConvex(ref Points);
        }
    }
}
