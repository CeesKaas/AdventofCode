using Utilities;

namespace AoC2020.Days;

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

        for (var i = 0; i < input.Count - 1; i++)
        {
            for (var j = i + 1; j < input.Count; j++)
            {
                if (input[i] + input[j] == 2020)
                {
                    Console.WriteLine($"found {input[i]} and {input[j]}");
                    return input[i] * input[j];
                }
            }
        }
        throw new NotImplementedException();
    }

    public async Task<int> Part2()
    {
        var input = (await _inputFetcher.GetTransformedSplitInputForDay(1, int.Parse)).ToList();

        for (var i = 0; i < input.Count - 2; i++)
        {
            for (var j = i + 1; j < input.Count - 1; j++)
            {
                for (var k = j + 1; k < input.Count; k++)
                {
                    if (input[i] + input[j] + input[k] == 2020)
                    {
                        Console.WriteLine($"found {input[i]} and {input[j]} and {input[k]}");
                        return input[i] * input[j] * input[k];
                    }
                }
            }
        }
        throw new NotImplementedException();
    }
}
