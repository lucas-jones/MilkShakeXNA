using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Core.Game.Components.Polygon
{
    public class PolygonRenderer : GameEntity
    {
        internal short[] _renderIndices;

        public virtual void GenerateRenderer(Vector2[] _verticies, short[] _indices)
        {
            throw new Exception("Not overriden!");
        }

        // [Sort out..]
        internal Matrix GetViewMatrix()
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

        // [Sort out..] Move to camera?
        internal Matrix GetProjectionMatrix()
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

        public Polygon Polygon { get { return (Polygon)Parent; } }
    }
}
