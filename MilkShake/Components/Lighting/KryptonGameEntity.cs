using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Components.Lighting
{
    public class KryptonGameEntity : GameEntity
    {
        public Vector2 KryptonPosition { get; protected set; }

        public KryptonGameEntity() : base()
        {
            // Update KryptonPosition on creation
            Position = Vector2.Zero;
        }

        public override Vector2 Position
        {
            get { return base.Position; }
            set
            {
                base.Position = value;

                KryptonPosition = WorldPositionToLightPosition(value);
            }
        }

        protected Vector2 WorldPositionToLightPosition(Vector2 position)
        {
            return new Vector2
            {
                X = position.X - Globals.ScreenWidthCenter,
                Y = -position.Y + Globals.ScreenHeightCenter
            };
        }

        public LightingComponent LightingComponent
        {
            get { return Scene.ComponentManager.GetComponent<LightingComponent>(); }
        }
    }
}
