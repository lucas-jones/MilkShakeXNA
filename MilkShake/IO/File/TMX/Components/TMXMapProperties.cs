using System.Xml;
using MilkShakeFramework.IO.File.XML;

namespace MilkShakeFramework.IO.File.TMX.Components
{
    public class TMXMapProperties : NodeReader
    {
        public string Version;
        public int Width;
        public int Height;
        public int TileWidth;
        public int TileHeight;
        public TMXOrientation Orientation;

        public TMXMapProperties(XmlNode mapNode) : base(mapNode)
        {
            Version =       GetValue(TMXKeywords.VERSION);
            Width =         GetValue<int>(TMXKeywords.WIDTH);
            Height =        GetValue<int>(TMXKeywords.HEIGHT);
            TileWidth =     GetValue<int>(TMXKeywords.TILE_WIDTH);
            TileHeight =    GetValue<int>(TMXKeywords.TILE_HEIGHT);
            Orientation =   (GetValue(TMXKeywords.ORIENTATION) == "orthogonal") ? TMXOrientation.Orthogonal : TMXOrientation.Isometric;
        }
    }
}
