using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework;
using MilkShakeFramework.IO.Input.Devices;
using Microsoft.Xna.Framework.Input;
using Krypton;
using MilkShakeFramework.Components.Lighting;
using MilkShakeFramework.Components.Tile;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using MilkShakeFramework.Core.Scenes.Components;
using MilkShakeFramework.Tools.Physics;
using FarseerPhysics.Common;

namespace Playground.Tools
{
    /*
     * http://triangulator.codeplex.com/
     * http://stackoverflow.com/questions/4527040/render-arbitrary-polygon-with-xna
     */

    public class DraftPolygon : GameEntity
    {
        private List<Vector2> points;
        private Texture2D lineTexture;

        private Texture2D dirtTextue;

        private bool isComplete;

        private BasicEffect effect;
        private VertexPositionColorTexture[] verticies;
        private short[] indices;

        private List<VertexPositionColor[]> outlineVerticies;

        RasterizerState wireframe = new RasterizerState
        {
            CullMode = CullMode.CullCounterClockwiseFace,
            FillMode = FillMode.WireFrame
        };

        public override void Load(LoadManager content)
        {
            lineTexture = MilkShake.ConentManager.Load<Texture2D>("linetexture");
            dirtTextue = MilkShake.ConentManager.Load<Texture2D>("dev");

            
            points = new List<Vector2>();
            
            effect = new BasicEffect(MilkShake.Graphics);

            effect.VertexColorEnabled = true;
            effect.DiffuseColor = Color.White.ToVector3();

            MilkShake.Graphics.RasterizerState = wireframe;


            base.Load(content);
        }

        public override void Draw()
        {
            DrawLine(new Vector2(Globals.ScreenWidthCenter, int.MinValue), new Vector2(Globals.ScreenWidthCenter, int.MaxValue), Color.Red);
            DrawLine(new Vector2(int.MinValue, Globals.ScreenHeightCenter), new Vector2(int.MaxValue, Globals.ScreenHeightCenter), Color.LimeGreen);

            if (isComplete)
            {
                DrawPolygon();
                Scene.RenderManager.End();
                Scene.RenderManager.RawBegin();
                //DrawHelper();
                Scene.RenderManager.End();
                Scene.RenderManager.Begin();
            }
            else
            {
                Scene.RenderManager.End();
                Scene.RenderManager.RawBegin();
                DrawHelper();
                Scene.RenderManager.End();
                Scene.RenderManager.Begin();
            }

            base.Draw();
        }

        private void DrawHelper()
        {
            for (int index = 0; index < points.Count - 1; index++)
            {
                DrawLine(points[index], points[index + 1], Color.White);
            }

            if (points.Count > 1)
            {
                DrawLine(points[points.Count - 1], points[0], Color.White);
            }

            if (points.Count > 0 && !isComplete)
            {
                DrawLine(points[points.Count - 1], new Vector2(MouseInput.X, MouseInput.Y), Color.White);
            }

            if (points.Count > 3 && !isComplete)
            {
                for (int i = 0; i < points.Count - 2; i++)
                {
                    DrawZone(points[i], points[i + 1], points[i + 2]);
                }

                DrawZone(points[points.Count - 2], points[points.Count - 1], points[0]);
                DrawZone(points[points.Count - 1], points[0], points[1]);
            }



            if (points.Count > 3 && !isComplete)
            {
                for (int i = 1; i < 2; i++)
                {
                    Vector2 angleVecA = AngleVector(i);
                    Vector2 pointA = points[i];

                    Vector2 angleVecB = AngleVector(i + 1);
                    Vector2 pointB = points[i + 1];

                    //DrawLine(angleVec * depth, angleVec * -height, Color.Red);
                    DrawLine(pointA + (angleVecA * depth), pointA + (angleVecA * -height), Color.Green);
                    DrawLine(pointB + (angleVecB * (depth * 2)), pointB + (angleVecB * -(height * 2)), Color.Red);
                }
            }

        }


        private Vector2 AngleVector(int pointIndex)
        {
            Vector2 pointA = points[pointIndex - 1];
            Vector2 pointB = points[pointIndex];
            Vector2 pointC = points[pointIndex + 1];


            //int size = 10;
            float firstA = (AngleBetweenVectors(pointA, pointB));
            float secondA = (AngleBetweenVectors(pointB, pointC));
            //

            float end = ((firstA + secondA) / 2);

            Vector2 first = Pow(end);

            return first;
        }


        private void DrawZone(Vector2 pointA, Vector2 pointB, Vector2 pointC)
        {
            //int size = 10;
            Vector2 first = Pow(AngleBetweenVectors(pointA, pointB));
            Vector2 second = Pow(AngleBetweenVectors(pointC, pointB));

            DrawLine(pointA + (first * depth), pointB - (second * depth), Color.Pink);
            DrawLine(pointA + (first * -height), pointB - (second * -height), Color.Pink);

            DrawLine(pointA + (first * depth), pointA + (first * -height), Color.White);
        }



        public static Vector2 Pow(float angle)
        {
            return new Vector2((float)Math.Sin(angle), (float)Math.Cos(angle));
        }

        private static double Normalize(double radians)
        {
            while (radians > Math.PI)
            {
                radians -= 2 * Math.PI;
            }
            while (radians < -Math.PI)
            {
                radians += 2 * Math.PI;
            }
            return radians;
        }

        public static float AngleBetweenVectors(Vector2 a, Vector2 b)
        {
            float angle = (float)Math.Atan2((double)(a.Y - b.Y),
                                             (double)(a.X - b.X));
            return -angle;
        }

        private void DrawPolygon()
        {
            effect.TextureEnabled = true;
            effect.Texture = dirtTextue;


            MilkShake.Graphics.SamplerStates[0] = new SamplerState()
            {
                AddressU = TextureAddressMode.Wrap,
                AddressV = TextureAddressMode.Wrap,
                AddressW = TextureAddressMode.Wrap
            };

            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            rs.FillMode = (KeyboardInput.isKeyDown(Keys.LeftShift)) ? FillMode.WireFrame : FillMode.Solid;
            rs.MultiSampleAntiAlias = true;
            MilkShake.Graphics.RasterizerState = rs;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                MilkShake.GraphicsManager.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, verticies, 0, verticies.Length, indices, 0, indices.Length / 3, VertexPositionColorTexture.VertexDeclaration);


               
            }

            effect.TextureEnabled = false;
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();

                foreach (VertexPositionColor[] bam in outlineVerticies)
                {
                    MilkShake.GraphicsManager.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, bam, 0, 2, VertexPositionColor.VertexDeclaration);
                }
                
            }
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

        public override void Update(GameTime gameTime)
        {
            effect.Projection = GetProjectionMatrix();
            effect.View = GetViewMatrix();

            if (!isComplete && (MouseInput.isLeftClicked() || (points.Count > 0 && Vector2.Distance(points[points.Count - 1], MouseInput.Position) > 5)))
            {
                points.Add(MouseInput.Position);
            }

            if (MouseInput.isRightClicked())
            {
                isComplete = false;
                points.Clear();
            }

            if (KeyboardInput.isKeyPressed(Keys.Enter))
            {
                isComplete = true;
               // points.Add(points[0]);

                Vector2[] sourceVerticies;
                int[] sourceIndicies;
                Triangulator.Triangulator.Triangulate(points.ToArray(), Triangulator.WindingOrder.Clockwise, out sourceVerticies, out sourceIndicies);

                verticies = new VertexPositionColorTexture[sourceVerticies.Length];
                



                for (int index = 0; index < sourceVerticies.Length; index++)
                {
                    Color mRandomColor = new Color(
          (Globals.Random.Next(50, 230)),
          (Globals.Random.Next(50, 230)),
          (Globals.Random.Next(50, 230)));


                    verticies[index] = new VertexPositionColorTexture(new Vector3(sourceVerticies[index], 0f), Color.White, sourceVerticies[index] / 10);
                }

                


                indices = new short[sourceIndicies.Length];
                for (int index = 0; index < sourceIndicies.Length; index++)
                {
                    indices[index] = (short)sourceIndicies[index];
                }


                //RepairTextureWrapSeam(verticies.ToList<VertexPositionColorTexture>(), indices.ToList<short>());

                //sourceVerticies.(points[0]);
                //sourceVerticies.Reverse();

                int numOfPoly = indices.Length / 3;

                for (int index = 0; index < numOfPoly; index++)
                {
                    List<short> currentIndie = indices.ToList<short>().GetRange(index * 3, 3);
                    List<Vector2> currentVerts = new List<Vector2>();

                    currentIndie.ForEach(i => currentVerts.Add(sourceVerticies[i]));


                    //= sourceVerticies.ToList<Vector2>().GetRange(index * 3, 3);
                    currentVerts.Reverse();
                    hull = new PolygonHull(WorldPosition, currentVerts.ToList<Vector2>());
                    hull.Hull.Position.Y -= Globals.ScreenHeight;
                    //hull.Hull.Opacity = 0.5f;
                    //hull = new BasicHull(WorldPosition,new Vector2(50, 50));
                    Scene.ComponentManager.GetComponent<LightingComponent>().Light.Hulls.Add(hull.Hull);

                    //currentVerts.Reverse();

                    /*
                     * List<Vector2> physicsVerts = new List<Vector2>();
                    currentVerts.ToList<Vector2>().ForEach(s => physicsVerts.Add(ConvertUnits.ToSimUnits(s)));
                    Body b = BodyFactory.CreatePolygon(PhysicsComponent.World, new FarseerPhysics.Common.Vertices(physicsVerts), 1);
                     */

                    List<Vector2> physicsVerts = new List<Vector2>();
                    currentVerts.ToList<Vector2>().ForEach(s => physicsVerts.Add(ConvertUnits.ToSimUnits(s)));

                    Path path = new Path(physicsVerts);
                    path.Closed = true;
                    path.Add(physicsVerts[0]);

                    Body body = BodyFactory.CreateBody(PhysicsComponent.World);
                    body.BodyType = BodyType.Static;
                    PathManager.ConvertPathToPolygon(path, body, 1.0f, currentVerts.Count);
                }



                outlineVerticies = new List<VertexPositionColor[]>();


                if (points.Count > 3)
                {
                    for (int i = 0; i < points.Count - 2; i++)
                    {
                        MakeZone(points[i], points[i + 1], points[i + 2]);
                    }

                    MakeZone(points[points.Count - 2], points[points.Count - 1], points[0]);
                    MakeZone(points[points.Count - 1], points[0], points[1]);
                }

                

            }

            //if (hull != null) hull.setPosition(points[0]);

            base.Update(gameTime);
        }

        public int depth = 1;
        public int height = 10;

        private void MakeZone(Vector2 pointA, Vector2 pointB, Vector2 pointC)
        {
            //int size = 10;
            Vector2 first = Pow(AngleBetweenVectors(pointA, pointB));
            Vector2 second = Pow(AngleBetweenVectors(pointC, pointB));

            //DrawLine(pointA + (first * depth), pointB - (second * depth), Color.Pink); // Top
            //DrawLine(pointA + (first * -height), pointB - (second * -height), Color.Pink); // Bottom
            //DrawLine(pointA + (first * depth), pointA + (first * -height), Color.White); // Sep

            VertexPositionColor[] ver = new VertexPositionColor[6];

            Color topLeft = new Color(1, 0, 0, 0f);
            Color topRight = new Color(1, 0, 0, 0f);

            Color bottomLeft = new Color(1, 0, 0, 1);
            Color bottomRight = new Color(1, 0, 0, 1);

            topLeft = topRight = new Color(0, 0, 0, 0f);
            bottomLeft = bottomRight = new Color(0, 0, 0, 1f);

            //topLeft = topRight = new Color(1, 0, 0, 0f);
            //bottomLeft = bottomRight = new Color(1, 0, 0, 1f);

            // First
            ver[0].Position = new Vector3(pointA + (first * depth), 0f); // Top Left
            ver[0].Color = topLeft;

            ver[1].Position = new Vector3(pointB - (second * depth), 0f); // Top Right
            ver[1].Color = topRight;

            ver[2].Position = new Vector3(pointB - (second * -height), 0f); // Bottom right
            ver[2].Color = bottomRight;




            ver[3].Position = new Vector3(pointA + (first * depth), 0f); // Top Left
            ver[3].Color = topLeft;

            ver[4].Position = new Vector3(pointB - (second * -height), 0f); // Bottom Right
            ver[4].Color = bottomRight;

            ver[5].Position = new Vector3(pointA + (first * -height), 0f); // Bottom Left
            ver[5].Color = bottomLeft;

            outlineVerticies.Add(ver);
        }


        private PhysicsComponent PhysicsComponent { get { return Scene.ComponentManager.GetComponent<PhysicsComponent>(); } }

        private PolygonHull hull;

        private void DrawLine(Vector2 start, Vector2 end, Color color)
        {
            start = (start - Scene.Camera.Position);
            end = (end - Scene.Camera.Position);

            Scene.RenderManager.SpriteBatch.Draw(lineTexture, start, null, color,
                             (float)Math.Atan2(end.Y - start.Y, end.X - start.X),
                             new Vector2(0f, (float)lineTexture.Height / 2),
                             new Vector2(Vector2.Distance(start, end), 1f),
                             SpriteEffects.None, 0f);
        }
    }

    public class PolygonCreator
    {
    }
}
