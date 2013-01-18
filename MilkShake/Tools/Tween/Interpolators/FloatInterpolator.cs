using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Tools.Tween.Interpolators
{
    public class FloatInterpolator : Interpolator<float>
    {
        protected override float Interpolate()
        {
            return smoothStep
                ? MathHelper.SmoothStep(value1, value2, (float)(currentDuration / totalDuration))
                : MathHelper.Lerp(value1, value2, (float)(currentDuration / totalDuration));
        }
    }
}
