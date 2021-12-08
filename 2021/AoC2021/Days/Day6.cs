using Utilities;

namespace AoC2021.Days;

public class Day6
{
    private readonly IInputFetcher _inputFetcher;
    public Day6(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 6 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 6 part 2 answer: {await Part2()}");
    }

    public async Task<long> Part1()
    {
        var input = (await _inputFetcher.GetTransformedSplitInputForDay(6, new[] { ',' }, int.Parse)).ToList();
        return RunDays(input, 80);
    }

    public async Task<long> Part2()
    {
        var input = (await _inputFetcher.GetTransformedSplitInputForDay(6, new[] { ',' }, int.Parse)).ToList();

        return RunDays(input, 256);
    }

    private static long RunDays(List<int> input, int days)
    {
        long[] fishOnDay = new long[9];
        foreach (int dayIncycle in input)
        {
            fishOnDay[dayIncycle]++;
        }
        for (int i = 0; i < days; i++)
        {
            var fishRespawning = fishOnDay[0];
            for (int j = 1; j < 9; j++)
            {
                fishOnDay[j - 1] = fishOnDay[j];
            }
            fishOnDay[6] += fishRespawning;
            fishOnDay[8] = fishRespawning;
            /*var todaysItems = input.Count;
            for (int j = 0; j < todaysItems; j++)
            {
                var nextValue = input[j] - 1;
                if (nextValue == -1)
                {
                    input[j] = 6;
                    input.Add(8);
                }
                else
                {
                    input[j] = nextValue;
                }
            }*/
        }
        return fishOnDay.Sum();
    }
}
