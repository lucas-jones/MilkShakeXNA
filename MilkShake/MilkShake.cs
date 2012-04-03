using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.IO.Input;

namespace MilkShakeFramework
{
    public class MilkShake : Game
    {
        public static GraphicsDeviceManager GraphicsManager;
        public static GraphicsDevice Graphics;
        public static ContentManager ConentManager;

        public MilkShake(int ScreenWidth = Globals.DefaultScreenWidth, int ScreenHeight = Globals.DefaultScreenHeight) : base()
        {
            GraphicsManager = new GraphicsDeviceManager(this);
            ConentManager = Content;

            ChangeResolution(ScreenWidth, ScreenHeight);
        }

        protected override void Initialize()
        {
            Graphics = GraphicsManager.GraphicsDevice;
            

            ConentManager.RootDirectory = Globals.ContentDirectory;
            SceneManager.Setup();
        }

        protected override void Draw(GameTime gameTime)
        {
            Graphics.Clear(Globals.ScreenColour);
            SceneManager.Draw();
        }

        protected override void Update(GameTime gameTime)
        {
            InputManager.UpdateStart();
            SceneManager.Update(gameTime);
            InputManager.UpdateEnd();
        }

        private void ChangeResolution(int Width, int Height)
        {
            GraphicsManager.PreferredBackBufferHeight = Height;
            GraphicsManager.PreferredBackBufferWidth = Width;

            GraphicsManager.SynchronizeWithVerticalRetrace = Globals.EnabledVSync;
            IsFixedTimeStep = Globals.EnabledVSync;

            Globals.ScreenWidth = Width;
            Globals.ScreenHeight = Height;

            GraphicsManager.ApplyChanges();
        }
    }
}
