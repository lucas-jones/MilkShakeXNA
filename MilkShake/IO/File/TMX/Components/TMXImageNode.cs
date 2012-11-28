using System.Xml;
using MilkShakeFramework.IO.File.XML;

namespace MilkShakeFramework.IO.File.TMX.Components
{
    public class TMXImageNode : NodeReader
    {
        public string Source;
        public int Width;
        public int Height;

        public TMXImageNode(XmlNode imageNode) : base(imageNode)
        {
            Source = GetValue(TMXKeywords.SOURCE);
            Width = GetValue<int>(TMXKeywords.WIDTH);
            Height = GetValue<int>(TMXKeywords.HEIGHT);
        }
    }
}
