using Utilities;

namespace AoC2022.Days;

public class Day2
{
    private readonly int Day = int.Parse(nameof(Day2).Replace("Day", ""));
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
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        var score = 0;
        foreach (var round in input)
        {
            var opponent = round[0];
            var me = round[2];

            score += (me - 'X') + 1;

            var win = opponent == 'A' && me == 'Y' || opponent == 'B' && me == 'Z' || opponent == 'C' && me == 'X';
            var draw = opponent - 'A' == me - 'X';
            var loss = !win && !draw;

            if (win)
            {
                score += 6; 
            }
            if (draw)
            {
                score += 3;
            }
        }
        return score;
    }

    public async Task<int> Part2()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        var score = 0;
        foreach (var round in input)
        {
            var opponent = round[0];
            var me = round[2];

            var win = me == 'Z';
               
            var draw = me == 'Y';
            var loss = !win && !draw;

            score += win ? opponent switch { 'A' => 2, 'B' => 3, 'C' => 1, _=>throw new InvalidDataException() }:
                draw ? (opponent - 'A') + 1:
                opponent switch { 'A' => 3, 'B' => 1, 'C' => 2, _ => throw new InvalidDataException() };

            if (win)
            {
                score += 6;
            }
            if (draw)
            {
                score += 3;
            }
        }
        return score;
    }
}
