namespace Utility;

public static class File
{
    public static uint[] GetValuesFromFile(string filename)
    {
        return GetTextFileValues(filename).Select(l => uint.Parse(l)).ToArray();
    }

    public static IEnumerable<string> GetTextFileValues(string filename)
    {
        foreach (var line in System.IO.File.ReadLines(filename)) yield return line;
    }
}