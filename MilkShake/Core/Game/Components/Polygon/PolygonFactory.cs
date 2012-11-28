using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Triangulator;

namespace MilkShakeFramework.Core.Game.Components.Polygon
{
    public class PolygonDataFactory
    {
        public static PolygonData PolygonFromPoints(List<Vector2> _points)
        {
            Vector2[] sourceVerticies;
            int[] sourceIntIndicies;

            // [Clean up namespace]
            Triangulator.Triangulator.Triangulate(_points.ToArray(), WindingOrder.Clockwise, out sourceVerticies, out sourceIntIndicies);

            // Convert int[] -> short[]
            short[] sourceShortIndicies = Array.ConvertAll<int, short>(sourceIntIndicies, p => (short)p);

            return new PolygonData() { Verticies = sourceVerticies, Indicies = sourceShortIndicies, Points = _points };
        }

        public static PolygonData Tri(int _width, int _height)
        {
            Vector2[] sourceVerticies = new Vector2[3];
            sourceVerticies[0] = new Vector2(0, 0);
            sourceVerticies[1] = new Vector2(_width, 0);
            sourceVerticies[2] = new Vector2(_width, _height);

            Array.Reverse(sourceVerticies);

            short[] sourceIndicies = new short[3];
            sourceIndicies[0] = 0;
            sourceIndicies[1] = 1;
            sourceIndicies[2] = 2;

            return new PolygonData() { Verticies = sourceVerticies, Indicies = sourceIndicies };
        }

        public static PolygonData Quad(int _width, int _height)
        {
            List<Vector2> quad = new List<Vector2>();
            quad.Add(Vector2.Zero);
            quad.Add(new Vector2(0, _height));
            quad.Add(new Vector2(_width, _height));
            quad.Add(new Vector2(_width, 0));

            return PolygonFromPoints(quad);
        }
    }
}
