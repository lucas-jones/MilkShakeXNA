using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using XNATweener;
using MilkShakeFramework.Tools.Tween;

namespace MilkShakeFramework.Core.Game.Components.Audio
{
    public class Music : GameEntity
    {
        public string URL { get; set; }        
        public bool AutoPlay { get; set; }
        private Song Song;

        public bool Loop { get; set; }

        public Music(string url) : base()
        {
            URL = url;
        }

        public Music() : base() { }

        public override void Load(Content.LoadManager content)
        {
            Song = MilkShake.ConentManager.Load<Song>(URL);
            base.Load(content);
        }

        public override void FixUp()
        {
            base.FixUp();

            if (AutoDraw) Play();
        }

        public void Play()
        {
            MediaPlayer.Play(Song);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;
        }

        public void Stop()
        {
            TweenerManager.AddTween(new Tweener(0.5f, 0, 2f, Linear.EaseNone), (value) => MediaPlayer.Volume = value);
            //MediaPlayer.Stop();
        }
    }
}
