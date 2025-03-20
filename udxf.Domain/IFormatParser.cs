namespace udxf.Domain
{
    public interface IFormatParser
    {
        TreeNode Deserialize(INode node);
    }
}
