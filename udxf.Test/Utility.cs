using udxf.Application;
using udxf.Utility;
using Xunit;
using static udxf.Domain.Enums;
namespace udxf.Test
{
    public class Utility
    {
        [Theory]
        [InlineData("<xml></xml>", "xml")]
        [InlineData("{}", "json")]
        [InlineData("", "undefined")]
        public void IsDataFormattedCorrectly(string data, string format)
        {
            string actualFormat = data.CheckFormat();
            Assert.Equal(format, actualFormat.ToLower());
        }

        [Theory]
        [InlineData("<person><name>miko</name><age>18</age></person>", "{\"person\":{\"name\":\"miko\",\"age\":18}}")]
        public void ValuesAreInterchangeable_StringToNode(string xml, string json)
        {
            var xmlFormat = xml.ToNode().Deserialize(XmlParser.GetXmlFormat());
            var jsonFormat = json.ToNode().Deserialize(JsonParser.GetJsonFormat());
            Assert.Equal(xmlFormat.Serialize(FormatType.Json), json);
            Assert.Equal(jsonFormat.Serialize(FormatType.Xml), xml);
        }

        [Theory]
        [InlineData("<person><name>miko</name><age>18</age></person>", "{\"person\":{\"name\":\"miko\",\"age\":18}}")]
        public void ValuesAreInterchangeable_StringToTree(string xml, string json)
        {
            var xmlFormat = xml.Deserialize(XmlParser.GetXmlFormat());
            var jsonFormat = json.Deserialize(JsonParser.GetJsonFormat());
            Assert.Equal(xmlFormat.Serialize(FormatType.Json), json);
            Assert.Equal(jsonFormat.Serialize(FormatType.Xml), xml);
        }
    }
}
