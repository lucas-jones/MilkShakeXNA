using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.Core.Scenes;

namespace MilkShakeFramework.Render
{
    public struct Text
    {
        public Vector2 Position;
        public String Message;
        public Color Color;
        public bool Shadow;
    }


    public class TextRenderer : Renderer
    {
        public static SpriteFont _spriteFont;

        private Queue<Text> _texts;

        public TextRenderer()
        {
            _texts = new Queue<Text>();
        }

        public override void Load(LoadManager content)
        {
            _spriteFont = MilkShake.ConentManager.Load<SpriteFont>("font");

            base.Load(content);
        }

        public override void FixUp()
        {
            base.FixUp();

            Scene.Listener.PostDraw[DrawLayer.Fourth] += new DrawEvent(PostDraw);
        }

        public void DrawText(Vector2 _position, String _text, Color _color, Boolean _shadow = true)
        {
            _texts.Enqueue(new Text() { Position = _position, Message = _text, Color = _color, Shadow = _shadow });
        }

        public void PostDraw()
        {
            Scene.RenderManager.Begin();
            while (_texts.Count > 0) RenderText(_texts.Dequeue());
            Scene.RenderManager.End();
        }

        private void RenderText(Text text)
        {
            text.Message = text.Message ?? "Null";

            if (text.Shadow)
            {
                Scene.RenderManager.SpriteBatch.DrawString(_spriteFont, text.Message, text.Position + new Vector2(1, 0), Color.Black);
                Scene.RenderManager.SpriteBatch.DrawString(_spriteFont, text.Message, text.Position + new Vector2(-1, 0), Color.Black);
                Scene.RenderManager.SpriteBatch.DrawString(_spriteFont, text.Message, text.Position + new Vector2(0, 1), Color.Black);
                Scene.RenderManager.SpriteBatch.DrawString(_spriteFont, text.Message, text.Position + new Vector2(0, -1), Color.Black);

                Scene.RenderManager.SpriteBatch.DrawString(_spriteFont, text.Message, text.Position + new Vector2(1, 1), Color.Black);
                Scene.RenderManager.SpriteBatch.DrawString(_spriteFont, text.Message, text.Position + new Vector2(0, 0), Color.Black);
                Scene.RenderManager.SpriteBatch.DrawString(_spriteFont, text.Message, text.Position + new Vector2(-1, 1), Color.Black);
                Scene.RenderManager.SpriteBatch.DrawString(_spriteFont, text.Message, text.Position + new Vector2(1, -1), Color.Black);

                Scene.RenderManager.SpriteBatch.DrawString(_spriteFont, text.Message, text.Position + new Vector2(2, 1), Color.Black);
                Scene.RenderManager.SpriteBatch.DrawString(_spriteFont, text.Message, text.Position + new Vector2(1, 2), Color.Black);
            }

            Scene.RenderManager.SpriteBatch.DrawString(_spriteFont, text.Message, text.Position, text.Color);
        }
    }
}
