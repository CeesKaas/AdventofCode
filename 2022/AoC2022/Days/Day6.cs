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
        var marker = new List<char>(markerLength-1);
        for (int i = 0; i < input.Length; i++)
        {
            var currentIndexInMarker = marker.IndexOf(input[i]);
            if (currentIndexInMarker >= 0)
            {
                do
                {
                    marker.RemoveAt(0);
                    currentIndexInMarker--;
                }
                while (currentIndexInMarker >= 0);
                marker.Add(input[i]);
            }
            else if (marker.Count < markerLength - 1)
            {
                marker.Add(input[i]);
            }
            else
            {
                return i + 1;
            }
        }
        return -1;
    }
}
