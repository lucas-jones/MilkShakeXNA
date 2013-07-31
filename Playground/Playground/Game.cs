using MilkShakeFramework;
using MilkShakeFramework.Core.Scenes;
using System;
using System.CodeDom.Compiler;
using MilkShakeFramework.Core.Game;
using System.IO;
using MilkShakeFramework.Core;
using MilkShakeFramework.Core.Filters;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.IO.Input.Devices;
using System.Drawing;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Components.Physics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Controllers;
using FarseerPhysics;
using MilkShakeFramework.Core.Game.Components.Misc;
using MilkShakeFramework.Tools.Utils;
using Samples.Scenes.Demo1;
using Samples.Scenes.Demos;

namespace Samples
{
    public class MilkShakeSamples : MilkShake
    {
        public MilkShakeSamples() : base(853, 480) { }

        protected override void Initialize()
        {
            base.Initialize();

            SceneManager.AddScene("Demo1", new Demo1Scene());
            SceneManager.AddScene("Demo2", new Demo2Scene());
            SceneManager.AddScene("Demo_BreakOut", new Demo_BreakOut());

            SceneManager.ChangeScreen("Demo_BreakOut");           
        }
    }
}

