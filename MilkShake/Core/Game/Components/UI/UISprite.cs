using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.IO.Input.Devices;

namespace MilkShakeFramework.Core.Game.Components.UI
{
    public class UISprite : Sprite
    {
        public bool isMouseOver;
        public BasicEvent onMouseClick;

        public BasicEvent onMouseEnter;
        public BasicEvent onMouseExit;

        public UISprite(string url) : base(url)
        {

        }

        public override void Update(GameTime gameTime)
        {
            Rectangle rect = new Rectangle((int)WorldPosition.X, (int)WorldPosition.Y, Width, Height);

            bool curMouseOver = rect.Intersects(new Rectangle(MouseInput.X, MouseInput.Y, 1, 1));

            if (curMouseOver != isMouseOver)
            {
                if (curMouseOver == true && onMouseEnter != null) onMouseEnter();
                if (curMouseOver == false && onMouseExit != null) onMouseExit();
            }

            isMouseOver = curMouseOver;

            if (isMouseOver)
            {
                if (MouseInput.isLeftClicked())
                {
                    OnClick();

                    if (onMouseClick != null) onMouseClick();
                }
            }

            base.Update(gameTime);
        }


        public virtual void OnClick()
        {

        }
    }
}
