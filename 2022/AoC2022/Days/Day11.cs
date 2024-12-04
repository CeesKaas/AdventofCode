using System.Runtime.CompilerServices;
using Utilities;

namespace AoC2022.Days;

/*
 
 Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3 
 
 */

public class Day11
{
    private readonly int Day = int.Parse(nameof(Day11).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;

    public Day11(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    private Monkey CreateMonkey(string input, int index)
    {
        return new Monkey(input, index);
    }

    private void PrintRound(int roundNumber, IEnumerable<Monkey> monkeys)
    {
        Console.WriteLine($"Round {roundNumber}\r\n{string.Join("\r\n", monkeys.Select((m, i) => $"Monkey {i} ({m.InvestigatedItems,6}): {string.Join(", ", m.Items)}"))}");
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 11 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 11 part 2 answer: {await Part2()}");
    }

    public async Task<long> Part1()
    {
        var input = await _inputFetcher.FetchInputAsString(Day);
        var monkeys = input.Replace("\r\n", "\n").Split("\n\n").Select(CreateMonkey).ToList();

        PrintRound(0, monkeys);
        const int rounds = 20;
        for (int i = 0; i < rounds; i++)
        {
            foreach (var monkey in monkeys)
            {
                monkey.PlayRoundPart1(monkeys);
            }

            PrintRound(i + 1, monkeys);
        }
        return monkeys.Select(m => m.InvestigatedItems).OrderByDescending(_ => _).Take(2).Aggregate((prev, cur) => prev * cur);
    }

    public async Task<long> Part2()
    {
        var input = await _inputFetcher.FetchInputAsString(Day);
        var monkeys = input.Replace("\r\n", "\n").Split("\n\n").Select(CreateMonkey).ToList();

        PrintRound(0, monkeys);
        const int rounds = 10000;
        for (int i = 0; i < rounds; i++)
        {
            foreach (var monkey in monkeys)
            {
                monkey.PlayRoundPart2(monkeys);
            }

            if (i % 1000 == 0)
            {
                PrintRound(i + 1, monkeys);
            }
        }
        return monkeys.Select(m => m.InvestigatedItems).OrderByDescending(_ => _).Take(2).Aggregate((prev, cur) => prev * cur);
    }
}
