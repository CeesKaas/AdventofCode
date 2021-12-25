using System.Text;
using Utilities;

namespace AoC2021.Days;

public class Day11
{
    private readonly int Day = int.Parse(nameof(Day11).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    public Day11(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        var answer = await Part1And2();
        Console.WriteLine($"Day 11 part 1 answer: {answer.partOne}");
        Console.WriteLine($"Day 11 part 2 answer: {answer.partTwo}");
    }

    public async Task<(int partOne, int partTwo)> Part1And2()
    {
        var input = (await _inputFetcher.GetTransformedSplitInputForDay(Day, s => s.Select(c => new Octopus(c - '0')).ToArray())).ToArray();
        var flashes = 0;
        var countFlashes = true;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                foreach (var neighbor in GetNeighbors(input, j, i))
                {
                    input[i][j].ConnectNeighbor(neighbor);
                }
                input[i][j].OnFlash += (sender, eventArgs) => flashes += countFlashes ? 1 : 0;
            }
        }

        var octupusCollection = input.SelectMany(_ => _).ToList();
        var allFlashedRound = 0;
        var turns = 100;
        Console.WriteLine("Initial");
        WriteField(input);
        for (int i = 0; allFlashedRound == 0; i++)
        {
            countFlashes = i < turns;
            foreach (var octupus in octupusCollection)
            {
                octupus.StartTurn();
            }
            foreach (var octupus in octupusCollection)
            {
                octupus.Turn();
            }
            if (allFlashedRound == 0 && octupusCollection.All(_ => _.Flashed))
            {
                allFlashedRound = i + 1;
            }
            Console.WriteLine($"Turn {i}");
            WriteField(input);
        }

        return (flashes, allFlashedRound);
    }

    private void WriteField(Octopus[][] input)
    {
        try
        {
            Console.Clear();
        }
        catch (Exception)
        {
        }
        var sb = new StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j].Flashed)
                    sb.Append('#');
                else
                    sb.Append(input[i][j].EnergyLevel);
            }
            sb.AppendLine();
        }
        Console.WriteLine(sb.ToString());
    }

    public async Task<int> Part2()
    {
        return 0;
    }

    private static IEnumerable<(int x, int y)> GetNeighbors(int x, int y, int width, int height)
    {
        if (y > 0)
        {
            if (x > 0)
            {
                yield return (x - 1, y - 1);
            }
            yield return (x, y - 1);
            if (x < width - 1)
            {
                yield return (x + 1, y - 1);
            }
        }

        if (x > 0)
        {
            yield return (x - 1, y);
        }
        if (x < width - 1)
        {
            yield return (x + 1, y);
        }

        if (y < height - 1)
        {
            if (x > 0)
            {
                yield return (x - 1, y + 1);
            }
            yield return (x, y + 1);
            if (x < width - 1)
            {
                yield return (x + 1, y + 1);
            }
        }
    }
    private static IEnumerable<Octopus> GetNeighbors(Octopus[][] all, int x, int y)
    {
        foreach (var location in GetNeighbors(x, y, all[y].Length, all.Length))
        {
            yield return all[location.y][location.x];
        }
    }

    internal class Octopus
    {
        public int EnergyLevel { get; set; }
        public bool Flashed { get; set; }

        public Octopus(int initialEnergyLevel)
        {
            EnergyLevel = initialEnergyLevel;
        }

        public event EventHandler OnFlash;

        public void ConnectNeighbor(Octopus neighbor)
        {
            neighbor.OnFlash += Increment;
        }

        private void Increment(object? sender, EventArgs e)
        {
            Increment();
        }

        private void Increment()
        {
            if (!Flashed)
            {
                EnergyLevel++;
                if (EnergyLevel > 9)
                {
                    Flashed = true;
                    OnFlash?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public void StartTurn()
        {
            if (Flashed)
            {
                EnergyLevel = 0;
                Flashed = false;
            }
        }

        public void Turn()
        {
            Increment();
        }
    }
}
