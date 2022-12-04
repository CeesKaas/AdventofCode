using Utilities;

namespace AoC2022.Days;

public class Day4
{
    private readonly int Day = int.Parse(nameof(Day4).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    public Day4(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 4 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 4 part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1()
    {
        var input = await _inputFetcher.GetTransformedSplitInputForDay(Day,inputLine=>inputLine.Split(',').Select(_=>_.Split('-').Select(int.Parse).ToArray()).ToArray());

        return input.Count(_ => _[0][0] >= _[1][0] && _[0][1] <= _[1][1]
        || _[1][0] >= _[0][0] && _[1][1] <= _[0][1]);
    }

    public async Task<int> Part2()
    {
        var input = await _inputFetcher.GetTransformedSplitInputForDay(Day, inputLine => inputLine.Split(',').Select(_ => _.Split('-').Select(int.Parse).ToArray()).ToArray());

        return input.Count(_ => 
           _[0][0] >= _[1][0] && _[0][0] <= _[1][1]
        || _[0][1] >= _[1][0] && _[0][1] <= _[1][1]
        || _[1][0] >= _[0][0] && _[1][0] <= _[0][1]
        || _[1][1] >= _[0][0] && _[1][1] <= _[0][1]);
    }
}
