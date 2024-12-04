using System.Collections.Concurrent;
using Utilities;

namespace AoC2024.Days;

public class Day1
{
    private readonly int Day = int.Parse(nameof(Day1).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    public Day1(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 1 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 1 part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1()
    {
        var inputLines = await _inputFetcher.FetchInputAsStrings(Day);
        var a = new List<int>(inputLines.Length);
        var b = new List<int>(inputLines.Length);
        foreach (var input in inputLines.Select(l => l.Split(" ", StringSplitOptions.RemoveEmptyEntries)))
        {
            a.Add(int.Parse(input[0]));
            b.Add(int.Parse(input[1]));
        }
        a.Sort();
        b.Sort();
        var diffSum = 0;
        for (var i = 0; i < a.Count; i++)
        {
            diffSum += Math.Max(a[i], b[i]) - Math.Min(a[i], b[i]);
        }

        return diffSum;
    }

    public async Task<int> Part2()
    {
        var inputLines = await _inputFetcher.FetchInputAsStrings(Day);
        var a = new List<int>();
        var b = new ConcurrentDictionary<int, int>();
        foreach (var input in inputLines.Select(l => l.Split(" ", StringSplitOptions.RemoveEmptyEntries)))
        {
            a.Add(int.Parse(input[0]));
            b.AddOrUpdate(int.Parse(input[1]), 1, (cur, value) => value + 1);
        }
        var diffSum = 0;
        for (var i = 0; i < a.Count; i++)
        {
            diffSum += a[i] * (b.TryGetValue(a[i], out var countInLeftList) ? countInLeftList : 0);
        }

        return diffSum;
    }
}
