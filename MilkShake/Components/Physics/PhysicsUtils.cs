using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Factories;

namespace MilkShakeFramework.Components.Physics
{
    public class PhysicsUtils
    {
        public static Vertices FromTextrure(Texture2D polygonTexture)
        {
            uint[] data = new uint[polygonTexture.Width * polygonTexture.Height];
            polygonTexture.GetData(data);
            
            return PolygonTools.CreatePolygon(data, polygonTexture.Width);
        }

    }
}
