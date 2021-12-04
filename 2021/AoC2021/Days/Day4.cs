using Utilities;

namespace AoC2021.Days;

public class Day4
{
    private readonly IInputFetcher _inputFetcher;
    public Day4(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
    }

    internal async Task Start()
    {
        Console.WriteLine($"Day 4 part 1 answer: {await Part1()}");
        Console.WriteLine($"Day 4 part 2 answer: {await Part2()}");
    }

    public async Task<int> Part1()
    {
        var input = await _inputFetcher.FetchInputAsString(4);

        var parts = input.Replace("\r\n", "\n").Split("\n\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToArray();
        var numbers = parts[0].Split(',').Select(int.Parse);
        var cards = parts[1..].Select(s => new BingoCard(s)).ToList();

        foreach (var card in cards)
        {
            Console.WriteLine(new string('=', 20) + "\r\n" + card);
        }

        foreach (var number in numbers)
        {
            Console.WriteLine($"playing {number}");
            foreach (var card in cards)
            {
                if (card.Play(number, out var cardScore))
                {
                    return cardScore;
                }
                Console.WriteLine(new string('=', 20) + "\r\n" + card);
            }
            Console.WriteLine(new string('=', 20) + "\r\n" + new string('=', 20));
        }

        return 0;
    }

    public async Task<int> Part2()
    {
        var input = await _inputFetcher.FetchInputAsString(4);

        var parts = input.Replace("\r\n", "\n").Split("\n\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToArray();
        var numbers = parts[0].Split(',').Select(int.Parse);
        var cards = parts[1..].Select(s => new BingoCard(s)).ToList();

        foreach (var card in cards)
        {
            Console.WriteLine(new string('=', 20) + "\r\n" + card);
        }

        foreach (var number in numbers)
        {
            Console.WriteLine($"playing {number}");
            var wonCards = new List<BingoCard>();
            foreach (var card in cards)
            {
                if (card.Play(number, out var cardScore))
                {
                    wonCards.Add(card);
                    if (cards.Count - wonCards.Count == 0)
                    {
                        return cardScore;
                    }
                }
                Console.WriteLine(new string('=', 20) + "\r\n" + card);
            }
            Console.WriteLine(new string('=', 20) + "\r\n" + new string('=', 20));
            cards.RemoveAll(card => wonCards.Contains(card));
        }

        return 0;
    }
}
class BingoCard
{
    private readonly List<int[]> _sections;

    public BingoCard(string board)
    {
        var boardItems = board
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                          .Select(int.Parse)
                          .ToArray())
            .ToArray();

        Rows = boardItems;
        Columns = new int[5][];
        for (int i = 0; i < 5; i++)
        {
            Columns[i] = new int[5];
            for (int j = 0; j < 5; j++)
            {
                Columns[i][j] = boardItems[j][i];
            }
        }

        _sections = Rows.Concat(Columns).ToList();
    }

    public bool Play(int number, out int cardScore)
    {
        bool finished = false;
        cardScore = -1;
        foreach (var section in _sections)
        {
            var numberLocation = Array.IndexOf(section, number);
            if (numberLocation >= 0)
            {
                section[numberLocation] = -1;
            }

            finished |= section.All(_ => _ == -1);
        }

        if (finished)
        {
            cardScore = Rows.Sum(r => r.Where(c => c >= 0).Sum()) * number;
        }
        return finished;
    }

    public int[][] Rows { get; }
    public int[][] Columns { get; }

    public override string ToString()
    {
        return $"{Rows[0][0],2} {Rows[0][1],2} {Rows[0][2],2} {Rows[0][3],2} {Rows[0][4],2}\r\n" +
               $"{Rows[1][0],2} {Rows[1][1],2} {Rows[1][2],2} {Rows[1][3],2} {Rows[1][4],2}\r\n" +
               $"{Rows[2][0],2} {Rows[2][1],2} {Rows[2][2],2} {Rows[2][3],2} {Rows[2][4],2}\r\n" +
               $"{Rows[3][0],2} {Rows[3][1],2} {Rows[3][2],2} {Rows[3][3],2} {Rows[3][4],2}\r\n" +
               $"{Rows[4][0],2} {Rows[4][1],2} {Rows[4][2],2} {Rows[4][3],2} {Rows[4][4],2}\r\n";
    }
}
