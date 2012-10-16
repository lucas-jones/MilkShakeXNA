using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Common;
using Triangulator;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Game.Components.Polygon.Render;

namespace MilkShakeFramework.Core.Game.Components.Polygon
{
    //public T GetVertice(int index)    

    public class Polygon : GameEntity
    {
        private Vector2[] _vertices;
        private short[] _indices;

        private PolygonRenderer _renderer;

        public Polygon(PolygonData polygonData, PolygonRenderer renderer) : base()
        {
            _vertices = polygonData.Verticies;
            _indices = polygonData.Indicies;
            _renderer = renderer;

            // Should auto loaded... needs removing
            _renderer.Load(null);
            
            AddNode(renderer);
        }

        public override void FixUp()
        {
            base.FixUp();

            UpdateRenderer();
        }

        public void UpdateRenderer()
        {
            _renderer.GenerateRenderer(_vertices, _indices);
        }

        // [Helpers]
        public Polygon(PolygonData polygonData) : this(polygonData, new BasicPolygonRenderer(Color.White)) { }
        public Polygon(PolygonData polygonData, Color color) : this(polygonData, new BasicPolygonRenderer(color)) { }

        public PolygonRenderer Renderer { get { return _renderer; } }
        public Vector2[] Vertices { get { return _vertices; } }
    }
}
