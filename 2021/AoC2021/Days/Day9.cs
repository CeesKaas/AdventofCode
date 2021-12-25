using Utilities;

namespace AoC2021.Days;

public class Day9
{
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
        var input = (await _inputFetcher.GetTransformedSplitInputForDay(9, s => s.Select(c => c - '0').ToArray())).ToArray();

        List<(int row, int column)> lowPoints = new();

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (i > 0)
                {
                    if (input[i][j] >= input[i - 1][j])
                    {
                        continue;
                    }
                }

                if (j > 0)
                {
                    if (input[i][j] >= input[i][j - 1])
                    {
                        continue;
                    }
                }

                if (i < input.Length - 1)
                {
                    if (input[i][j] >= input[i + 1][j])
                    {
                        continue;
                    }
                }

                if (j < input[i].Length - 1)
                {
                    if (input[i][j] >= input[i][j + 1])
                    {
                        continue;
                    }
                }
                Console.WriteLine($"low point {input[i][j]} at {i},{j}");
                lowPoints.Add((i, j));
            }
        }

        return lowPoints.Sum(_ => input[_.row][_.column] + 1);
    }

    public async Task<int> Part2()
    {
        return 0;
    }
}
