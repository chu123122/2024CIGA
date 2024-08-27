using System.Collections.Generic;

namespace Utilities
{
    public class TreeNode<T>
    {
        public T Value { get; set; }
        public List<TreeNode<T>> Children { get; set; }

        public TreeNode(T value, List<TreeNode<T>> children)
        {
            Value = value;
            Children = children;
        }

        public void ChildrenAdd(TreeNode<T> node)
        {
            Children.Add(node);
        }

        public TreeNode<T> GetChild(T value)
        {
            return Children.Find(x => Equals(x.Value, value));
        }
    }
}