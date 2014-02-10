using System.Collections.Generic;
using System;

namespace MilkShakeFramework.Core
{
    public class TreeNode : Node, ITreeNode
    {
        public List<INode> Nodes { get; private set; }

        public TreeNode(string name = null) : base(name)
        {
            Nodes = new List<INode>();
        }

        public virtual void AddNode(INode node, int index = -1)
        {
            node.SetParent(this);

            if (index == -1)
            {                
                if (Globals.BackToFrontRender) Nodes.Add(node); // Adds to Bottom
                else Nodes.Insert(0, node);                     // Adds to Top
            }
            else
            {
                Nodes.Insert(index, node);
            }
        }

        public virtual void RemoveNode(INode gameObject)
        {
            if (Nodes.Contains(gameObject))
            {
                Nodes.Remove(gameObject);
            }
            else
            {
                throw new Exception("Requested node isn't child of this node.");
            }
        }

        public INode GetNodeByName(string name)
        {
            foreach (INode node in Nodes)
            {
                if (node.Name == name) return node;
            }

            return null;
        }

        public T GetNodeByName<T>(string name)
        {
            return (T)GetNodeByName(name);
        }
    }
}
