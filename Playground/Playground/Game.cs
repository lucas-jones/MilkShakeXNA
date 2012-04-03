using MilkShakeFramework;
using MilkShakeFramework.Core.Scenes;
using Microsoft.Xna.Framework;
using System;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.IO.Input.Devices;

namespace Playground
{
    public class Playground : MilkShake
    {
        public Playground() : base(1280, 720) 
        {
            
        }

        protected override void Initialize()
        {
            base.Initialize();

            SceneManager.AddScene("BasicScene", new BasicScene());
            SceneManager.ChangeScreen("BasicScene");
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        
    }

    public class BasicScene : Scene
    {

        private GameEntity tileGroup;

        public BasicScene() : base()
        {
        }

        private void Pixelize()
        {
            int zoom = 16;
            RenderWidth = Globals.ScreenWidth / zoom;
            RenderHeight = Globals.ScreenHeight / zoom;
            RenderManager.SamplerState = SamplerState.PointClamp;

            Camera.Width = Globals.ScreenWidth / zoom;
            Camera.Height = Globals.ScreenHeight / zoom;
        }

        public override void Setup()
        {
            AddNode(new Sprite("background") { Width = Globals.ScreenWidth, Height = Globals.ScreenHeight });

            tileGroup = new GameEntity();           

            tileGroup.AddNode(new Sprite("Tiles/Dirt Block"));
            tileGroup.AddNode(new Sprite("Tiles/Grass Block") { Position = new Vector2(100, 0) });
            tileGroup.AddNode(new Sprite("Tiles/Grass Block") { Position = new Vector2(200, 0) });
            tileGroup.AddNode(new Sprite("Tiles/Grass Block") { Position = new Vector2(300, 0) });
            tileGroup.AddNode(new Sprite("Tiles/Grass Block") { Position = new Vector2(400, 0) });
            tileGroup.AddNode(new Sprite("Tiles/Dirt Block") { Position = new Vector2(500, 0) });

            AddNode(tileGroup);

            AddNode(new Tile());
            AddNode(new Tile() { Position = new Vector2(100, 0) });
            AddNode(new Tile() { Position = new Vector2(200, 100) });
            AddNode(new Tile() { Position = new Vector2(300, 0) });
            AddNode(new Tile() { Position = new Vector2(400, 0) });
            AddNode(new Tile() { Position = new Vector2(500, 0) });
            AddNode(new Tile() { Position = new Vector2(600, 0) });
            AddNode(new Tile() { Position = new Vector2(700, 0) });

            base.Setup();
        }

        public override void Update(GameTime gameTime)
        {            
            base.Update(gameTime);

            tileGroup.Position = MouseInput.Position + Camera.Position;

            ControlCamera();
        }

        private void ControlCamera()
        {
            Vector2 movementStash = Vector2.Zero;

            if (KeyboardInput.isKeyDown(Keys.D)) movementStash.X++;
            if (KeyboardInput.isKeyDown(Keys.A)) movementStash.X--;
            if (KeyboardInput.isKeyDown(Keys.W)) movementStash.Y--;
            if (KeyboardInput.isKeyDown(Keys.S)) movementStash.Y++;

            if (KeyboardInput.isKeyDown(Keys.E)) Camera.Zoom += 0.001f;
            if (KeyboardInput.isKeyDown(Keys.Q)) Camera.Zoom -= 0.001f;

            Camera.Position = (movementStash * 4) + Camera.Position;
        }

        public override void Draw()
        {
            base.Draw();
        }
    }
}
