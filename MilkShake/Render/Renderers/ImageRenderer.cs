using System;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;
using Microsoft.Xna.Framework;
using System.IO;

namespace MilkShakeFramework.Render
{
    public class ImageRenderer : Renderer
    {
        public const string DEFAULT_MISSING_TEXTURE = "MilkShake//MISSING_IMAGE";

        public Texture2D Texture { get; private set; }
        public string URL { get; private set; }
        public bool FromStream { get; set; }

        public ImageRenderer(string url)
        {
            URL = url;
        }

        public override void Load(LoadManager content)
        {
            try
            {
                if (FromStream)
                {
                    Texture = Texture2D.FromStream(MilkShake.Graphics, File.OpenRead(URL));
                }
                else
                {
                    Texture = MilkShake.ConentManager.Load<Texture2D>(URL);
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("Missing Texture " + URL);

                Texture = MilkShake.ConentManager.Load<Texture2D>(DEFAULT_MISSING_TEXTURE);
            }

            base.Load(content);
        }

        public void Draw(Vector2 position)
        {
            Draw(position, Texture.Width, Texture.Height, 0, Vector2.Zero, Color.White);
        }

        public void Draw(Vector2 position, Color color)
        {
            Draw(position, Texture.Width, Texture.Height, 0, Vector2.Zero, color);
        }

        public void Draw(Vector2 position, int width, int height, float rotation, Vector2 origin, Color color, float scaleX = 1, float scaleY = 1)
        {
            if (RenderManager.IsRawDraw)
            {
                RenderManager.RawDraw(position, Texture, width, height, rotation, origin, color, scaleX, scaleY);
            }
            else
            {
                RenderManager.Draw(position, Texture, width, height, rotation, origin, color, scaleX, scaleY);
            }
        }

        public Vector2 ImageCenter
        {
            get { return new Vector2(Texture.Width / 2, Texture.Height / 2);}
        }
    }
}
