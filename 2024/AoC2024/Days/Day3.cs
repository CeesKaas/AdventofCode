using System.Text.RegularExpressions;
using Utilities;

namespace AoC2024.Days;

public partial class Day3
{
    private readonly int Day = int.Parse(nameof(Day3).Replace("Day", ""));
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
        var input = await _inputFetcher.FetchInputAsString(Day);
        var regex = FetchMul();

        var matches = regex.Matches(input.Replace('\n', ' '));
        var sum = 0;
        for (int i = 0; i < matches[0].Groups[2].Captures.Count; i++)
        {
            sum += int.Parse(matches[0].Groups[2].Captures[i].Value) * int.Parse(matches[0].Groups[3].Captures[i].Value);
        }
        return sum;
    }

    public async Task<int> Part2()
    {
        var input = (await _inputFetcher.FetchInputAsString(Day)).AsSpan();

        var capturing = true;
        var capturingNumber = false;
        const string mul = "mul(";
        var startOfFirstNumber = -1;
        var endOfFirstNumber = -1;
        var startOfSecondNumber = -1;
        var endOfSecondNumber = -1;
        List<(int a, int b)> foundPairs = [];

        for (int i = 0; i < input.Length; i++)
        {
            if (capturingNumber)
            {
                if (char.IsDigit(input[i]))
                {
                    continue;
                }
                if (input[i] == ',')
                {
                    endOfFirstNumber = i;
                    startOfSecondNumber = i + 1;
                    continue;
                }
                if (input[i] == ')' && startOfSecondNumber != -1)
                {
                    endOfSecondNumber = i;
                    foundPairs.Add((int.Parse(input[startOfFirstNumber..endOfFirstNumber]), int.Parse(input[startOfSecondNumber..endOfSecondNumber])));
                }
                capturingNumber = false;
                startOfFirstNumber = -1;
                endOfFirstNumber = -1;
                startOfSecondNumber = -1;
                endOfSecondNumber = -1;
            }

            if (capturing)
            {
                if (input[i..].StartsWith(mul))
                {
                    capturingNumber = true;
                    i += mul.Length;
                    startOfFirstNumber = i;
                }
                if (input[i..].StartsWith("don't"))
                {
                    capturing = false;
                    capturingNumber = false;
                    continue;
                }
            }

            if (input[i..].StartsWith("do") && !input[i..].StartsWith("don't"))
            {
                capturing = true;
            }
        }

        return foundPairs.Aggregate(0,(s,p)=>s+(p.a*p.b));
    }

    [GeneratedRegex(@"(?:.*?mul\(((\d+),(\d+))\))*")]
    public static partial Regex FetchMul();
}
