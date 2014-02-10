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

        public int RenderWidth { get; protected set; }
        public int RenderHeight { get; protected set; }

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public Color SceneColor { get; protected set; }
        public Color ClearColor { get; protected set; }

        public MatrixStack MatrixStack;
        public BasicEffect Effect;

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

            MatrixStack = new MatrixStack();
            SceneColor = Color.White;
            ClearColor = Globals.ScreenColour;

            Effect = new BasicEffect(MilkShake.Graphics);
            Effect.View = Matrix.Identity;
            Effect.World = Matrix.Identity;
            Effect.Projection = Matrix.CreateOrthographic(MilkShake.Graphics.Viewport.Width, -MilkShake.Graphics.Viewport.Height, 1, -1);
            
            Effect.TextureEnabled = true;
        }
        
        public override void Setup()
        {
            Width = SetValueOrDefault(Width, Globals.ScreenWidth);
            Height = SetValueOrDefault(Height, Globals.ScreenHeight);

            RenderWidth = SetValueOrDefault(RenderWidth, Globals.ScreenWidth);
            RenderHeight = SetValueOrDefault(RenderHeight, Globals.ScreenHeight);

            RenderTarget = new RenderTarget2D(MilkShake.Graphics, RenderWidth, RenderHeight, false, SurfaceFormat.Color, DepthFormat.Depth16, Globals.MultiSampleRate, RenderTargetUsage.PreserveContents);

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
            //MatrixStack.LoadIdentity();

            RenderManager.SetRenderTarget(RenderTarget);
            MilkShake.Graphics.Clear(ClearColor);

            MatrixStack.Clear();
            MatrixStack.Push(Matrix.CreateTranslation(new Vector3(-Globals.ScreenCenter, 0)));


            Listener.OnPreDraw();
            RenderManager.Begin(Effect);
            
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
            RenderManager.RawDraw(Position, RenderTarget, Width, Height, SceneColor);            
            RenderManager.End();

            Listener.OnPostSceneRender();
        }

        public virtual void OnEnterSceen() { }
        public virtual void OnExitSceen() { }

        public override Vector2 WorldPosition { get { return Position; } }
    }
}
