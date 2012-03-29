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

        public GameEntity()
        {
            mPosition = Vector2.Zero;
        }

        public override void SetParent(ITreeNode parent)
        {
            if (!(parent is GameEntity)) throw new Exception("Parent must be of GameEntity");

            base.SetParent(parent);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (GameEntity gameEntity in Nodes) gameEntity.Update(gameTime);
        }

        public virtual void Draw()
        {
            foreach (GameEntity gameEntity in Nodes) gameEntity.Draw();
        }

        // [Public]
        public Vector2 Position { get { return mPosition; } }
    }
}
