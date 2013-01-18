using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Tools.Tween.Interpolators
{
    public class Vector2Interpolator : Interpolator<Vector2>
    {
        protected override Vector2 Interpolate()
        {
            return smoothStep
                ? Vector2.SmoothStep(value1, value2, (float)(currentDuration / totalDuration))
                : Vector2.Lerp(value1, value2, (float)(currentDuration / totalDuration));
        }
    } 
}
