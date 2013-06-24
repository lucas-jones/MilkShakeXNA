using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework.Graphics;

namespace MilkShakeFramework.Components.PostProccessing
{
    public class PostProcessingEffect : GameEntity
    {
        public override void FixUp()
        {
            base.FixUp();

            //Scene.Listener.PostSceneRender += new DrawEvent(PostSceneRender);
        }

        public virtual void PostSceneRender()
        {
            // For override
        }

        public RenderTarget2D RenderTarget { get { return Scene.RenderTarget; } } 
    }

}
