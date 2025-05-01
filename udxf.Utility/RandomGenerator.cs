using System.Text.Json;
using System.Xml.Linq;

namespace udxf.Utility
{
    public static class RandomGenerator
    {
        public static string CreateJson(int numberOfEntries)
        {
            var random = new Random();
            var jsonDict = new Dictionary<string, object>();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            for (int i = 0; i < numberOfEntries; i++)
            {
                string randomKey = new string(Enumerable.Repeat(chars, 6)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                object randomValue = random.Next(2) == 0
                    ? (object)random.Next(1, 100) 
                    : (object)$"Value{random.Next(1, 100)}"; 

                jsonDict[randomKey] = randomValue;
            }
            return JsonSerializer.Serialize(jsonDict, new JsonSerializerOptions { WriteIndented = true });
        }

        public static string CreateXml(int numberOfEntries)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            XElement root = new XElement("Root");

            for (int i = 0; i < numberOfEntries; i++)
            {
                string randomKey = new string(Enumerable.Repeat(chars, 6)
                    .Select(s => s[random.Next(s.Length)]).ToArray());

                string randomValue = random.Next(2) == 0
                    ? random.Next(1, 100).ToString() 
                    : $"Value{random.Next(1, 100)}"; 

                root.Add(new XElement(randomKey, randomValue));
            }

            return root.ToString();
        }
    }
}
