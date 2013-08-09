using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Tools.Utils
{
    public class TextureUtils
    {
        public static Texture2D GenerateTexture(int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(MilkShake.Graphics, width, height);

            Color[] colorArray = Enumerable.Range(0, (width * height)).Select(i => color).ToArray();

            texture.SetData<Color>(colorArray);

            return texture;
        }

        public static Texture2D GenerateTexureWithBorder(int width, int height, Color color, int borderThickness, Color borderColor)
        {
            Texture2D texture = new Texture2D(MilkShake.Graphics, width, height);

            Color[] colorArray = Enumerable.Range(0, (width * height)).Select(i => color).ToArray();

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    if (x < borderThickness || y < borderThickness ||
                        x >= width - borderThickness || y >= height - borderThickness)
                    {
                        colorArray[x + (width * y)] = borderColor;
                    }
                }

            texture.SetData<Color>(colorArray);

            return texture;
        }
    }
}
