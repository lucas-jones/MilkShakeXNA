using System;
using System.Linq;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Filters;
using MilkShakeFramework.Tools.Maths;
using MilkShakeFramework.IO.Input.Devices;

namespace MilkShakeFramework.Core.Game
{
    public class GameEntity : Entity
    {
        public virtual Vector2 Position { get; set; }
        public virtual Vector2 WorldPosition { get { return (Parent as GameEntity).WorldPosition + Position; } }

        public virtual float X { get { return Position.X; } set { Position = new Vector2(value, Position.Y); } }
        public virtual float Y { get { return Position.Y; } set { Position = new Vector2(Position.X, value); } }

        public virtual GameEntityListener Listener { get; protected set; }
        public virtual bool AutoDraw { get; set; }

        // Todo: BoundingBox needs to be generated correctly..
        public virtual RotatedRectangle BoundingBox { get { return new RotatedRectangle((int)WorldPosition.X, (int)WorldPosition.Y, 10, 10); } }

        protected Filter _filter;

        public GameEntity()
        {
            Position = Vector2.Zero;
            AutoDraw = true;
            Listener = new GameEntityListener();
        }

        public override void SetParent(ITreeNode parent)
        {
            if (!(parent is GameEntity)) throw new Exception("Parent must be of GameEntity");
            
            base.SetParent(parent);
        }

        public override void FixUp()
        {
            base.FixUp();

            Listener.OnFixup();
        }

        public override void Load(LoadManager content)
        {
            base.Load(content);

            Listener.OnLoad();
        }

        public override void Setup()
        {
            base.Setup();

            Listener.OnSetup();
        }

        public virtual void Update(GameTime gameTime)
        {
            Listener.OnUpdate(gameTime);

            foreach (GameEntity gameEntity in Nodes.OfType<GameEntity>().ToArray<GameEntity>()) if(gameEntity.IsLoaded) gameEntity.Update(gameTime);
        }

        public virtual Matrix GenerateMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(Position, 0));
        }

        public virtual void Draw()
        {
            if(AutoDraw)
            {
                foreach (GameEntity gameEntity in Nodes.OfType<GameEntity>().ToArray<GameEntity>())
                {
                    //Matrix backup = Scene.Camera.Matrix;
                    //Matrix newMatrix = ;
                    //Scene.Camera.Matrix = newMatrix * backup;

                    Scene.MatrixStack.Push(gameEntity.GenerateMatrix());

                    // Setup Matrix
                    //Scene.RenderManager.End();
                    //Scene.RenderManager.Begin();
                    Scene.Effect.World = Scene.MatrixStack.Top;

                    if (gameEntity.IsLoaded)
                    {
                        if (gameEntity.Filter != null) gameEntity.Filter.Begin();
                        gameEntity.Draw();
                        if (gameEntity.Filter != null) gameEntity.Filter.End();
                    }

                    Scene.MatrixStack.Pop();
                }
            }

                      
        }

        public virtual Filter Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                if (_filter != null) RemoveNode(_filter);
                _filter = value;
                AddNode(_filter);
            }
        }
    }
}
