using System.Text.RegularExpressions;
using Utilities;

namespace AoC2023.Days;

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
        var input = await _inputFetcher.FetchInputAsStrings(Day);
        var games = ParseGames(input);
        Console.WriteLine($"Day 2 part 1 answer: {Part1(games)}");
        Console.WriteLine($"Day 2 part 2 answer: {Part2(games)}");
    }

    public int Part1(List<Game> games)
    {        
        return games.Where(Valid).Sum(g=>g.Id);
    }

    private bool Valid(Game game)
    {
        return game.Draws.All(d => d.Red <= 12 
                                && d.Green <= 13 
                                && d.Blue <= 14);
    }

    private List<Game> ParseGames(string[] input)
    {
        var gameParser = new Regex("Game (?<GameIndex>[0-9]+):(?<Draws>.*)", RegexOptions.Compiled);
        var drawParser = new Regex("(?<NumberDrawn>[0-9]+) (?<ColorDrawn>red|blue|green)", RegexOptions.Compiled);
        var games = new List<Game>(input.Length);
        foreach (var line in input)
        {
            var match = gameParser.Match(line);
            var game = new Game(int.Parse(match.Groups["GameIndex"].Value));
            foreach (var draw in match.Groups["Draws"].Value.Split(';'))
            {
                var parsedDraw = drawParser.Matches(draw);
                int red = int.Parse(parsedDraw.FirstOrDefault(m => m.Groups["ColorDrawn"].Value == "red")?.Groups["NumberDrawn"].Value ?? "0");
                int blue = int.Parse(parsedDraw.FirstOrDefault(m => m.Groups["ColorDrawn"].Value == "blue")?.Groups["NumberDrawn"].Value ?? "0");
                int green = int.Parse(parsedDraw.FirstOrDefault(m => m.Groups["ColorDrawn"].Value == "green")?.Groups["NumberDrawn"].Value ?? "0");
                game.Draws.Add(new(red, green, blue));
            }
            games.Add(game);
        }
        return games;
    }

    public int Part2(List<Game> games)
    {
        return games.Sum(game =>
        {
            var maxDrawn = game.Draws.Aggregate((int[])[1, 1, 1], (acc, d) => [Math.Max(d.Red, acc[0]), Math.Max(d.Green, acc[1]), Math.Max(d.Blue, acc[2])]);
            return maxDrawn.Aggregate(1,(acc,item)=>acc*item);
        });
    }


    public async Task<int> Part1()
    {
        return Part1(ParseGames(await _inputFetcher.FetchInputAsStrings(Day)));
    }

    public async Task<int> Part2()
    {
        return Part2(ParseGames(await _inputFetcher.FetchInputAsStrings(Day)));
    }
}

public class Game(int id)
{
    public int Id { get; } = id;
    public List<Draw> Draws {get;}= new();
}
public readonly record struct Draw(int Red, int Green, int Blue);