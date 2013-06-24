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

namespace Playground
{
    public class Playground : MilkShake
    {
        public Playground() : base(1280, 720) { }

        protected override void Initialize()
        {
            base.Initialize();

            SceneManager.AddScene("BasicScene", new BasicScene());
            SceneManager.ChangeScreen("BasicScene");

            
           
        }
    }

    public class BasicScene : Scene
    {
        private Sprite _longCat;

        public BasicScene() : base()
        {
            new SceneView(this).Show();

            AddNode(_longCat = new Sprite("longcat"));
        }

        public override void FixUp()
        {
            base.FixUp();

            _longCat.Filter = new GaussianBlurFilter(1, 100, 100, new Vector2(10, 10));
        }
    }
}

