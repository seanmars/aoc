namespace AdventOfCode2023.Extensions;

public static class CharExtension
{
    public static int ToInt(this char c)
    {
        return int.Parse(c.ToString());
    }
}