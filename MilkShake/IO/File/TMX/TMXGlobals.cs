using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.IO.File.TMX
{
    public class TMXKeywords
    {
        public const string VERSION = "version";
        public const string WIDTH = "width";
        public const string HEIGHT = "height";
        public const string TILE_WIDTH = "tilewidth";
        public const string TILE_HEIGHT = "tileheight";
        public const string ORIENTATION = "orientation";
        public const string SOURCE = "source";
        public const string FIRSTGID = "firstgid";
        public const string NAME = "name";
        public const string TILES_SET = "tileset";
        public const string LAYER = "layer";
        public const string DATA = "data";
        public const string ENCODING = "encoding";
        public const string PROPERTIES = "properties";
        public const string VALUE = "value";
    }

    public class TMXEncoding
    {
        public const string CSV = "csv";
    }

    public enum TMXOrientation
    {
        Orthogonal,
        Isometric
    }
        
}