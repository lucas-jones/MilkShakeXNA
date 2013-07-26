using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Core.Scenes
{
    public delegate void BasicEvent();
    public delegate void GameEntityEvent();
    public delegate void EntityEvent(Entity node);
    public delegate void DrawEvent();
    public delegate void UpdateEvent(GameTime gameTime);

    public class SceneListener
    {
        public event EntityEvent EntityAdded;
        public event EntityEvent EntityRemoved;

        public DrawEvent[] PreDraw;
        public DrawEvent Draw;
        public DrawEvent[] PostDraw;

        public DrawEvent PreSceneRender;
        public DrawEvent PostSceneRender;

        public BasicEvent LoadContent;


        public event UpdateEvent Update;

        public SceneListener()
        {
            PreDraw = new DrawEvent[5];
            PostDraw = new DrawEvent[5];
        }

        public void OnEntityAdded(Entity entity)
        {
            if (EntityAdded != null) EntityAdded(entity);
        }

        public void OnEntityRemoved(Entity entity)
        {
            if (EntityRemoved != null) EntityRemoved(entity);
        }

        public void OnPreDraw()
        {
            foreach (DrawEvent drawEvent in PreDraw)if (drawEvent != null) drawEvent();            
        }

        public void OnPostDraw()
        {
            foreach (DrawEvent drawEvent in PostDraw) if (drawEvent != null) drawEvent();
        }

        public void OnPreSceneRender()
        {
            if (PreSceneRender != null) PreSceneRender();
        }

        public void OnPostSceneRender()
        {
            if (PostSceneRender != null) PostSceneRender();
        }

        public void OnUpdate(GameTime gameTime)
        {
            if (Update != null) Update(gameTime);
        }

        public void OnLoad()
        {
            if (LoadContent != null) LoadContent();
        }


        internal void onDraw()
        {
            if (Draw != null) Draw();
        }
    }

    public class DrawLayer
    {
        public const int First = 0;
        public const int Second = 1;
        public const int Third = 2;
        public const int Fourth = 3;
        public const int Fifth = 4;
    }

}
