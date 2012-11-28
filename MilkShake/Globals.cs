using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MilkShakeFramework
{
    public static class Globals
    {
        // [Screen]
        public const int DefaultScreenHeight = 480;
        public const int DefaultScreenWidth = 853;

        public static int ScreenWidth;
        public static int ScreenHeight;

        public static int ScreenWidthCenter { get { return (int)ScreenWidth / 2; } }
        public static int ScreenHeightCenter { get { return (int)ScreenHeight / 2; } }

        // [Render]
        public static Color ScreenColour = Color.Salmon;
        public static bool BackToFrontRender = true;
        public static bool EnabledVSync = true;
        public static int MultiSampleRate = 1;

        // [Physics]
        public static float DisplayUnitToSimUnitRatio = 24f;

        public static string ContentDirectory = "Content";

        public static Random Random = new Random();

        public static RasterizerState WireframeState = new RasterizerState()
        {
            CullMode = CullMode.None,
            FillMode = FillMode.WireFrame
        };
    }
}
