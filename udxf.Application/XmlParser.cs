using System.Xml.Linq;
using udxf.Domain;

namespace udxf.Application
{
    public class XmlParser : FormatParser
    {
        protected override TreeNode Convert(INode node)
        {
            var element = (XElement)node.Node;
            var treeNode = new TreeNode(element.Name.LocalName);

            foreach (var child in element.Elements())
            {
                treeNode.AddChild(Convert(new Domain.XNode { Node = child }));
            }

            if (!element.HasElements)
            {
                treeNode.Value = element.Value;
            }

            return treeNode;
        }
    }
}
