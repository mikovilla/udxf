using System.Text.Json.Nodes;
using udxf.Domain;

namespace udxf.Application
{
    public class JsonFormatter : Formatter
    {
        private JsonFormatter() { }
        private static JsonFormatter? _formatter = null;
        public static JsonFormatter GetInstance()
        {
            if(_formatter == null)
            {
                _formatter = new JsonFormatter();
            }
            return _formatter;
        }

        protected override TreeNode Parse(INode node)
        {
            string nodeName = node.NodeName;
            var nodeValue = node.Node;
            TreeNode treeNode = new TreeNode(nodeName);
            if (nodeValue is JsonObject nodeObj && nodeObj.Count == 1)
            {
                string rootKey = nodeObj.First().Key;
                JsonNode rootValue = nodeObj[rootKey]!;
                treeNode = Parse(new JNode { Node = rootValue, NodeName = rootKey });
            }
            else
            {
                if (nodeValue is JsonObject obj)
                {
                    foreach (var property in obj)
                    {

                        if (property.Value is JsonObject || property.Value is JsonArray)
                        {
                            treeNode.AddChild(Parse(new JNode { Node = property.Value, NodeName = property.Key }));
                        }
                        else
                        {
                            treeNode.AddChild(new TreeNode(property.Key, property.Value?.ToString()));
                        }
                    }
                }
                else if (nodeValue is JsonArray array)
                {
                    int index = 0;
                    foreach (var item in array)
                    {
                        treeNode.AddChild(Parse(new JNode { Node = item!, NodeName = $"Item {index++}" }));
                    }
                }
                else
                {
                    treeNode.Value = nodeValue.ToString();
                }
            }

            return treeNode;
        }
    }
}
