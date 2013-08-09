using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;
using XNATweener;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Scenes;

namespace MilkShakeFramework.Tools.Tween
{
    public delegate void TweenPositionUpdate<T>(T value);
    public delegate void ColorTweenPositionUpdate(Color value);

    // Todo: Clean up & Convert to GameEntity / SceneComponent
    public class TweenerManager
    {
        private static List<ITweener> _tweens;
        private static List<ColorTweener> _colorTweens;

        public static void Boot()
        {
            _tweens = new List<ITweener>();
            _colorTweens = new List<ColorTweener>();
        }

        public static void AddTween<T>(BaseTweener<T> tweener, TweenPositionUpdate<T> updateCallback, BasicEvent endCallback = null)
        {
            _tweens.Add(tweener);

            tweener.PositionChanged += new PositionChangedHandler<T>((value) => updateCallback(value));
            tweener.Ended += () =>
            {
                if (endCallback != null) endCallback();

                _tweens.Remove(tweener);
            };
        }

        public static void AddTween(ColorTweener tweener, ColorTweenPositionUpdate updateCallback, BasicEvent endCallback = null)
        {
            _colorTweens.Add(tweener);

            tweener.PositionChanged += new PositionChangedHandler<Color>((value) => updateCallback(value));
            tweener.Ended += () =>
            {
                if (endCallback != null) endCallback();

                _colorTweens.Remove(tweener);
            };
        }

        public static void Update(GameTime gameTime)
        {
            _tweens.ForEach(tween => tween.Update(gameTime));
            _colorTweens.ForEach(tween => tween.Update(gameTime));
        }
    }
}
