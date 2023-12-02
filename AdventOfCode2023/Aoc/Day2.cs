using AdventOfCode2023.Interfaces;

namespace AdventOfCode2023.Aoc;

public class Day2 : IDay
{
    private struct GameSet
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
    }

    private struct Game
    {
        public int Id { get; set; }
        public List<GameSet> Sets { get; set; }
    }

    private GameSet ParseSet(string str)
    {
        // 2 red, 2 green
        var parts = str.Split(',').Select(s => s.Trim());

        var set = new GameSet();
        foreach (var part in parts)
        {
            var cube = part.Split(' ');
            var count = int.Parse(cube[0]);
            var color = cube[1];

            switch (color)
            {
                case "red":
                    set.Red = count;
                    break;
                case "green":
                    set.Green = count;
                    break;
                case "blue":
                    set.Blue = count;
                    break;
            }
        }

        return set;
    }

    private Game ParseLineToGame(string line)
    {
        try
        {
            // Game 1: 2 red, 2 green; 6 red, 3 green; 2 red, 1 green, 2 blue; 1 red
            var parts = line.Trim().Split(':');

            var id = int.Parse(parts[0].Split(' ')[1]);

            var setParts = parts[1].Split(';');
            var sets = new List<GameSet>();
            for (var i = 0; i < setParts.Length; i++)
            {
                sets.Add(ParseSet(setParts[i]));
            }

            return new Game
            {
                Id = id,
                Sets = sets
            };
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private int SolvePart1()
    {
        // 找出所有符合條件的 game id 然後算總和
        // 每場 game 的 set 都不能超過 target
        const int targetRed = 12;
        const int targetGreen = 13;
        const int targetBlue = 14;

        var lines = DataLoader.LoadToLines("data/Day2.txt");

        var games = new List<Game>();
        foreach (var line in lines)
        {
            games.Add(ParseLineToGame(line));
        }

        var sumOfValidGames = games.Where(g =>
            {
                return !g.Sets.Any(gs => gs.Red > targetRed || gs.Green > targetGreen || gs.Blue > targetBlue);
            })
            .Sum(g => g.Id);

        return sumOfValidGames;
    }

    private int GetPowerNumber(Game game)
    {
        var minRed = game.Sets.Max(s => s.Red);
        var minGreen = game.Sets.Max(s => s.Green);
        var minBlue = game.Sets.Max(s => s.Blue);

        return minRed * minGreen * minBlue;
    }

    private int SolvePart2()
    {
        // 計算每場 game 所需要的最大各色 cube 數量，並將其相乘後為該 game 的 power number
        // 計算所有 game 的 power number 總和
        var lines = DataLoader.LoadToLines("data/Day2.txt");

        var games = new List<Game>();
        foreach (var line in lines)
        {
            games.Add(ParseLineToGame(line));
        }

        var sumOfPowerNumbers = games.Sum(GetPowerNumber);

        return sumOfPowerNumbers;
    }

    public object Solve()
    {
        // return SolvePart1();
        return SolvePart2();
    }
}