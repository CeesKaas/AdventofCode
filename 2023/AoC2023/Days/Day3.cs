using System.Text.RegularExpressions;
using Utilities;

namespace AoC2023.Days;

public partial class Day3
{
    private readonly int Day = int.Parse(nameof(Day3).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    public Day3(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        var (numbers, symbols) = await Parse();
        Console.WriteLine($"Day 3 part 1 answer: {Part1(numbers, symbols)}");
        Console.WriteLine($"Day 3 part 2 answer: {Part2(numbers, symbols)}");
    }

    private async Task<(List<(int Start, int Length, int Line, int Number)> numbers, List<(int Line, int Index, string Symbol)> symbols)> Parse()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        var numberRegex = NumberRegex();
        var symbolsRegex = SymbolRegex();

        var numbers = input.Select((l, i) => (Line: i, foundMatches: numberRegex.Matches(l))).SelectMany(_ => _.foundMatches.Select(m => (Start: m.Index, m.Length, _.Line, Number: int.Parse(m.Value)))).ToList();
        var symbols = input.Select((l, i) => (Line: i, Found: symbolsRegex.Matches(l))).SelectMany(_ => _.Found.Select(s => (_.Line, s.Index, Symbol: s.Value))).ToList();
        return (numbers, symbols);
    }

    public int Part1(List<(int Start, int Length, int Line, int Number)> numbers, List<(int Line, int Index, string Symbol)> symbols)
    {
        var validNumbers = numbers.Where(number => symbols.Any(s => s.Line >= number.Line - 1
                                                                 && s.Line <= number.Line + 1
                                                                 && s.Index >= number.Start - 1
                                                                 && s.Index <= number.Start + number.Length)).ToList();

        return validNumbers.Sum(_=>_.Number);
    }



    public int Part2(List<(int Start, int Length, int Line, int Number)> numbers, List<(int Line, int Index, string Symbol)> symbols)
    {
        var validNumbers = symbols.Where(s=>s.Symbol == "*").Select(s =>
        {
            var matching = numbers.Where(number => s.Line >= number.Line - 1
                           && s.Line <= number.Line + 1
                           && s.Index >= number.Start - 1
                           && s.Index <= number.Start + number.Length).ToList();
            return matching.Count == 2 ? matching[0].Number * matching[1].Number : 0;
        }).ToList();

        return validNumbers.Sum();
    }
    public async Task<int> Part1()
    {
        var value = await Parse();
        return Part1(value.numbers, value.symbols);
    }

    public async Task<int> Part2()
    {
        var value = await Parse();
        return Part2(value.numbers, value.symbols);
    }

    [GeneratedRegex("[^0-9.]", RegexOptions.Compiled)]
    private static partial Regex SymbolRegex();
    [GeneratedRegex("[0-9]+", RegexOptions.Compiled)]
    private static partial Regex NumberRegex();
}
