using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Scenes;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Scenes.Components;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Common;
using MilkShakeFramework.Tools.Physics;
using FarseerPhysics.Factories;

namespace MilkShakeFramework.Core.Game.Components.Polygon.Modify
{
    // PhysicsPolygonAddon?
    public class PhysicsPolygonModifier : PolygonModifier
    {
        private List<Body> _bodys = new List<Body>();

        public override void FixUp()
        {
            base.FixUp();

            Polygon.OnRendererRefresh += new BasicEvent(OnRendererRefresh);
        }

        public void OnRendererRefresh()
        {
            // Remove old bodys    
            _bodys.ForEach(b => b.Dispose());

            int polyCount = Polygon.Indices.Length / 3;

            for (int index = 0; index < polyCount; index++)
            {
                List<short> currentIndie = Polygon.Indices.ToList<short>().GetRange(index * 3, 3);
                List<Vector2> currentVerts = new List<Vector2>();

                currentIndie.ForEach(i => currentVerts.Add(Polygon.Vertices[i]));
                currentVerts.Reverse();

                List<Vector2> physicsVerts = new List<Vector2>();
                currentVerts.ToList<Vector2>().ForEach(s => physicsVerts.Add(ConvertUnits.ToSimUnits(s + Polygon.WorldPosition)));

                Body body = BodyFactory.CreatePolygon(PhysicsComponent.World, new Vertices(physicsVerts), 1);
                body.BodyType = BodyType.Static;

                _bodys.Add(body);
            }
        }

        private PhysicsComponent PhysicsComponent { get { return Scene.ComponentManager.GetComponent<PhysicsComponent>(); } }
    }
}
