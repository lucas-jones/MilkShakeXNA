using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework;
using MilkShakeFramework.IO.Input.Devices;
using MilkShakeFramework.Core;
using MilkShakeFramework.Render;
using MilkShakeFramework.Tools.Utils;

namespace Samples.Scenes.Demos
{
    public class Ball : Sprite
    {
        public static int BALL_WIDTH = 200;
        public static int BALL_HEIGHT = 30;

        public Vector2 Velocity;

        public Ball() : base(TextureUtils.GenerateTexture(20, 20, Color.White))
        {
            AutoCenter = true;
            Position = Globals.ScreenCenter;
            Velocity = new Vector2(5, 5);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            Position += Velocity;
            
            // Left & Right Walls
            if (X > Globals.ScreenWidth || X < 0) Velocity.X *= -1;
            
            // Bottom
            if (Y > Globals.ScreenHeight)
            {
                Position = Globals.ScreenCenter;
                Velocity = new Vector2(5, 5);
            }
            // Paddle Collision
            if (BoundingBox.Intersects(DemoScene.Paddle.BoundingBox))
            {
                Velocity.Y *= -1;
            }
            
            foreach (INode node in DemoScene.Bricks.Nodes.ToArray().ToList())
            {
                if ((node as Sprite).BoundingBox.CollisionRectangle.Intersects(BoundingBox.CollisionRectangle))
                {
                    Velocity.Y *= -1;

                    node.Parent.RemoveNode(node);
                }
            }
        }

        public Demo_BreakOut DemoScene
        {
            get { return Scene as Demo_BreakOut; }
        }
    }

    public class Demo_BreakOut : DemoScene
    {
        public static int BRICK_WIDTH = 200;
        public static int BRICK_HEIGHT = 30;

        public static int TILE_X_COUNT = 8;
        public static int TILE_Y_COUNT = 5;

        public GameEntity Bricks { get; private set; }
        public Sprite Paddle { get; private set; }
        private Ball ball;

        private PrimitiveRenderer primitiveRenderer;

        public Demo_BreakOut() : base("", "")
        {
            MilkShake.Game.IsMouseVisible = true;

            AddNode(Bricks = new GameEntity());

            Texture2D redBrick = TextureUtils.GenerateTexureWithBorder(BRICK_WIDTH, BRICK_HEIGHT, Color.Red, 1, Color.DarkRed);
            Texture2D blueBrick = TextureUtils.GenerateTexureWithBorder(BRICK_WIDTH, BRICK_HEIGHT, Color.Blue, 1, Color.DarkBlue);

            for (int x = 0; x < TILE_X_COUNT; x++)
            for (int y = 0; y < TILE_Y_COUNT; y++)
            {
                Bricks.AddNode(new Sprite(((x + y) % 2 == 0) ? redBrick : blueBrick)
                {
                    Position = new Vector2(BRICK_WIDTH * x, BRICK_HEIGHT * y)
                });
            }

            Paddle = new Sprite(TextureUtils.GenerateTexture(300, 30, Color.White))
            {
                Y = Globals.ScreenHeight - 100,
                AutoCenter = true
            };

            AddNode(ball = new Ball());
            AddNode(Paddle);
        }

        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (PadInput.GetPad(PlayerIndex.One).isButtonPressed(Microsoft.Xna.Framework.Input.Buttons.A))
            {
                AddNode(new Ball());
            }

            Paddle.X += PadInput.GetPad(PlayerIndex.One).ThumbSticks.Left.X * 10;
        }
    }
}
