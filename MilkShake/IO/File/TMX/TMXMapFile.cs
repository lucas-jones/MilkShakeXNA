using System.Collections.Generic;
using System.Xml;

using MilkShakeFramework.Core.Game;
using MilkShakeFramework.Core.Content;
using MilkShakeFramework.IO.File.TMX.Components;

namespace MilkShakeFramework.IO.File.TMX
{
    public class TMXMapFile : Entity
    {
        public TMXMapProperties MapProperties;
        public TMXProperties Properties;

        public List<TMXTileSet> TileSets;
        public List<TMXLayer> Layers;

        private string mPath;

        public TMXMapFile(string path)
        {
            mPath = path;
        }

        public override void Load(LoadManager content)
        {
            base.Load(content);

            XmlDocument tiledDocument = new XmlDocument();
            tiledDocument.Load(mPath);

            XmlNode mapNode = tiledDocument.SelectSingleNode("/map");

            MapProperties = new TMXMapProperties(mapNode);
            Layers = new List<TMXLayer>();
            TileSets = new List<TMXTileSet>();

            foreach (XmlNode node in mapNode)
            {
                if (node.Name == TMXKeywords.TILES_SET) TileSets.Add(new TMXTileSet(node));
                if (node.Name == TMXKeywords.LAYER) Layers.Add(new TMXLayer(node, Layers.Count));
                if (node.Name == TMXKeywords.PROPERTIES) Properties = new TMXProperties(node);
            }
        }

    }
}
