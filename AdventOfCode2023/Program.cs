using AdventOfCode2023.Aoc;
using AdventOfCode2023.Interfaces;

try
{
    IDay day;

    // day = new Day1();
    day = new Day2();

    day.GetResult();
}
catch (Exception e)
{
    Console.WriteLine(e);
}