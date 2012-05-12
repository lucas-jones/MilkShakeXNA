using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework.Render;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Krypton;
using MilkShakeFramework.Tools.Physics;
using MilkShakeFramework.Core.Scenes.Components;
using MilkShakeFramework.Components.Lighting;
using FarseerPhysics.Common;

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
        private Body mBody;
        private BasicHull mBasicHull;

        public Tile(int x, int y, int ID)
        {
            mX = x;
            mY = y;
            mID = ID;

            Position = new Vector2(x * 64, y * 64);
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
               
                for (int y = 0; y < mSpriteSheet.Texture.Height / 64; y++)
                for (int x = 0; x < mSpriteSheet.Texture.Width / 64; x++)
                {
                    mTiles.Add(new Rectangle(x * 64, y * 64, 64, 64));
                }
            }

            if (ID != -1)
            {
                if (Scene.ComponentManager.HasComponent<PhysicsComponent>())
                {
                    float pWidth = ConvertUnits.ToSimUnits(65);
                    mBody = BodyFactory.CreateRectangle(Scene.ComponentManager.GetComponent<PhysicsComponent>().World, pWidth, pWidth, 1);
                   
                    mBody.BodyType = BodyType.Static;
                    mBody.Position = ConvertUnits.ToSimUnits(WorldPosition + new Vector2(32, 32));
                    mBody.Friction = 3;


                    
                }

                if(Scene.ComponentManager.HasComponent<LightingComponent>())
                {
                    LightingComponent lightComponent = Scene.ComponentManager.GetComponent<LightingComponent>();
                    mBasicHull = new BasicHull(WorldPosition, new Vector2(64, 64));

                    lightComponent.Light.Hulls.Add(mBasicHull.Hull);
                }
                
            }            
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

            if (mID != -1) mSpriteSheet.Draw(Position, 64, 64, mTiles[mID]);        

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
}
