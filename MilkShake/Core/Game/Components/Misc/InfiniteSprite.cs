using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Render;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Core.Game.Components.Misc
{
    public class InfiniteSprite : GameEntity
    {
        private Image _image;

        private string _url;

        private int _xCount;
        private int _yCount;
        private Color _color;

        public InfiniteSprite(string url)
        {
            _url = url;
            _color = Color.White;

            AddNode(_image = new Image(_url));
        }

        public override void Load(Content.LoadManager content)
        {
            base.Load(content);

            _xCount = (Globals.ScreenWidth / _image.Texture.Width) + 1;
            _yCount = (Globals.ScreenHeight / _image.Texture.Height) + 1;

        }

        public override void Draw()
        {

            Vector2 offset = new Vector2((int)(Position.X / _image.Texture.Width), (int)(Position.Y / _image.Texture.Height));

            Console.WriteLine(offset);

            Vector2 pos = WorldPosition - (offset * new Vector2(_image.Texture.Width, _image.Texture.Height));
            
            for (int x = -1; x < _xCount; x++)
            for (int y = -1; y < _yCount; y++)
                _image.Draw(pos + (new Vector2(_image.Texture.Width, _image.Texture.Height) * new Vector2(x, y)), _color);           

            base.Draw();
        }

        public Color Color { get { return _color; } set { _color = value; } }
    }
}
