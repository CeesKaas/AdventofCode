using Utilities;

namespace AoC2021;

public class DayTemplate
{
    private readonly IInputFetcher _inputFetcher;
    public DayTemplate(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal void Start()
    {
        Console.WriteLine($"Day Template part 1 answer: {Part1()}");
        Console.WriteLine($"Day Template part 2 answer: {Part2()}");
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
