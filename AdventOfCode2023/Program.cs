using AdventOfCode2023.Aoc;
using AdventOfCode2023.Interfaces;

try
{
    IDay day;

    // day = new Day1();
    // day = new Day2();
    // day = new Day3();
    // day = new Day4();
    day = new Day5();

    day.GetResult(2);
}
catch (Exception e)
{
    Console.WriteLine(e);
}