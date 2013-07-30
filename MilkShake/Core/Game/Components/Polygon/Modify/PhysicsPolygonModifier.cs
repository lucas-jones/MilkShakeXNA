using System.Collections.Generic;
using System.Linq;
using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Scenes.Components;
using MilkShakeFramework.Tools.Physics;
using MilkShakeFramework.Components.Physics;
using FarseerPhysics;

namespace MilkShakeFramework.Core.Game.Components.Polygon.Modify
{
    public class PhysicsPolygonModifier : PolygonModifier
    {
        private Body _physicsBody;

        public override void FixUp()
        {
            base.FixUp();

            Polygon.OnRendererRefresh += new BasicEvent(OnRendererRefresh);
        }
        
        public void OnRendererRefresh()
        {
            if(_physicsBody != null) _physicsBody.Dispose();

            List<Vector2> physicsVerts = new List<Vector2>();

            // Append World Position
            Polygon.PolygonData.Points.ToList<Vector2>().ForEach(s => physicsVerts.Add(ConvertUnits.ToSimUnits(s + Polygon.WorldPosition)));

            // Convert points to verticies
            /*
            List<Vertices> vers = EarclipDecomposer.ConvexPartition(new Vertices(physicsVerts));

            _physicsBody = BodyFactory.CreateCompoundPolygon(PhysicsComponent.World, vers, 1);
            _physicsBody.Position = ConvertUnits.ToSimUnits(Position);
            _physicsBody.BodyType = BodyType.Static;
            _physicsBody.Friction = 2;
            _physicsBody.UserData = new IgnoreWater();
             * */
        }

        public override void Update(GameTime gameTime)
        {
            Polygon.Position = ConvertUnits.ToDisplayUnits(_physicsBody.Position);

            base.Update(gameTime);
        }

        private Body PhysicsBody { get { return _physicsBody; } }
        private PhysicsComponent PhysicsComponent { get { return Scene.ComponentManager.GetComponent<PhysicsComponent>(); } }
    }
}
