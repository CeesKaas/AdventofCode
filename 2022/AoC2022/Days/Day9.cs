using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;
using Utilities;

namespace AoC2022.Days;

public class Day9
{
    private readonly int Day = int.Parse(nameof(Day9).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    public Day9(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 9 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 9 part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        return MoveRope(input, 2);
    }

    private int MoveRope(string[] input, int ropeLength)
    {
        var rope = Enumerable.Repeat(new Point(0, 0), ropeLength).ToArray();
        HashSet<Point> tailLocations = new();
        foreach (var line in input)
        {
            var direction = line[0];
            var steps = int.Parse(line[2..]);

            for (int i = 0; i < steps; i++)
            {
                for (int j = 0; j < ropeLength; j++)
                {
                    rope[j] = j == 0
                        ? direction switch
                        {
                            'R' => rope[0] with { X = rope[0].X + 1 },
                            'L' => rope[0] with { X = rope[0].X - 1 },
                            'U' => rope[0] with { Y = rope[0].Y + 1 },
                            'D' => rope[0] with { Y = rope[0].Y - 1 }
                        }
                    : MoveToHead(rope[j], rope[j - 1]);
                }
                tailLocations.Add(rope.Last());
            }
        }

        return tailLocations.Count;
    }

    private Point MoveToHead(Point t, Point h)
    {
        if (Math.Abs(t.X - h.X) <= 1 && Math.Abs(t.Y - h.Y) <= 1) return t;
        return new Point(
            t.X == h.X ? t.X :
            t.X < h.X ? t.X + 1 :
            t.X - 1
            ,
            t.Y == h.Y ? t.Y :
            t.Y < h.Y ? t.Y + 1 :
            t.Y - 1);
    }

    public async Task<int> Part2()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        return MoveRope(input, 10);
    }
}

record struct Point(int X, int Y);
