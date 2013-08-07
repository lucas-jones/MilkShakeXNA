using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Krypton;

namespace MilkShakeFramework.Components.Lighting.Lights
{
    public class ConicLight : AbstractLight
    {
        private int mSize;
        private float mFOV;
        private float mNearPlaneDistance;

        public ConicLight(int size, float FOV, float nearPlaneDistance = 1) : base()
        {
            mSize = size;
            mFOV = FOV;
            mNearPlaneDistance = nearPlaneDistance;
        }

        public override Texture2D GenerateTexture()
        {
            return LightTextureBuilder.CreateConicLight(MilkShake.Graphics, mSize, mFOV, mNearPlaneDistance);
        }
    }
}
