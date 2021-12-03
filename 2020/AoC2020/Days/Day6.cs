using Utilities;

namespace AoC2020.Days;

public class Day6
{
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
        var input = await _inputFetcher.FetchInputAsString(6);
        var groups = input.Replace("\r\n", "\n").Split("\n\n");
        var uniqueGroupAnswers = groups.Select(list => list.Replace("\n", "").Distinct().ToArray()).ToArray();
        return uniqueGroupAnswers.Select(_ => _.Length).Sum();
    }

    public async Task<int> Part2()
    {
        var input = await _inputFetcher.FetchInputAsString(6);
        var groups = input.Trim().Replace("\r\n", "\n").Split("\n\n");
        var answer = 0;

        foreach (var group in groups)
        {
            var yesAnswers = group.Split('\n').Select(s => s.OrderBy(_ => _).ToArray());
            var allYesQuesions = yesAnswers.Aggregate(Enumerable.Range('a', 26).Select(num => (char)num), Enumerable.Intersect).ToArray();

            Console.WriteLine($"{new string('=', 30)}\r\n{string.Join("\r\n", yesAnswers.Select(_ => new string(_)))}\r\n result: {new string(allYesQuesions)} {allYesQuesions.Length}");

            answer += allYesQuesions.Count();
        }

        return answer;
    }
}
