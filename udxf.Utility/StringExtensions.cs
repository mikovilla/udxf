using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml;
using System.Xml.Linq;

namespace udxf.Utility
{
    public static class StringExtensions
    {
        public static string CheckFormat(this string data) 
        {
            return data.IsJson() ? "json" : 
                data.IsXML() ? "xml" : "undefined";
        }

        public static bool IsXML(this string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return false;
            }

            try
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(data);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public static bool IsJson(this string data)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return false;
            }

            try
            {
                using (JsonDocument.Parse(data))
                {
                    return true;
                }
            }
            catch (JsonException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public static TreeNode UnifiedFormat(this string data)
        {
            if (data.IsXML())
            {
                return TreeParser.ParseXmlToTree(XElement.Parse(data));
            }
            else if (data.IsJson())
            {
                return TreeParser.ParseJsonToTree(JsonNode.Parse(data)!);
            }
            else
            {
                throw new ArgumentException("Cannot be converted to a unified format.");
            }
        }
    }
}
