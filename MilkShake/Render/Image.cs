using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Scenes;

namespace MilkShakeFramework.Render
{
    public class Image : Renderer
    {
        private Texture2D mTexture2D;
        private string mURL;

        public Image(string url)
        {
            mURL = url;
        }

        public override void Load(LoadManager content)
        {
            Console.WriteLine("Load Called");
        }

        public void Draw(Vector2 position)
        {
        }

        public Texture2D Texture { get { return mTexture2D; } }
        public string URL { get { return mURL; } }
    }
}
