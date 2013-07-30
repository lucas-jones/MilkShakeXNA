using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Components.Physics;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.IO.Input.Devices;
using Microsoft.Xna.Framework.Input;
using MilkShakeFramework.Render;
using MilkShakeFramework.Tools.Utils;
using MilkShakeFramework.Core.Scenes.Components;
using FarseerPhysics.Factories;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using MilkShakeFramework.Tools.Physics;
using FarseerPhysics.Common.Decomposition;
using MilkShakeFramework.Core.Scenes;
using FarseerPhysics;

namespace MilkShakeFramework.Core.Game.Components.Polygon.GiftWrap.Render
{
    public class TextureGiftWrapRenderer : GiftWrapRenderer
    {
        private BasicEffect _effect;
        private Dictionary<GiftWrapQuad, VertexPositionTexture[]> _renderVerticies;

        private ImageRenderer _texture;

        public TextureGiftWrapRenderer()
        {
            AddNode(_texture = new ImageRenderer("band"));
        }

        public Color Color { set { _effect.DiffuseColor = value.ToVector3(); } }

        public override void Load(LoadManager content)
        {
            base.Load(content);

            _effect = new BasicEffect(MilkShake.Graphics);

            // [Needed?]
            _effect.VertexColorEnabled = true;
            _effect.DiffuseColor = new Color(1.0f, 1, 1, 0.5f).ToVector3();
            //_effect.AmbientLightColor = new Color(1.0f, 0, 0, 0.5f).ToVector3();
        }

        public override void FixUp()
        {
            base.FixUp();

            //Scene.Listener.PostDraw[DrawLayer.Fifth] +=  PreDraw;
        }

        List<GiftWrapQuad> _giftWrapQuads;

        public override void GenerateRenderer(List<GiftWrapQuad> giftWrapQuads)
        {
            // [Temp]
            _giftWrapQuads = giftWrapQuads;

            _renderVerticies = new Dictionary<GiftWrapQuad, VertexPositionTexture[]>();

            foreach (GiftWrapQuad quad in giftWrapQuads)
            {
                float rotation = quad.Rotation;

                //if (rotation < 90 || rotation > 270) 
                GenerateQuad(quad);
            }
        }

        private void GenerateQuad(GiftWrapQuad quad)
        {

            PhysicsComponent physics = Scene.ComponentManager.GetComponent<PhysicsComponent>();

            Vertices vets = new Vertices();
            vets.Add(quad.TopLeft);
            vets.Add(quad.TopRight);
            vets.Add(quad.BottomRight);
            vets.Add(quad.BottomLeft);

            List<Vector2> physicsVerts = new List<Vector2>();
            vets.ToList<Vector2>().ForEach(s => physicsVerts.Add(ConvertUnits.ToSimUnits(s)));
            /*
            List<Vertices> vers = EarclipDecomposer.ConvexPartition(new Vertices(physicsVerts));

            Body body = BodyFactory.CreateCompoundPolygon(physics.World, vers, 1);
            body.BodyType = BodyType.Static;
            body.IsSensor = true;

            body.OnCollision += (a, b, c) =>
                {
                    quad._collision++;
                    return true;
                };

            body.OnSeparation += (a, b) =>
                {
                    quad._collision--;
                };
            //body.CollisionCategories = Category.Cat28;
            //body.CollidesWith = Category.Cat27;

            


            VertexPositionTexture[] currentQuad = new VertexPositionTexture[6];

            float topDistance = Vector2.Distance(quad.TopLeft, quad.TopRight) / 100;
            float bottomDistance = Vector2.Distance(quad.BottomLeft, quad.BottomRight) / 100;

            currentQuad[0].Position = new Vector3(quad.TopLeft, 0);
            currentQuad[0].TextureCoordinate = new Vector2(0, 0);

            currentQuad[1].Position = new Vector3(quad.TopRight, 0);
            currentQuad[1].TextureCoordinate = new Vector2(topDistance, 0);

            currentQuad[2].Position = new Vector3(quad.BottomRight, 0);
            currentQuad[2].TextureCoordinate = new Vector2(bottomDistance, 1);


            currentQuad[3].Position = new Vector3(quad.TopLeft, 0);
            currentQuad[3].TextureCoordinate = new Vector2(0, 0);

            currentQuad[4].Position = new Vector3(quad.BottomRight, 0);
            currentQuad[4].TextureCoordinate = new Vector2(bottomDistance, 1);

            currentQuad[5].Position = new Vector3(quad.BottomLeft, 0);
            currentQuad[5].TextureCoordinate = new Vector2(0, 1);

            _renderVerticies.Add(quad, currentQuad);*/
        }

        public override void Draw()
        {
            MilkShake.Graphics.SamplerStates[0] = new SamplerState()
            {
                AddressU = TextureAddressMode.Wrap,
                AddressV = TextureAddressMode.Clamp,
                AddressW = TextureAddressMode.Wrap
            };

            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            rs.FillMode = (KeyboardInput.isKeyDown(Keys.LeftShift)) ? FillMode.WireFrame : FillMode.Solid;
            rs.MultiSampleAntiAlias = true;
            MilkShake.Graphics.RasterizerState = rs;

            _effect.TextureEnabled = true;
            _effect.Texture = _texture.Texture;
            _effect.LightingEnabled = false;
            _effect.VertexColorEnabled = false;

            foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                foreach (VertexPositionTexture[] currentQuad in _renderVerticies.Values)
                {
                    MilkShake.GraphicsManager.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, currentQuad, 0, 2, VertexPositionTexture.VertexDeclaration);
                }
            }

            base.Draw();
        }

        private float curTime;

        public override void Update(GameTime gameTime)
        {
            curTime += gameTime.ElapsedGameTime.Milliseconds;

            int currentIndex = 0;
            foreach (VertexPositionTexture[] currentSet in _renderVerticies.Values)
            {
                GiftWrapQuad currentQuad = _giftWrapQuads[currentIndex++];



                Vector2 firstVector = MilkShakeFramework.Tools.Utils.MathUtils.AngleToVector2(currentQuad.LeftRotationTwo, 45);

                Vector2 daPosition = currentQuad.PointB + firstVector;

                float distance = Vector2.Distance(currentQuad.TopLeft, daPosition);

                if (KeyboardInput.isKeyDown(Keys.P)) distance = 0;

                float topDistance = (Vector2.Distance(currentQuad.TopLeft, currentQuad.TopRight)) / (100);
                

                float sinValue = (float)Math.Sin((double)(curTime + (currentIndex * 100)) * 0.002f) * (0.10f);
                

                currentSet[0].TextureCoordinate.X = sinValue;
               currentSet[1].TextureCoordinate.X = topDistance + sinValue;

               currentSet[3].TextureCoordinate.X = sinValue;



               if (currentQuad._collision > 0 && currentSet[0].TextureCoordinate.Y > -0.25f)
               {
                   sinValue = 0.1f;
                   currentSet[0].TextureCoordinate.X = sinValue;
                   currentSet[1].TextureCoordinate.X = topDistance + sinValue;

                   currentSet[3].TextureCoordinate.X = sinValue;


                   float sinValue2 = currentSet[0].TextureCoordinate.Y - 0.03f;
                   currentSet[0].TextureCoordinate.Y = sinValue2;
                   currentSet[1].TextureCoordinate.Y = sinValue2;

                   currentSet[3].TextureCoordinate.Y = sinValue2;
               } else if (currentQuad._collision == 0 && currentSet[0].TextureCoordinate.Y < 0.01f)
               {
                   float sinValue2 = currentSet[0].TextureCoordinate.Y + 0.005f;
                   currentSet[0].TextureCoordinate.Y = sinValue2;
                   currentSet[1].TextureCoordinate.Y = sinValue2;

                   currentSet[3].TextureCoordinate.Y = sinValue2;
               }
            }




            base.Update(gameTime);

            currentIndex = 0;
            foreach (VertexPositionTexture[] currentSet in _renderVerticies.Values)
            {
               // GiftWrapQuad currentQuad = _giftWrapQuads[currentIndex++];
              //  if (currentQuad._collision > 0) currentQuad._collision--;
            }


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
