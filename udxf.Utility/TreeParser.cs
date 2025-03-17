using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace udxf.Utility
{
    public static class TreeParser
    {
        public static TreeNode ParseJsonToTree(JsonNode node, string nodeName = "Root")
        {
            TreeNode treeNode = new TreeNode(nodeName);
            if (node is JsonObject nodeObj && nodeObj.Count == 1)
            {
                string rootKey = nodeObj.First().Key;
                JsonNode rootValue = nodeObj[rootKey]!;
                treeNode = ParseJsonToTree(rootValue, rootKey);
            }
            else
            {
                if (node is JsonObject obj)
                {
                    foreach (var property in obj)
                    {

                        if (property.Value is JsonObject || property.Value is JsonArray)
                        {
                            treeNode.AddChild(ParseJsonToTree(property.Value, property.Key));
                        }
                        else
                        {
                            treeNode.AddChild(new TreeNode(property.Key, property.Value?.ToString()));
                        }
                    }
                }
                else if (node is JsonArray array)
                {
                    int index = 0;
                    foreach (var item in array)
                    {
                        treeNode.AddChild(ParseJsonToTree(item!, $"Item {index++}"));
                    }
                }
                else
                {
                    treeNode.Value = node.ToString();
                }
            }

            return treeNode;
        }

        public static TreeNode ParseXmlToTree(XElement element)
        {
            var treeNode = new TreeNode(element.Name.LocalName);

            foreach (var child in element.Elements())
            {
                treeNode.AddChild(ParseXmlToTree(child));
            }

            if (!element.HasElements)
            {
                treeNode.Value = element.Value;
            }

            return treeNode;
        }
    }
}
