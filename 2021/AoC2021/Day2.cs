using Utilities;

namespace AoC2021;

public class Day2
{
    private readonly IInputFetcher _inputFetcher;
    public Day2(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal void Start()
    {
        Console.WriteLine($"Day 2 part 1 answer: {Part1()}");
        Console.WriteLine($"Day 2 part 2 answer: {Part2()}");
    }

    public int Part1()
    {
        return 0;
    }

    public int Part2()
    {
        return 0;
    }
}
