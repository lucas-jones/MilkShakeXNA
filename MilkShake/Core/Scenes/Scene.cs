using MilkShakeFramework.Core.Cameras;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Render;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Scenes.Components;
using MilkShakeFramework.Tools.Tween;

namespace MilkShakeFramework.Core.Scenes
{
    public class Scene : GameEntity
    {
        public SceneListener Listener { get; protected set; }

        public SceneComponentManager ComponentManager { get; protected set; }

        public RenderManager RenderManager { get; protected set; }
        public CameraManager CameraManager { get; protected set; }
        public LoadManager LoadManager { get; protected set; }
        public RenderTarget2D RenderTarget { get; protected set; }

        public Camera Camera { get { return CameraManager.CurrentCamera; } }

        private int mRenderWidth, mRenderHeight;
        private int mWidth, mHeight;
        private Color mClearColour;
        private Color mColor;

        public Scene()
        {
            SetScene(this);

            Listener = new SceneListener();

            AddNode(ComponentManager = new SceneComponentManager());
            ComponentManager.AddComponent(LoadManager = new LoadManager());            
            ComponentManager.AddComponent(CameraManager = new CameraManager());
            ComponentManager.AddComponent(RenderManager = new RenderManager());

            // Change to alike of CameraManager?
            TweenerManager.Boot();

            mColor = Color.White;
            mClearColour = Globals.ScreenColour;
        }
        
        public override void Setup()
        {
            mWidth = SetValueOrDefault(mWidth, Globals.ScreenWidth );
            mHeight = SetValueOrDefault(mHeight, Globals.ScreenHeight);

            mRenderWidth = SetValueOrDefault(mRenderWidth, Globals.ScreenWidth);
            mRenderHeight = SetValueOrDefault(mRenderHeight, Globals.ScreenHeight);

            RenderTarget = new RenderTarget2D(MilkShake.Graphics, mRenderWidth, mRenderHeight, false, SurfaceFormat.Color, DepthFormat.Depth16, Globals.MultiSampleRate, RenderTargetUsage.PreserveContents); // Setup

            base.Setup();
        }

        internal void LoadScene()
        {            
            Load(LoadManager);

            Listener.OnLoad();

            LoadManager.SceneLoaded();
        }

        public override void Update(GameTime gameTime)
        {
            LoadManager.Update();
            CameraManager.Update(gameTime);
            Listener.OnUpdate(gameTime);

            TweenerManager.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw()
        {            
            RenderScene(); // Draws to Target Renderer
            DrawScene();   // Draws Scene on screen         
        }

        public void RenderScene()
        { 
            RenderManager.SetRenderTarget(RenderTarget);
            MilkShake.Graphics.Clear(mClearColour);

            Listener.OnPreDraw();
            RenderManager.Begin();

            if(Filter != null) Filter.Begin();

            base.Draw();

            if (Filter != null) Filter.End();

            RenderManager.End();
            Listener.OnPostDraw();

            RenderManager.SetRenderTarget(null);
        }

        private void DrawScene()
        {
            Listener.OnPreSceneRender();

            RenderManager.RawBegin();
            RenderManager.RawDraw(Position, RenderTarget, mWidth, mHeight, mColor);            
            RenderManager.End();

            Listener.OnPostSceneRender();
        }

        public virtual void OnEnterSceen()
        {
        }

        public virtual void OnExitSceen()
        {
        }

        public int RenderWidth { get { return mRenderWidth; } set { mRenderWidth = value; } }
        public int RenderHeight { get { return mRenderHeight; } set { mRenderHeight = value; } }

        public int Width { get { return mWidth; } set { mWidth = value; } }
        public int Height { get { return mHeight; } set { mHeight = value; } }

        public Color SceneColor { get { return mColor; } set { mColor = value; } }
        public Color ClearColor { get { return mClearColour; } set { mClearColour = value; } }

        public override Vector2 WorldPosition { get { return Position; } }
    }
}
