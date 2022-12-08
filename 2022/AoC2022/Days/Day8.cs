using Utilities;

namespace AoC2022.Days;

public class Day8
{
    private readonly int Day = int.Parse(nameof(Day8).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    public Day8(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 8 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 8 part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1()
    {
        var grid = (await _inputFetcher.GetTransformedSplitInputForDay(Day, line => line.Select(c => (byte)(c - '0')).ToArray())).ToArray();

        var seenTrees = new HashSet<(int i, int j)>(
            Enumerable.Range(0, grid[0].Length).Select(j => (0, j)) // top row
            .Concat(Enumerable.Range(0, grid[0].Length).Select(j => (grid.Length - 1, j))) //bottom row
            .Concat(Enumerable.Range(0, grid.Length).Select(i => (i, 0))) // left column
            .Concat(Enumerable.Range(0, grid.Length).Select(i => (i, grid[0].Length - 1)))); // right column

        for (int i = 1; i < grid.Length - 1; i++)
        {
            int rightOrBottomMost = grid[i].Length - 1;
            var fromLeftMax = grid[i][0];
            var fromRightMax = grid[i][rightOrBottomMost];
            var fromTopMax = grid[0][i];
            var fromBottomMax = grid[rightOrBottomMost][i];
            for (int j = 1; j < rightOrBottomMost; j++)
            {
                if (grid[i][j] > fromLeftMax)
                {
                    seenTrees.Add((i, j));
                    fromLeftMax = grid[i][j];
                }

                int fromRightJ = rightOrBottomMost - j;
                if (grid[i][fromRightJ] > fromRightMax)
                {
                    seenTrees.Add((i, fromRightJ));
                    fromRightMax = grid[i][fromRightJ];
                }

                if (grid[j][i] > fromTopMax)
                {
                    seenTrees.Add((j, i));
                    fromTopMax = grid[j][i];
                }

                int fromTopI = rightOrBottomMost - j;
                if (grid[fromTopI][i] > fromBottomMax)
                {
                    seenTrees.Add((fromTopI, i));
                    fromBottomMax = grid[fromTopI][i];
                }
            }
        }

        return seenTrees.Count;
    }

    public async Task<int> Part2()
    {
        var grid = (await _inputFetcher.GetTransformedSplitInputForDay(Day, line => line.Select(c => (byte)(c - '0')).ToArray())).ToArray();

        var maxScenicScore = 0;

        for (int i = 1; i < grid.Length - 1; i++)
        {
            int rightOrBottomMost = grid[i].Length - 1;
            for (int j = 1; j < rightOrBottomMost; j++)
            {
                var up = 0;
                var down = 0;
                var left = 0;
                var right = 0;

                for (int x = j-1; x >= 0; x--)
                {
                    left++;
                    if (grid[i][x] >= grid[i][j])
                    {
                        break;
                    }
                }

                for (int x = j+1; x < grid[i].Length; x++)
                {
                    right++;
                    if (grid[i][x] >= grid[i][j])
                    {
                        break;
                    }
                }


                for (int y = i-1; y >= 0; y--)
                {
                    up++;
                    if (grid[y][j] >= grid[i][j])
                    {
                        break;
                    }
                }

                for (int y = i+1; y < grid.Length; y++)
                {
                    down++;
                    if (grid[y][j] >= grid[i][j])
                    {
                        break;
                    }
                }


                maxScenicScore = Math.Max(maxScenicScore, up * left * right * down);
            }
        }

        return maxScenicScore;
    }
}
