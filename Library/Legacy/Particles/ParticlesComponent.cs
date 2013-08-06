using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Scenes.Components;
using MilkShakeFramework.Core.Scenes;
using ProjectMercury;
using ProjectMercury.Renderers;
using ProjectMercury.Emitters;
using ProjectMercury.Modifiers;
using Microsoft.Xna.Framework;
using MilkShakeFramework.IO.Input.Devices;
using MilkShakeFramework.Core.Game;

namespace MilkShakeFramework.Components.Particles
{
    public class ParticlesComponent : SceneComponent
    {
        private SpriteBatchRenderer mSpriteBatchRenderer;

        public ParticlesComponent()
        {
            Scene.Listener.EntityAdded += new EntityEvent(Listener_EntityAdded);
            Scene.Listener.PostDraw[DrawLayer.First] += new DrawEvent(Draw);
            Scene.Listener.Update += new UpdateEvent(Listener_Update);
            Scene.Listener.LoadContent += new BasicEvent(LoadConent);

            mSpriteBatchRenderer = new SpriteBatchRenderer();

            mSpriteBatchRenderer.GraphicsDeviceService = MilkShake.GraphicsManager;
        }

        public void LoadConent()
        {
            mSpriteBatchRenderer.LoadContent(MilkShake.ConentManager);
        }

        private void Listener_EntityAdded(Entity node)
        {
        }

        void Listener_Update(GameTime gameTime)
        {
            
        }

        private void Draw()
        {
        }

        public SpriteBatchRenderer ParticleRenderer { get { return mSpriteBatchRenderer; } set { mSpriteBatchRenderer = value; } }      
    }
}
