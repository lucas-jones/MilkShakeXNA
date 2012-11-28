using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MilkShakeFramework.IO.Input.Devices;

namespace MilkShakeFramework.Core.Game.Components.Polygon.GiftWrap.Render
{
    public class BasicGiftWrapRenderer : GiftWrapRenderer
    {
        private BasicEffect _effect;
        private List<VertexPositionColor[]> _renderVerticies;

        private Color _topColor;
        private Color _bottomColor;

        public BasicGiftWrapRenderer(Color color) : this(color, color) { }

        public BasicGiftWrapRenderer(Color topColor, Color bottomColor)
        {
            _topColor = topColor;
            _bottomColor = bottomColor;
        }

        public override void Load(LoadManager content)
        {
            base.Load(content);

            _effect = new BasicEffect(MilkShake.Graphics);

            // [Needed?]
            _effect.VertexColorEnabled = true;
            _effect.DiffuseColor = Color.White.ToVector3();
        }

        public override void GenerateRenderer(List<GiftWrapQuad> giftWrapQuads)
        {
            _renderVerticies = new List<VertexPositionColor[]>();

            foreach (GiftWrapQuad quad in giftWrapQuads)
            {
                GenerateQuad(quad);
            }
        }

        private void GenerateQuad(GiftWrapQuad quad)
        {
            VertexPositionColor[] currentQuad = new VertexPositionColor[6];

            Color topLeft, topRight, bottomLeft, bottomRight;

            topLeft = topRight = _topColor;
            bottomLeft = bottomRight = _bottomColor;

            currentQuad[0].Position = new Vector3(quad.TopLeft, 0);
            currentQuad[0].Color = topLeft;

            currentQuad[1].Position = new Vector3(quad.TopRight, 0);
            currentQuad[1].Color = topRight;

            currentQuad[2].Position = new Vector3(quad.BottomRight, 0);
            currentQuad[2].Color = bottomRight;


            currentQuad[3].Position = new Vector3(quad.TopLeft, 0);
            currentQuad[3].Color = topLeft;

            currentQuad[4].Position = new Vector3(quad.BottomRight, 0);
            currentQuad[4].Color = bottomRight;

            currentQuad[5].Position = new Vector3(quad.BottomLeft, 0);
            currentQuad[5].Color = bottomLeft;

            _renderVerticies.Add(currentQuad);
        }

        public override void Draw()
        {

            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            rs.FillMode = (KeyboardInput.isKeyDown(Keys.LeftShift)) ? FillMode.WireFrame : FillMode.Solid;
            rs.MultiSampleAntiAlias = true;
            MilkShake.Graphics.RasterizerState = rs;

            _effect.TextureEnabled = false;

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                foreach (VertexPositionColor[] currentQuad in _renderVerticies)
                {
                    MilkShake.GraphicsManager.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, currentQuad, 0, 2, VertexPositionColor.VertexDeclaration);
                }
            }

            base.Draw();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _effect.Projection = GetProjectionMatrix();
            _effect.View = GetViewMatrix();
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
