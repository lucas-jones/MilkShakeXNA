using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Core
{
    public interface ITreeNode
    {
        List<INode> Nodes { get; }
        void AddNode(INode node, int index = -1);
        void RemoveNode(INode node);
    }
}
