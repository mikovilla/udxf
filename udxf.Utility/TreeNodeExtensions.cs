﻿using System.Text.Json;
using System.Xml.Linq;
using udxf.Domain;
using static udxf.Domain.Enums;

namespace udxf.Utility
{
    public static class TreeNodeExtensions
    {
        public static string Serialize(this TreeNode node, FormatType format, (byte[] Key, byte[] IV)? cryptoParam = null)
        {
            string serializedData = string.Empty;
            bool mustEncrypt = (cryptoParam != null && cryptoParam.Value.Key != null && cryptoParam.Value.IV != null);
            if (format == FormatType.Json)
            {
                var jsonObject = new Dictionary<string, object>()
                {
                    { node.Name, JsonSerialize(node) }
                };
                serializedData = JsonSerializer.Serialize(jsonObject);
            }
            else if (format == FormatType.Xml)
            {
                serializedData = XmlSerialize(node).ToString(SaveOptions.DisableFormatting);
            }
            else
            {
                throw new NotSupportedException();
            }

            return mustEncrypt ? serializedData.Encrypt(cryptoParam!.Value.Key!, cryptoParam!.Value.IV!) : serializedData;
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
