using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Tools.Utils;

namespace MilkShakeFramework.Core.Game.Components.Polygon.GiftWrap
{
    public class GiftWrapPolygonModifier : PolygonModifier
    {
        private List<Vector2> _points;
        private List<GiftWrapQuad> _giftWrapQuads;

        private GiftWrapRenderer _giftWrapRenderer;

        private float _height;
        private float _depth;

        public GiftWrapRenderer Renderer { get { return _giftWrapRenderer; } } 

        public GiftWrapPolygonModifier(List<Vector2> points, GiftWrapRenderer giftWrapRenderer, float height = 10, float depth = 10)
        {
            _points = points;
            _giftWrapRenderer = giftWrapRenderer;
            _height = height;
            _depth = depth;

            AddNode(_giftWrapRenderer);
        }
        
        public override void FixUp()
        {
            base.FixUp();

            Polygon.OnRendererRefresh += new BasicEvent(OnRendererRefresh);
        }

        public void OnRendererRefresh()
        {
            _giftWrapQuads = new List<GiftWrapQuad>();

            for (int index = 0; index < _points.Count - 4; index++)
            {
                MakeZone(_points[index], _points[index + 1], _points[index + 2], _points[index + 3]);
            }

            MakeZone(_points[_points.Count - 4], _points[_points.Count - 3], _points[_points.Count - 2], _points[_points.Count - 1]);
            MakeZone(_points[_points.Count - 3], _points[_points.Count - 2], _points[_points.Count - 1], _points[0]);

            MakeZone(_points[_points.Count - 2], _points[_points.Count - 1], _points[0], _points[1]);
            MakeZone(_points[_points.Count - 1], _points[0], _points[1], _points[2]);

            _giftWrapRenderer.GenerateRenderer(_giftWrapQuads);
        }

        private void MakeZone(Vector2 pointA, Vector2 pointB, Vector2 pointC, Vector2 pointD)
        {
            float firstRotationOne = MathUtils.AngleBetweenTwoVectors(pointA, pointB) + MathHelper.ToRadians(90);
            float firstRotationTwo = MathUtils.AngleBetweenTwoVectors(pointB, pointC) + MathHelper.ToRadians(90);

            float firstRotation = adverageAngle(firstRotationOne, firstRotationTwo);

            float secondRotationOne = MathHelper.ToRadians(MathHelper.ToDegrees((float)MilkShakeFramework.Tools.Utils.MathUtils.AngleBetweenTwoVectors(pointB, pointC)) + 90);
            float secondRotationTwo = MathHelper.ToRadians(MathHelper.ToDegrees((float)MilkShakeFramework.Tools.Utils.MathUtils.AngleBetweenTwoVectors(pointC, pointD)) + 90);
            float secondRotation = adverageAngle(secondRotationOne, secondRotationTwo);

            Vector2 firstVector = MathUtils.AngleToVector2(firstRotation, 1);
            Vector2 secondVector = MathUtils.AngleToVector2(secondRotation, 1);

            Vector2 topLeft = pointB + (firstVector * _height);
            Vector2 topRight = pointC + (secondVector * _height);
            Vector2 bottomLeft = pointB - (firstVector * _depth);
            Vector2 bottomRight = pointC - (secondVector * _depth);

            _giftWrapQuads.Add(new GiftWrapQuad(topLeft, topRight, bottomLeft, bottomRight, firstRotationOne, firstRotationTwo, secondRotationOne, secondRotationTwo, pointA, pointB, pointC, pointD));
        }

        public float V2ToAngle(Vector2 direction)
        {
            return (float)Math.Atan2(direction.Y, direction.X);
        }

        public float adverageAngle(float angleOne, float angleTwo)
        {
            Vector2 one = MilkShakeFramework.Tools.Utils.MathUtils.AngleToVector2(angleOne, 1);
            Vector2 two = MilkShakeFramework.Tools.Utils.MathUtils.AngleToVector2(angleTwo, 1);

            Vector2 newVector = one + ((two - one) / 2);

            return V2ToAngle(newVector);
        }

        private Matrix GetViewMatrix()
        {
            Matrix view = Matrix.Identity;

            float xTranslation = -1 * Scene.Camera.Position.X - Globals.ScreenWidthCenter;
            float yTranslation = -1 * Scene.Camera.Position.Y - Globals.ScreenHeightCenter;
            Vector3 translationVector = new Vector3(xTranslation, yTranslation, 0f);
            view = Matrix.Identity;
            view.Translation = translationVector;
            view *= Matrix.CreateRotationZ(MathHelper.ToRadians(Scene.Camera.Rotation));
            return view;
        }

        private Matrix GetProjectionMatrix()
        {
            Matrix projection = Matrix.Identity;

            float zoom = Scene.Camera.Zoom;
            float width = (1f / zoom) * Globals.ScreenWidth;
            float height = (-1f / zoom) * Globals.ScreenHeight;
            float zNearPlane = 0f;
            float zFarPlane = 1000000f;

            projection = Matrix.CreateOrthographic(width, height, zNearPlane, zFarPlane);

            return projection;
        }
    }
}
