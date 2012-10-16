using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game.Components.Polygon.Render;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Core.Game.Components.Polygon.Modify
{
    internal class WireframePolygonRenderer : BasicPolygonRenderer
    {
        public WireframePolygonRenderer(Color color) : base(color, true) { }

        public override Polygon Polygon
        {
            get
            {
                WireframePolygonModifier modifier = (WireframePolygonModifier)Parent;
                return (Polygon)modifier.Parent;
            }
        }
    }

    public class WireframePolygonModifier : PolygonModifier
    {
        private WireframePolygonRenderer _wireFrameRenderer;

        public WireframePolygonModifier() : this(Color.White) { }

        public WireframePolygonModifier(Color color)
        {
            _wireFrameRenderer = new WireframePolygonRenderer(color);

            _wireFrameRenderer.Load(null);
            AddNode(_wireFrameRenderer);
        }

        public override void FixUp()
        {
            base.FixUp();

            UpdateWireframe();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UpdateWireframe();
        }

        private void UpdateWireframe()
        {
            _wireFrameRenderer.GenerateRenderer(Polygon.Vertices, Polygon.Indices);
        }
    }
}
