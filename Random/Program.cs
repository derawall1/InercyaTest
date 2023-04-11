


using System.Diagnostics;
using System.Text;

var random = new Random();
var hashSet = new HashSet<int>();

var stopWatch = Stopwatch.StartNew();

while (hashSet.Count < 1000 * 1000)
{
    hashSet.Add(random.Next(0, int.MaxValue));
}

saveFile(hashSet, "..\\..\\..\\files\\numbers.txt");
Console.WriteLine("Time Taken in milliseconds: " + stopWatch.ElapsedMilliseconds);
Console.ReadLine();

 void saveFile(HashSet<int> hashSet, string fileName)
{
    StringBuilder stringBuilder = new StringBuilder();

    foreach (var element in hashSet)
    {
        stringBuilder.Append(element);
        stringBuilder.Append("\n\r");
    }

    string text = stringBuilder.ToString().Trim();
    File.WriteAllText(fileName, text);
}