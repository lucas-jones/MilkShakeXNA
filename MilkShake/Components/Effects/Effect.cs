using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework.Core.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MilkShakeFramework.Components.Effects
{
    public class Effect : GameEntity
    {
        public override void FixUp()
        {
            base.FixUp();

            Scene.Listener.PostSceneRender += new DrawEvent(PostSceneRender);
        }

        public virtual void PostSceneRender()
        {
            // For override
        }

        public RenderTarget2D RenderTarget { get { return Scene.RenderTarget; } } 
    }

}
