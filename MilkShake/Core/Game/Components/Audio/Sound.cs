using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.IO.Input.Devices;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Game.Components.Misc;
using MilkShakeFramework.Render;

namespace MilkShakeFramework.Core.Game.Components.Audio
{
    public class Sound : GameEntity
    {
        private String _url;
        private SoundEffect _soundEffect;
        private SoundEffectInstance _soundEffectInstance;

        private float _volume = 1;

        private bool _autoPlay;
        private bool _isLooped;
        private bool _is3D;

        public string URL
        {
            get { return _url; }
        }

        public bool AutoPlay
        {
            get { return _autoPlay; }
            set 
            {
                if (value == true) Play();
                else Stop();

                _autoPlay = value;
            }
        }

        public bool Is3D
        {
            get { return _is3D; }
            set { _is3D = value; }
        }

        public float Distance
        {
            get;
            set;
        }

        private PrimitiveRenderer _lineDraw;

        public Sound(String url, bool autoPlay = true, bool isLooped = true, bool is3D = false)
            : base()
        {
            _url = url;
            _autoPlay = autoPlay;
            _isLooped = isLooped;
            _is3D = is3D;

            if (Globals.EditorMode) AddNode(_lineDraw = new PrimitiveRenderer());
        }

        public float Volume
        {
            get { return _volume; }
            set
            {
                _volume = value;
            }
        }

        public override void Load(LoadManager content)
        {
            base.Load(content);

            _soundEffect = MilkShake.ConentManager.Load<SoundEffect>(_url);
            _soundEffectInstance = _soundEffect.CreateInstance();

            _soundEffectInstance.IsLooped = _isLooped;
            _soundEffectInstance.Volume = _volume;
        }

        public override void FixUp()
        {
            base.FixUp();

            if(_autoPlay) _soundEffectInstance.Play();
        }

        public void Play()
        {
            _soundEffectInstance.Play();
        }

        public void Stop()
        {
            if(_soundEffectInstance != null) _soundEffectInstance.Stop();
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            _soundEffectInstance.Volume = Volume;

            if(_is3D) SetSoundForCamera(_soundEffectInstance, Position, Volume);
        }

        public bool SetSoundForCamera(SoundEffectInstance sound, Vector2 position, float baseVolume)
        {
            float distance = (Position - Scene.Camera.Position - Globals.ScreenCenter).Length();
            float fade = 1 - MathHelper.Clamp(distance / Distance, 0, 1);
            sound.Volume = MathHelper.Clamp(fade * fade * baseVolume, 0, 1);

            float distanceTwo = ((Position - (Scene.Camera.Position)).X - Globals.ScreenCenter.X) / Distance;

            sound.Pan = MathHelper.Clamp(distanceTwo, -1, 1);

            return fade > 0;
        }

        public override Tools.Maths.RotatedRectangle BoundingBox
        {
            get
            {
                return new Tools.Maths.RotatedRectangle((int)WorldPosition.X, (int)WorldPosition.Y, 50, 50);
            }
        }

        public override void Draw()
        {
            base.Draw();

            if (Globals.EditorMode)
            {
                _lineDraw.DrawRectangle(new Rectangle((int)Position.X - (int)(Distance / 2), (int)Position.Y - (int)(Distance / 2), (int)Distance, (int)Distance), Color.Blue);
            }
        }

        public override void Destroy()
        {
            Stop();

            base.Destroy();
        }

        public override void TearDown()
        {
            Stop();

            base.TearDown();
        }
    }
}
