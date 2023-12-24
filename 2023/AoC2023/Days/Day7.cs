using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using Utilities;
using static AoC2023.Days.Day7;

namespace AoC2023.Days;

public class Day7
{
    private readonly int Day = int.Parse(nameof(Day7).Replace("Day", ""));
    private readonly IInputFetcher _inputFetcher;
    private readonly Lazy<ImmutableList<(Hand hand, long Bid)>> _input;
    public Day7(IInputFetcher? inputFetcher = null)
    {
        _inputFetcher = inputFetcher ?? new InputFetcher();
        _input = new(() => _inputFetcher.FetchInputAsStrings(Day).GetAwaiter().GetResult().Select(ParseHandAndBid).ToImmutableList());
    }

    internal async Task Start()
    {
        var sw = Stopwatch.StartNew();
        Console.WriteLine($"Day 7 part 1 answer: {Part1()}");
        Console.WriteLine($"Day 7 part 2 answer: {Part2()}");
        Console.WriteLine($"Day 7 part 1 answer: {Part1Parallel()}");
        Console.WriteLine($"Day 7 part 2 answer: {Part2Parallel()}");
        sw.Stop();
        Console.WriteLine($"Warmup tool {sw.Elapsed}");
        const int _runs = 10_000;
        {
            var simple = new List<(TimeSpan, long)>();
            var parallel = new List<(TimeSpan, long)>();
            long answer;
            for (int i = 0; i < _runs; i++)
            {
                sw.Restart();
                answer = Part1();
                simple.Add((sw.Elapsed, answer));
                sw.Restart();
                answer = Part1Parallel();
                parallel.Add((sw.Elapsed, answer));
            }
            Console.WriteLine($"part 1 simple took {TimeSpan.FromTicks((long)simple.Select(_ => _.Item1.Ticks).Average())}");
            Console.WriteLine($"part 1 parallel took {TimeSpan.FromTicks((long)parallel.Select(_ => _.Item1.Ticks).Average())}");
        }
        {
            var simple = new List<(TimeSpan, long)>();
            var parallel = new List<(TimeSpan, long)>();
            long answer;
            for (int i = 0; i < _runs; i++)
            {
                sw.Restart();
                answer = Part2();
                simple.Add((sw.Elapsed, answer));
                sw.Restart();
                answer = Part2Parallel();
                parallel.Add((sw.Elapsed, answer));
            }
            Console.WriteLine($"part 2 simple took {TimeSpan.FromTicks((long)simple.Select(_ => _.Item1.Ticks).Average())}");
            Console.WriteLine($"part 2 parallel took {TimeSpan.FromTicks((long)parallel.Select(_ => _.Item1.Ticks).Average())}");
        }
    }

    public long Part1()
    {
        return _input.Value
            .OrderBy(_ => _.hand, HandComparer.Part1)
            .Select(CalulateWinnings)
            .Sum();
    }

    public long Part2()
    {

        return _input.Value
            .OrderBy(_ => _.hand, HandComparer.Part2)
            .Select(CalulateWinnings)
            .Sum();
    }

    public long Part1Parallel()
    {

        return _input.Value.AsParallel()
            .OrderBy(_ => _.hand, HandComparer.Part1)
            .Select(CalulateWinnings)
            .Sum();
    }
    public long Part2Parallel()
    {
        return _input.Value.AsParallel()
            .OrderBy(_ => _.hand, HandComparer.Part2)
            .Select(CalulateWinnings)
            .Sum();
    }

    private static long CalulateWinnings((Hand hand, long Bid) handBid, int index)
    {
        return handBid.Bid * (index + 1);
    }


    public static (Hand hand, long Bid) ParseHandAndBid(string input)
    {
        var parts = input.Split(" ");
        return (new(parts[0]), long.Parse(parts[1]));
    }

    public class Hand
    {
        public Hand(string cards)
        {
            Cards = cards.Select(ExtensionMethods.ToCardValue).ToArray();
            var cardCounts = Cards.GroupBy(_ => _).ToDictionary(_ => _.Key, _ => _.Count());
            HandType = cardCounts.ToHandType();
            HandType2 = cardCounts.ToHandType2();
        }

        public HandType HandType { get; }
        public HandType HandType2 { get; }
        public CardValue[] Cards { get; }
    }

    public class HandComparer : IComparer<Hand>
    {
        public static readonly HandComparer Part1 = new(false);
        public static readonly HandComparer Part2 = new(true);
        private readonly bool _jackAsJoker;

        private HandComparer(bool jackAsJoker)
        {
            _jackAsJoker = jackAsJoker;
        }

        public int Compare(Hand? x, Hand? y)
        {
            if (x == null || y == null) throw new NotSupportedException();
            int comparedType;
            if (_jackAsJoker)
            {
                comparedType = x.HandType2.CompareTo(y.HandType2);
            }
            else
            {
                comparedType = x.HandType.CompareTo(y.HandType);
            }

            if (comparedType == 0)
            {
                for (int i = 0; i < x.Cards.Length; i++)
                {
                    var relativeValue = x.Cards[i].CompareTo(y.Cards[i]);
                    if (relativeValue != 0)
                    {
                        if (_jackAsJoker && x.Cards[i] == CardValue.JackOrJoker)
                        {
                            return 1;
                        }
                        if (_jackAsJoker && y.Cards[i] == CardValue.JackOrJoker)
                        {
                            return -1;
                        }
                        return relativeValue;
                    }
                }
            }
            return comparedType;
        }
    }

    public enum HandType : byte
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }
    public enum CardValue : byte
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        JackOrJoker,
        Queen,
        King,
        Ace
    }
}

public static class ExtensionMethods
{
    public static Day7.HandType ToHandType(this Dictionary<CardValue, int> cardCounts)
    {
        return cardCounts.ToHandType2(false);
    }
    public static Day7.CardValue ToCardValue(this char c)
    {
        return c switch
        {
            '2' => Day7.CardValue.Two,
            '3' => Day7.CardValue.Three,
            '4' => Day7.CardValue.Four,
            '5' => Day7.CardValue.Five,
            '6' => Day7.CardValue.Six,
            '7' => Day7.CardValue.Seven,
            '8' => Day7.CardValue.Eight,
            '9' => Day7.CardValue.Nine,
            'T' => Day7.CardValue.Ten,
            'J' => Day7.CardValue.JackOrJoker,
            'Q' => Day7.CardValue.Queen,
            'K' => Day7.CardValue.King,
            'A' => Day7.CardValue.Ace,
            _ => throw new NotImplementedException($"no card value for {c} has been set")
        };
    }
    public static Day7.HandType ToHandType2(this Dictionary<CardValue, int> cardCounts, bool jackAsJoker = true)
    {
        var orderedCardCounts = cardCounts.Where(_ => !jackAsJoker || _.Key != CardValue.JackOrJoker).Select(_ => _.Value).OrderDescending().ToArray();
        if (jackAsJoker)
        {
            var jokerCount = cardCounts.TryGetValue(CardValue.JackOrJoker, out var count) ? count : 0;
            if (jokerCount == 5) return HandType.FiveOfAKind;

            orderedCardCounts[0] += jokerCount;
        }
        switch (orderedCardCounts[0])
        {
            case 5:
                return HandType.FiveOfAKind;
            case 4:
                return HandType.FourOfAKind;
            case 3:
                if (orderedCardCounts[1] == 2)
                {
                    return HandType.FullHouse;
                }
                return HandType.ThreeOfAKind;
            case 2:
                if (orderedCardCounts[1] == 2)
                {
                    return HandType.TwoPair;
                }
                return HandType.OnePair;
            default:
                return HandType.HighCard;
        }
    }
}
