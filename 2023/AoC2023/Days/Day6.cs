using Utilities;

namespace AoC2023.Days;

public class Day6
{
    private readonly int Day = int.Parse(nameof(Day6).Replace("Day", ""));
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
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        var timesAvailable = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1..].Select(int.Parse).ToArray();
        var existingRecords = input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1..].Select(int.Parse).ToArray();
        var accumulate = 1;
        for (var i = 0; i < timesAvailable.Length; i++)
        {
            int msToWaitAtStart = 1;
            while (!BeatsRecord(msToWaitAtStart, timesAvailable[i], existingRecords[i]))
            {
                msToWaitAtStart++;
            }
            int msToWaitAtEnd = timesAvailable[i] - 1;
            do
            {
                msToWaitAtEnd--;
            } while (!BeatsRecord(msToWaitAtEnd, timesAvailable[i], existingRecords[i]));

            int numberOfWaysToBeatRecord = (msToWaitAtEnd - msToWaitAtStart) + 1;
            Console.WriteLine($"{numberOfWaysToBeatRecord} ways to beat the race (wait at least {msToWaitAtStart} and at most {msToWaitAtEnd})");
            accumulate *= numberOfWaysToBeatRecord;
        }

        return accumulate;
    }

    private bool BeatsRecord(int msToWaitAtStart, int timeAvailable, int recordToBeat)
    {
        return msToWaitAtStart * (timeAvailable - msToWaitAtStart) > recordToBeat;
    }
    private bool BeatsRecord(long msToWaitAtStart, long timeAvailable, long recordToBeat)
    {
        return msToWaitAtStart * (timeAvailable - msToWaitAtStart) > recordToBeat;
    }

    public async Task<long> Part2()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        var timeAvailable = long.Parse(string.Join("",input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1..]));
        var existingRecord = long.Parse(string.Join("", input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1..]));
        long accumulate = 1;
        long msToWaitAtStart = 1;
        while (!BeatsRecord(msToWaitAtStart, timeAvailable, existingRecord))
        {
            msToWaitAtStart++;
        }
        long msToWaitAtEnd = timeAvailable - 1;
        do
        {
            msToWaitAtEnd--;
        } while (!BeatsRecord(msToWaitAtEnd, timeAvailable, existingRecord));

        long numberOfWaysToBeatRecord = (msToWaitAtEnd - msToWaitAtStart) + 1;
        Console.WriteLine($"{numberOfWaysToBeatRecord} ways to beat the race (wait at least {msToWaitAtStart} and at most {msToWaitAtEnd})");
        return numberOfWaysToBeatRecord;
    }

}
