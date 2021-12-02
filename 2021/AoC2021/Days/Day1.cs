using Utilities;

namespace AoC2021.Days;

public class Day1
{
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
        var input = (await _inputFetcher.GetTransformedSplitInputForDay(1, int.Parse)).ToList();
        var count = 0;
        for (var i = 1; i < input.Count; i++)
        {
            if (input[i] > input[i - 1])
                count++;
        }
        return count;
    }

    public async Task<int> Part2()
    {
        var input = (await _inputFetcher.GetTransformedSplitInputForDay(1, int.Parse)).ToList();
        var sumsByThree = SumsByThree(input).ToList();
        var count = 0;
        for (var i = 1; i < sumsByThree.Count; i++)
        {
            if (sumsByThree[i] > sumsByThree[i - 1])
                count++;
        }
        return count;
    }

    private IEnumerable<int> SumsByThree(List<int> input)
    {
        for (var i = 0; i < input.Count - 2; i++)
        {
            yield return input[i] + input[i + 1] + input[i + 2];
        }
    }
}
