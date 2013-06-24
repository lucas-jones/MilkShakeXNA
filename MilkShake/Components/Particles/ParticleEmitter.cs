using System;
using MilkShakeFramework.Core.Game;
using ProjectMercury;
using MilkShakeFramework.Core.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;
using ProjectMercury.Emitters;
using MilkShakeFramework.IO.Input.Devices;

namespace MilkShakeFramework.Components.Particles
{
    public class ParticleEmitter : GameEntity
    {
        private String mAssetURL;
        private ParticleEffect mParticleEffect;

        public bool AutoTrigger { get; set; }

        public ParticleEmitter(string _AssetURL)
        {
            mAssetURL = _AssetURL;
            AutoTrigger = false;
            Visible = true;
        }

        public override void Load(LoadManager content)
        {
            base.Load(content);

            mParticleEffect = MilkShake.ConentManager.Load<ParticleEffect>(mAssetURL).DeepCopy(); // Deep Copy to stop referances
            mParticleEffect.Initialise();

            foreach (Emitter emitter in mParticleEffect)
            {
                // Fix

                String fileName = mAssetURL.Split('/')[mAssetURL.Split('/').Length - 1];
                String folderURL = mAssetURL.Replace(fileName, "");

                emitter.ParticleTexture = MilkShake.ConentManager.Load<Texture2D>(folderURL + emitter.ParticleTextureAssetName);
                if (!emitter.Initialised) emitter.Initialise();
                //EmitterBlendMode.
            }
        }


        public override void Draw()
        {
            if (Visible)
            {
                base.Draw();
                Scene.RenderManager.End();

                Matrix mMatrix = Matrix.CreateTranslation(new Vector3(-(Scene.Camera.Position + Globals.ScreenCenter), 0)) * Matrix.CreateScale(Scene.Camera.Zoom) * Matrix.CreateTranslation(new Vector3(Globals.ScreenCenter, 0));


                Scene.RenderManager.Begin(mMatrix);




                //ParticleComponent.ParticleRenderer.RenderEmitter(mParticleEffect[0], ref mMatrix);


                ParticleComponent.ParticleRenderer.RenderEffect(mParticleEffect, Scene.RenderManager.SpriteBatch);

                Scene.RenderManager.End();
                Scene.RenderManager.Begin();

            }
            //Scene.RenderManager.Begin();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            mParticleEffect.Update(deltaSeconds);

            if(AutoTrigger) Trigger();            
        }

        public void Trigger()
        {
            mParticleEffect.Trigger(WorldPosition);
        }


        // [Sort out..]
        internal Matrix GetViewMatrix()
        {
            Matrix view = Matrix.Identity;

            float xTranslation = -1 * Scene.Camera.Position.X - Globals.ScreenWidthCenter;
            float yTranslation = -1 * Scene.Camera.Position.Y - Globals.ScreenHeightCenter;
            Vector3 translationVector = new Vector3(xTranslation, yTranslation, 0f);
            view = Matrix.Identity;
            view.Translation = translationVector;
            view *= Matrix.CreateRotationZ(MathHelper.ToRadians(Scene.Camera.Rotation));
            return view;
        }

        // [Sort out..] Move to camera?
        internal Matrix GetProjectionMatrix()
        {
            Matrix projection = Matrix.Identity;

            float zoom = Scene.Camera.Zoom;
            float width = (1f / zoom) * Globals.ScreenWidth;
            float height = (-1f / zoom) * Globals.ScreenHeight;
            float zNearPlane = 0f;
            float zFarPlane = 1000000f;

            projection = Matrix.CreateOrthographic(width, height, zNearPlane, zFarPlane);

            return projection;
        }

        public ParticleEffect ParticleEffect { get { return mParticleEffect; } }
        public bool HasParticle { get { return Scene.ComponentManager.HasComponent<ParticlesComponent>(); } }
        public ParticlesComponent ParticleComponent { get { return Scene.ComponentManager.GetComponent<ParticlesComponent>(); } }

        public bool Visible { get; set; }
    }
}
