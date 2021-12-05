using Utilities;

namespace AoC2020.Days;

public class Day8
{
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
        (string op, int arg)[] input = (await _inputFetcher.GetTransformedSplitInputForDay(8, s =>
        {
            var parts = s.Split(' ', StringSplitOptions.TrimEntries);
            return (parts[0], int.Parse(parts[1]));
        })).ToArray();

        var accumulator = 0;
        var stackTrace = new HashSet<int>();

        for (int i = 0; i < input.Length; i++)
        {
            switch (input[i].op)
            {
                case "nop": break;
                case "acc":
                    accumulator += input[i].arg;
                    break;
                case "jmp":
                    i += input[i].arg;
                    i--;
                    break;
                default:
                    throw new InvalidOperationException();
            }
            if (!stackTrace.Add(i))
            {
                return accumulator;
            }
        }

        return 0;
    }

    public async Task<int> Part2()
    {
        (string op, int arg)[] input = (await _inputFetcher.GetTransformedSplitInputForDay(8, s =>
        {
            var parts = s.Split(' ', StringSplitOptions.TrimEntries);
            return (parts[0], int.Parse(parts[1]));
        })).ToArray();

        var accumulator = 0;
        var stackTrace = new Stack<int>();
        input[413].op = "nop";
        for (int i = 0; i < input.Length; i++)
        {
            if (stackTrace.Contains(i))
            {
                return accumulator;
            }
            stackTrace.Push(i);
            switch (input[i].op)
            {
                case "nop": break;
                case "acc":
                    accumulator += input[i].arg;
                    break;
                case "jmp":
                    i += input[i].arg;
                    i--;
                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        return accumulator;
    }
}
