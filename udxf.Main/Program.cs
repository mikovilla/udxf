using System.Diagnostics;
using udxf.Utility;

var numberOfEntries = new[] { 10, 100, 1_000, 10_000, 100_000, 1_000_000 };

Stopwatch sw = Stopwatch.StartNew();
foreach (var entry in numberOfEntries)
{
    var json = RandomGenerator.CreateJson(entry);
    var xml = json.Reformat();
    Console.WriteLine($"Json ({json.Length} characters) with {entry} objects serialized to Xml ({xml.Length}) for {sw.ElapsedMilliseconds} ms");
}
sw.Stop();

Console.WriteLine();

sw = Stopwatch.StartNew();
foreach (var entry in numberOfEntries)
{
    var xml = RandomGenerator.CreateJson(entry);
    var json = xml.Reformat();
    Console.WriteLine($"Xml ({xml.Length} characters) with {entry} objects serialized to Json ({json.Length}) for {sw.ElapsedMilliseconds} ms");
}
sw.Stop();