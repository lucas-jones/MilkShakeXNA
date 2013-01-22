using System;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Tools.Utils
{
    public class MathUtils
    {
        public static float AngleBetweenVectors(Vector2 a, Vector2 b)
        {
            return (float)Math.Atan2(a.Y - b.Y,
                                     a.X - b.X);
        }

        public static Vector2 AngleToV2(float angle, float length)
        {
            Vector2 direction = Vector2.Zero;
            direction.X = (float)Math.Cos(angle) * length;
            direction.Y = (float)Math.Sin(angle) * length;
            return direction;
        }

        public static float V2ToAngle(Vector2 direction)
        {
            return (float)Math.Atan2(direction.Y, direction.X);
        }

        public static Vector2 SmoothStep(Vector2 value1, Vector2 value2, float amount)
        {
            float X = MathHelper.SmoothStep(value1.X, value2.X, amount);
            float Y = MathHelper.SmoothStep(value1.Y, value2.Y, amount);

            return new Vector2(X, Y);
        }
    }
}
