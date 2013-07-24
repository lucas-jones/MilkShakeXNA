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
}
