using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Core
{
    public interface ITreeNode
    {
        List<INode> Nodes { get; }
        void AddNode(INode node);
        void RemoveNode(INode node);
    }

    public class TreeNode : Node, ITreeNode
    {
        private List<INode> mNodes;

        public TreeNode(string name = null) : base(name)
        {
            mNodes = new List<INode>();
        }

        public virtual void AddNode(INode node)
        {
            node.SetParent(this);

            if (!Globals.BackToFrontRender) mNodes.Insert(0, node); // Adds to Top
            if (Globals.BackToFrontRender) mNodes.Add(node);        // Adds to Bottom
        }

        public virtual void RemoveNode(INode gameObject)
        {
            mNodes.Remove(gameObject);
        }

        public INode GetNodeByName(string name)
        {
            return mNodes.Find(n => n.Name == name);
        }

        public INode GetNodeByGUID(Guid guid)
        {
            return mNodes.Find(n => n.GUID == guid);
        }

        // [Public]
        public List<INode> Nodes { get { return mNodes; } }

    }
}
