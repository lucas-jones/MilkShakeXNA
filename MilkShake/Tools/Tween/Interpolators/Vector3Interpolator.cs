using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Tools.Tween.Interpolators
{
    public class Vector3Interpolator : Interpolator<Vector3>
    {
        protected override Vector3 Interpolate()
        {
            return smoothStep
                ? Vector3.SmoothStep(value1, value2, (float)(currentDuration / totalDuration))
                : Vector3.Lerp(value1, value2, (float)(currentDuration / totalDuration));
        }
    }
}
