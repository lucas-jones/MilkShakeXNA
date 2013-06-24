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

    public class TreeNode : Node, ITreeNode
    {
        private List<INode> mNodes;

        public TreeNode(string name = null) : base(name)
        {
            mNodes = new List<INode>();
        }

        public virtual void AddNode(INode node, int index = -1)
        {
            node.SetParent(this);

            if (index == -1)
            {
                if (!Globals.BackToFrontRender) mNodes.Insert(0, node); // Adds to Top
                if (Globals.BackToFrontRender) mNodes.Add(node);        // Adds to Bottom
            }
            else
            {
                mNodes.Insert(index, node);
            }
        }

        public virtual void RemoveNode(INode gameObject)
        {
            gameObject.Destroy();
            mNodes.Remove(gameObject);
        }

        public INode GetNodeByName(string name)
        {
            INode node = null;

            foreach (INode n in mNodes)
	        {
                if (n.Name == name)
                {
                    node = n;
                    break;
                }

                if (n is TreeNode)
                {
                    node = (n as TreeNode).GetNodeByName(name);
                }
	        }

            return node;
        }

        public T GetNodeByName<T>(string name)
        {
            return (T)GetNodeByName(name);
        }

        public INode GetNodeByGUID(Guid guid)
        {
            return mNodes.Find(n => n.GUID == guid);
        }

        // [Public]
        public List<INode> Nodes { get { return mNodes; } }

    }
}
