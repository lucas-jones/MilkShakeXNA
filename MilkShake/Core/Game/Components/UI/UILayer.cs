﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Scenes;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Core.Game.Components.UI
{
    public enum DrawMode
    {
        Pre,
        Post
    }

    public class UILayer : GameEntity
    {
        private DrawMode mDrawMode = DrawMode.Post;
        private int mDrawLayer = 4;

        public UILayer(DrawMode _drawMode = DrawMode.Post, int _drawLayer = 4)
            : base()
        {
            mDrawMode = _drawMode;
            mDrawLayer = _drawLayer;
        }

        public override void FixUp()
        {
            base.FixUp();

            if (mDrawMode == DrawMode.Post) Scene.Listener.PostDraw[mDrawLayer] += new DrawEvent(NewDraw);
            if (mDrawMode == DrawMode.Pre) Scene.Listener.PreDraw[mDrawLayer] += new DrawEvent(NewDraw);

        }

        public override void Draw()
        {
            // Disable normal draw call
        }

        public void NewDraw()
        {
            Scene.RenderManager.RawBegin();
            Scene.RenderManager.IsRawDraw = true;
            base.Draw();
            Scene.RenderManager.IsRawDraw = false;
            Scene.RenderManager.End();
        }

    }
}
