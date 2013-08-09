using System;
using System.Collections.Generic;
using MilkShakeFramework.Core.Scenes;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework.Core.Scenes.Components;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Components.PostProccessing
{
    public class EffectsComponent : SceneComponent
    {
        private List<PostProcessingEffect> mEffects;

        public EffectsComponent()
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

    // [Todo] Remove this...
    public class ShockWave : Sprite
    {
        private Vector2 _startingPosition;

        public ShockWave(Vector2 staringPosition, float alpha = 1) : base("Scene//Levels//Images//shockwave3")
        {
            _startingPosition = staringPosition;
        }

        public override void FixUp()
        {
            base.FixUp();

            Width = 0;
            Height = 0;
        }

        public override void Update(GameTime gameTime)
        {
            Width += 40;
            Height += 40;

            Position = _startingPosition - new Vector2(Width / 2, Height / 2);


            base.Update(gameTime);
        }
    }
}
