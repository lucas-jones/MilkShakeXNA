using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Scenes;

namespace MilkShakeFramework.Core.Game
{
    public class GameEntity : Entity
    {
        private Vector2 mPosition;
        private GameEntityListener mListener;

        public GameEntity()
        {
            mPosition = Vector2.Zero;
            mListener = new GameEntityListener();
        }

        public override void SetParent(ITreeNode parent)
        {
            if (!(parent is GameEntity)) throw new Exception("Parent must be of GameEntity");

            base.SetParent(parent);
        }

        public override void FixUp()
        {
            base.FixUp();

            mListener.OnFixup();
        }

        public override void Load(LoadManager content)
        {
            base.Load(content);

            mListener.OnLoad();
        }

        public override void Setup()
        {
            base.Setup();

            mListener.OnSetup();
        }

        public virtual void Update(GameTime gameTime)
        {
            mListener.OnUpdate(gameTime);

            foreach (GameEntity gameEntity in Nodes.OfType<GameEntity>().ToArray<GameEntity>()) if(gameEntity.IsLoaded) gameEntity.Update(gameTime);
        }

        public virtual void Draw()
        {
            foreach (GameEntity gameEntity in Nodes.OfType<GameEntity>().ToArray<GameEntity>()) if (gameEntity.IsLoaded) gameEntity.Draw();
        }


        // [Public]
        public virtual GameEntityListener Listener { get { return mListener; } }
        public virtual Vector2 Position { get { return mPosition; } set { mPosition = value; }  }
        public virtual float X { get { return mPosition.X; } set { mPosition = new Vector2(value, mPosition.Y); } }
        public virtual float Y { get { return mPosition.Y; } set { mPosition = new Vector2(mPosition.X, value); } }

        public virtual Vector2 WorldPosition { get { return (Parent as GameEntity).Position + Position; } }
    }
}
