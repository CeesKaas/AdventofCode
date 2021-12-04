using System.Text.RegularExpressions;
using Utilities;

namespace AoC2020.Days;

public class Day7
{
    private readonly IInputFetcher _inputFetcher;
    public Day7(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 7 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 7 part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1()
    {
        var regex = new Regex("(?<ContainerColor>.*) bags contain(\\s*([0-9]+) (?<ContianedColor>.*?) bags?,?)*");
        var input = await _inputFetcher.FetchInputAsStrings(7);

        var contianed = new Dictionary<string, List<string>>();

        foreach (var item in input)
        {
            var match = regex.Match(item);
            string containerColor = match.Groups["ContainerColor"].Value;
            List<string> containedColors = match.Groups["ContianedColor"].Captures.Select(_ => _.Value).ToList();
            foreach (var containedColor in containedColors)
            {
                if (!contianed.TryGetValue(containedColor, out var containingColors))
                {
                    containingColors = new List<string>();
                    contianed.Add(containedColor, containingColors);
                }
                containingColors.Add(containerColor);
            }
        }

        var bagsContainingShinyGold = new List<string>();

        FindContainingBagsRecursively("shiny gold", contianed, bagsContainingShinyGold);

        return bagsContainingShinyGold.Distinct().Count();
    }

    private void FindContainingBagsRecursively(string color, Dictionary<string, List<string>> contianed, List<string> bagsContainingShinyGold)
    {
        if (contianed.TryGetValue(color, out var colorList))
        {
            bagsContainingShinyGold.AddRange(colorList);
            foreach (var item in colorList)
            {
                FindContainingBagsRecursively(item, contianed, bagsContainingShinyGold);
            }
        }
    }

    public async Task<int> Part2()
    {
        var regex = new Regex("(?<ContainerColor>.*) bags contain(\\s*(?<ContianedColor>[0-9]+ .*?) bags?,?)*");
        var input = await _inputFetcher.FetchInputAsStrings(7);

        var contianers = new Dictionary<string, List<(string Color, int Number)>>();

        foreach (var item in input)
        {
            var match = regex.Match(item);
            string containerColor = match.Groups["ContainerColor"].Value;
            List<(string Color, int Number)> containedColors = match.Groups["ContianedColor"].Captures.Select(_ =>
            {
                string[] containedColorParts = _.Value.Split(' ', 2, StringSplitOptions.TrimEntries);
                return (containedColorParts[1], int.Parse(containedColorParts[0]));
            }).ToList();
            contianers.Add(containerColor, containedColors);
        }

        var numberOfContainedBags = FindContainedBagsRecursively("shiny gold", contianers);

        return numberOfContainedBags;
    }

    private int FindContainedBagsRecursively(string color, Dictionary<string, List<(string Color, int Number)>> contianers)
    {
        var numberOfContainedBags = 0;
        if (contianers.TryGetValue(color, out var foundBags))
        {
            numberOfContainedBags += foundBags.Sum(b => b.Number);
            foreach (var bag in foundBags)
            {
                numberOfContainedBags += FindContainedBagsRecursively(bag.Color, contianers) * bag.Number;
            }
        }
        return numberOfContainedBags;
    }
}
