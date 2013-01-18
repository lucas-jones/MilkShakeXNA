using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Filters;
using Microsoft.Xna.Framework.Graphics;

namespace MilkShakeFramework.Core.Game
{
    public class GameEntity : Entity
    {
        private Vector2 mPosition;
        private bool mAutoDraw;
        private GameEntityListener mListener;

        private Filter mFilter;

        public GameEntity()
        {
            mPosition = Vector2.Zero;
            mAutoDraw = true;
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
            if(mAutoDraw)
            {
                foreach (GameEntity gameEntity in Nodes.OfType<GameEntity>().ToArray<GameEntity>())
                {
                    if (gameEntity.IsLoaded)
                    {
                        if (gameEntity.Filter != null) gameEntity.Filter.Begin();
                        gameEntity.Draw();
                        if (gameEntity.Filter != null) gameEntity.Filter.End();
                    }
                }
            }
        }

        public virtual Filter Filter
        {
            get { return mFilter; }
            set 
            {
                /* Feels a bit dirty this */
                if (Filter != null) RemoveNode(Filter);
                mFilter = value;
                AddNode(Filter);
            }
        }

        // [Public]
        public virtual GameEntityListener Listener { get { return mListener; } }
        public virtual Vector2 Position { get { return mPosition; } set { mPosition = value; }  }
        public virtual float X { get { return mPosition.X; } set { mPosition = new Vector2(value, mPosition.Y); } }
        public virtual float Y { get { return mPosition.Y; } set { mPosition = new Vector2(mPosition.X, value); } }

        public virtual Vector2 WorldPosition { get { return (Parent as GameEntity).Position + Position; } }

        public virtual bool AutoDraw { get { return mAutoDraw; } set { mAutoDraw = value; } }
    }
}
