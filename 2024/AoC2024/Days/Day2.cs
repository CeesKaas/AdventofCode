using Utilities;
using System.Linq;
using System.Text;

namespace AoC2024.Days;

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
        //Console.WriteLine($"Day 2 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 2 part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        var safe = 0;
        foreach (var item in input)
        {
            var parts = item.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var prev = parts[0];
            var diffs = new List<int>();
            var ordered = true;
            var direction = Dir.Unknown;
            for (int i = 1; i < parts.Length; i++)
            {
                var part = parts[i];
                var diff = part - prev;
                diffs.Add(diff);
                /*
                if (diff == 0)
                {
                    Console.WriteLine($"not safe, failed at {i + 1} as items are equal, '{item}'");
                    ordered = false;
                    break;
                }

                if (direction == Dir.Unknown)
                {
                    if (prev != int.MaxValue)
                    {
                        direction = diff > 0 ? Dir.Increase : Dir.Decrease; //second item
                    }
                    prev = part;
                    continue;
                }

                if (Math.Abs(diff) > 3)
                {
                    Console.WriteLine($"not safe, {diff} > 3 failed at {i + 1}, '{item}'");
                    ordered = false;
                    break;
                }

                 
                if (!((diff, direction) switch
                {
                    (-1 or -2 or -3, Dir.Decrease) => true,
                    (1 or 2 or 3, Dir.Increase) => true,
                    _ => false
                }))
                {
                    Console.WriteLine($"not safe, {direction} {diff} failed at {i + 1}, '{item}'");
                    ordered = false;
                    break;
                }*/
                prev = part;
            }
            /*if (ordered)
            {
                safe++;
            }*/
            if (diffs.All(d => d > 0 && d <= 3) || diffs.All(d => d < 0 && d >= -3))
            {
                safe++;
            }
            else
            {
                Console.WriteLine($"not safe '{item}', '{string.Join(" ", diffs)}'");
            }
        }
        return safe;
    }

    enum Dir
    {
        Unknown,
        Increase,
        Decrease,
    }

    public async Task<int> Part2()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        return input.Count(testItem);
    }

    public static bool testItem(string item)
    {
        var parts = item.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        var grouped = parts.GroupBy(_ => _);
        if (grouped.Any(_ => _.Count() > 2) || grouped.Count(_ => _.Count() > 1) >= 2)
        {
            //Console.WriteLine($"not safe {item} multiple duplicates");
            return false;
        }
        if (!testItem(item, parts, false))
        {
            var sb = new StringBuilder();
            var prev = parts[0];
            sb.Append($"{prev,2}");
            foreach (var part in parts.Skip(1))
            {
                sb.Append($" {prev-part,3:+0;-0;0} {part,2}");
                prev = part;
            }
            Console.WriteLine($"not safe {item,-25}: {sb}");
            return false;
        }
        return true;
    }

    public static bool testItem(string item, int[] parts, bool usedDamper)
    {
        var prev = parts[0];
        var diffs = new List<int>();
        var firstDiff = 0;
        for (int i = 1; i < parts.Length; i++)
        {
            var part = parts[i];
            var diff = part - prev;
            if (!usedDamper && diff == 0)
            {
                return testItem(item, parts.WithoutItemAt(i), true);
            }

            if (!usedDamper && ((diff > 3 && firstDiff >= 0)
                             || (diff < -3 && firstDiff <= 0)
                             || (diff < 0 && firstDiff > 0)
                             || (diff > 0 && firstDiff < 0)))
            {
                return testItem(item, parts.WithoutItemAt(i), true) || testItem(item, parts.WithoutItemAt(i - 1), true) || (i>=2? testItem(item, parts.WithoutItemAt(i-2), true):false);
            }
            if (firstDiff == 0)
            {
                firstDiff = diff;
            }
            diffs.Add(diff);
            prev = part;
        }
        if (diffs.All(d => d > 0 && d <= 3) || diffs.All(d => d < 0 && d >= -3))
        {
            //Console.WriteLine($"safe '{item}', '{string.Join(" ", parts)}', '{string.Join(" ", diffs)}'");
            return true;
        }
        else
        {
            //Console.WriteLine($"not safe (usedDamper: {usedDamper}) '{item}', '{string.Join(" ", parts)}', '{string.Join(" ", diffs)}'");
            return false;
        }
    }
}
static class ReadOnlySpanExtension
{
    public static T[] WithoutItemAt<T>(this T[] input, int index)
    {
        T[]? retval = null;
        if (index == 0)
            retval = input[1..];
        if (index > 0 && index < input.Length - 1)
            retval = [.. input[0..index], .. input[(index + 1)..]];
        if (index == input.Length - 1)
            retval = [.. input[0..index]];
        if (retval?.Length != input.Length - 1)
        {
            throw new InvalidOperationException("nothing was skipped");
        }
        return retval ?? throw new ArgumentOutOfRangeException(nameof(index));
    }
}
