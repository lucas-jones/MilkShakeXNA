using System;
using MilkShakeFramework.Core.Game;
using ProjectMercury;
using MilkShakeFramework.Core.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;

namespace MilkShakeFramework.Components.Particles
{
    public class ParticleEmitter : GameEntity
    {
        private String mAssetURL;
        private ParticleEffect mParticleEffect;

        public ParticleEmitter(string _AssetURL)
        {
            mAssetURL = _AssetURL;            
        }

        public override void Load(LoadManager content)
        {
            base.Load(content);

            mParticleEffect = MilkShake.ConentManager.Load<ParticleEffect>(mAssetURL).DeepCopy(); // Deep Copy to stop referances

            foreach (var emitter in mParticleEffect.Emitters)
            {
                // Fix
                emitter.ParticleTexture = MilkShake.ConentManager.Load<Texture2D>("LensFlare");
                if(!emitter.Initialised) emitter.Initialise();
            }

            Scene.Listener.PostDraw[DrawLayer.First] += new DrawEvent(DrawParticle);
        }
   
        public void DrawParticle()
        {
            Matrix blankMatrix = Matrix.Identity;            
            Vector3 cameraPosition = new Vector3(0, 0, 0);

            Matrix view = Scene.Camera.Matrix;

            Matrix mMatrix = Matrix.Identity;
            mMatrix.Translation = new Vector3(-Scene.Camera.Position, 0);

            ParticleComponent.ParticleRenderer.Transformation = mMatrix * view;


            ParticleComponent.ParticleRenderer.RenderEffect(mParticleEffect, ref view, ref blankMatrix, ref blankMatrix, ref cameraPosition);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            mParticleEffect.Update(deltaSeconds);
        }

        public void Trigger()
        {
            var position = new Vector3(WorldPosition, 0);
            mParticleEffect.Trigger(ref position);   
        }        

        public ParticleEffect ParticleEffect { get { return mParticleEffect; } }
        public bool HasParticle { get { return Scene.ComponentManager.HasComponent<ParticlesComponent>(); } }
        public ParticlesComponent ParticleComponent { get { return Scene.ComponentManager.GetComponent<ParticlesComponent>(); } }
    }
}
