namespace AdventOfCode2023;

public static class DataLoader
{
    public static IEnumerable<string> LoadToLines(string path)
    {
        return File.ReadAllLines(path);
    }
}