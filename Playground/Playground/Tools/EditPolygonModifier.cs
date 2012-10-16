using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game.Components.Polygon;
using Microsoft.Xna.Framework;
using MilkShakeFramework.IO.Input.Devices;
using MilkShakeFramework.Render;
using MilkShakeFramework.Core.Scenes;

namespace Playground.Tools
{
    public class EditPolygonModifier : PolygonModifier
    {
        private Image _anchorImage, _anchorSelectedImage;

        private int _selectedVertIndex = -1;

        public EditPolygonModifier() : base(true)
        {
            _anchorImage = new Image("icons//blue_big");
            _anchorSelectedImage = new Image("icons//red_big");

            AddNode(_anchorImage);
            AddNode(_anchorSelectedImage);
        }

        public override void FixUp()
        {
            base.FixUp();

            // Render outside the Polygon render loop?
            Scene.Listener.PostDraw[DrawLayer.Fifth] += PostDraw;
        }

        private float Distance(Vector2 a, Vector2 b)
        {
            return Vector2.Distance(a, b);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            if (_selectedVertIndex == -1)
            {
                // Non selected
                if (MouseInput.isLeftDown())
                {
                    List<Vector2> vertList = Polygon.Vertices.ToList<Vector2>();

                    Vector2 a = vertList.OrderBy(v => Math.Abs(Distance(v + WorldPosition, MouseInput.Position))).ToArray()[0];


                    if (vertList.Count > 0)
                    {
                        _selectedVertIndex = vertList.IndexOf(a);
                    }
                    else
                    {
                        Console.WriteLine("Non in range");
                    }
                }
            }
            else
            {
                Polygon.Vertices[_selectedVertIndex] = MouseInput.Position - Polygon.WorldPosition;
                Polygon.UpdateRenderer();

                if (MouseInput.isLeftReleased())
                {
                    _selectedVertIndex = -1;
                }
            }
            
            base.Update(gameTime);
        }

        public void PostDraw()
        {
            Scene.RenderManager.Begin();

            for (int i = 0; i < Polygon.Vertices.Length; i++)
            {
                _anchorImage.Draw(Polygon.Vertices[i] + Polygon.WorldPosition - _anchorImage.ImageCenter);
            }

            // Draw Selected
            if (_selectedVertIndex != -1) _anchorSelectedImage.Draw(Polygon.Vertices[_selectedVertIndex] + Polygon.WorldPosition - _anchorSelectedImage.ImageCenter);

            Scene.RenderManager.End();
        }

    }
}
