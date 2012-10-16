using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;

namespace MilkShakeFramework.Core.Game.Components.Polygon.Render
{
    public class BasicPolygonRenderer : PolygonRenderer
    {
        private BasicEffect _effect;
        private VertexPositionColor[] _renderVerticies;

        private Color _color;
        private bool _wireFrame;

        public BasicPolygonRenderer(Color color, bool wireFrame = false)
        {
            _color = color;
            _wireFrame = wireFrame;
        }

        public override void Load(LoadManager content)
        {
            base.Load(content);

            _effect = new BasicEffect(MilkShake.Graphics);

            // [Needed?]
            _effect.VertexColorEnabled = true;
            _effect.DiffuseColor = Color.White.ToVector3();
        }

        public override void GenerateRenderer(Vector2[] _verticies, short[] _indices)
        {
            _renderIndices = _indices;
            _renderVerticies = Array.ConvertAll<Vector2, VertexPositionColor>(_verticies, v => new VertexPositionColor(new Vector3(Polygon.WorldPosition + v, 0), _color));
        }

        public override void Draw()
        {
            // Cache raster state
            RasterizerState currentRasterstate = MilkShake.Graphics.RasterizerState;

            if (_wireFrame) // [ToDo] Move wireframe stuff into parent class?
            {
                // Apply wireframe
                MilkShake.Graphics.RasterizerState = Globals.WireframeState;
            }

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                MilkShake.GraphicsManager.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, _renderVerticies, 0, _renderVerticies.Length, _renderIndices, 0, _renderIndices.Length / 3, VertexPositionColorTexture.VertexDeclaration);
            }

            // Revert
            if (_wireFrame) MilkShake.Graphics.RasterizerState = currentRasterstate;
        }

        public override void Update(GameTime gameTime)
        {
            _effect.Projection = GetProjectionMatrix();
            _effect.View = GetViewMatrix();
        }
    }
}
