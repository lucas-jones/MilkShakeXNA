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

            int vertSize = Water.WaterColumns.Length * 12;

            float interval = Water.Width / Water.WaterColumns.Length;

            _renderVerticies = new VertexPositionColor[vertSize];
            int index = 0;
            for (int i = 0; i < vertSize; i += 12)
            {
                Vector2 topLeft = new Vector2(interval * index, 0);
                Vector2 bottomLeft = new Vector2(interval * index, Water.Height);

                Vector2 topRight = new Vector2((index + 1) * interval, 0);
                Vector2 bottomRight = new Vector2((index + 1) * interval, Water.Height);

                index++;
                _renderVerticies[i] = new VertexPositionColor(new Vector3(topLeft, 0), _topColor);
                _renderVerticies[i + 1] = new VertexPositionColor(new Vector3(topRight, 0), _topColor);
                _renderVerticies[i + 2] = new VertexPositionColor(new Vector3(bottomLeft, 0), _bottomColor);

                _renderVerticies[i + 3] = new VertexPositionColor(new Vector3(topRight, 0), _topColor);
                _renderVerticies[i + 4] = new VertexPositionColor(new Vector3(bottomRight, 0), _bottomColor);
                _renderVerticies[i + 5] = new VertexPositionColor(new Vector3(bottomLeft, 0), _bottomColor);

                Color outline = new Color(105, 211, 206);
                // Outline
                _renderVerticies[i + 6] = new VertexPositionColor(new Vector3(topLeft, 0), outline);
                _renderVerticies[i + 7] = new VertexPositionColor(new Vector3(topRight, 0), outline);
                _renderVerticies[i + 8] = new VertexPositionColor(new Vector3(topLeft - new Vector2(0, 10), 0), outline);

                _renderVerticies[i + 9] = new VertexPositionColor(new Vector3(topRight, 0), outline);
                _renderVerticies[i + 10] = new VertexPositionColor(new Vector3(topRight - new Vector2(0, 10), 0), outline);
                _renderVerticies[i + 11] = new VertexPositionColor(new Vector3(topLeft - new Vector2(0, 10), 0), outline);
            }

            Console.WriteLine("Total: " + index);

            // -----------------------

            _effect = new BasicEffect(MilkShake.Graphics);

            // [Needed?]
            _effect.VertexColorEnabled = true;
            _effect.Alpha = _alpha;
        }

        public override void Draw()
        {
            // Wireframe
            /* 
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.FillMode = FillMode.WireFrame;
            MilkShake.Graphics.RasterizerState = rasterizerState;
            */
            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                MilkShake.GraphicsManager.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _renderVerticies, 0, _renderVerticies.Length / 3, VertexPositionColor.VertexDeclaration);       
            }

            base.Draw();

            // Revert to normal rendering
            Scene.RenderManager.End();
            Scene.RenderManager.Begin();
        }

        public override void Update(GameTime gameTime)
        {
            _effect.Projection = GetProjectionMatrix();
            _effect.View = GetViewMatrix();

            int index = 0;
            for (int i = 0; i < _renderVerticies.Length - 12; i += 12)
            {
                float leftHeight = Water.Height - Water.GetHeight(index);
                float rightHeight = Water.Height - Water.GetHeight(index + 1);

                index++;

                _renderVerticies[i].Position.Y = leftHeight;
                _renderVerticies[i + 1].Position.Y = rightHeight;
                _renderVerticies[i + 3].Position.Y = rightHeight;


                // 6 7 9
                _renderVerticies[i + 6].Position.Y = leftHeight;
                _renderVerticies[i + 7].Position.Y = rightHeight;
                _renderVerticies[i + 9].Position.Y = rightHeight;

                /// 8 10 11
                _renderVerticies[i + 8].Position.Y = leftHeight + 4;
                _renderVerticies[i + 10].Position.Y = rightHeight + 4;
                _renderVerticies[i + 11].Position.Y = leftHeight + 4;
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
