using System.Collections.Generic;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Krypton;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Components.Effects;
using MilkShakeFramework.Components.Effects.Presets;
using MilkShakeFramework.Components.Lighting;
using MilkShakeFramework.Components.Lighting.Lights;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework.Core.Scenes.Components;
using MilkShakeFramework.Render;
using MilkShakeFramework.Tools.Physics;

namespace MilkShakeFramework.Components.Tile
{
    public class Tile : GameEntity
    {
        public static SpriteSheet mSpriteSheet;
        public static List<Rectangle> mTiles;

        public int X { get { return mX; } }
        public int Y { get { return mY; } }
        public int ID { get { return mID; } }

        private int mX;
        private int mY;
        private int mID;
        
        private Body mBodyTop;
        private Body mBodyBottom;
        private Body mBodyLeft;
        private Body mBodyRight;

        private BasicHull mBasicHull;

        public Tile(int x, int y, int ID)
        {
            mX = x;
            mY = y;
            mID = ID;

            Position = new Vector2(x * 32, y * 32);
            mSpriteSheet = new SpriteSheet("tiles1");
        }

        public override void SetParent(Core.ITreeNode parent)
        {
            base.SetParent(parent);

            if (mSpriteSheet == null) Parent.AddNode(mSpriteSheet);
        }

        public override void FixUp()
        {
            base.FixUp();


            if (mTiles == null)
            {
                mTiles = new List<Rectangle>();
               
                for (int y = 0; y < mSpriteSheet.Texture.Height / 32; y++)
                for (int x = 0; x < mSpriteSheet.Texture.Width / 32; x++)
                {
                    mTiles.Add(new Rectangle(x * 32, y * 32, 32, 32));
                }
            }

            /*

            if (ID == 15)
            {
                // Lava
                // Addparica
                
                AddNode(new ConicLight(512, 8)
                    {
                        Position = WorldPosition + new Vector2(0, 32),
                        Range = 1000,
                        Intensity = 0.3f,
                        Color = new Color(255, 0 , 0)
                    });
                

                
            }

            if (ID == 6)
            {
                if (Scene.ComponentManager.HasComponent<EffectsComponent>())
                {
                    EffectsComponent effectComponent = Scene.ComponentManager.GetComponent<EffectsComponent>();

                    effectComponent.AddEffect(new ReflectiveTileEffect(this));
                }
            }
            */

            if (ID == 124)
            {
                if (Scene.ComponentManager.HasComponent<PhysicsComponent>())
                {

                    float fullWidth = ConvertUnits.ToSimUnits(32);
                    float halfWidth = ConvertUnits.ToSimUnits(32 / 2);



                    Vertices top = new Vertices();
                    top.Add(new Vector2(0, 0));
                    top.Add(new Vector2(-halfWidth, -halfWidth));
                    top.Add(new Vector2(halfWidth, -halfWidth));

                    Vertices left = new Vertices();
                    left.Add(new Vector2(0, 0));
                    left.Add(new Vector2(-halfWidth, halfWidth));
                    left.Add(new Vector2(-halfWidth, -halfWidth));

                    Vertices right = new Vertices();
                    right.Add(new Vector2(0, 0));
                    right.Add(new Vector2(halfWidth, -halfWidth));
                    right.Add(new Vector2(halfWidth, halfWidth));

                    Vertices bottom = new Vertices();
                    bottom.Add(new Vector2(0, 0));
                    bottom.Add(new Vector2(halfWidth, halfWidth));
                    bottom.Add(new Vector2(-halfWidth, halfWidth));


                    mBodyTop = BodyFactory.CreatePolygon(Scene.ComponentManager.GetComponent<PhysicsComponent>().World, top, 1);
                    mBodyBottom = BodyFactory.CreatePolygon(Scene.ComponentManager.GetComponent<PhysicsComponent>().World, bottom, 1);
                    mBodyLeft = BodyFactory.CreatePolygon(Scene.ComponentManager.GetComponent<PhysicsComponent>().World, left, 1);
                    mBodyRight = BodyFactory.CreatePolygon(Scene.ComponentManager.GetComponent<PhysicsComponent>().World, right, 1);

                    fullWidth = 32 / 2;

                    mBodyTop.Position = ConvertUnits.ToSimUnits(WorldPosition + new Vector2(fullWidth, fullWidth));
                    mBodyBottom.Position = ConvertUnits.ToSimUnits(WorldPosition + new Vector2(fullWidth, fullWidth));
                    mBodyLeft.Position = ConvertUnits.ToSimUnits(WorldPosition + new Vector2(fullWidth, fullWidth));
                    mBodyRight.Position = ConvertUnits.ToSimUnits(WorldPosition + new Vector2(fullWidth, fullWidth));

                    mBodyTop.Friction = 0.3f;
                    mBodyTop.UserData = "floor";
                    //mBodyTop.LinearDamping = 0

                    if (mID == 30) mBodyTop.Restitution = 1f;

                    mBodyRight.Friction = 0;
                    mBodyRight.LinearDamping = 0;
                    mBodyLeft.LinearDamping = 0;
                    mBodyLeft.Friction = 0;

                 

                        if (Scene.ComponentManager.HasComponent<LightingComponent>())
                        {

                            LightingComponent lightComponent = Scene.ComponentManager.GetComponent<LightingComponent>();
                            mBasicHull = new BasicHull(WorldPosition, new Vector2(32, 32));

                            lightComponent.Light.Hulls.Add(mBasicHull.Hull);
                        }
                    
                    // mBodyTop.Friction = 0.5f;



                }
                
             
                
            }
           // }            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);            
        }

        public override void Setup()
        {
            base.Setup();

            AddNode(mSpriteSheet);
        }

        public override void Draw()
        {
            base.Draw();

            if (mID > mTiles.Count) mID = 0;

            if (mID != -1) mSpriteSheet.Draw(Position, 32, 32, mTiles[mID]);

            //if (mID == 6) mBasicHull.Hull.Opacity = 0.7f;

        }
        
    }


   


    public class BasicHull : Entity
    {
        private ShadowHull mShadowHull;

        public BasicHull(Vector2 position, Vector2 size)
        {
            Vector2 newPosition = new Vector2();
            newPosition.X = position.X - Globals.ScreenWidthCenter + (size.X / 2);
            newPosition.Y = -position.Y + Globals.ScreenHeightCenter - (size.Y / 2);

            ShadowHull pHull = ShadowHull.CreateRectangle(size);
            pHull.Position = newPosition;

            mShadowHull = pHull;
        }

        public void setPosition(Vector2 position, Vector2 size)
        {
            Vector2 newPosition = new Vector2();
            newPosition.X = position.X - Globals.ScreenWidthCenter + (size.X / 2);
            newPosition.Y = -position.Y + Globals.ScreenHeightCenter - (size.Y / 2);

            mShadowHull.Position = newPosition;
        }



        public ShadowHull Hull { get { return mShadowHull; } }
    }

    public class PolygonHull : Entity
    {
        private ShadowHull mShadowHull;

        public PolygonHull(Vector2 position, List<Vector2> points)
        {

         

            Vector2[] pointsArray = points.ToArray();

            for (int i = 0; i < pointsArray.Length; i++)
            {
                pointsArray[i] = worldToShadow(pointsArray[i]);
            }

            //Array.Reverse(pointsArray);
            ShadowHull pHull = ShadowHull.CreateConvex(ref pointsArray);
            
            Vector2 newPosition = new Vector2();
            newPosition.X = position.X - Globals.ScreenWidthCenter;
            newPosition.Y = -position.Y + Globals.ScreenHeightCenter;

            pHull.Position = newPosition;

            mShadowHull = pHull;
        }

        private Vector2 worldToShadow(Vector2 position)
        {
            Vector2 newPosition = new Vector2();
            newPosition.X = position.X; ;
            newPosition.Y = Globals.ScreenHeight - position.Y;
            //newPosition.Y = 0;

            return newPosition;
        }

        public void setPosition(Vector2 position)
        {
            Vector2 newPosition = new Vector2();
            newPosition.X = position.X - Globals.ScreenWidthCenter;
            newPosition.Y = -position.Y + Globals.ScreenHeightCenter;

            mShadowHull.Position = newPosition;
        }

        public ShadowHull Hull { get { return mShadowHull; } }
    }
}
