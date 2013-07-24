using System.Collections.Generic;

namespace MilkShakeFramework.Core
{
    public class TreeNode : Node, ITreeNode
    {
        private List<INode> Nodes { get; private set; }

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
            gameObject.Destroy();
            Nodes.Remove(gameObject);
        }

        public INode GetNodeByName(string name)
        {
            return Nodes.Find(n => n.Name == name);
        }

        public T GetNodeByName<T>(string name)
        {
            return (T)GetNodeByName(name);
        }
    }
}
