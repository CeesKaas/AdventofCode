using System.Diagnostics;
using System.Text;
using Utilities;

namespace AoC2023.Days;

public class Day10
{
    private readonly int Day = int.Parse(nameof(Day10).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    public Day10(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 10 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 10 part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1() => (await Part1Internal()).FurthestDistance;
    public async Task<(int FurthestDistance, Map Map, HashSet<Pipe> Seen)> Part1Internal()
    {
        var input = await _inputFetcher.FetchInputAsStrings(Day);

        var map = new Map(input);

        var start = map.Tiles.OfType<Start>().First();
        Pipe? previous = null;
        Pipe? current = start;
        var seen = new HashSet<Pipe>();
        var steps = 0;
        while (current is not null)
        {
            var moveOptions = current.Connected.Where(_ => _ != previous && _ != start).ToList();
            previous = current;
            seen.Add(current);
            current = moveOptions.FirstOrDefault();
            if (moveOptions.Count == 2)
            {
                Console.WriteLine($"found 2 options while only 1 was expected {moveOptions[0]} {moveOptions[1]}");
            }
        }
        return (seen.Count / 2, map, seen);
    }

    public async Task<int> Part2()
    {
        var (_, map, seen) = await Part1Internal();

        var start = map.Tiles.OfType<Start>().Single();
        start.FixActualChar();

        var sb = new StringBuilder();
        var insideLoop = false;
        var tilesInside = 0;
        Pipe? startingJoint = null;
        for (var i = 0; i < map.Tiles.GetLength(0); i++)
        {
            for (var j = 0; j < map.Tiles.GetLength(1); j++)
            {
                if (seen.Contains(map.Tiles[i, j]))
                {
                    var tile = ((Pipe)map.Tiles[i, j]).C;
                    sb.Append(tile switch
                    {
                        'S' => '#',
                        '|' => '║',
                        '-' => '═',
                        'F' => '╔',
                        '7' => '╗',
                        'L' => '╚',
                        'J' => '╝',
                        char c => c
                    });
                    if (tile == '|')
                    {
                        insideLoop = !insideLoop;
                    }
                    else if ("FL".Contains(tile))
                    {
                        startingJoint = (Pipe)map.Tiles[i, j];
                    }
                    else if (tile == 'J')
                    {
                        if (startingJoint?.C == 'F')
                        {
                            insideLoop = !insideLoop;
                        }
                        startingJoint = null;
                    }
                    else if (tile == '7')
                    {
                        if (startingJoint?.C == 'L')
                        {
                            insideLoop = !insideLoop;
                        }
                        startingJoint = null;
                    }
                }
                else
                {
                    if (insideLoop)
                    {
                        tilesInside++;
                        sb.Append("%");
                    }
                    else
                    {
                        sb.Append("0");
                    }
                }
            }
            sb.AppendLine();
        }
        Console.WriteLine(sb.ToString());
        return tilesInside;
    }
}

public class Map
{
    public Tile[,] Tiles { get; }
    public Map(string[] input)
    {
        Tiles = new Tile[input.Length, input[0].Length];

        for (var i = 0; i < input.Length; i++)
        {
            for (var j = 0; j < input[i].Length; j++)
            {
                var c = input[i][j];
                Tile newTile = c switch
                {
                    '.' => new Ground(j, i),
                    'S' => new Start(j, i),
                    _ => new Pipe(j, i, c)
                };

                Tiles[i, j] = newTile;
            }
        }
        foreach (Pipe p in Tiles.OfType<Pipe>())
        {
            var i = p.Y;
            var j = p.X;

            if (p.ConnectsNorth && i > 0 && Tiles[i - 1, j] is Pipe otherNorth && otherNorth.ConnectsSouth)
            {
                otherNorth.Connected.Add(p);
                p.Connected.Add(otherNorth);
            }

            if (p.ConnectsSouth && (i + 1) < Tiles.GetLength(0) && Tiles[i + 1, j] is Pipe otherSouth && otherSouth.ConnectsNorth)
            {
                otherSouth.Connected.Add(p);
                p.Connected.Add(otherSouth);
            }

            if (p.ConnectsEast && (j + 1) < Tiles.GetLength(1) && Tiles[i, j + 1] is Pipe otherEast && otherEast.ConnectsWest)
            {
                otherEast.Connected.Add(p);
                p.Connected.Add(otherEast);
            }

            if (p.ConnectsWest && j > 0 && Tiles[i, j - 1] is Pipe otherWest && otherWest.ConnectsSouth)
            {
                otherWest.Connected.Add(p);
                p.Connected.Add(otherWest);
            }
        }
    }
}
public static class Day10Extensions
{
}

public class Tile(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;

    public override string ToString()
    {
        return $"[{x},{y}] {GetType().Name}";
    }
}
public class Ground(int x, int y) : Tile(x, y) { }
public class Pipe(int x, int y, char c) : Tile(x, y)
{
    public HashSet<Pipe> Connected { get; } = new(2);
    public virtual char C { get; } = c;
    public bool ConnectsNorth { get; } = c is '|' or 'L' or 'J' or 'S';
    public bool ConnectsSouth { get; } = c is '|' or '7' or 'F' or 'S';
    public bool ConnectsEast { get; } = c is '-' or 'L' or 'F' or 'S';
    public bool ConnectsWest { get; } = c is '-' or 'J' or '7' or 'S';

    public override string ToString()
    {
        return $"{base.ToString()} {C}";
    }
}
public class Start(int x, int y) : Pipe(x, y, 'S')
{

    private char _actualChar = 'S';
    public override char C => _actualChar;

    public void FixActualChar()
    {
        var connected = Connected;
        var connectNorth = false;
        var connectSouth = false;
        var connectEast = false;
        var connectWest = false;

        connectNorth = connected.Any(con => con.Y == Y + 1);
        connectSouth = connected.Any(con => con.Y == Y - 1);
        connectEast = connected.Any(con => con.X == Y + 1);
        connectWest = connected.Any(con => con.X == Y - 1);

        _actualChar = (connectNorth, connectSouth, connectEast, connectWest) switch
        {
            (true, true, false, false) => '|',
            (false, false, true, true) => '-',
            (true, false, true, false) => 'L',
            (true, false, false, true) => 'J',
            (false, true, false, true) => '7',
            (false, true, true, false) => 'F'
        };
    }
}