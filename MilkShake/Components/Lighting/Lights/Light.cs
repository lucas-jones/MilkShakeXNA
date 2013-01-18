using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Krypton.Lights;
using Krypton;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Scenes.Components;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework.Core.Content;

namespace MilkShakeFramework.Components.Lighting.Lights
{
    public abstract class Light : GameEntity
    {
        internal Light2D mLight2D;

        public float Range
        {
            get { return mLight2D.Range; }
            set { mLight2D.Range = value; }
        }

        public Color Color
        {
            get { return mLight2D.Color; }
            set { mLight2D.Color = value; }
        }

        public float Fov
        {
            get { return mLight2D.Fov; }
            set { mLight2D.Fov = value; }
        }

        public float Angle
        {
            get { return mLight2D.Angle; }
            set { mLight2D.Angle = value; }
        }

        public float Intensity
        {
            get { return mLight2D.Intensity; }
            set { mLight2D.Intensity = value; }
        }

        public float Alpha
        {
            get { return mLight2D.Color.A / 255; }
            // Wtf..
            set { mLight2D.Color = new Color(mLight2D.Color.ToVector3()) { A = (byte)(value * 255) }; }
        }

        public bool IsOn
        {
            get { return mLight2D.IsOn; }
            set { mLight2D.IsOn = value; }
        }

        public Light()
        {
            mLight2D = new Light2D();

            // [Default Values]
            Range = 400;
        }


        public override void Load(LoadManager content)
        {
            base.Load(content);
            
            mLight2D.Texture = Texture;
        }

        public override void FixUp()
        {
            base.FixUp();

            LightingComponent.Light.Lights.Add(mLight2D);
        }

        private LightingComponent LightingComponent
        {
            get { return Scene.ComponentManager.GetComponent<LightingComponent>(); }
        }

        public override Vector2 Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                base.Position = value;
                mLight2D.Position =  PositionToLightPosition(value);                
            }
        }

        public override float X
        {
            get
            {
                return base.X;
            }
            set
            {
                Position = new Vector2(X, Position.Y);
                base.X = value;
            }
        }

        public override float Y
        {
            get
            {
                return base.Y;
            }
            set
            {
                Position = new Vector2(Position.X, Y);
                base.Y = value;
            }
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            mLight2D.Position = PositionToLightPosition(WorldPosition);
        }

        public virtual Texture2D Texture
        {
            get { return LightTextureBuilder.CreateConicLight(MilkShake.Graphics, 512, 8); }
        }

        private Vector2 PositionToLightPosition(Vector2 aPosition)
        {
            return new Vector2
            {
                X = aPosition.X - Globals.ScreenWidthCenter,
                Y = -aPosition.Y + Globals.ScreenHeightCenter
            };
        }

    }
}
