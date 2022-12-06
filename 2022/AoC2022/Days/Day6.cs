using Utilities;

namespace AoC2022.Days;

public class Day6
{
    private readonly int Day = int.Parse(nameof(Day6).Replace("Day", ""));
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

    public async Task<int> Part1()
    {
        var input = await _inputFetcher.FetchInputAsString(Day);

        return FindStartOfPacket(input);
    }

    public async Task<int> Part2()
    {
        var input = await _inputFetcher.FetchInputAsString(Day);

        return FindStartOfMessage(input);
    }

    private int FindStartOfPacket(ReadOnlySpan<char> input) => FindIndexOfAfterMarker(input, 4);
    private int FindStartOfMessage(ReadOnlySpan<char> input) => FindIndexOfAfterMarker(input, 14);

    private int FindIndexOfAfterMarker(ReadOnlySpan<char> input, int markerLength)
    {
        var maybeMarkerStart = 0;
        for (int i = 1; i < input.Length; i++)
        {
            var maybeMarker = input[maybeMarkerStart..i];
            if (maybeMarker.Length == markerLength)
            {
                return i;
            }
            var currentChar = input[i];
            var currentIndexInMarker = maybeMarker.IndexOf(currentChar);
            if (currentIndexInMarker >= 0)
            {
                maybeMarkerStart += currentIndexInMarker + 1;
            }
        }
        return -1;
    }
}
