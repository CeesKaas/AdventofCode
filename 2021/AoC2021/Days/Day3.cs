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
            gamma[i] = GetMostCommonInPositionI(numbers, i);
        }

        var gammaNumeric = BitArrayToInt(gamma);

        var epsilon = (BitArray)gamma.Clone();
        epsilon.Xor(new BitArray(numbers[0].Length, true));
        var epsilonNumeric = BitArrayToInt(epsilon);
        return gammaNumeric * epsilonNumeric;
    }

    public async Task<int> Part2()
    {
        var numbers = (await _inputFetcher.GetTransformedSplitInputForDay(3, s =>
        {
            return new BitArray(s.Select(c => c == '1').Reverse().ToArray());
        })).ToArray();

        var o2Value = 0;
        var co2ScrubberValue = 0;
        var remainingNumbers = new List<BitArray>(numbers);

        for (var i = numbers[0].Length - 1; i >= 0; i--)
        {
            var mostCommonInPosition = GetMostCommonInPositionI(remainingNumbers, i);

            remainingNumbers.RemoveAll(c => c[i] != mostCommonInPosition);
            if (remainingNumbers.Count == 1)
            {
                o2Value = BitArrayToInt(remainingNumbers[0]);
            }
        }
        Console.WriteLine($"O2 value is {o2Value} ({Convert.ToString(o2Value, toBase: 2)})");

        remainingNumbers = new List<BitArray>(numbers);
        for (var i = numbers[0].Length - 1; i >= 0; i--)
        {
            var mostCommonInPosition = GetMostCommonInPositionI(remainingNumbers, i);

            remainingNumbers.RemoveAll(c => c[i] == mostCommonInPosition);
            if (remainingNumbers.Count == 1)
            {
                co2ScrubberValue = BitArrayToInt(remainingNumbers[0]);
            }
        }
        Console.WriteLine($"CO2Scrubber value is {co2ScrubberValue} ({Convert.ToString(co2ScrubberValue, toBase: 2)})");

        return o2Value * co2ScrubberValue;
    }

    private static bool GetMostCommonInPositionI(ICollection<BitArray> numbers, int i)
    {
        var oneCount = numbers.Count(n => n[i]);
        return oneCount >= numbers.Count / 2.0;
    }
    private static int BitArrayToInt(BitArray epsilon)
    {
        var epsilonNumericArray = new int[1];
        epsilon.CopyTo(epsilonNumericArray, 0);
        var epsilonNumeric = epsilonNumericArray[0];
        return epsilonNumeric;
    }
}
