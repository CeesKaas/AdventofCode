using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Utilities;

namespace AoC2023.Days;

public class Day5
{
    private readonly int Day = int.Parse(nameof(Day5).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    private readonly Lazy<(long[] Seeds, Almanac Almanac)> _parsedSource;
    public Day5(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
        _parsedSource = new Lazy<(long[] Seeds, Almanac Almanac)>(() =>
        {
            var input = _inputFetcher.FetchInputAsStrings(Day).GetAwaiter().GetResult();
            var seeds = input[0].Split(' ')[1..].Select(long.Parse).ToArray();
            var almanac = new Almanac(input[1..]);
            return (seeds, almanac);
        });
    }

    internal void Start()
    {
        var sw = Stopwatch.StartNew();
        var prefill = _parsedSource.Value;
        Console.WriteLine($"parse took {sw.Elapsed} {prefill.Seeds}");
        sw.Restart();
        Console.WriteLine($"Day 5 part 1 answer: {Part1()}");
        Console.WriteLine($"part 1 took {sw.Elapsed}");
        sw.Restart();
        Console.WriteLine($"Day 5 part 2 answer: {Part2()}");
        Console.WriteLine($"part 2 took {sw.Elapsed}");
        sw.Restart();
        Console.WriteLine($"Day 5 part 1 answer: {Part1()}");
        Console.WriteLine($"part 1 took {sw.Elapsed}");
        sw.Restart();
        Console.WriteLine($"Day 5 part 2 answer: {Part2()}");
        Console.WriteLine($"part 2 took {sw.Elapsed}");
        sw.Restart();
        Console.WriteLine($"Day 5 part 1 answer: {Part1()}");
        Console.WriteLine($"part 1 took {sw.Elapsed}");
        sw.Restart();
        Console.WriteLine($"Day 5 part 2 answer: {Part2()}");
        Console.WriteLine($"part 2 took {sw.Elapsed}");
    }

    public long Part1()
    {
        var (seeds, almanac) = _parsedSource.Value;
        var locations = seeds.Select(almanac.MapSeedToLocation);
        return locations.Min();
    }

    public long Part2()
    {
        var (seedsRanges, almanac) = _parsedSource.Value;
        var mappedLocations = new ConcurrentBag<long>();
        var mapperRanges = new List<MapperRange>();
        for (int i = 0; i < seedsRanges.Length; i += 2)
        {
            long seed = seedsRanges[i];
            long numberOfSeeds = seedsRanges[i + 1];
            mapperRanges.Add(new MapperRange(seed, numberOfSeeds));
        }
        //Parallel.ForEach(mapperRanges, mapper =>
        foreach (var mapper in mapperRanges)
        {
            var mappedLocationRanges = almanac.MapSeedRangeToLocation(mapper);

            mappedLocations.Add(mappedLocationRanges.Min(r => r.Start));
        }//);
        return mappedLocations.Min();
    }

    private class Almanac
    {
        private Mapper[] _maps;

        public Almanac(string[] mapperLines)
        {
            Mapper? mapper = null;
            List<Mapper> maps = new();
            foreach (var mapperLine in mapperLines)
            {
                var line = mapperLine.Trim();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                if (line.EndsWith("map:"))
                {
                    mapper?.FillEmptySections();
                    mapper = new Mapper(line);
                    maps.Add(mapper);
                }
                else
                {
                    mapper?.AddLine(line);
                }
            }
            _maps = maps.ToArray();
        }

        public long MapSeedToLocation(long seed)
        {
            var current = seed;
            foreach (var map in _maps)
            {
                current = map.Convert(current);
            }
            return current;
        }

        public List<MapperRange> MapSeedRangeToLocation(MapperRange seedRange)
        {
            var current = new List<MapperRange> { seedRange };
            foreach (var map in _maps)
            {
                var newCurrent = new List<MapperRange>();
                //Console.WriteLine($"need to check {current.Count}");
                foreach (var item in current)
                {
                    newCurrent.AddRange(map.ConvertRange(item));
                }
                current = newCurrent;
            }
            return current;
        }
    }
    private class Mapper
    {
        private readonly List<MapperLine> _lines = new();
        private bool _filled;

        public Mapper(string nameLine)
        {
            Name = nameLine.Split(' ')[0];
        }

        public string Name { get; }

        public long Convert(long current)
        {
            var matchingLine = _lines.Find(l => l.Match(current));
            if (matchingLine == default)
            {
                return current;
            }
            //var offset = current - matchingLine.SourceStart;
            return current + matchingLine.Offset;
        }

        public List<MapperRange> ConvertRange(MapperRange input)
        {
            var result = new List<MapperRange>();

            var remainingRange = input;
            while (remainingRange.Length > 0)
            {
                var matchingLine = _lines.Find(l => l.Match(remainingRange.Start));
                if (matchingLine == default)
                {
                    // this means it's outside the mapped values
                    result.Add(remainingRange);
                    return result;
                }
                var offsetWithinRange = remainingRange.Start - matchingLine.SourceStart;
                var matchingLength = Math.Min(matchingLine.RangeLength - offsetWithinRange, remainingRange.Length);

                result.Add(new MapperRange(remainingRange.Start + matchingLine.Offset, matchingLength));
                remainingRange = new MapperRange(remainingRange.Start + matchingLength, remainingRange.Length - matchingLength);
                //Console.WriteLine(remainingRange);
            }

            return result;
        }

        public void FillEmptySections()
        {
            if (_filled) return;

            var existing = _lines.OrderBy(_ => _.SourceStart).ToList();

            long current = 0;
            foreach (var item in existing)
            {
                if (current < item.SourceStart)
                {
                    _lines.Add(new MapperLine(current, current, item.SourceStart, 0));
                }
                current = item.SourceStart + item.RangeLength + 1;
            }

            _filled = true;
        }

        public void AddLine(string line)
        {
            var items = line.Split(' ').Select(long.Parse).ToArray();
            _lines.Add(new(items[0], items[1], items[2], items[0] - items[1]));
        }
    }
    private readonly record struct MapperLine(long DestinationStart, long SourceStart, long RangeLength, long Offset)
    {
        public bool Match(long current)
        {
            return SourceStart <= current
                && current < SourceStart + RangeLength;
        }
    }
    private readonly record struct MapperRange(long Start, long Length);
}
