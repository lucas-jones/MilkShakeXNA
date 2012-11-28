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
using MilkShakeFramework.Core.Scenes;

namespace MilkShakeFramework.Core.Game.Components.Polygon
{
    //public T GetVertice(int index)    

    public class Polygon : GameEntity
    {
        private PolygonData _polygonData;
        private PolygonRenderer _renderer;

        public BasicEvent OnRendererRefresh;

        public Polygon(PolygonData polygonData, PolygonRenderer renderer) : base()
        {
            _polygonData = polygonData;
            _renderer = renderer;

            // Should auto loaded... needs removing
            //_renderer.Load(null);
            
            AddNode(renderer);
        }

        public override void FixUp()
        {
            base.FixUp();

            UpdateRenderer();
        }

        public void UpdateRenderer()
        {
            _renderer.GenerateRenderer(_polygonData.Verticies, _polygonData.Indicies);

            if (OnRendererRefresh != null) OnRendererRefresh();
        }

        // [Helpers]
        public Polygon(PolygonData polygonData) : this(polygonData, new BasicPolygonRenderer(Color.White)) { }
        public Polygon(PolygonData polygonData, Color color) : this(polygonData, new BasicPolygonRenderer(color)) { }

        public PolygonRenderer Renderer { get { return _renderer; } }

        // [Helpers]
        public Vector2[] Vertices { get { return _polygonData.Verticies; } }
        public short[] Indices { get { return _polygonData.Indicies; } }
        public List<Vector2> Points { get { return _polygonData.Points; }  }

        public PolygonData PolygonData { get { return _polygonData; } }
    }
}
