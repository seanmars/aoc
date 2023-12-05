using System.Collections.Immutable;

namespace AdventOfCode2023.Extensions;

public static class StringExtension
{
    public static IEnumerable<string> SplitBySpace(this string input)
    {
        return input.Split(' ', StringSplitOptions.TrimEntries);
    }

    public static ImmutableList<int> ToImmutableIntList(this string input)
    {
        return input.SplitBySpace().Select(int.Parse).ToImmutableList();
    }

    public static ImmutableList<long> ToImmutableLongList(this string input)
    {
        return input.SplitBySpace().Select(long.Parse).ToImmutableList();
    }
}