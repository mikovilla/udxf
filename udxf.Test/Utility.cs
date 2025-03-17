using Newtonsoft.Json;
using udxf.Utility;
using Xunit;
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
            Assert.Equal(TreeNodeToString(xml.UnifiedFormat()), TreeNodeToString(json.UnifiedFormat()));
        }

        [Theory]
        [InlineData("<person><name>miko</name><age>18</age></person>", "{\"person\":{\"name\":\"miko\",\"age\":18}}")]
        public void ValuesAreInterchangeable(string xml, string json)
        {
            Assert.Equal(xml.UnifiedFormat().ToJson(), json);
            Assert.Equal(json.UnifiedFormat().ToXml(), xml);
        }

        private string TreeNodeToString(TreeNode node)
        {
            return JsonConvert.SerializeObject(node);
        }
    }
}
