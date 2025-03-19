namespace udxf.Domain
{
    public class JNode : INode
    {
        public required object Node { get; set; }
        public string NodeName { get; set; } = "Root";
    }
}
