using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Krypton;
using Microsoft.Xna.Framework.Graphics;
using Krypton.Lights;

namespace MilkShakeFramework.Components.Lighting.Lights
{
    public class PointLight : AbstractLight
    {
        // [Light Settings]
        private int mSize;

        public PointLight(int size, ShadowType type = ShadowType.Illuminated) : base()
        {
            mSize = size;
            Light.ShadowType = type;
        }

        public override Texture2D GenerateTexture()
        {
            return LightTextureBuilder.CreatePointLight(MilkShake.Graphics, mSize);
        }
    }
}
