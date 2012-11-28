using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Render;

namespace MilkShakeFramework.Core.Game.Components.Polygon.Render
{
    public class TexturedPolygonRenderer : PolygonRenderer
    {
        private BasicEffect _effect;
        private VertexPositionColorTexture[] _renderVerticies;


        private Image _image;
        private float _scale;

        public TexturedPolygonRenderer(string assetURL, float scale = 1f)
        {
            _image = new Image(assetURL);
            _scale = scale;

            AddNode(_image);
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
            _renderVerticies = Array.ConvertAll<Vector2, VertexPositionColorTexture>(_verticies, v => new VertexPositionColorTexture(new Vector3(Polygon.WorldPosition + v, 0), Color.White, v / (100 * _scale)));
        }

        public override void Draw()
        {
            
            MilkShake.Graphics.SamplerStates[0] = new SamplerState()
            {
                AddressU = TextureAddressMode.Wrap,
                AddressV = TextureAddressMode.Wrap,
                AddressW = TextureAddressMode.Wrap
            };
            
            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                MilkShake.GraphicsManager.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleList, _renderVerticies, 0, _renderVerticies.Length, _renderIndices, 0, _renderIndices.Length / 3, VertexPositionColorTexture.VertexDeclaration);
            }

            base.Draw();
        }

        public override void Update(GameTime gameTime)
        {
            _effect.Projection = GetProjectionMatrix();
            _effect.View = GetViewMatrix();

            _effect.TextureEnabled = true;
            _effect.Texture = _image.Texture;
        }

        public float Scale { get { return _scale; } set { _scale = value; } }
    }
}
