using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework;
using MilkShakeFramework.IO.Input.Devices;
using Playground;

namespace Samples.Scenes.Demos
{

    public class NodeTest : DemoScene
    {
        GameEntity container;
        DisplayObject car;
        Sprite wheel1, wheel2, window;
        public NodeTest() : base("", "")
        {
            car = new DisplayObject();
            car.Position = new Vector2(100, 100);

            car.AddNode(wheel1 = new Sprite("Scenes//Demo1//wheel") { Position = new Vector2(16 + 24, 50 + 20), AutoCenter = true });
            car.AddNode(wheel2 = new Sprite("Scenes//Demo1//wheel") { Position = new Vector2(230 + 24, 50 + 20), AutoCenter = true });
            car.AddNode(window = new Sprite("Scenes//Demo1//window") { Position = new Vector2(125, 5) });
            car.AddNode(new Sprite("Scenes//Demo1//body") { AutoCenter = false });

            AddNode(container = new GameEntity());
            container.AddNode(car);
                        
            new SceneView(this).Show();            
        }

        public override void Update(GameTime gameTime)
        {
            car.Position = MouseInput.Position;

            if (MouseInput.isLeftDown())
            {
                wheel2.Rotation++;
                wheel1.Rotation++;
            }

            if (MouseInput.isRightDown())
            {
                car.Scale = Vector2.Multiply(car.Scale, 1.1f);
                car.Rotation += 0.1f;
            }

            if (KeyboardInput.isKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                window.Y += 1;
            }

            base.Update(gameTime);
        }
    }
}
