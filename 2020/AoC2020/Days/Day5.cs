using System.Collections;
using Utilities;

namespace AoC2020.Days;

public class Day5
{
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
        var input = (await _inputFetcher.GetTransformedSplitInputForDay(5, GetSeatId)).ToArray();
        return input.Max();
    }

    public int GetSeatId(string input)
    {
        //var row = new BitArray(input[0..6].Select(c => c == 'B').ToArray()).ToInt();

        //var column = new BitArray(input[7..9].Select(c => c == 'R').ToArray()).ToInt();

        return new BitArray(input.Select(c => c == 'B' || c == 'R').Reverse().ToArray()).ToInt();
    }

    public async Task<int> Part2()
    {
        var input = (await _inputFetcher.GetTransformedSplitInputForDay(5, GetSeatId)).OrderBy(_ => _).ToArray();

        for (int i = 0; i < input.Length - 1; i++)
        {
            if (input[i] + 2 == input[i + 1])
                return input[i] + 1;
        }

        return 0;
    }
}
