using System;

namespace MilkShakeFramework.Core.Game.Components.Polygon
{
    public class PolygonModifier : GameEntity
    {
        public override void SetParent(ITreeNode parent)
        {
            if (!(parent is Polygon)) throw new Exception("Parent must be of Polygon");

            base.SetParent(parent);
        }

        public Polygon Polygon { get { return (Polygon)Parent; } }
    }
}
