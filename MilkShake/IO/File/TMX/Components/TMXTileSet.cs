using System.Xml;
using MilkShakeFramework.IO.File.XML;

namespace MilkShakeFramework.IO.File.TMX.Components
{
    public class TMXTileSet : NodeReader
    {
        public string Name;
        public int FirstGID;
        public int TileWidth;
        public int TileHeight;

        public TMXImageNode ImageNode;

        public TMXTileSet(XmlNode tileNode) : base(tileNode)
        {
            Name = GetValue(TMXKeywords.NAME);
            FirstGID = GetValue<int>(TMXKeywords.FIRSTGID);
            TileWidth = GetValue<int>(TMXKeywords.TILE_WIDTH);
            TileHeight = GetValue<int>(TMXKeywords.TILE_HEIGHT);

            ImageNode = new TMXImageNode(tileNode.SelectSingleNode("image"));
        }

    }
}
