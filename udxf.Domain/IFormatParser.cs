namespace udxf.Domain
{
    public interface IFormatParser
    {
        TreeNode ToTree(INode node);
    }
}
