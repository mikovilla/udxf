namespace udxf.Domain
{
    public class XNode : INode
    {
        public required object Node { get; set; }
        public string NodeName { get; set; } = string.Empty;
    }
}
