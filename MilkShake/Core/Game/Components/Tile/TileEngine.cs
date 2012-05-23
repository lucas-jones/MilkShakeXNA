using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.IO.File.TMX;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework.IO.File.TMX.Components;
using MilkShakeFramework.Core;

namespace MilkShakeFramework.Components.Tile
{


    public class TileEngine : GameEntity // GameComponent?
    {
        public event OnTileAdded OnTileAddedEvent;
        public delegate void OnTileAdded(Tile tile);

        private TMXMapFile mMapFile;
        private List<TileLayer> mLayers;

        public TileEngine(string mapPath) : base()
        {
            mMapFile = new TMXMapFile(mapPath);
            mLayers = new List<TileLayer>();
        }

        public override void Setup()
        {
            mMapFile.Load(null); // No Node!
            ProccessMap();

            base.Setup();
        }

        private void ProccessMap()
        {
            foreach (TMXLayer layer in mMapFile.Layers)
            {
                TileLayer currentLayer = new TileLayer(layer);
                AddNode(currentLayer);

                for (int x = 0; x < layer.Width; x++)
                for (int y = 0; y < layer.Height; y++)
                {
                    Tile pTile = new Tile(x, y, layer.Data[x, y]);
                    currentLayer.AddNode(pTile);

                    if(OnTileAddedEvent != null) OnTileAddedEvent(pTile);
                }
                
                    
            }
        }

        public override void Draw()
        {
            base.Draw();
        }


        public override void AddNode(INode node)
        {
            if (node is TileLayer) Layers.Add(node as TileLayer);

            base.AddNode(node);
        }

        public TMXMapFile MapFile { get { return mMapFile; } }
        public List<TileLayer> Layers { get { return mLayers; } }
    }
}
