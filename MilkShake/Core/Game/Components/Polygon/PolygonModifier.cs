using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game.Components.Polygon;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework.Core;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Render;

namespace MilkShakeFramework.Core.Game.Components.Polygon
{
    public class PolygonModifier : GameEntity
    {
        private bool _autoUpdate = false;

        public PolygonModifier(bool autoUpdate = false)
        {
            _autoUpdate = autoUpdate;
        }

        public override void SetParent(ITreeNode parent)
        {
            if (!(parent is Polygon)) throw new Exception("Parent must be of Polygon");

            base.SetParent(parent);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if(_autoUpdate) Polygon.UpdateRenderer();

            base.Update(gameTime);
        }

        public Polygon Polygon { get { return (Polygon)Parent; } }
    }
}
