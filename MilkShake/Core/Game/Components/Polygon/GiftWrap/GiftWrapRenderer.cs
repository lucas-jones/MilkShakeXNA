using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.IO.Input.Devices;
using Microsoft.Xna.Framework.Input;

namespace MilkShakeFramework.Core.Game.Components.Polygon.GiftWrap
{
    public class GiftWrapRenderer : GameEntity
    {
        public virtual void GenerateRenderer(List<GiftWrapQuad> giftWrapQuad)
        {

        }

        public override void Draw()
        {
            base.Draw();

            // Revert to normal rendering
            Scene.RenderManager.End();
            Scene.RenderManager.Begin();
        }
    }
}
