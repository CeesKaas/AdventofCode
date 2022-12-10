using System.Text;
using Utilities;

namespace AoC2022.Days;

public class Day10
{
    private readonly int Day = int.Parse(nameof(Day10).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    private readonly StringBuilder _crt = new();
    private int _crtColumn = 0;
    public Day10(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 10 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 10 part 2 answer: \r\n{await Part2()}");
    }

    public async Task<int> Part1()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);

        var xAccumulator = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int i = 1;
        int x = 1;
        foreach (var line in input)
        {

            string[] parts = line.Split(' ');
            var op = parts[0];
            Increment(x, ref i);
            switch (op)
            {
                case "noop":
                    {
                        var accumulatorIndex = (int)Math.Round((i - 20) / 40f, 0, MidpointRounding.ToPositiveInfinity);
                        //Console.WriteLine($"{i,3} {accumulatorIndex} : {xAccumulator[accumulatorIndex]}");
                    }

                    break;
                case "addx":
                    {
                        var opAmount = int.Parse(parts[1]);
                        Increment(x, ref i);
                        x += opAmount;
                        var accumulatorIndex = (int)Math.Round((i - 20) / 40f, 0, MidpointRounding.ToPositiveInfinity);
                        xAccumulator[accumulatorIndex] = x;
                        //Console.WriteLine($"{i,3} {accumulatorIndex} : {xAccumulator[accumulatorIndex]} {opAmount}");
                    }
                    break;
            }

        }
        Console.WriteLine(_crt.ToString());
        var enumerable = xAccumulator.Take(6).Select((val, index) => val * ((index * 40) + 20)).ToArray();
        Console.WriteLine(string.Join("\r\n", enumerable));
        return enumerable.Sum();
    }

    private void Increment(int x, ref int i)
    {
        if (_crtColumn % 40 == 0)
        {
            _crt.AppendLine();
            _crtColumn = 0;
        }

        _crt.Append(Math.Abs(_crtColumn++ - x) <= 1 ? "█" : " ");


        i++;
    }

    public async Task<string> Part2()
    {
        return _crt.ToString();
    }
}
