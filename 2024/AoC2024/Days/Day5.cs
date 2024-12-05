using Utilities;

namespace AoC2024.Days;

public class Day5
{
    private readonly int Day = int.Parse(nameof(Day5).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    public Day5(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 5 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 5 part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        return 0;
    }

    public async Task<int> Part2()
    {
        return 0;
    }
}
