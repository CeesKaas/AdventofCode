using Utilities;

namespace AoC2022.Days;

public class Day3
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
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        var prioritySum = 0;
        foreach (var line in input)
        {
            var half1 = line.Take(line.Length / 2);
            var half2 = line.Skip(line.Length / 2).Take(line.Length / 2);
            var overlap = half1.Intersect(half2).Single();

            prioritySum += char.IsLower(overlap) ? overlap - ('a' - 1) : 26 + (overlap - ('A' - 1));
        }
        return prioritySum;
    }

    public async Task<int> Part2()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        var prioritySum = 0;
        for (int i = 0; i < input.Length; i+=3)
        {
            var overlap = input[i].Intersect(input[i+1]).Intersect(input[i + 2]).Single();

            prioritySum += char.IsLower(overlap) ? overlap - ('a' - 1) : 26 + (overlap - ('A' - 1));
        }
        return prioritySum;
    }
}
