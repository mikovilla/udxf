using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace udxf.Utility
{
    public static class Serialization
    {
        public static string ToJson(this TreeNode node)
        {
            var jsonObject = new Dictionary<string, object>()
            {
                { node.Name, ConvertTreeNodeToDictionary(node) }
            };
            return JsonSerializer.Serialize(jsonObject);
        }

        public static string ToXml(this TreeNode node)
        {
            return ConvertTreeNodeToXml(node).ToString(SaveOptions.DisableFormatting);
        }

        private static object ConvertTreeNodeToDictionary(TreeNode node)
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
                        list.Add(ConvertTreeNodeToDictionary(child));
                    }
                    else
                    {
                        var existingValue = jsonObject[child.Name];
                        jsonObject[child.Name] = new List<object> { existingValue, ConvertTreeNodeToDictionary(child) };
                    }
                }
                else
                {
                    jsonObject[child.Name] = ConvertTreeNodeToDictionary(child);
                }
            }

            return jsonObject;
        }

        private static XElement ConvertTreeNodeToXml(TreeNode node)
        {
            XElement element = new XElement(node.Name);

            if (!string.IsNullOrEmpty(node.Value))
            {
                element.Value = node.Value;
            }

            foreach (var child in node.Children ?? Enumerable.Empty<TreeNode>())
            {
                element.Add(ConvertTreeNodeToXml(child));
            }

            return element;
        }

        public static object CorrectType(this string input)
        {
            if (double.TryParse(input, out double intValue))
                return intValue;

            if (bool.TryParse(input, out bool boolValue))
                return boolValue;

            return input;
        }

    }
}
