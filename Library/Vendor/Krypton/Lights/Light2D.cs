using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Microsoft.Xna.Framework.Input;

using Krypton.Common;

namespace Krypton.Lights
{
    public enum ShadowType
    {
        Solid = 1,
        Illuminated = 2,
        Occluded = 3
    };

    public class Light2D : ILight2D
    {
        private bool mIsOn = true;
        private Vector2 mPosition = Vector2.Zero;
        private float mAngle = 0;
        private Texture2D mTexture = null;
        private Color mColor = Color.White;
        private float mRange = 1;
        private float mFov = MathHelper.TwoPi;
        private float mIntensity = 1;
        private ShadowType mShadowType = ShadowType.Solid;

        #region Parameters

        /// <summary>
        /// The light's position
        /// </summary>
        public Vector2 Position
        {
            get { return this.mPosition; }
            set { this.mPosition = value; }
        }

        /// <summary>
        /// The X coordinate of the light's position
        /// </summary>
        public float X
        {
            get { return this.mPosition.X; }
            set { this.mPosition.X = value; }
        }

        /// <summary>
        /// The Y coordinate of the light's position
        /// </summary>
        public float Y
        {
            get { return this.mPosition.Y; }
            set { this.mPosition.Y = value; }
        }

        /// <summary>
        /// The light's angle
        /// </summary>
        public float Angle
        {
            get { return this.mAngle; }
            set { this.mAngle = value; }
        }

        /// <summary>
        /// The texture used as the base light map, from which shadows will be subtracted
        /// </summary>
        public Texture2D Texture
        {
            get { return this.mTexture; }
            set { this.mTexture = value; }
        }

        /// <summary>
        /// The color used to tint the light's texture
        /// </summary>
        public Color Color
        {
            get { return this.mColor; }
            set { this.mColor = value; }
        }

        /// <summary>
        /// The light's maximum radius (or "half width", if you will)
        /// </summary>
        public float Range
        {
            get { return this.mRange; }
            set { this.mRange = value; }
        }

        /// <summary>
        /// Gets or sets the light's field of view. This value determines the angles at which the light will cease to draw
        /// </summary>
        public float Fov
        {
            get { return this.mFov; }
            set
            {
                this.mFov = MathHelper.Clamp(value, 0, MathHelper.TwoPi);
            }
        }

        /// <summary>
        /// Gets or sets the light's intensity. Think pixel = (tex * color) ^ (1 / intensity)
        /// </summary>
        public float Intensity
        {
            get { return this.mIntensity; }
            set { this.mIntensity = MathHelper.Clamp(value, 0.01f, 3f); }
        }

        /// <summary>
        /// Gets or sets a value indicating what type of shadows this light should cast
        /// </summary>
        public ShadowType ShadowType
        {
            get { return this.mShadowType; }
            set { this.mShadowType = value; }
        }

        #endregion Parameters

        /// <summary>
        /// Gets or sets a value indicating weither or not to draw the light
        /// </summary>
        public bool IsOn
        {
            get { return this.mIsOn; }
            set { this.mIsOn = value; }
        }

        /// <summary>
        /// Draws shadows from the light's position outward
        /// </summary>
        /// <param name="helper">A render helper for drawing shadows</param>
        /// <param name="hulls">The shadow hulls used to draw shadows</param>
        public void Draw(KryptonRenderHelper helper, List<ShadowHull> hulls)
        {
            // Draw the light only if it's on
            if (!this.mIsOn)
                return;

            // Make sure we only render the following hulls
            helper.ShadowHullVertices.Clear();
            helper.ShadowHullIndicies.Clear();

            // Loop through each hull
            foreach (ShadowHull hull in hulls)
            {
                //if(hull.Bounds.Intersects(this.Bounds))
                // Add the hulls to the buffer only if they are within the light's range
                if (hull.Visible && Light2D.IsInRange(hull.Position - this.Position, hull.MaxRadius * Math.Max(hull.Scale.X, hull.Scale.Y) + this.Range))
                {
                    helper.BufferAddShadowHull(hull);
                }
            }

            // Set the effect and parameters
            helper.Effect.Parameters["LightPosition"].SetValue(this.mPosition);
            helper.Effect.Parameters["Texture0"].SetValue(this.mTexture);
            helper.Effect.Parameters["LightIntensityFactor"].SetValue(1 / (this.mIntensity * this.mIntensity));


            switch (this.mShadowType)
            {
                case (ShadowType.Solid):
                    helper.Effect.CurrentTechnique = helper.Effect.Techniques["PointLight_Shadow_Solid"];
                    break;

                case (ShadowType.Illuminated):
                    helper.Effect.CurrentTechnique = helper.Effect.Techniques["PointLight_Shadow_Illuminated"];
                    break;

                case (ShadowType.Occluded):
                    helper.Effect.CurrentTechnique = helper.Effect.Techniques["PointLight_Shadow_Occluded"];
                    break;

                default:
                    throw new NotImplementedException("Shadow Type does not exist: " + this.mShadowType);
            }

            foreach (var pass in helper.Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                helper.BufferDraw();
            }

            helper.Effect.CurrentTechnique = helper.Effect.Techniques["PointLight_Light"];
            foreach (var pass in helper.Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                helper.DrawClippedFov(this.mPosition, this.mAngle, this.mRange * 2, this.mColor, this.mFov);
            }

            helper.Effect.CurrentTechnique = helper.Effect.Techniques["ClearTarget_Alpha"];
            foreach (var pass in helper.Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                helper.GraphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip, KryptonRenderHelper.UnitQuad, 0, 2);
            }
        }

        /// <summary>
        /// Determines if a vector's length is less than a specified value
        /// </summary>
        /// <param name="offset">Offset</param>
        /// <param name="dist">Distance</param>
        /// <returns></returns>
        private static bool IsInRange(Vector2 offset, float dist)
        {
            // a^2 + b^2 < c^2 ?
            return offset.X * offset.X + offset.Y * offset.Y < dist * dist;
        }

        /// <summary>
        /// Gets the world-space bounds which contain the light
        /// </summary>
        public BoundingRect Bounds
        {
            get
            {
                BoundingRect rect = new BoundingRect();

                rect.Min.X = this.mPosition.X - this.mRange;
                rect.Min.Y = this.mPosition.Y - this.mRange;
                rect.Max.X = this.mPosition.X + this.mRange;
                rect.Max.Y = this.mPosition.Y + this.mRange;

                return rect;
            }
        }
    }
}
