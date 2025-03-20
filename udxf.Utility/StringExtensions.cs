using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml;
using System.Xml.Linq;
using udxf.Application;
using udxf.Domain;
using static udxf.Domain.Enums;

namespace udxf.Utility
{
    public static class StringExtensions
    {
        public static string CheckFormat(this string data) 
        {
            return data.IsJson() ? "json" : 
                data.IsXml() ? "xml" : "undefined";
        }

        public static bool IsXml(this string data)
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

        public static object CorrectType(this string input)
        {
            if (double.TryParse(input, out double intValue))
                return intValue;

            if (bool.TryParse(input, out bool boolValue))
                return boolValue;

            return input;
        }

        public static INode ToNode(this string data)
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

        public static TreeNode Deserialize(this string data, IFormatter formatParser)
        {
            return formatParser.Deserialize(data.ToNode());
        }

        public static string Reformat(this string data, FormatType formatType)
        {
            if (data.IsXml())
            {
                return data.Deserialize(XmlFormatter.GetInstance()).Serialize(formatType);
            }
            else if (data.IsJson())
            {
                return data.Deserialize(JsonFormatter.GetInstance()).Serialize(formatType);
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
