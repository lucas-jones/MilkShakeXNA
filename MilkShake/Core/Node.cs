using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MilkShakeFramework.Core
{
    public interface INode
    {
        Guid GUID { get; }
        string Name { get; set; }
        ITreeNode Parent { get; }

        void SetParent(ITreeNode parent);
        void Destroy();
    }

    public class Node : INode
    {
        private Guid mGUID;
        private String mName;
        private ITreeNode mParent;

        public Node(string name = null)
        {
            mGUID = Guid.NewGuid();
            mName = (name == null) ? "Undefined" : name;
        }

        public virtual void SetParent(ITreeNode node)
        {
            if (Parent != null) Parent.RemoveNode(this);

            mParent = node;
        }

        public virtual void Destroy()
        {

        }

        // [Public]
        public Guid GUID { get { return mGUID; } }
        public string Name { get { return mName; } set { mName = value; } }
        public ITreeNode Parent { get { return mParent; } }

        // [Helper]
        public int SetValueOrDefault(int value, int defaultValue)
        {
            return (value == 0) ? defaultValue : value;
        }
    }
}
