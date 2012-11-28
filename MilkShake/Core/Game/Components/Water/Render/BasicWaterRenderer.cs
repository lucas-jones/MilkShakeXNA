using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Content;

namespace MilkShakeFramework.Core.Game.Components.Water.Render
{
    public class BasicWaterRenderer : WaterRenderer
    {
        private float _alpha;
        private Color _topColor;
        private Color _bottomColor;

        private BasicEffect _effect;

        private VertexPositionColor[] _renderVerticies;

        public BasicWaterRenderer() : this(Color.Aqua, Color.Blue, 0.5f) { }

        public BasicWaterRenderer(Color topColor, Color bottomColor, float alpha)
        {
            _topColor = topColor;
            _bottomColor = bottomColor;
            _alpha = alpha;
        }

        public override void Load(LoadManager content)
        {
            base.Load(content);

            int vertSize = Water.WaterColumns.Length * 6;

            float interval = Water.Width / 6;

            _renderVerticies = new VertexPositionColor[vertSize];
            int index = 0;
            for (int i = 0; i < vertSize; i += 6)
            {
                Vector2 topLeft = new Vector2(interval * index, 0);
                Vector2 bottomLeft = new Vector2(interval * index, Water.Height);

                Vector2 topRight = new Vector2(interval * (index + 1), 0);
                Vector2 bottomRight = new Vector2(interval * (index + 1), Water.Height);

                index++;
                _renderVerticies[i] = new VertexPositionColor(new Vector3(topLeft, 0), _topColor);
                _renderVerticies[i + 1] = new VertexPositionColor(new Vector3(topRight, 0), _topColor);
                _renderVerticies[i + 2] = new VertexPositionColor(new Vector3(bottomLeft, 0), _bottomColor);

                _renderVerticies[i + 3] = new VertexPositionColor(new Vector3(topRight, 0), _topColor);
                _renderVerticies[i + 4] = new VertexPositionColor(new Vector3(bottomRight, 0), _bottomColor);
                _renderVerticies[i + 5] = new VertexPositionColor(new Vector3(bottomLeft, 0), _bottomColor);
            }

            // -----------------------

            _effect = new BasicEffect(MilkShake.Graphics);

            // [Needed?]
            _effect.VertexColorEnabled = true;
            _effect.Alpha = _alpha;
        }

        public override void Draw()
        {
            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                MilkShake.GraphicsManager.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _renderVerticies, 0, _renderVerticies.Length / 3, VertexPositionColor.VertexDeclaration);       
            }

            base.Draw();
        }

        public override void Update(GameTime gameTime)
        {
            _effect.Projection = GetProjectionMatrix();
            _effect.View = GetViewMatrix();

            int index = 0;
            for (int i = 0; i < _renderVerticies.Length - 6; i += 6)
            {
                float leftHeight = Water.Height - Water.GetHeight(index);
                float rightHeight = Water.Height - Water.GetHeight(index + 1);

                index++;

                _renderVerticies[i].Position.Y = leftHeight;
                _renderVerticies[i + 1].Position.Y = rightHeight;
                _renderVerticies[i + 3].Position.Y = rightHeight;
            }
        }

        // [Sort out..]
        internal Matrix GetViewMatrix()
        {
            Matrix view = Matrix.Identity;

            float xTranslation = -1 * Scene.Camera.Position.X - Globals.ScreenWidthCenter + Water.Position.X;
            float yTranslation = -1 * Scene.Camera.Position.Y - Globals.ScreenHeightCenter + Water.Position.Y;
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
    }
}
