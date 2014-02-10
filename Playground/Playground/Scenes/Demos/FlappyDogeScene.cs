using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework.IO.Input.Devices;
using MilkShakeFramework.Tools.Utils;
using MilkShakeFramework;

namespace Samples.Scenes.Demos
{
    public class FlappyDogeScene : Scene
    {
        DisplayObject container;
        DisplayObject floor;

        float gravity = 0.2f;
        float gravityForce = 0.01f;

        Sprite doge;
        float vel = 0;

        public FlappyDogeScene() : base()
        {
            ClearColor = new Microsoft.Xna.Framework.Color(74, 195, 206);

            container = new DisplayObject() { Y = 200 };

            container.AddNode(new Sprite("Scenes//Flappy//clouds"));
            container.AddNode(new Sprite("Scenes//Flappy//buildings") { Y = 65 });
            container.AddNode(new Sprite("Scenes//Flappy//buildings") { Y = 65, X = 180 });
            container.AddNode(new Sprite("Scenes//Flappy//buildings") { Y = 65, X = 180 * 2 });
            container.AddNode(new Sprite("Scenes//Flappy//buildings") { Y = 65, X = 180 * 3 });

            floor = new DisplayObject() { Y = 65 + 165 + 200 };

            for (int i = 0; i < 20; i++)
            {
                floor.AddNode(new Sprite("Scenes//Flappy//floor") { X = 60 * i });
            }
         
            doge = new Sprite("Scenes//Flappy//birds");

            AddNode(container);
            AddNode(floor);
            AddNode(doge);

            GeneratePipe();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            floor.X -= 5;

            if (floor.X < -(60 * 4)) floor.X = 0;

            vel += 0.2f;

            doge.Y += vel;

            if (MouseInput.isLeftClicked())
            {
                vel = 0;
                vel += -5;
            }

            if (MouseInput.isRightClicked())
            {
                GeneratePipe();
            }

            bool gameOver = false;
            Pipes.ForEach(pipe =>
            {
                pipe.X -= 5;

                if ((pipe.Nodes[0] as Sprite).BoundingBox.Intersects(doge.BoundingBox) || (pipe.Nodes[1] as Sprite).BoundingBox.Intersects(doge.BoundingBox))
                {
                    gameOver = true;             
                }
            });

            if (gameOver == true)
            {
                foreach (DisplayObject item in Pipes)
                {
                    Environment.Exit(1);
                }
                //Pipes.ForEach(pipea => );
            }
        }

        public List<DisplayObject> Pipes = new List<DisplayObject>();

        private void GeneratePipe()
        {
            DisplayObject pipe = new DisplayObject() { Name = "Pipe" };

            Sprite pipeBot = new Sprite("Scenes//Flappy//pipeB") { X = 300, Y = 300, };
            pipe.AddNode(pipeBot);

            Sprite pipeTop = new Sprite("Scenes//Flappy//pipe") { X = 300, Y = -600 };
            pipe.AddNode(pipeTop);

            AddNode(pipe);

            Pipes.Add(pipe);

            pipe.Y = (float)(Globals.Random.NextDouble() * 200) - 100;

            new Wait(1400, () =>
            {
                GeneratePipe();
            });
        }
    }
}
