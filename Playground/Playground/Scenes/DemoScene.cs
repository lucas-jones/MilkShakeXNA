using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework;
using MilkShakeFramework;
using MilkShakeFramework.Core.Game.Components.UI;

namespace Samples.Scenes
{
    public class DemoScene : Scene
    {
        public string Title { get; private set; }
        public string Description { get; private set; }

        private UILayer uiLayer;
        private Textfield _title;
        private Textfield _description;

        public DemoScene(string title, string description) : base()
        {
            Title = title;
            Description = description;

            AddNode(uiLayer = new UILayer(DrawMode.Post));

            uiLayer.AddNode(_title = new Textfield("Scenes//Global//DemoFont")
            {
                Text = Title,
                Color = Color.White,
                Y = 20
            });

            uiLayer.AddNode(_description = new Textfield("Scenes//Global//DemoFont")
            {
                Text = Description,
                Color = Color.White,
                Position = new Vector2(0, 50)
            });
        }

        public override void FixUp()
        {
            base.FixUp();

            _title.X = Globals.ScreenCenter.X - (_title.Width / 2);
            _description.X = Globals.ScreenCenter.X - (_description.Width / 2);
        }
    }
}
