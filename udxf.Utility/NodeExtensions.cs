using udxf.Domain;

namespace udxf.Utility
{
    public static class NodeExtensions
    {
        public static TreeNode Deserialize(this INode node, IFormatter formatParser)
        {
            return formatParser.Deserialize(node);
        }
    }
}
