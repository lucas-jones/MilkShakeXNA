using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MilkShakeFramework.Core.Game
{
    public class Textfield : GameEntity
    {
        public SpriteFont SpriteFont { get; private set; }
        public string FontURL { get; private set; }
        public string Text { get; set; }
        public Color Color { get; set; }
        public bool AutoCenter { get; set; }
        public Vector2 Origin { get; set; }

        public Textfield(string fontURL, string text = "")
        {
            FontURL = fontURL;
            Text = text;
        }

        public override void Load(LoadManager content)
        {
            SpriteFont = MilkShake.ConentManager.Load<SpriteFont>(FontURL);
        }

        public override void FixUp()
        {
            base.FixUp();

            if (AutoCenter) Origin = SpriteFont.MeasureString(Text) / 2;
        }

        public override void Draw()
        {
            Scene.RenderManager.SpriteBatch.DrawString(SpriteFont, Text, WorldPosition - Scene.Camera.Position - Origin, Color);
        }
    }
}
