using System.Security.Cryptography;
using udxf.Application;
using udxf.Utility;
using Xunit;
using static udxf.Domain.Enums;
namespace udxf.Test
{
    public class Utility
    {
        private readonly string xml, json;
        public Utility()
        {
            xml = "<person><name>miko</name><age>18</age></person>";
            json = "{\"person\":{\"name\":\"miko\",\"age\":18}}";
        }

        [Theory]
        [InlineData("<xml></xml>", "xml")]
        [InlineData("{}", "json")]
        [InlineData("", "undefined")]
        public void IsDataFormattedCorrectly(string data, string format)
        {
            string actualFormat = data.CheckFormat();
            Assert.Equal(format, actualFormat.ToLower());
        }

        [Fact]
        public void ValuesAreInterchangeable_StringToNode()
        {
            var xmlFormat = xml.ToNode().Deserialize(XmlFormatter.GetInstance());
            var jsonFormat = json.ToNode().Deserialize(JsonFormatter.GetInstance());
            Assert.Equal(xmlFormat.Serialize(FormatType.Json), json);
            Assert.Equal(jsonFormat.Serialize(FormatType.Xml), xml);
        }

        [Fact]
        public void ValuesAreInterchangeable_StringToTree()
        {
            var xmlFormat = xml.Deserialize(XmlFormatter.GetInstance());
            var jsonFormat = json.Deserialize(JsonFormatter.GetInstance());
            Assert.Equal(xmlFormat.Serialize(FormatType.Json), json);
            Assert.Equal(jsonFormat.Serialize(FormatType.Xml), xml);
        }

        [Fact]
        public void ValuesAreInterchangeable_Reserialize()
        {
            Assert.Equal(xml.Reformat(), json);
            Assert.Equal(json.Reformat(), xml);
        }

        [Fact]
        public void CanEncryptDecrypt()
        {
            string plainText = "Hello, world!";
            string passPhrase = "miko";
            var saltedKey = passPhrase.ApplySalt("salt");
            var cipherText = plainText.Encrypt(saltedKey.Key, saltedKey.IV);

            Assert.Equal(plainText, cipherText.Decrypt(saltedKey.Key, saltedKey.IV));
        }

        [Fact]
        public void CanReformatEncryptedString()
        {
            string passPhrase = "miko";
            var saltedKey = passPhrase.ApplySalt("salt");

            var encryptedXml = xml.Encrypt(saltedKey.Key, saltedKey.IV);

            Assert.Equal(
                expected: json, 
                actual:
                    encryptedXml.Reformat(
                        cryptoParam: (saltedKey.Key, saltedKey.IV)
                    )
            );
        }
    }
}
