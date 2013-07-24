using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game.Components.UI;
using MilkShakeFramework.Render;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Tools.Debug.Watch;

namespace MilkShakeFramework.Core.Game.Components.Debug
{
    public class WatchedDisplay : UILayer
    {
        private TextRenderer _textDraw;

        public WatchedDisplay() : base(DrawMode.Post)
        {
            AddNode(_textDraw = new TextRenderer());
        }

        public override void Update(GameTime gameTime)
        {
            for (int index = 0; index < WatchedManager.Entries.Count; index++)
			{
			    WatchEntry entry = WatchedManager.Entries[index];

                _textDraw.DrawText(new Vector2(10, 10 + index * 15), entry.FieldInfo.Name + " = " + entry.GetValue(), Color.White);
			}            

            base.Update(gameTime);
        }
    }
}
