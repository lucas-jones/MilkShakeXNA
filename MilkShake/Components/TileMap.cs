using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework.Render;
using MilkShakeFramework.Core.Content;
using Microsoft.Xna.Framework;
using MilkShakeFramework.IO.Input.Devices;

namespace MilkShakeFramework.Components
{
    public class TileMap : GameEntity
    {
        private SpriteSheet mSpriteSheet;

        private int[,] mMap;
        private int[,] mTileMap;
        private int mWidth = 50;
        private int mHeight = 40;

        public TileMap()
        {
            mSpriteSheet = new SpriteSheet("tileset");

            mMap = new int[mWidth, mHeight];

            for (int x = 0; x < mWidth; x++)
            for (int y = 0; y < mHeight; y++)
                mMap[x, y] = 1;


            mMap[5, 5] = 0;
            mMap[4, 5] = 0;
            mMap[3, 5] = 0;

            mMap[5, 4] = 0;
            mMap[4, 4] = 0;
            mMap[3, 4] = 0;



           
        }        

        private void generateMap()
        {
            mTileMap = new int[mWidth, mHeight];

            for (int x = 0; x < mWidth; x++)
            for (int y = 0; y < mHeight; y++)
                mTileMap[x, y] = 15;

            for (int x = 1; x < mWidth - 1; x++)
            {
                for (int y = 1; y < mHeight - 1; y++)
                {
                    if (mMap[x, y] == 1)
                    {
                        int above = (mMap[x, y - 1] == 1) ? 1 : 0;
                        int right = (mMap[x + 1, y] == 1) ? 2 : 0;
                        int below = (mMap[x, y + 1] == 1) ? 4 : 0;
                        int left  = (mMap[x - 1, y] == 1) ? 8 : 0;

                        mTileMap[x, y] = (above + below + left + right);
                    }
                }
            }

            for (int x = 0; x < mWidth; x++)
                for (int y = 0; y < mHeight; y++)
                    OnTiledAdded(x, y, mMap[x, y]);

        }

        public virtual void OnTiledAdded(int x, int y, int type)
        {

        }

        public override void Setup()
        {
            generateMap();

            AddNode(mSpriteSheet);
            base.Setup();
        }

        public override void Load(LoadManager content)
        {
            base.Load(content);
        }

        public override void FixUp()
        {
            base.FixUp();
        }

        public override void Draw()
        {            
            MilkShake.Graphics.Clear(new Color(152, 142, 133));

            for (int x = 0; x < mWidth; x++)
            {
                for (int y = 0; y < mHeight; y++)
                {
                    int index = mMap[x, y];

                    if (index == 1)
                    {

                        mSpriteSheet.Draw(new Vector2(x * 30, y * 30), 1, 1, new Rectangle(15 * 30, 0, 30, 30));
                    }
                }
            }            

            base.Draw();
        }

        public override void Update(GameTime gameTime)
        {
            if (MouseInput.isLeftClicked())
            {
                Vector2 mousePosition = MouseInput.Position;

                int x = (int)mousePosition.X / 30;
                int y = (int)mousePosition.Y / 30;

                try
                {
                    mMap[x, y] = 0;

                }
                catch (Exception e)
                { }
                generateMap();
                
            }

            if (MouseInput.isRightClicked())
            {
                Vector2 mousePosition = MouseInput.Position;

                int x = (int)mousePosition.X / 30;
                int y = (int)mousePosition.Y / 30;

                mMap[x, y] = 1;
                generateMap();
            }
            
            

            base.Update(gameTime);
        }

    }


}
