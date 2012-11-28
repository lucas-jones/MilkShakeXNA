using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MilkShakeFramework.Core.Game.Components.Misc;
using MilkShakeFramework.IO.Input.Devices;
using MilkShakeFramework.Core.Game.Components.Water.Render;
using MilkShakeFramework.Tools.Physics;
using MilkShakeFramework.Core.Scenes.Components;
using MilkShakeFramework.Core.Game.Components.Water.Modify;

namespace MilkShakeFramework.Core.Game.Components.Water
{
    public class Water : GameEntity
    {
        private int _width;
        private int _height;

        private float _tension = 0.025f;
        private float _dampening = 0.025f;
        private float _spread = 0.25f;

        private WaterColumn[] _columns;
        private WaterRenderer _renderer;

        public Water(int width, int height, int columns, WaterRenderer renderer = null, float tension = 0.025f, float dampening =  0.025f, float spread = 0.025f)
        {
            _width = width;
            _height = height;

            _tension = tension;
            _dampening = dampening;
            _spread = spread;

            _columns = new WaterColumn[columns];

            for (int i = 0; i < _columns.Length; i++)
            {
                _columns[i] = new WaterColumn()
                {
                    Height = height,
                    TargetHeight = height,
                    Speed = 0
                };
            }

            if (renderer == null) renderer = new BasicWaterRenderer();
            AddNode(renderer);

            AddNode(new BasicWaterRenderer());
            AddNode(new PhysicsWaterModifier());
            AddNode(new SplashWaterModifier());
        }

        public void Splash(float xPosition, float speed)
        {
            int index = (int)MathHelper.Clamp(xPosition / (Width / WaterColumns.Length), 0, _columns.Length - 1);
            for (int i = Math.Max(0, index - 0); i < Math.Min(_columns.Length - 1, index + 1); i++)
                _columns[index].Speed += speed;
        }

        public float GetHeight(float x)
        {
            if (x < 0 || x > 800) return 240;

            return _columns[(int)(x / 1)].Height;
        }

        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < _columns.Length; i++) _columns[i].Update(_dampening, _tension);

            float[] lDeltas = new float[_columns.Length];
            float[] rDeltas = new float[_columns.Length];

            // Do some passes where _columns pull on their neighbours
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < _columns.Length; i++)
                {
                    if (i > 0)
                    {
                        lDeltas[i] = _spread * (_columns[i].Height - _columns[i - 1].Height);
                        _columns[i - 1].Speed += lDeltas[i];
                    }
                    if (i < _columns.Length - 1)
                    {
                        rDeltas[i] = _spread * (_columns[i].Height - _columns[i + 1].Height);
                        _columns[i + 1].Speed += rDeltas[i];
                    }
                }

                for (int i = 0; i < _columns.Length; i++)
                {
                    if (i > 0)
                        _columns[i - 1].Height += lDeltas[i];
                    if (i < _columns.Length - 1)
                        _columns[i + 1].Height += rDeltas[i];
                }
            }

            base.Update(gameTime);
        }

        public WaterColumn[] WaterColumns { get { return _columns; } }
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
    }

}
