using AdventOfCode2023.Extensions;
using AdventOfCode2023.Interfaces;

namespace AdventOfCode2023.Aoc;

public class Day5 : IDay
{
    private class Block(long destination, long source, long range)
    {
        private readonly long source = source;
        private readonly long _sourceEnd = source + range - 1;
        private readonly long destination = destination;

        public bool Contains(long key)
        {
            return key >= source && key <= _sourceEnd;
        }

        public bool TryGet(long key, out long value)
        {
            if (key >= source && key <= _sourceEnd)
            {
                value = key - source + destination;
                return true;
            }

            value = -1;
            return false;
        }
    }

    private class Document
    {
        private readonly List<Block> _blocks = new();

        public void AddBlock(long source, long destination, long range)
        {
            _blocks.Add(new Block(source, destination, range));
        }

        public long Get(long key)
        {
            foreach (var block in _blocks)
            {
                if (block.TryGet(key, out var value))
                {
                    return value;
                }
            }

            return key;
        }
    }

    private const string TagSeed = "seeds:";
    private const string TagSoil = "seed-to-soil map:";
    private const string TagFertilizer = "soil-to-fertilizer map:";
    private const string TagWater = "fertilizer-to-water map:";
    private const string TagLight = "water-to-light map:";
    private const string TagTemperature = "light-to-temperature map:";
    private const string TagHumidity = "temperature-to-humidity map:";
    private const string TagLocation = "humidity-to-location map:";

    private IEnumerable<string> _input;

    private List<long> _seeds = new();
    private Document _seedToSoil = new();
    private Document _soilToFertilizer = new();
    private Document _fertilizerToWater = new();
    private Document _waterToLight = new();
    private Document _lightToTemperature = new();
    private Document _temperatureToHumidity = new();
    private Document _humidityToLocation = new();

    public Day5()
    {
        _input = File.ReadAllLines("data/day5_input.txt");
        ParseInput();
    }

    private void ParseInput()
    {
        _seeds.Clear();

        var currentTag = string.Empty;
        foreach (var rawLine in _input)
        {
            var line = rawLine.Trim();
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            if (line.StartsWith(TagSeed))
            {
                currentTag = TagSeed;
            }
            else if (line.StartsWith(TagSoil))
            {
                currentTag = TagSoil;
                continue;
            }
            else if (line.StartsWith(TagFertilizer))
            {
                currentTag = TagFertilizer;
                continue;
            }
            else if (line.StartsWith(TagWater))
            {
                currentTag = TagWater;
                continue;
            }
            else if (line.StartsWith(TagLight))
            {
                currentTag = TagLight;
                continue;
            }
            else if (line.StartsWith(TagTemperature))
            {
                currentTag = TagTemperature;
                continue;
            }
            else if (line.StartsWith(TagHumidity))
            {
                currentTag = TagHumidity;
                continue;
            }
            else if (line.StartsWith(TagLocation))
            {
                currentTag = TagLocation;
                continue;
            }

            switch (currentTag)
            {
                case TagSeed:
                    var seed = line.Split(":");
                    _seeds.AddRange(seed[1].Trim().Split(' ').Select(long.Parse));
                    currentTag = string.Empty;
                    break;

                case TagSoil:
                    var soil = line.ToImmutableLongList();
                    _seedToSoil.AddBlock(soil[0], soil[1], soil[2]);
                    break;

                case TagFertilizer:
                    var fertilizer = line.ToImmutableLongList();
                    _soilToFertilizer.AddBlock(fertilizer[0], fertilizer[1], fertilizer[2]);
                    break;

                case TagWater:
                    var water = line.ToImmutableLongList();
                    _fertilizerToWater.AddBlock(water[0], water[1], water[2]);
                    break;

                case TagLight:
                    var light = line.ToImmutableLongList();
                    _waterToLight.AddBlock(light[0], light[1], light[2]);
                    break;

                case TagTemperature:
                    var temperature = line.ToImmutableLongList();
                    _lightToTemperature.AddBlock(temperature[0], temperature[1], temperature[2]);
                    break;

                case TagHumidity:
                    var humidity = line.ToImmutableLongList();
                    _temperatureToHumidity.AddBlock(humidity[0], humidity[1], humidity[2]);
                    break;

                case TagLocation:
                    var location = line.ToImmutableLongList();
                    _humidityToLocation.AddBlock(location[0], location[1], location[2]);
                    break;

                default:
                    continue;
            }
        }
    }

    private object SolvePart1()
    {
        var locations = new List<long>();

        foreach (var seed in _seeds)
        {
            var soil = _seedToSoil.Get(seed);
            var fertilizer = _soilToFertilizer.Get(soil);
            var water = _fertilizerToWater.Get(fertilizer);
            var light = _waterToLight.Get(water);
            var temperature = _lightToTemperature.Get(light);
            var humidity = _temperatureToHumidity.Get(temperature);
            var location = _humidityToLocation.Get(humidity);

            locations.Add(location);
        }


        return locations.Min();
    }

    private object SolvePart2()
    {
        var locations = new List<long>();

        var chunks = _seeds.Chunk(2);

        foreach (var chunk in chunks)
        {
            var seedEnd = chunk[0] + chunk[1];
            for (long seed = chunk[0]; seed < seedEnd; seed++)
            {
                var soil = _seedToSoil.Get(seed);
                var fertilizer = _soilToFertilizer.Get(soil);
                var water = _fertilizerToWater.Get(fertilizer);
                var light = _waterToLight.Get(water);
                var temperature = _lightToTemperature.Get(light);
                var humidity = _temperatureToHumidity.Get(temperature);
                var location = _humidityToLocation.Get(humidity);

                locations.Add(location);
            }
        }

        return locations.Min();
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