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
    public delegate void TweenPositionUpdate(float value);
    public delegate void ColorTweenPositionUpdate(Color value);

    public class TweenerManager
    {
        private static List<Tweener> _tweens;
        private static List<ColorTweener> _colorTweens;

        public static void Boot()
        {
            _tweens = new List<Tweener>();
            _colorTweens = new List<ColorTweener>();
        }

        public static void AddTween(Tweener tweener, TweenPositionUpdate updateCallback, BasicEvent endCallback = null)
        {
            _tweens.Add(tweener);

            tweener.PositionChanged += new PositionChangedHandler<float>((value) => updateCallback(value));
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
