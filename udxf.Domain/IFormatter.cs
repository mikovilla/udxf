namespace udxf.Domain
{
    public interface IFormatter
    {
        TreeNode Deserialize(INode node);
    }
}
