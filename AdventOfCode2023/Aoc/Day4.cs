using AdventOfCode2023.Interfaces;

namespace AdventOfCode2023.Aoc;

public class Day4 : IDay
{
    private IEnumerable<string> _input;

    private static string[] _splitSymbol = new string[] { ":", "|" };

    public Day4()
    {
        _input = File.ReadAllLines("data/day4_input.txt");
    }

    private object SolvePart1()
    {
        var sum = 0;
        foreach (var line in _input)
        {
            var blocks = line.Split(_splitSymbol, StringSplitOptions.None);
            var winningNumbers = blocks[1]
                .Trim()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);
            var myNumbers = blocks[2]
                .Trim()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            var wins = winningNumbers.Intersect(myNumbers).Count() - 1;
            sum += (int)Math.Pow(2, wins);
        }

        return sum;
    }

    private object SolvePart2()
    {
        var totalCard = 0;
        var cards = Enumerable.Repeat(1, _input.Count()).ToList();

        foreach (var line in _input)
        {
            var blocks = line.Split(_splitSymbol, StringSplitOptions.None);
            var card = blocks[0].Trim();
            var id = int.Parse(blocks[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)[1].Trim());
            var cardIdx = id - 1;

            var winningNumbers = blocks[1]
                .Trim()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);
            var myNumbers = blocks[2]
                .Trim()
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            var wins = winningNumbers.Intersect(myNumbers).Count();
            for (var idx = cardIdx + 1; idx <= cardIdx + wins; idx++)
            {
                cards[idx] += cards[cardIdx];
            }
        }

        totalCard = cards.Sum();
        return totalCard;
    }

    public object Solve(int part)
    {
        return part switch
        {
            1 => SolvePart1(),
            2 => SolvePart2(),
            _ => throw new ArgumentOutOfRangeException(nameof(part), part, null)
        };
    }
}