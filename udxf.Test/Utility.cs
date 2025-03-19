using Newtonsoft.Json;
using udxf.Application;
using udxf.Domain;
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
        public void ValuesAreEqual(string xml, string json)
        {
            var jsonNode = Unify.GetNode(json);
            var xmlNode = Unify.GetNode(xml);
            Assert.Equal(
                TreeNodeToString(Unify.Format(xmlNode)), 
                TreeNodeToString(Unify.Format(jsonNode)));
        }

        [Theory]
        [InlineData("<person><name>miko</name><age>18</age></person>", "{\"person\":{\"name\":\"miko\",\"age\":18}}")]
        public void ValuesAreInterchangeable(string xml, string json)
        {
            var xmlFormat = xml.GetNode().Format();
            var jsonFormat = json.GetNode().Format();
            Assert.Equal(xmlFormat.Serialize(FormatType.Json), json);
            Assert.Equal(jsonFormat.Serialize(FormatType.Xml), xml);
        }

        private string TreeNodeToString(TreeNode node)
        {
            return JsonConvert.SerializeObject(node);
        }
    }
}
