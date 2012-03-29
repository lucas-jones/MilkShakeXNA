using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MilkShakeFramework.Render
{
    public class RenderManager
    {
        private SpriteBatch mSpriteBatch;

        public RenderManager()
        {
            mSpriteBatch = new SpriteBatch(MilkShake.Graphics);
        }

        
    }
}
