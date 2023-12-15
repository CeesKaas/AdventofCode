using Utilities;

namespace AoC2023.Days;

public class Day1
{
    private readonly int Day = int.Parse(nameof(Day1).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    private string[] numberWords =
    [
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine"
    ];
    public Day1(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 1 part 1 answer: {Part1(await ParseSimple())}");
        Console.WriteLine($"Day 1 part 2 answer: {Part2(await Parse())}");
    }

    public async Task<int[]> ParseSimple()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);

        return input.Select(line =>
        {
            int? firstDigit = null;
            int? lastDigit = null;
            for (var i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    firstDigit ??= line[i] - '0';
                }
                if (char.IsDigit(line[line.Length - i - 1]))
                {
                    lastDigit ??= line[line.Length - i - 1] - '0';
                }
                if (firstDigit is not null && lastDigit is not null)
                {
                    break;
                }
            }
            return (firstDigit * 10 + lastDigit) ?? 0;
        }).ToArray();
    }
    public async Task<int[]> Parse()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);

        return input.Select(line =>
        {
            int? firstDigit = null;
            int? lastDigit = null;
            for (var i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    firstDigit ??= line[i] - '0';
                }
                if (char.IsDigit(line[line.Length - i - 1]))
                {
                    lastDigit ??= line[line.Length - i - 1] - '0';
                }
                for (int number = 1; number <= numberWords.Length; number++)
                {
                    string? numberWord = numberWords[number - 1];
                    if (line[i..].StartsWith(numberWord))
                    {
                        firstDigit ??= number;
                    }
                    if (line[^(i+1)..].StartsWith(numberWord))
                    {
                        lastDigit ??= number;
                    }
                }
                if (firstDigit is not null && lastDigit is not null)
                {
                    break;
                }
            }
            return (firstDigit * 10 + lastDigit) ?? 0;
        }).ToArray();
    }

    public int Part1(int[] calibrationLines)
    {
        return calibrationLines.Sum();
    }

    public int Part2(int[] calibrationLines)
    {
        return calibrationLines.Sum();
    }

    public async Task<int> Part1()
    {
        return Part1(await ParseSimple());
    }

    public async Task<int> Part2()
    {
        return Part2(await Parse());
    }
}
