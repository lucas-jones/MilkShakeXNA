using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Core.Game;
using MilkShakeFramework.IO.File.TMX.Components;

namespace MilkShakeFramework.Components.Tile
{
    public class TileLayer : GameEntity
    {
        private TMXLayer mLayer;

        public TileLayer(TMXLayer layer)
        {
            mLayer = layer;
        }
    }
}
