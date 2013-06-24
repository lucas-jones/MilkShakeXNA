using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;
using Microsoft.Xna.Framework;
using MilkShakeFramework.IO.Input.Devices;

namespace MilkShakeFramework.Core.Game
{
    public class Sprite3D : GameEntity
    {
        private String _url;
        private Model _model;
        private Texture2D _colour;

        public Sprite3D(String url)
        {
            _url = url;
        }

        public override void Load(LoadManager content)
        {
            base.Load(content);

            _model = MilkShake.ConentManager.Load<Model>(_url);
            _colour = MilkShake.ConentManager.Load<Texture2D>("Scene//Map//Color Map");
        }

        public override void Draw()
        {
            DrawModel(_model);
            base.Draw();
        }

        float timePassed = 0;

        private void DrawModel(Model m)
        {
            timePassed += 0.1f;

            Matrix[] transforms = new Matrix[m.Bones.Count];
            float aspectRatio = MilkShake.Graphics.Viewport.AspectRatio;
            m.CopyAbsoluteBoneTransformsTo(transforms);
            Matrix projection = Matrix.CreateOrthographicOffCenter(-MilkShake.Graphics.Viewport.Width / 2f, MilkShake.Graphics.Viewport.Width / 2f, -MilkShake.Graphics.Viewport.Height / 2f, MilkShake.Graphics.Viewport.Height / 2f, 1, 10000);
                //Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                //aspectRatio, 0.2f, 500.0f);
            Matrix view = Matrix.CreateLookAt(new Vector3(0.0f, 50.0f, 1),
                Vector3.Zero, Vector3.Up);



            MilkShake.Graphics.RasterizerState = new RasterizerState() { CullMode = CullMode.CullClockwiseFace /*, FillMode = FillMode.WireFrame */ };

            Scene.RenderManager.End();
            Scene.RenderManager.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            
            foreach (ModelMesh mesh in m.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    
                    //effect.LightingEnabled = true;
                    effect.Alpha = 1f;
                    effect.SpecularPower = 0.5f;
                    effect.AmbientLightColor = new Vector3(.3f, .3f, .3f);
                    effect.DiffuseColor = new Vector3(1, 1, 1);
                    effect.SpecularColor = new Vector3(0, 1f, 1f);
                    effect.DirectionalLight0.Enabled = true;
                    effect.DirectionalLight0.Direction = Vector3.Normalize(new Vector3(4, -1, -1));
                    effect.DirectionalLight0.DiffuseColor = Color.White.ToVector3();
                    effect.DirectionalLight0.SpecularColor = Color.White.ToVector3();
                     
                    //effect.TextureEnabled = true;
                    //effect.Texture = _colour;
                    
                    effect.View = view;
                    effect.Projection = projection;
                    effect.World = Matrix.CreateScale(Scale) * Matrix.CreateRotationX(MathHelper.ToRadians(-180)) *
        Matrix.CreateRotationZ(MathHelper.ToRadians(timePassed * 10)) * transforms[mesh.ParentBone.Index] *
                                    Matrix.CreateTranslation(new Vector3(WorldPosition.X - Scene.Camera.X, 0, WorldPosition.Y - Scene.Camera.Y));
                }
                mesh.Draw();
            }

            Scene.RenderManager.SpriteBatch.End();
            Scene.RenderManager.Begin();
        }

        public float Scale = 2.5f;
    }
}
