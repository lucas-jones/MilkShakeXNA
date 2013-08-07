using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Krypton;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Components.Lighting.Hulls
{
    public abstract class AbstractHull : LightComponentGameEntity
    {
        public ShadowHull ShadowHull { get; protected set; }

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
