using udxf.Domain;

namespace udxf.Application
{
    public abstract class Formatter : IFormatter
    {
        protected abstract TreeNode Parse(INode node);
        public TreeNode Deserialize(INode node)
        {
            return Parse(node);
        }
    }
}
