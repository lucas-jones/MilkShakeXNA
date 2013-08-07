using Krypton;
using Krypton.Lights;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Game;

namespace MilkShakeFramework.Components.Lighting.Lights
{
    public abstract class AbstractLight : LightComponentGameEntity
    {
        public Light2D Light { get; protected set; }

        public float Range
        {
            get { return Light.Range; }
            set { Light.Range = value; }
        }

        public Color Color
        {
            get { return Light.Color; }
            set { Light.Color = value; }
        }

        public float Fov
        {
            get { return Light.Fov; }
            set { Light.Fov = value; }
        }

        public float Angle
        {
            get { return Light.Angle; }
            set { Light.Angle = value; }
        }

        public float Intensity
        {
            get { return Light.Intensity; }
            set { Light.Intensity = value; }
        }

        public float Alpha
        {
            get { return Light.Color.A / 255; }
            // Wtf..
            set { Light.Color = new Color(Light.Color.ToVector3()) { A = (byte)(value * 255) }; }
        }

        public bool IsOn
        {
            get { return Light.IsOn; }
            set { Light.IsOn = value; }
        }

        public AbstractLight()
        {
            Light = new Light2D();

            // [Default Values]
            Range = 400;
        }


        public override void Load(LoadManager content)
        {
            base.Load(content);
            
            Light.Texture = GenerateTexture();

            LightingComponent.Krypton.Lights.Add(Light);
        }

        public override void Destroy()
        {
            base.Destroy();

            LightingComponent.Krypton.Lights.Remove(Light);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Light.Position = KryptonPosition;
        }

        public abstract Texture2D GenerateTexture();
    }
}
