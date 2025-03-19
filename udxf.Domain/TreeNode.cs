namespace udxf.Domain
{
    public class TreeNode
    {
        public string Name { get; set; }
        public string? Value { get; set; }
        public List<TreeNode>? Children { get; private set; } = null;

        public TreeNode(string name, string? value = null)
        {
            Name = name;
            Value = value;
        }

        public void AddChild(TreeNode child)
        {
            if (Children == null)
            {
                Children = new List<TreeNode>();
            }
            Children.Add(child);
        }
    }
}
