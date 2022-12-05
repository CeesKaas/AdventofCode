using System.Text.RegularExpressions;
using Utilities;

namespace AoC2022.Days;

public class Day5
{
    private readonly int Day = int.Parse(nameof(Day5).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;

    private static readonly Regex _moveParser = new Regex("move (?<amount>[0-9]+) from (?<source>[0-9]+) to (?<destination>[0-9]+)", RegexOptions.Compiled);
    public Day5(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        var (moves, stackInputs) = await Parse();
        var stacks = stackInputs.Select(_ => new Stack<char>(_.Reverse<char>())).ToArray();
        Console.WriteLine($"Day 5 part 1 answer: {await Part1(stacks, moves)}");
        stacks = stackInputs.Select(_ => new Stack<char>(_.Reverse<char>())).ToArray();
        Console.WriteLine($"Day 5 part 2 answer: {await Part2(stacks, moves)}");
    }
    /*
01234567890123456
input:
    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2
*/
    private async Task<string> Part1(Stack<char>[] stacks, List<Move> moves)
    {
        foreach (var move in moves)
        {
            for (int k = 0; k < move.amount; k++)
            {
                stacks[move.dstIndex].Push(stacks[move.srcIndex].Pop());
            }
        }
        return new string(stacks.Select(_ => _.Peek()).ToArray());
    }

    private async Task<string> Part2(Stack<char>[] stacks, List<Move> moves)
    {
        foreach (var move in moves)
        {
            var movingStack = new Stack<char>();
            for (int k = 0; k < move.amount; k++)
            {
                movingStack.Push(stacks[move.srcIndex].Pop());
            }
            while(movingStack.TryPop(out var box))
            {
                stacks[move.dstIndex].Push(box);
            }
        }
        return new string(stacks.Select(_ => _.Peek()).ToArray());
    }

    public async Task<string> Part1()
    {
        var (moves, stackInputs) = await Parse();
        var stacks = stackInputs.Select(_ => new Stack<char>(_.Reverse<char>())).ToArray();
        return await Part1(stacks, moves);
    }

    public async Task<string> Part2()
    {
        var (moves, stackInputs) = await Parse();
        var stacks = stackInputs.Select(_ => new Stack<char>(_.Reverse<char>())).ToArray();
        return await Part2(stacks, moves);
    }
    private async Task<(List<Move> moves, List<char>[] stackInputs)> Parse()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        var firstLine = input.First();
        var noStacks = (int)Math.Ceiling(firstLine.Length / 4f);
        var stackInputs = Enumerable.Range(0, noStacks).Select(_ => new List<char>()).ToArray();
        var i = 0;
        for (; i < input.Length; i++)
        {
            string? line = input[i];
            if (line?.Contains('[') ?? false)
            {
                for (int j = 0; j < noStacks; j++)
                {
                    var box = line[j * 4 + 1];
                    if (char.IsLetter(box))
                    {
                        stackInputs[j].Add(box);
                    }
                }
            }
            else
            {
                break;
            }
        }
        // i is now at stack number row
        i += 1;
        // i is now at moves
        List<Move> moves = new();
        for (; i < input.Length; i++)
        {
            var match = _moveParser.Match(input[i]);
            moves.Add(new(
                int.Parse(match.Groups["amount"].ValueSpan),
                int.Parse(match.Groups["source"].ValueSpan) - 1,
                int.Parse(match.Groups["destination"].ValueSpan) - 1
                ));
        }
        return (moves, stackInputs);
    }
}

internal record struct Move(int amount, int srcIndex, int dstIndex);