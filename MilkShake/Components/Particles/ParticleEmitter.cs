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

        public ParticleEmitter(string _AssetURL)
        {
            mAssetURL = _AssetURL;            
        }

        public override void Load(LoadManager content)
        {
            base.Load(content);

            mParticleEffect = MilkShake.ConentManager.Load<ParticleEffect>(mAssetURL).DeepCopy(); // Deep Copy to stop referances
            mParticleEffect.Initialise();

            foreach (Emitter emitter in mParticleEffect)
            {
                // Fix
                emitter.ParticleTexture = MilkShake.ConentManager.Load<Texture2D>("distort_part");
                if (!emitter.Initialised) emitter.Initialise();
                //EmitterBlendMode.
            }
        }


        public override void Draw()
        {
            base.Draw();
            Scene.RenderManager.End();

            Matrix blankMatrix = Matrix.Identity;            
            Vector3 cameraPosition = new Vector3(0, 0, 0);

            Matrix view = Scene.Camera.Matrix;

            Matrix mMatrix = GetViewMatrix() * GetProjectionMatrix();
            //Matrix mMatrix = Matrix.Identity;
            mMatrix.Translation = new Vector3(Scene.Camera.Position, 0);


            ParticleComponent.ParticleRenderer.RenderEffect(mParticleEffect, ref mMatrix);
            

            //ParticleComponent.ParticleRenderer.RenderEffect(mParticleEffect, ref view, ref blankMatrix, ref blankMatrix, ref cameraPosition);

            Scene.RenderManager.Begin();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            mParticleEffect.Update(deltaSeconds);

            if (MouseInput.isLeftClicked()) Trigger();            
        }

        public void Trigger()
        {
            mParticleEffect.Trigger(Vector2.Zero);
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
    }
}
