using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Krypton;
using MilkShakeFramework.Components.Lighting.Hulls;
using Microsoft.Xna.Framework;

namespace MilkShake_Xbox.Components.Lighting.Hulls
{
    public class RectangleHull : AbstractHull
    {
        public float Width { get; protected set; }
        public float Height { get; protected set; }

        public RectangleHull(float width, float height)
        {
            Width = width;
            Height = height;
        }

        protected override ShadowHull GenerateShadowHull()
        {
            return ShadowHull.CreateRectangle(new Vector2(Width, Height));
        }
    }
}
