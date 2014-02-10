using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MilkShakeFramework.Render
{
    public class MatrixStack
    {
        public Matrix Top { get; private set; }
        private Stack<Matrix> Stack;

        public MatrixStack()
        {
            Stack = new Stack<Matrix>();
            
            Clear();
        }

        public void Clear()
        {
            Stack.Clear();
            Top = Matrix.Identity;
            Push(Matrix.Identity);
        }

        public void Push(Matrix matrix)
        {
            Matrix newMatrix = matrix * Top;
            Top = newMatrix;
            Stack.Push(Top);
        }

        public void Pop()
        {
            Stack.Pop();
            Top = Stack.Peek();
        }
    }
}
