using Utilities;

namespace AoC2021.Days;

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
        var input = (await _inputFetcher.GetTransformedSplitInputForDay(8, s => s
        .Split("|", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        [1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))).SelectMany(_ => _);

        int[] wantedNumberOfActiveSegments = new int[] { 2, 3, 4, 7 };
        return input.GroupBy(_ => _.Length).Where(_ => wantedNumberOfActiveSegments.Contains(_.Key)).Sum(_ => _.Count());
    }

    public async Task<int> Part2()
    {
        ICollection<(string[] Examples, string[] ActualMeasurement)>? input = (await _inputFetcher.GetTransformedSplitInputForDay(8, s =>
        {
            string[] parts = s.Split("|", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            return (parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(_ => new string(_.OrderBy(c => c).ToArray())).ToArray(),
                    parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(_ => new string(_.OrderBy(c => c).ToArray())).ToArray());
        }));
        var accumulation = 0;
        foreach (var (examples, actualMeasurement) in input)
        {
            var translationDictionary = new Dictionary<string, int>();
            string oneInput = examples.Single(_ => _.Length == 2);
            string sevenInput = examples.Single(_ => _.Length == 3);
            translationDictionary[oneInput] = 1;
            translationDictionary[sevenInput] = 7;
            translationDictionary[examples.Single(_ => _.Length == 4)] = 4;
            translationDictionary[examples.Single(_ => _.Length == 7)] = 8;

            /*
             
               0:      1:      2:      3:      4:
                 aaaa    ....    aaaa    aaaa    ....
                b    c  .    c  .    c  .    c  b    c
                b    c  .    c  .    c  .    c  b    c
                 ....    ....    dddd    dddd    dddd
                e    f  .    f  e    .  .    f  .    f
                e    f  .    f  e    .  .    f  .    f
                 gggg    ....    gggg    gggg    ....

              5:      6:      7:      8:      9:
                 aaaa    aaaa    aaaa    aaaa    aaaa
                b    .  b    .  .    c  b    c  b    c
                b    .  b    .  .    c  b    c  b    c
                 dddd    dddd    ....    dddd    dddd
                .    f  e    f  .    f  e    f  .    f
                .    f  e    f  .    f  e    f  .    f
                 gggg    gggg    ....    gggg    gggg
                    0 1 2 3 4 5 6 7 8 9
                a : 0   2 3   5 6 7 8 9 8
                b : 0       4 5 6   8 9 6
                c : 0 1 2 3 4     7 8 9 8
                d :     2 3 4 5 6   8 9 7
                e : 0   2       6   8   4
                f : 0 1   3 4 5 6 7 8 9 9
                g : 0   2 3   5 6   8 9 7
                    6 2 5 5 4 5 6 3 7 6

            0 6 9 : 
             0 missing d 
             6 missing c 
             9 missing e

            2 3 5
             2 no B and F
             3 no B and E
             5 no C and E
             */
            var inputSignalCountsPerExample = examples.SelectMany(c => c).GroupBy(_ => _).ToDictionary(_ => _.Key, _ => _.Count());
            var fSegment = inputSignalCountsPerExample.Single(_ => _.Value == 9).Key;
            var cSegment = oneInput.Except(new[] { fSegment }).Single();
            var eSegment = inputSignalCountsPerExample.Single(_ => _.Value == 4).Key;

            var sixInput = examples.Where(_ => _.Length == 6).First(_ => !_.Contains(cSegment));
            var nineInput = examples.Where(_ => _.Length == 6).First(_ => !_.Contains(eSegment));
            var zeroInput = examples.Where(_ => _.Length == 6).First(_ => _ != sixInput && _ != nineInput);
            translationDictionary[zeroInput] = 0;
            translationDictionary[sixInput] = 6;
            translationDictionary[nineInput] = 9;

            var fiveInput = examples.Where(_ => _.Length == 5).First(_ => !_.Contains(cSegment) && !_.Contains(eSegment));
            var threeInput = examples.Where(_ => _.Length == 5).First(_ => !_.Contains(eSegment) && _ != fiveInput);
            var twoInput = examples.Where(_ => _.Length == 5).First(_ => _ != threeInput && _ != fiveInput);

            translationDictionary[fiveInput] = 5;
            translationDictionary[threeInput] = 3;
            translationDictionary[twoInput] = 2;

            accumulation += translationDictionary[actualMeasurement[0]] * 1000;
            accumulation += translationDictionary[actualMeasurement[1]] * 100;
            accumulation += translationDictionary[actualMeasurement[2]] * 10;
            accumulation += translationDictionary[actualMeasurement[3]] * 1;
        }
        return accumulation;
    }
}
