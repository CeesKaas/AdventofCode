using Utilities;

namespace AoC2020.Days;

public class DayTemplate
{
    private readonly IInputFetcher _inputFetcher;
    public DayTemplate(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day Template part 1 answer: {await Part1()}");
        Console.WriteLine($"Day Template part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1()
    {
        return 0;
    }

    public async Task<int> Part2()
    {
        return 0;
    }
}
