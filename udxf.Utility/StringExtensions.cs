using System.Text.Json;
using System.Xml;

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
    }
}
