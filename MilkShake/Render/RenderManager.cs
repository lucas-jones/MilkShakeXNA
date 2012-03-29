using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Scenes;

namespace MilkShakeFramework.Render
{
    public class RenderManager : SceneComponent
    {
        private SpriteBatch mSpriteBatch;

        public RenderManager(Scene scene) : base(scene)
        {
            mSpriteBatch = new SpriteBatch(MilkShake.Graphics);
        }
        
    }
}
