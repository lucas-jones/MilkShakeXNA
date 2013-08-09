using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Krypton;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Components.Lighting.Hulls
{
    public abstract class AbstractHull : KryptonGameEntity
    {
        public ShadowHull ShadowHull { get; protected set; }

        public float Rotation
        {
            get { return ShadowHull.Angle; }
            set { ShadowHull.Angle = value; }
        }

        public float Opacity
        {
            get { return ShadowHull.Opacity; }
            set { ShadowHull.Opacity = value; }
        }

        public Vector2 Scale
        {
            get { return ShadowHull.Scale; }
            set { ShadowHull.Scale = value; }

        }
        public AbstractHull() { }

        public override void Load(Core.Content.LoadManager content)
        {
            ShadowHull = GenerateShadowHull();

            LightingComponent.Krypton.Hulls.Add(ShadowHull);

            base.Load(content);
        }

        public override void Destroy()
        {
            LightingComponent.Krypton.Hulls.Remove(ShadowHull);

            base.Destroy();
        }

        public override void Update(GameTime gameTime)
        {
            ShadowHull.Position = KryptonPosition;

            base.Update(gameTime);
        }

        protected abstract ShadowHull GenerateShadowHull();
    }
}
