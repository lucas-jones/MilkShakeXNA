using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;
using Microsoft.Xna.Framework.Graphics;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.IO.Input.Devices;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Core.Filters
{
    public class Filter : Entity
    {
        private String _url;
        private Effect _effect;
        private Boolean _enabled;

        public Filter(String url)
        {
            _url = url;
            _enabled = true;
        }

        public override void Load(LoadManager content)
        {
            _effect = MilkShake.ConentManager.Load<Effect>(_url).Clone();
        }

        public void Begin()
        {
            if (_enabled) 
            {
                Scene.RenderManager.End();
                Scene.RenderManager.Begin(_effect);
            }
        }

        public void End()
        {
            if (_enabled)
            {
                Scene.RenderManager.End();
                Scene.RenderManager.Begin();
            }
        }

        public Boolean Enabled { get { return _enabled; } set { _enabled = value; } }
        public EffectParameterCollection Parameters { get { return _effect.Parameters; } }
    }

    public class GrayscaleFilter : Filter
    {
        public GrayscaleFilter() : base("Effects//GrayScale")
        {
            Name = "GrayScale";
        }
    }

    public class InvertFilter : Filter
    {
        public InvertFilter() : base("Effects//Invert")
        {
            Name = "Invert";
        }
    }


    public class WaveFilter : Filter
    {
        public float WaveLength = 200;

        public WaveFilter(int waveLength) : base("Effects//Wave")
        {
            WaveLength = waveLength;
        }

        public override void FixUp()
        {
            base.FixUp();

            Scene.Listener.Update += new Scenes.UpdateEvent(Update);
        }

        private void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            Parameters["wavelength"].SetValue(MouseInput.X);
        }
    }

    public class GaussianBlurFilter : Filter
    {
        private float _amount;
        private float _radx;
        private float _rady;
        private Vector2 _scale;

        public GaussianBlurFilter(float amount, float radx, float rady, Vector2 scale) : base("Effects//GaussianBlur")
        {
            _amount = amount;
            _radx = radx;
            _rady = rady;
            _scale = scale;

            
        }

        public override void FixUp()
        {
            base.FixUp();

            Scene.Listener.Update += new Scenes.UpdateEvent(Listener_Update);
        }

        void Listener_Update(GameTime gameTime)
        {
            float gaussianBound = 8;

            if (_radx >= gaussianBound)
            {
                _radx = gaussianBound - 0.000001F;
            }
            if (_rady >= gaussianBound)
            {
                _rady = gaussianBound - 0.000001F;
            }
            //If blur is too great, image becomes transparent,
            //so cap how much blur can be used.
            //Reduces quality of very small images.

            Vector2[] offsetsHoriz, offsetsVert;
            float[] kernelx = new float[(int)(_radx * 2 + 1)];
            float sigmax = _radx / _amount;
            float[] kernely = new float[(int)(_rady * 2 + 1)];
            float sigmay = _rady / _amount;
            //Initialise kernels and sigmas (separately to allow for different scale factors in x and y)

            float twoSigmaSquarex = 2.0f * sigmax * sigmax;
            float sigmaRootx = (float)Math.Sqrt(twoSigmaSquarex * Math.PI);
            float twoSigmaSquarey = 2.0f * sigmay * sigmay;
            float sigmaRooty = (float)Math.Sqrt(twoSigmaSquarey * Math.PI);
            float totalx = 0.0f;
            float totaly = 0.0f;
            float distance = 0.0f;
            int index = 0;
            //Initialise gaussian constants, as well as totals for normalisation.

            offsetsHoriz = new Vector2[kernelx.Length];
            offsetsVert = new Vector2[kernely.Length];

            float xOffset = 1.0f / _scale.X;
            float yOffset = 1.0f / _scale.Y;
            //Set offsets for use in the HLSL shader.

            for (int i = -(int)_radx; i <= _radx; ++i)
            {
                distance = i * i;
                index = i + (int)_radx;
                kernelx[index] = (float)Math.Exp(-distance / twoSigmaSquarex) / sigmaRootx;
                //Set x kernel values with gaussian function.
                totalx += kernelx[index];
                offsetsHoriz[index] = new Vector2(i * xOffset, 0.0f);
                //Set x offsets.
            }

            for (int i = -(int)_rady; i <= _rady; ++i)
            {
                distance = i * i;
                index = i + (int)_rady;
                kernely[index] = (float)Math.Exp(-distance / twoSigmaSquarey) / sigmaRooty;
                //Set y kernel values with gaussian function.
                totaly += kernely[index];
                offsetsVert[index] = new Vector2(0.0f, i * yOffset);
                //Set y offsets.
            }

            for (int i = 0; i < kernelx.Length; ++i)
                kernelx[i] /= totalx;

            for (int i = 0; i < kernely.Length; ++i)
                kernely[i] /= totaly;

            //Normalise kernel values.

            Parameters["weightX"].SetValue(kernelx);
            Parameters["weightY"].SetValue(kernely);
            Parameters["offsetH"].SetValue(offsetsHoriz);
            Parameters["offsetV"].SetValue(offsetsVert);
            //Set HLSL values.
        }

        
    }

    public class BlurFilter : Filter
    {
        public bool hor = false;
        public float Alpha = 1;

        public BlurFilter(bool a) : base("Effects//Blur")
        {
            hor = a;
        }

        public override void FixUp()
        {
            base.FixUp();

            Update();
        }

        public void Update()
        {
            if (hor)
            {
                SetBlurEffectParameters(1.0f / (Parent as Sprite).Width, 0);

            }
            else
            {
                SetBlurEffectParameters(0, 1.0f / (Parent as Sprite).Height);
            }

            Parameters["Alpha"].SetValue(Alpha);
        }

        void SetBlurEffectParameters(float dx, float dy)
        {
            // Look up the sample weight and offset effect parameters.
            EffectParameter weightsParameter, offsetsParameter;

            weightsParameter = Parameters["SampleWeights"];
            offsetsParameter = Parameters["SampleOffsets"];


            // Look up how many samples our gaussian blur effect supports.
            int sampleCount = weightsParameter.Elements.Count;

            // Create temporary arrays for computing our filter settings.
            float[] sampleWeights = new float[sampleCount];
            Vector2[] sampleOffsets = new Vector2[sampleCount];

            // The first sample always has a zero offset.
            sampleWeights[0] = ComputeGaussian(0);
            sampleOffsets[0] = new Vector2(0);

            // Maintain a sum of all the weighting values.
            float totalWeights = sampleWeights[0];

            // Add pairs of additional sample taps, positioned
            // along a line in both directions from the center.
            for (int i = 0; i < sampleCount / 2; i++)
            {
                // Store weights for the positive and negative taps.
                float weight = ComputeGaussian(i + 1);

                sampleWeights[i * 2 + 1] = weight;
                sampleWeights[i * 2 + 2] = weight;

                totalWeights += weight * 2;

                // To get the maximum amount of blurring from a limited number of
                // pixel shader samples, we take advantage of the bilinear filtering
                // hardware inside the texture fetch unit. If we position our texture
                // coordinates exactly halfway between two texels, the filtering unit
                // will average them for us, giving two samples for the price of one.
                // This allows us to step in units of two texels per sample, rather
                // than just one at a time. The 1.5 offset kicks things off by
                // positioning us nicely in between two texels.
                float sampleOffset = i * 2 + 1.5f;

                Vector2 delta = new Vector2(dx, dy) * sampleOffset;

                // Store texture coordinate offsets for the positive and negative taps.
                sampleOffsets[i * 2 + 1] = delta;
                sampleOffsets[i * 2 + 2] = -delta;
            }

            // Normalize the list of sample weightings, so they will always sum to one.
            for (int i = 0; i < sampleWeights.Length; i++)
            {
                sampleWeights[i] /= totalWeights;
            }

            // Tell the effect about our new filter settings.
            weightsParameter.SetValue(sampleWeights);
            offsetsParameter.SetValue(sampleOffsets);
        }


        /// <summary>
        /// Evaluates a single point on the gaussian falloff curve.
        /// Used for setting up the blur filter weightings.
        /// </summary>
        float ComputeGaussian(float n)
        {
            float theta = 5;

            return (float)((1.0 / Math.Sqrt(2 * Math.PI * theta)) *
                           Math.Exp(-(n * n) / (2 * theta * theta)));
        }
    }

 

}
