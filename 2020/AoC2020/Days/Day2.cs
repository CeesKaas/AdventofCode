using System.Text.RegularExpressions;
using Utilities;

namespace AoC2020.Days;

public class Day2
{
    private readonly IInputFetcher _inputFetcher;
    public Day2(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 2 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 2 part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1()
    {
        var input = (await _inputFetcher.GetTransformedSplitInputForDay(2, PasswordPolicy.Parse)).ToList();
        return input.Count(_ => _.WasMatch());
    }

    public async Task<int> Part2()
    {
        var input = (await _inputFetcher.GetTransformedSplitInputForDay(2, PasswordPolicy2.Parse)).ToList();
        return input.Count(_ => _.WasMatch());
    }
}

public class PasswordPolicy
{
    public int MinimumOccurences { get; private init; }
    public int MaximumOccurences { get; private init; }
    public char RequiredChar { get; private init; }
    public string TriedPassword { get; private init; }

    public bool WasMatch()
    {
        var requiredCharFoundNTimes = TriedPassword.Count(c => c == RequiredChar);
        return requiredCharFoundNTimes >= MinimumOccurences && requiredCharFoundNTimes <= MaximumOccurences;
    }
    public static PasswordPolicy Parse(string input)
    {
        var regex = new Regex("([1-9]+[0-9]*)-([1-9]+[0-9]*) ([a-z]): ([a-z]*)");
        var match = regex.Match(input);
        return match.Success ? new PasswordPolicy
        {
            MinimumOccurences = int.Parse(match.Groups[1].Value),
            MaximumOccurences = int.Parse(match.Groups[2].Value),
            RequiredChar = match.Groups[3].Value[0],
            TriedPassword = match.Groups[4].Value
        } : throw new ArgumentOutOfRangeException(nameof(input), $"input did not match expected format (input: {input})");
    }
}
public class PasswordPolicy2
{
    public int FirstAllowedIndex { get; private init; }
    public int SecondAllowedIndex { get; private init; }
    public char RequiredChar { get; private init; }
    public string TriedPassword { get; private init; }

    public bool WasMatch()
    {
        return TriedPassword[FirstAllowedIndex - 1] == RequiredChar ^ TriedPassword[SecondAllowedIndex - 1] == RequiredChar;
    }
    public static PasswordPolicy2 Parse(string input)
    {
        var regex = new Regex("([1-9]+[0-9]*)-([1-9]+[0-9]*) ([a-z]): ([a-z]*)");
        var match = regex.Match(input);
        return match.Success ? new PasswordPolicy2
        {
            FirstAllowedIndex = int.Parse(match.Groups[1].Value),
            SecondAllowedIndex = int.Parse(match.Groups[2].Value),
            RequiredChar = match.Groups[3].Value[0],
            TriedPassword = match.Groups[4].Value
        } : throw new ArgumentOutOfRangeException(nameof(input), $"input did not match expected format (input: {input})");
    }
}