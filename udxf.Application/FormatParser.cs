using udxf.Domain;

namespace udxf.Application
{
    public abstract class FormatParser : IFormatParser
    {
        protected abstract TreeNode Convert(INode node);
        public TreeNode Deserialize(INode node)
        {
            return Convert(node);
        }
    }
}
