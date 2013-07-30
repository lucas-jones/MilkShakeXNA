using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Scenes.Components;
using MilkShakeFramework.Core.Scenes;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Render;
using MilkShakeFramework.Tools.Physics;
using MilkShakeFramework.Core.Game.Components.Misc;
using MilkShakeFramework.Tools.Utils;
using FarseerPhysics.Dynamics;
using FarseerPhysics;

namespace MilkShakeFramework.Components.Physics
{
    public struct RayCastResult
    {
        public bool Collision;
        public Vector2 CollisionPoint;
        public Vector2 Normal;
        public Fixture Fixture;

        public override string ToString()
        {
            return "{ Collision = " + Collision + " }";
        }
    }

    public class RayCastUtils
    {
        public bool DEBUG_MODE = false;

        public static Boolean ClosestRayCastBool(Vector2 pointA, Vector2 pointB)
        {
            return ClosestRayCast(pointA, pointB).Collision;
        }

        public static Vector2 ClosestRayCastPosition(Vector2 pointA, Vector2 pointB)
        {
            RayCastResult result = ClosestRayCast(pointA, pointB);

            if(result.Collision) return result.CollisionPoint;
            else return pointB;
        }

        public static RayCastResult ClosestRayCast(Vector2 pointA, Vector2 pointB, Func<Fixture, Boolean> filter = null, PrimitiveRenderer lineDraw = null)
        {
            float minFrac = float.MaxValue;

            Fixture collisionFixture = null;
            Vector2 collisionPoint = pointB;
            bool hasCollision = false;
            Vector2 collisionNormal = Vector2.Zero;

            PhysicsComponent.World.RayCast((fixture, point, normal, fraction) =>
            {
                if (filter == null || filter(fixture))
                {
                    if (fraction < minFrac)
                    {
                        minFrac = fraction;
                        hasCollision = true;
                        collisionPoint = ConvertUnits.ToDisplayUnits(point);
                        collisionNormal = normal;
                        collisionFixture = fixture;
                    }
                }

                return 1;
            }, ConvertUnits.ToSimUnits(pointA), ConvertUnits.ToSimUnits(pointB));

            if (lineDraw != null)
            {
                lineDraw.DrawLine(pointA, pointB, Color.White);
                if (hasCollision) lineDraw.DrawLine(pointA, collisionPoint, Color.Red);

                if (hasCollision) lineDraw.DrawLine(collisionPoint, collisionPoint + (collisionNormal * 30), Color.Blue);
            }

            return new RayCastResult() { Collision = hasCollision, CollisionPoint = collisionPoint, Normal = collisionNormal, Fixture = collisionFixture };
        }

        public static PhysicsComponent PhysicsComponent { get { return SceneManager.CurrentScene.ComponentManager.GetComponent<PhysicsComponent>(); } }
    }

    public class RaycastFilters
    {
        public static Func<Fixture, Boolean> OnlyFixture(Fixture fixture)
        {
            return (compareFix) => compareFix == fixture;
        }

        public static Func<Fixture, Boolean> OnlyBodyType(BodyType bodyType)
        {
            return (compareFix) => compareFix.Body.BodyType == bodyType;
        }
    }
}
