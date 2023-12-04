using System.Collections.Immutable;
using AdventOfCode2023.Interfaces;

namespace AdventOfCode2023.Aoc;

public class Day3 : IDay
{
    private class SymbolPoint
    {
        public char Symbol { get; set; }
        public int Line { get; set; }
        public int Index { get; set; }
    }

    private class NumberPoint
    {
        public int Line { get; set; }
        public int Value { get; set; }
        public int Start { get; set; }
        public int End { get; set; }

        public bool IsValidNumber { get; set; } = false;
    }

    private IEnumerable<string> _input;

    private char[] _splitSymbols = new[] { '.' };
    private char[] _allSymbols;
    private List<SymbolPoint> _symbolPoints;
    private List<NumberPoint> _numberPoints;

    public Day3()
    {
        _input = File.ReadAllLines("data/day3_input.txt");
        (_allSymbols, _symbolPoints) = FindAllSymbolsFormInput(_input);
        _numberPoints = FindNumberFormInput(_input);
    }

    private (char[], List<SymbolPoint> symbolPoints) FindAllSymbolsFormInput(IEnumerable<string> input)
    {
        var enumerable = input as string[] ?? input.ToArray();
        var lines = enumerable.ToImmutableList();

        var allSymbols = new List<char>();
        var symbolPoints = new List<SymbolPoint>();
        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            for (var cIdx = 0; cIdx < line.Length; cIdx++)
            {
                var c = line[cIdx];

                if (char.IsDigit(c)) continue;
                if (c == _splitSymbols[0]) continue;

                if (!allSymbols.Contains(c))
                {
                    allSymbols.Add(c);
                }

                symbolPoints.Add(new SymbolPoint
                {
                    Symbol = c,
                    Line = i,
                    Index = cIdx
                });
            }
        }

        return (allSymbols.ToArray(), symbolPoints);
    }

    private List<NumberPoint> FindNumberFormInput(IEnumerable<string> input)
    {
        var enumerable = input as string[] ?? input.ToArray();
        var lines = enumerable.ToImmutableList();

        // 要把所有符號跟 `.` 都當作 split 字元
        var allSplitSymbols = _allSymbols.Concat(_splitSymbols).ToArray();
        var numberPoints = new List<NumberPoint>();
        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            var blocks = line.Split(allSplitSymbols, StringSplitOptions.None);

            var accumulationShift = 0;
            for (var bIdx = 0; bIdx < blocks.Length; bIdx++)
            {
                var block = blocks[bIdx];

                // empty block
                if (block.Length == 0) continue;

                // not a number
                if (!int.TryParse(block, out var number)) continue;

                var start = accumulationShift + bIdx;
                var end = start + block.Length;

                numberPoints.Add(new NumberPoint
                {
                    Line = i,
                    Value = number,
                    Start = start,
                    End = end
                });

                accumulationShift += block.Length;
            }
        }

        return numberPoints;
    }

    private object SolvePart1()
    {
        var lines = _input.ToImmutableList();

        foreach (var numberPoint in _numberPoints)
        {
            var start = numberPoint.Start == 0 ? 0 : numberPoint.Start - 1;
            // 確認數字周圍是否有符號
            for (var i = -1; i <= 1; i++)
            {
                var checkLine = numberPoint.Line + i;
                if (checkLine < 0 || checkLine >= lines.Count) continue;

                var line = lines[checkLine];
                var end = numberPoint.End == line.Length ? line.Length : numberPoint.End + 1;

                numberPoint.IsValidNumber = line[start..end].IndexOfAny(_allSymbols) != -1;

                if (numberPoint.IsValidNumber) break;
            }
        }

        var sum = _numberPoints.Where(np => np.IsValidNumber)
            .Sum(np => np.Value);

        return sum;
    }

    private int FindOverlapping(int start1, int end1, int start2, int end2)
    {
        return Math.Max(0, Math.Min(end1, end2) - Math.Max(start1, start2) + 1);
    }

    private object SolvePart2()
    {
        var lines = _input.ToImmutableList();
        var lineLength = lines[0].Length;
        var lineCount = lines.Count;

        // 找出 * 符號周圍的數字
        var starSymbolPoints = _symbolPoints.Where(sp => sp.Symbol == '*').ToList();

        var sum = 0;
        foreach (var sp in starSymbolPoints)
        {
            var start = sp.Index == 0 ? 0 : sp.Index - 1;
            var end = sp.Index == lineLength ? lineLength : sp.Index + 1;

            var startLine = sp.Line == 0 ? sp.Line : sp.Line - 1;
            var endLine = sp.Line == lineCount ? lineCount : sp.Line + 1;

            // 判斷周圍的數字
            var nps = _numberPoints.Where(np =>
                    (np.Line >= startLine && np.Line <= endLine) &&
                    (FindOverlapping(start, end, np.Start, np.End - 1) > 0)
                )
                .ToList();

            if (nps.Count < 2) continue;

            sum += nps.Select(x => x.Value).Aggregate((x, y) => x * y);
        }

        return sum;
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