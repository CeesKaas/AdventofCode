using System.Drawing;
using System.Drawing.Imaging;
using Utilities;
using static System.Math;
namespace AoC2021.Days;

public class Day5
{
    private readonly IInputFetcher _inputFetcher;
    public Day5(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        var result = await Part1();
        Console.WriteLine($"Day 5 part 1 answer: {result.Item1}");
        Console.WriteLine($"Day 5 part 2 answer: {result.Item2}");
    }

    public async Task<(int, int)> Part1()
    {
        //example Line: 0,9 -> 5,9
        ICollection<(Point A, Point B)>? input = await _inputFetcher.GetTransformedSplitInputForDay(5, s =>
        {
            var parts = s.Split(" -> ").Select(p => p.Split(',').Select(int.Parse).ToArray()).ToArray();
            return (new Point(parts[0][1], parts[0][0]), new Point(parts[1][1], parts[1][0]));
        });

        var filtered = input;
        var bounds = filtered.Aggregate(new Point(), (acc, item) => new Point(Max(acc.X, Max(item.A.X, item.B.X)), Max(acc.Y, Max(item.A.Y, item.B.Y))));

        var fieldPart1 = new int[bounds.X + 1, bounds.Y + 1];
        var fieldPart2 = new int[bounds.X + 1, bounds.Y + 1];

        foreach (var item in filtered)
        {
            if (item.A.X == item.B.X)
            {
                //vertical line
                var start = Min(item.A.Y, item.B.Y);
                var end = Max(item.A.Y, item.B.Y) + 1;

                for (var i = start; i < end; i++)
                {
                    fieldPart1[item.A.X, i]++;
                    fieldPart2[item.A.X, i]++;
                }
            }
            else if (item.A.Y == item.B.Y)
            {
                //horizontal line
                var start = Min(item.A.X, item.B.X);
                var end = Max(item.A.X, item.B.X) + 1;

                for (var i = start; i < end; i++)
                {
                    fieldPart1[i, item.A.Y]++;
                    fieldPart2[i, item.A.Y]++;
                }
            }
            else
            {
                var lowestX = item.A.X < item.B.X ? item.A : item.B;
                var highestX = item.A.X < item.B.X ? item.B : item.A;
                //diagonal
                // top right -> bottow left => a.x < b.x && a.y < b.y
                if (lowestX.Y < highestX.Y)
                {
                    for (int x = lowestX.X, y = lowestX.Y; x <= highestX.X || y <= highestX.Y; x++, y++)
                    {
                        fieldPart2[x, y]++;
                    }
                }
                else
                {
                    for (int x = lowestX.X, y = lowestX.Y; x <= highestX.X || y > highestX.Y; x++, y--)
                    {
                        fieldPart2[x, y]++;
                    }
                }
            }
        }

        var twoCountPart1 = 0;

        foreach (var cel in fieldPart1)
        {
            if (cel >= 2)
            {
                twoCountPart1++;
            }
        }

        var twoCountPart2 = 0;

        foreach (var cel in fieldPart2)
        {
            if (cel >= 2)
            {
                twoCountPart2++;
            }
        }

        if (OperatingSystem.IsWindows())
        {
            static Color GetColor(int lineCount)
            {
                switch (lineCount)
                {
                    case 0: return Color.White;
                    case 1: return Color.Black;
                    case 2: return Color.Red;
                    case 3: return Color.Green;
                    case 4: return Color.Blue;
                    case 5: return Color.Yellow;
                    case 6: return Color.Purple;
                    case 7: return Color.Gray;
                    default: return Color.HotPink;
                }
            }
            var imagePart1 = new Bitmap(bounds.X + 1, bounds.Y + 1);
            var imagePart2 = new Bitmap(bounds.X + 1, bounds.Y + 1);

            for (int x = 0; x <= bounds.X; x++)
            {
                for (int y = 0; y <= bounds.Y; y++)
                {
                    imagePart1.SetPixel(x, y, GetColor(fieldPart1[x, y]));
                    imagePart2.SetPixel(x, y, GetColor(fieldPart2[x, y]));
                }
            }
            imagePart1.Save("part1.bmp", ImageFormat.Bmp);
            imagePart2.Save("part2.bmp", ImageFormat.Bmp);
        }
        return (twoCountPart1, twoCountPart2);
    }
}
