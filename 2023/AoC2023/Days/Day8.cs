using System.Numerics;
using System.Xml.Linq;
using Utilities;

namespace AoC2023.Days;

public class Day8
{
    private readonly int Day = int.Parse(nameof(Day8).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    public Day8(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 8 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 8 part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        var route = input[0];
        // 01234567890123456
        // AAA = (BBB, CCC)
        var map = input[1..].ToDictionary(s => s[0..3], s => (Left: s[7..10], Right: s[12..15]));

        int steps = 0;
        string location = "AAA";
        while (location != "ZZZ")
        {
            (string Left, string Right) node = map[location];

            location = route[steps % route.Length] switch
            {
                'R' => node.Right,
                'L' => node.Left
            };
            steps++;
        }

        return steps;
    }

    public async Task<long> Part2()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        var route = input[0];
        // 01234567890123456
        // AAA = (BBB, CCC)
        var map = input[1..].ToDictionary(s => s[0..3], s => (Left: s[7..10], Right: s[12..15]));

        int steps = 0;
        string[] locations = map.Keys.Where(_ => _[2]=='A').ToArray();
        long?[] locationsFirstZ= Enumerable.Repeat((long?)null, locations.Length).ToArray();
        while (!(locations.All(_ => _[2]=='Z')))
        {
            Func< (string Left, string Right),string> moveNext = route[steps % route.Length] switch
            {
                'R' => (node) => node.Right,
                'L' => (node) => node.Left
            };
            for (int i = 0; i < locations.Length; i++)
            {
                locations[i] = moveNext(map[locations[i]]);
                if (locations[i][2]=='Z')
                {
                    locationsFirstZ[i] ??= steps + 1;
                }
            }
            steps++;
            if (!locationsFirstZ.Any(_=>_==null))
            {
                var lcm = locationsFirstZ.Aggregate(locationsFirstZ[0].Value, (a, b) => LCM(a, b.Value));
                Console.WriteLine($"found a route to a Z on every start {string.Join(", ",locationsFirstZ)}. their lcm is {lcm}");
                return lcm;
            }
        }

        return steps;
    }

    static long GCF(long a, long b)
    {
        while (b != 0)
        {
            (a, b) = (b, a % b);
        }
        return a;
    }

    static long LCM(long a, long b)
    {
        return (a / GCF(a, b)) * b;
    }
}
