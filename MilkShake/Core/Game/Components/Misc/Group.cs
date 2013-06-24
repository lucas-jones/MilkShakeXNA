using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MilkShakeFramework.Tools.Maths;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Core.Game.Components.Misc
{
    public class Group : GameEntity
    {
        public Group() : base() { }
        public Group(List<GameEntity> nodes) : base() { nodes.ForEach(node => AddNode(node)); }

        private RotatedRectangle _boundingBox;

        public GameEntity this[String key]
        {
            get
            {
                return (GameEntity)GetNodeByName(key);
            }
        }

        private RotatedRectangle GetRectangle()
        {
            float topX = Nodes.OfType<GameEntity>().ToList<GameEntity>().OrderBy(e => e.BoundingBox.UpperLeftCorner().X).First().BoundingBox.UpperLeftCorner().X;
            float topY = Nodes.OfType<GameEntity>().ToList<GameEntity>().OrderByDescending(e => e.BoundingBox.UpperLeftCorner().Y).Last().BoundingBox.UpperLeftCorner().Y;

            float bottomX = Nodes.OfType<GameEntity>().ToList<GameEntity>().OrderByDescending(e => e.BoundingBox.LowerRightCorner().X).First().BoundingBox.LowerRightCorner().X;
            float bottomY = Nodes.OfType<GameEntity>().ToList<GameEntity>().OrderBy(e => e.BoundingBox.LowerRightCorner().Y).Last().BoundingBox.LowerRightCorner().Y;


            return new RotatedRectangle((int)topX, (int)topY, (int)(bottomX - topX), (int)(bottomY - topY));
        }

        public override Tools.Maths.RotatedRectangle BoundingBox
        {
            get
            {
                return (Nodes.Count > 0) ? GetRectangle() : new RotatedRectangle((int)Position.X, (int)Position.Y, 100, 100);
            }
        }
    }
}
