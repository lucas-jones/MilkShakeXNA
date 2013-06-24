using System;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;

namespace MilkShakeFramework.Core.Filters
{
    public class Filter : Entity
    {
        private String _url;
        private Effect _effect;
        private Boolean _enabled;

        public Filter(String url)
        {
            _url = url;
            _enabled = true;
        }

        public override void Load(LoadManager content)
        {
            _effect = MilkShake.ConentManager.Load<Effect>(_url).Clone();
        }

        public virtual void Begin(BlendState blend)
        {
            if (_enabled)
            {
                Scene.RenderManager.End();
                Scene.RenderManager.Begin(_effect, blend);
            }
        }

        public virtual void Begin()
        {
            Begin(BlendState.AlphaBlend);
        }

        public virtual void End()
        {
            if (_enabled)
            {
                Scene.RenderManager.End();
                Scene.RenderManager.Begin(BlendState.AlphaBlend);
            }
        }

        public Effect Effect { get { return _effect; } }
        public Boolean Enabled { get { return _enabled; } set { _enabled = value; } }
        public EffectParameterCollection Parameters { get { return _effect.Parameters; } }
    }
}
