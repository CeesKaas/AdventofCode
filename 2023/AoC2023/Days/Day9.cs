using Utilities;

namespace AoC2023.Days;

public class Day9
{
    private readonly int Day = int.Parse(nameof(Day9).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    public Day9(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 9 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 9 part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1()
    {
        var input = (await _inputFetcher.FetchInputAsStrings(Day)).Select(s=>s.Split(' ').Select(int.Parse).ToArray()).AsParallel();

        var nextItems = input.Select(_=>PredictNext(_));

        return nextItems.Sum();
    }

    private int PredictNext(int[] input, bool part2=false)
    {
        if (input.Length == 1)
        {
            return input[0];
        }
        var differences = new int[input.Length-1];
        for (var i = 1; i < input.Length; i++)
        {
            differences[i-1] = input[i]-input[i-1];
        }
        if (Array.TrueForAll(differences, EqualsZero))
        {
            return input.Last();
        }
        if (!part2)
        {
            return input.Last() + PredictNext(differences);
        }
        else
        {
            return input.First() - PredictNext(differences, true);
        }
    }

    private static bool EqualsZero(int a)=>a==0;

    public async Task<int> Part2()
    {
        string[] strings = (await _inputFetcher.FetchInputAsStrings(Day));
        var input = strings.Select(s => s.Split(' ',StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).AsParallel();

        var nextItems = input.Select(_ => PredictNext(_, true));

        return nextItems.Sum();
    }
}
