using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Scenes.Components;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Game.Components.Distortion;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework.IO.Input.Devices;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Components.Particles;

namespace MilkShakeFramework.Components.Effects
{

    public class ShockWave : Sprite
    {
        private Vector2 _startingPosition;

        public ShockWave(Vector2 staringPosition)
            : base("shockwave3")
        {
            _startingPosition = staringPosition;
            
            Alpha = 1;
        }

        public override void Update(GameTime gameTime)
        {
            if (KeyboardInput.isKeyDown(Keys.Y))
            {
                Width += 40;
                Height += 40;
            }
            Position = _startingPosition - new Vector2(Width / 2, Height / 2);

            if (Height > 6000)
            {
                Height = 0;
                Width = 0;
            }
            base.Update(gameTime);
        }
    }

    public class EffectsComponent : SceneComponent
    {
        private List<PostProcessingEffect> mEffects;
        public DistortionEffect effecta;
        public EffectsComponent(Scene scene) : base(scene)
        {
            mEffects = new List<PostProcessingEffect>();

           

            

           

          
        }

        public void AddEffect(PostProcessingEffect effect)
        {
            // Add it to scene for Load/Fixup/Update etc..
            Scene.AddNode(effect);

            mEffects.Add(effect);
        }



        public T GetEffect<T>()
        {
            foreach (PostProcessingEffect sceneComponent in mEffects)
            {
                if (sceneComponent.GetType() == typeof(T)) return (T)(object)sceneComponent;
            }

            throw new Exception("Effect " + typeof(T).Name + " dosn't exists");
        }
    }
}
