using MilkShakeFramework.IO.File.TMX.Components;

namespace MilkShakeFramework.Core.Game.Components.Tile
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
