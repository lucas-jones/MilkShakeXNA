using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.IO.Input;

namespace MilkShakeFramework
{
    public class MilkShake : Game
    {
        public static GraphicsDeviceManager GraphicsManager;
        public static GraphicsDevice Graphics;
        public static ContentManager ConentManager;
        public static Game Game;

        public MilkShake(int ScreenWidth = Globals.DefaultScreenWidth, int ScreenHeight = Globals.DefaultScreenHeight) : base()
        {
            GraphicsManager = new GraphicsDeviceManager(this);

            ConentManager = Content;
            Game = this;

            ApplySettings(ScreenWidth, ScreenHeight, Globals.IsFullscreen);
        }

        protected override void Initialize()
        {
            Graphics = GraphicsManager.GraphicsDevice;
            IsMouseVisible = Globals.IsMouseVisible;

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

        private void ApplySettings(int Width, int Height, bool isFullscreen = false, bool PreferMultiSampling = false)
        {
            GraphicsManager.PreferredBackBufferHeight = Height;
            GraphicsManager.PreferredBackBufferWidth = Width;
            GraphicsManager.IsFullScreen = isFullscreen;

            IsFixedTimeStep = Globals.EnabledVSync;
            IsMouseVisible = Globals.IsMouseVisible;

            Globals.ScreenWidth = Width;
            Globals.ScreenHeight = Height;

            GraphicsManager.ApplyChanges();
        }
    }
}
