using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using udxf.Domain;
using udxf.Utility;
using static udxf.Domain.Enums;

namespace udxf.Application
{
    public static class Unify
    {
        public static INode GetNode(this string data)
        {
            if (data.IsJson())
            {
                return new JNode { Node = JsonNode.Parse(data)! };
            }
            else if (data.IsXml())
            {
                return new Domain.XNode { Node = XElement.Parse(data)! };
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public static TreeNode Format(this INode node)
        {
            if (node is JNode)
            {
                return new JsonParser().ToTree(node);
            }
            else if (node is Domain.XNode)
            {
                return new XmlParser().ToTree(node);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        public static string Serialize(this TreeNode node, FormatType format)
        {
            if (format == FormatType.Json)
            {
                var jsonObject = new Dictionary<string, object>()
                {
                    { node.Name, JsonSerialize(node) }
                };
                return JsonSerializer.Serialize(jsonObject);
            }
            else if (format == FormatType.Xml)
            {
                return XmlSerialize(node).ToString(SaveOptions.DisableFormatting);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private static object JsonSerialize(TreeNode node)
        {
            if (node.Children == null || node.Children?.Count == 0)
            {
                return node.Value!.CorrectType();
            }

            var jsonObject = new Dictionary<string, object>();

            foreach (var child in node.Children ?? Enumerable.Empty<TreeNode>())
            {
                if (jsonObject.ContainsKey(child.Name))
                {
                    if (jsonObject[child.Name] is List<object> list)
                    {
                        list.Add(JsonSerialize(child));
                    }
                    else
                    {
                        var existingValue = jsonObject[child.Name];
                        jsonObject[child.Name] = new List<object> { existingValue, JsonSerialize(child) };
                    }
                }
                else
                {
                    jsonObject[child.Name] = JsonSerialize(child);
                }
            }

            return jsonObject;
        }

        private static XElement XmlSerialize(TreeNode node)
        {
            XElement element = new XElement(node.Name);

            if (!string.IsNullOrEmpty(node.Value))
            {
                element.Value = node.Value;
            }

            foreach (var child in node.Children ?? Enumerable.Empty<TreeNode>())
            {
                element.Add(XmlSerialize(child));
            }

            return element;
        }
    }
}
