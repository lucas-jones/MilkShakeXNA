using System.Xml;
using MilkShakeFramework.IO.File.XML;
using System;

namespace MilkShakeFramework.IO.File.TMX.Components
{
    public class TMXLayer : NodeReader
    {
        public string Name;
        public TMXProperties Properties;
        public int Width;
        public int Height;
        public int Index;

        public string Encoding;
        public int[,] Data;

        public TMXLayer(XmlNode layerNode, int layerIndex) : base(layerNode)
        {
            Name = GetValue(TMXKeywords.NAME);
            Width = GetValue<int>(TMXKeywords.WIDTH);
            Height = GetValue<int>(TMXKeywords.HEIGHT);
            Index = layerIndex;

            Properties = new TMXProperties();
            Data = new int[Width, Height];

            foreach (XmlNode node in layerNode)
            {
                if (node.Name == TMXKeywords.PROPERTIES) Properties = new TMXProperties(node);
                if (node.Name == TMXKeywords.DATA) ProccesData(node);
            }
        }

        private void ProccesData(XmlNode data)
        {
            Encoding = data.Attributes[TMXKeywords.ENCODING].Value;

            if (Encoding == TMXEncoding.CSV) ProccessCSV(data);
            else Console.WriteLine("[TMX] Unknown Encoding: " + Encoding);
        }

        private void ProccessCSV(XmlNode data)
        {
            string[] csvData = data.FirstChild.Value.PadLeft(1).Split('\n');

            for (int y = 0; y < Height; y++)
            {
                string[] currentRowData = csvData[y + 1].Split(',');

                for (int x = 0; x < Width; x++)
                {
                    Data[x, y] = int.Parse(currentRowData[x]) - 1;
                }
            }
        }
    }
}
