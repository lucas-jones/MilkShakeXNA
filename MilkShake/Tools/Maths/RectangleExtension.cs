using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Tools.Maths
{
    public static class RectangleExtension
    {
        public static bool Intersects(this Rectangle rect, RotatedRectangle rot)
        {
            return rect.Intersects(rot.CollisionRectangle);
        }
    }
}
