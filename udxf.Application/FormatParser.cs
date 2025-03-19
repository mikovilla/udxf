using udxf.Domain;

namespace udxf.Application
{
    public abstract class FormatParser : IFormatParser
    {
        protected abstract TreeNode Convert(INode node);
        public TreeNode ToTree(INode node)
        {
            return Convert(node);
        }
    }
}
