using System.Collections;
using System.Linq;
using Utilities;

namespace AoC2021.Days;

public class Day3
{
    private readonly IInputFetcher _inputFetcher;
    public Day3(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 3 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 3 part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1()
    {
        var numbers = (await _inputFetcher.GetTransformedSplitInputForDay(3, s =>
        {
            return new BitArray(s.Select(c => c == '1').Reverse().ToArray());
        })).ToArray();
        var gamma = new BitArray(new bool[numbers[0].Length]);

        for (var i = 0; i < numbers[0].Length; i++)
        {
            var oneCount = numbers.Count(n => n[i]);
            if (oneCount > numbers.Length / 2)
            {
                gamma[i] = true;
            }
        }

        var gammaNumericArray = new int[1];
        gamma.CopyTo(gammaNumericArray, 0);
        var gammaNumeric = gammaNumericArray[0];

        var epsilon = (BitArray) gamma.Clone();
        epsilon.Xor(new BitArray(numbers[0].Length, true));
        var epsilonNumericArray = new int[1];
        epsilon.CopyTo(epsilonNumericArray, 0);
        var epsilonNumeric = epsilonNumericArray[0];
        return gammaNumeric * epsilonNumeric;
    }

    public async Task<int> Part2()
    {
        return 0;
    }
}
