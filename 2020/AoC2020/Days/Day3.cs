using System.Text;
using Utilities;

namespace AoC2020.Days;

internal record struct Heading(int Right, int Down);

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
        var input = (await _inputFetcher.GetTransformedSplitInputForDay(3, s => s.Select(c => c == '#').ToArray())).ToArray();

        return GetTreesHitOnHeading(input, new Heading { Right = 3, Down = 1 });
    }

    public async Task<long> Part2()
    {
        var input = (await _inputFetcher.GetTransformedSplitInputForDay(3, s => s.Select(c => c == '#').ToArray())).ToArray();
        long oneOnOne = GetTreesHitOnHeading(input, new Heading { Right = 1, Down = 1 });
        long threeOnOne = GetTreesHitOnHeading(input, new Heading { Right = 3, Down = 1 });
        long fiveOnOne = GetTreesHitOnHeading(input, new Heading { Right = 5, Down = 1 });
        long sevenOnOne = GetTreesHitOnHeading(input, new Heading { Right = 7, Down = 1 });
        long oneOnTwo = GetTreesHitOnHeading(input, new Heading { Right = 1, Down = 2 });
        return oneOnOne * threeOnOne * fiveOnOne * sevenOnOne * oneOnTwo;
    }
    private int GetTreesHitOnHeading(bool[][] input, Heading heading)
    {
        Console.WriteLine(new string('=', 20));
        Console.WriteLine(heading);
        var inputWidth = input[0].Length;
        var treesHit = 0;
        for (int i = 0, j = 0; i < input.Length; i += heading.Down, j = (j + heading.Right) % inputWidth)
        {
            if (input[i][j])
            {
                treesHit++;
            }
            //PrintMap(input, i, j);
        }
        return treesHit;
    }

    private void PrintMap(bool[][] input, int verticalPosition, int horizontalPosition)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[i].Length; j++)
            {
                if (i == verticalPosition && j == horizontalPosition)
                    sb.Append(input[i][j] ? "X" : "O");
                else
                    sb.Append(input[i][j] ? "#" : ".");
            }
            sb.AppendLine();
        }
        Console.WriteLine();
        Console.WriteLine(sb.ToString());
    }
}
