using Utilities;

namespace AoC2021.Days;

public class Day7
{
    private readonly IInputFetcher _inputFetcher;
    public Day7(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 7 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 7 part 2 answer: {await Part2()}");
    }

    public async Task<double> Part1()
    {
        var input = (await _inputFetcher.GetTransformedSplitInputForDay(7, new[] { ',' }, double.Parse)).OrderBy(_ => _).ToArray();
        var median = Math.Round(MathNet.Numerics.Statistics.SortedArrayStatistics.Median(input), 0);
        return input.Sum(_ => _ > median ? _ - median : median - _);
    }

    public async Task<int> Part2()
    {
        var input = (await _inputFetcher.GetTransformedSplitInputForDay(7, new[] { ',' }, double.Parse)).OrderBy(_ => _).ToArray();
        var mean = (int)Math.Round(input.Average(), 0);
        var min = int.MaxValue;
        for (int i = mean - 5; i <= mean + 5; i++)
        {
            min = Math.Min(min, input.Sum(_ => Enumerable.Range(1, (int)(_ > i ? _ - i : i - _)).Sum()));
        }
        return min;
    }
}
