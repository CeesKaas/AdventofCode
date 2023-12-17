using Utilities;

namespace AoC2023.Days;

public class Day4
{
    private readonly int Day = int.Parse(nameof(Day4).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    public Day4(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 4 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 4 part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        var totalPoints = 0;
        foreach ( var item in input )
        {
            var numbers = item.Split([':', '|']);
            var card = numbers[0];
            var winningNumbers = numbers[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var drawnNumbers = numbers[2].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var winningDrawn = drawnNumbers.Intersect(winningNumbers).Count();
            if (winningDrawn > 0)
            {
                totalPoints += (int) Math.Pow(2, winningDrawn-1);
            }
        }
        return totalPoints;
    }

    public async Task<int> Part2()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        var totalPointsPerCard = Enumerable.Repeat(1,input.Length).ToList();
        for (int i = 0; i < input.Length; i++)
        {
            string? item = input[i];
            var numbers = item.Split([':', '|']);
            var card = numbers[0];
            var winningNumbers = numbers[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var drawnNumbers = numbers[2].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var winningDrawn = drawnNumbers.Intersect(winningNumbers).Count();
            for (int j = 1; j <= winningDrawn && i+j < input.Length; j++)
            {
                totalPointsPerCard[i + j] += totalPointsPerCard[i];
            }
        }
        return totalPointsPerCard.Sum();
    }
}
