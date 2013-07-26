using System;
using MilkShakeFramework.Core.Cameras;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Render;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Tools.Physics;
using MilkShakeFramework.Core.Scenes.Components;
using MilkShakeFramework.Core.Events;
using MilkShakeFramework.Tools.Tween;

namespace MilkShakeFramework.Core.Scenes
{
    public class Scene : GameEntity
    {
        private SceneListener mSceneListener;

        private RenderManager mRenderManager;
        private CameraManager mCameraManager;
        private LoadManager mLoadManager;

        private SceneComponentManager mComponentManager;

        private RenderTarget2D mRenderTarget;
        private int mRenderWidth, mRenderHeight;
        private int mWidth, mHeight;
        private Color mClearColour;
        private Color mColor;

        private EventDispatcher mEventDispatcher;
        
        public Scene()
        {
            SetScene(this);

            mSceneListener = new SceneListener();
            mEventDispatcher = new EventDispatcher();

            mLoadManager = new LoadManager(this);
            mCameraManager = new CameraManager(this);
            mRenderManager = new RenderManager(this);

            TweenerManager.Boot();

            mComponentManager = new SceneComponentManager();
                        
            ConvertUnits.SetDisplayUnitToSimUnitRatio(24f);
            mColor = Color.White;
            mClearColour = Globals.ScreenColour;
        }

        public Scene(Color clearColor) : this()
        {
            mClearColour = clearColor;
        }

        public override void Setup()
        {
            mWidth = SetValueOrDefault(mWidth, Globals.ScreenWidth );
            mHeight = SetValueOrDefault(mHeight, Globals.ScreenHeight);
            mRenderWidth = SetValueOrDefault(mRenderWidth, Globals.ScreenWidth);
            mRenderHeight = SetValueOrDefault(mRenderHeight, Globals.ScreenHeight);

            mRenderTarget = new RenderTarget2D(MilkShake.Graphics, mRenderWidth, mRenderHeight, false, SurfaceFormat.Color, DepthFormat.Depth16, Globals.MultiSampleRate, RenderTargetUsage.PreserveContents); // Setup

            base.Setup();
        }

        internal void LoadScene()
        {            
            Load(ContentManager);
            mLoadManager.SceneLoaded();

            Listener.OnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            mLoadManager.Update();
            mCameraManager.Update(gameTime);
            TweenerManager.Update(gameTime);
            Listener.OnUpdate(gameTime);

            base.Update(gameTime);

            Console.WriteLine(Nodes.Count);
        }

        public override void Draw()
        {
            
            RenderScene(); // Draws to Target Renderer
            DrawScene();   // Draws Scene on screen         
         
        }

        public void RenderScene()
        {            
            RenderManager.SetRenderTarget(mRenderTarget);

            MilkShake.Graphics.Clear(mClearColour);

            mSceneListener.OnPreDraw();
            RenderManager.Begin();

            if(Filter != null) Filter.Begin();
            base.Draw();
            if (Filter != null) Filter.End();

            RenderManager.End();
            mSceneListener.OnPostDraw();
            RenderManager.SetRenderTarget(null);
        }

        private void DrawScene()
        {
            RenderManager.RawBegin();
            mSceneListener.OnPreSceneRender();
            RenderManager.RawDraw(Position, mRenderTarget, mWidth, mHeight, mColor);            
            RenderManager.End();
            mSceneListener.OnPostSceneRender();
        }

        
        
        

        // [Public]
        public SceneListener Listener { get { return mSceneListener; } }
        public EventDispatcher EventDispatcher { get { return mEventDispatcher; } }


        public CameraManager CameraManager { get { return mCameraManager; } }
        public LoadManager ContentManager { get { return mLoadManager; } }



        // Component
        public SceneComponentManager ComponentManager { get { return mComponentManager; } }

        public Camera Camera { get { return mCameraManager.CurrentCamera; } }
        public RenderManager RenderManager { get { return mRenderManager; } }

        public RenderTarget2D RenderTarget { get { return mRenderTarget; } } 

        public int RenderWidth { get { return mRenderWidth; } set { mRenderWidth = value; } }
        public int RenderHeight { get { return mRenderHeight; } set { mRenderHeight = value; } }

        public int Width { get { return mWidth; } set { mWidth = value; } }
        public int Height { get { return mHeight; } set { mHeight = value; } }

        public Color Color { get { return mColor; } set { mColor = value; } }
        public Color ClearColor { get { return mClearColour; } set { mClearColour = value; } }

        public override Vector2 WorldPosition { get { return Position; } }
    }
}
