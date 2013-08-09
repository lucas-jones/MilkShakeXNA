using System;

namespace MilkShakeFramework.Core
{
    public class Node : INode
    {
        public const string UNDEFINED_NODE_NAME = "Undefined";

        public Guid GUID { get; private set; }
        public virtual string Name { get; set; }
        public ITreeNode Parent { get; private set; }

        public Node(string name = null)
        {
            GUID = Guid.NewGuid();
            Name = name ?? UNDEFINED_NODE_NAME;
        }

        public virtual void SetParent(ITreeNode node)
        {
            if (Parent != null) Parent.RemoveNode(this);

            Parent = node;
        }

        public virtual void Destroy() { }

        // [Helper]
        public int SetValueOrDefault(int value, int defaultValue)
        {
            return (value == 0) ? defaultValue : value;
        }
    }
}
