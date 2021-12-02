using AoC2021.Entities;
using Utilities;

namespace AoC2021.Days;

public class Day2
{
    private readonly IInputFetcher _inputFetcher;

    public Day2(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 2 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 2 part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1()
    {
        var commands = await _inputFetcher.GetTransformedSplitInputForDay(2, SubmarineCommandParser.Parse);
        var sub = new Submarine();
        foreach (var command in commands)
        {
            sub.Execute(command);
        }
        return sub.Horizontal * sub.Depth;
    }

    public async Task<int> Part2()
    {
        return 0;
    }
}
