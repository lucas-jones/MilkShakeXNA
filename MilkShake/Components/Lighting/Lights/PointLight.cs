using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Krypton;
using Microsoft.Xna.Framework.Graphics;
using Krypton.Lights;

namespace MilkShakeFramework.Components.Lighting.Lights
{
    public class PointLight : Light
    {
        // [Light Settings]
        private int mSize;

        public PointLight(int size, ShadowType type = ShadowType.Solid) : base()
        {
            mSize = size;
            mLight2D.ShadowType = type;

        }

        public override Texture2D Texture
        {
            get { return LightTextureBuilder.CreatePointLight(MilkShake.Graphics, mSize); }
        }

    }
}
