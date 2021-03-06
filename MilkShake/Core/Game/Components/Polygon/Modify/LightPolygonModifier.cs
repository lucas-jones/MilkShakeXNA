﻿using System.Collections.Generic;
using System.Linq;
using Krypton;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Components.Lighting;
using MilkShakeFramework.Core.Scenes;

namespace MilkShakeFramework.Core.Game.Components.Polygon.Modify
{
    public class LightPolygonModifier : PolygonModifier
    {
        private List<ShadowHull> _shadowHulls = new List<ShadowHull>();

        public override void FixUp()
        {
            base.FixUp();

            Polygon.OnRendererRefresh += new BasicEvent(OnRendererRefresh);
        }

        public void OnRendererRefresh()
        {
            // Remove old shadows
            _shadowHulls.ForEach(sH => LightingComponent.Krypton.Hulls.Remove(sH));

            int polyCount = Polygon.Indices.Length / 3;

            for (int index = 0; index < polyCount; index++)
            {
                List<short> currentIndie = Polygon.Indices.ToList<short>().GetRange(index * 3, 3);
                List<Vector2> currentVerts = new List<Vector2>();

                currentIndie.ForEach(i => currentVerts.Add(Polygon.Vertices[i] + Polygon.WorldPosition));
                currentVerts.Reverse();
                /*
                PolygonHull hull = new PolygonHull(WorldPosition, currentVerts.ToList<Vector2>());

                // Hacky fix
                hull.Hull.Position.Y -= Globals.ScreenHeight;

                _shadowHulls.Add(hull.Hull);
                LightingComponent.Light.Hulls.Add(hull.Hull);*/
            }
        }

        private LightingComponent LightingComponent { get { return Scene.ComponentManager.GetComponent<LightingComponent>(); } }
    }
}
