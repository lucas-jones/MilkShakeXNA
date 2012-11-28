using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Core.Game.Components.Water
{
    public struct WaterColumn
    {
        public float TargetHeight;
        public float Height;
        public float Speed;

        public void Update(float dampening, float tension)
        {
            float x = TargetHeight - Height;

            Speed += tension * x - Speed * dampening;
            Height += Speed;
        }
    }
}
