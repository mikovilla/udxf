using System.Xml.Linq;
using udxf.Domain;

namespace udxf.Application
{
    public class XmlFormatter : Formatter
    {
        private static XmlFormatter? _xmlParser = null;
        private XmlFormatter() { }
        public static XmlFormatter GetInstance()
        {
            if(_xmlParser == null)
            {
                _xmlParser = new XmlFormatter();
            }
            return _xmlParser;
        }

        protected override TreeNode Parse(INode node)
        {
            var element = (XElement)node.Node;
            var treeNode = new TreeNode(element.Name.LocalName);

            foreach (var child in element.Elements())
            {
                treeNode.AddChild(Parse(new Domain.XNode { Node = child }));
            }

            if (!element.HasElements)
            {
                treeNode.Value = element.Value;
            }

            return treeNode;
        }
    }
}
