using AdventOfCode2023.Extensions;
using AdventOfCode2023.Interfaces;

namespace AdventOfCode2023.Aoc;

public class Day1 : IDay
{
    private int SolvePart1()
    {
        var lines = DataLoader.LoadToLines("data/day1_input.txt");

        var numbers = lines.Select(l =>
            {
                // find the first digit
                var firstDigit = l.First(char.IsDigit).ToInt();
                var lastDigit = l.Reverse().First(char.IsDigit).ToInt();

                return firstDigit * 10 + lastDigit;
            })
            .ToList();

        return numbers.Sum();
    }

    private static readonly List<string> DigitWord = new()
    {
        "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
    };

    private int Part2ParseFirstDigit(string str, bool isReverse)
    {
        try
        {
            var firstDigit = str.First(char.IsDigit);
            var indexOfFirstDigit = str.IndexOf(firstDigit);
            if (indexOfFirstDigit == -1)
            {
                indexOfFirstDigit = int.MaxValue;
            }

            var min = DigitWord
                .Select(d =>
                {
                    var digitWord = isReverse ? new string(d.Reverse().ToArray()) : d;

                    return new
                    {
                        Index = str.IndexOf(digitWord, StringComparison.Ordinal),
                        Digit = d
                    };
                })
                .Where(i => i.Index >= 0)
                .MinBy(i => i.Index);

            if (min == null || indexOfFirstDigit < min.Index)
            {
                return firstDigit.ToInt();
            }

            return DigitWord.IndexOf(min!.Digit);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private int Part2StringToInt(string str)
    {
        var firstDigit = Part2ParseFirstDigit(str, false);
        var lastDigit = Part2ParseFirstDigit(new string(str.Reverse().ToArray()), true);

        return firstDigit * 10 + lastDigit;
    }

    private int SolvePart2()
    {
        var lines = DataLoader.LoadToLines("data/Day1.txt");

        var numbers = lines.Select(Part2StringToInt)
            .ToList();

        return numbers.Sum();
    }

    public object Solve(int part)
    {
        return part switch
        {
            1 => SolvePart1(),
            2 => SolvePart2(),
            _ => throw new NotImplementedException()
        };
    }
}